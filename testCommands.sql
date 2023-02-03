USE [master]
GO
-- Create a new login for an user that can only see the data only on eCommerce, not on eCommerceAudit
CREATE LOGIN [viewer] WITH PASSWORD=N'viewer', DEFAULT_DATABASE=[eCommerce], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

USE [eCommerce]
GO

CREATE USER [viewer] FOR LOGIN [viewer]
GO

ALTER USER [viewer] WITH DEFAULT_SCHEMA=[dbo]
GO

ALTER ROLE [db_datareader] ADD MEMBER [viewer]
GO

-- Data mask for viewer on user email
GRANT SELECT ON [dbo].[UserT] to [viewer];

USE [master]
GO
-- Create a simple dev that can read/write on eCommerce, but can only see data in eCommerceAudit
ALTER LOGIN [simpleDev] WITH PASSWORD=N'simpleDev'
GO

USE [eCommerce]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [simpleDev]
GO

USE [eCommerceAudit]
GO
CREATE USER [simpleDev] FOR LOGIN [simpleDev]
GO

ALTER USER [simpleDev] WITH DEFAULT_SCHEMA=[Audit]
GO

ALTER ROLE [db_datareader] ADD MEMBER [simpleDev]
GO



USE [master]
GO
-- Create database for sql tables logins
IF (NOT EXISTS (SELECT name FROM master.sys.databases WHERE name = N'eCommerceAudit'))
BEGIN 
	CREATE DATABASE [eCommerceAudit];
END
GO

USE [eCommerceAudit];
GO

IF (NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'Audit')) 
BEGIN
    EXEC ('CREATE SCHEMA [Audit] AUTHORIZATION [dbo]')
END
GO

-- Create table that stores the information for auditing specific details about the product comments
IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'AuditProductComment' AND TABLE_SCHEMA = N'Audit'))
BEGIN
	CREATE TABLE [Audit].[AuditProductComment]
	(
		AuditId int Identity(1, 1) primary key,
		ProductCommentId int NOT NULL,
		ProductId int NOT NULL,
		UserNameComment nvarchar(35) NOT NULL,
		ProductRating int,
		Comment nvarchar(max) NOT NULL,
		CommentDate datetime NOT NULL,
		CommentTitle nvarchar(max) NOT NULL,
		ActionType nvarchar(6),
		ModifiedBy nvarchar(128),
		ModifiedOn datetime default GETDATE()
	);
END
GO

-- Create table that stores the information for auditing specific important tables from eCommerce
IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'AuditUser' AND TABLE_SCHEMA = N'Audit'))
BEGIN
	CREATE TABLE [Audit].[AuditUser]
	(
		AuditId int Identity(1, 1) primary key,
		UserId int,
		UserEmail nvarchar(128),
		ActionType nvarchar(6),
		ModifiedBy nvarchar(128),
		ModifiedOn datetime default GETDATE()
	);
END
GO

-- Set trigger on eCommerce tables UserT and ProductComment
USE [eCommerce]
GO

CREATE OR ALTER TRIGGER [TR_AuditUserTrigger] on [dbo].[UserT]
AFTER UPDATE, INSERT, DELETE
AS
BEGIN
	DECLARE @Action as char(6);
    SET @Action = (CASE WHEN EXISTS(SELECT * FROM INSERTED) AND EXISTS(SELECT * FROM DELETED)
							THEN 'Update'  -- Set updated action
                        WHEN EXISTS(SELECT * FROM INSERTED)
							THEN 'Create'  -- Set created action
                        WHEN EXISTS(SELECT * FROM DELETED)
							THEN 'Delete'  -- Set deleted action
                    END)

	INSERT INTO [eCommerceAudit].[Audit].[AuditUser]
		(UserId, UserEmail, ActionType, ModifiedBy, ModifiedOn)
	SELECT m.UserId, m.Email, @Action, SUSER_SNAME(), GETDATE()
	FROM [eCommerce].[dbo].[UserT] u
	inner join inserted m on u.UserId = m.UserId

	INSERT INTO [eCommerceAudit].[Audit].[AuditUser]
		(UserId, UserEmail, ActionType, ModifiedBy, ModifiedOn)
	SELECT m.UserId, m.Email, @Action, SUSER_SNAME(), GETDATE()
	FROM [eCommerce].[dbo].[UserT] u
	inner join deleted m on u.UserId = m.UserId
END
GO

-- Audit product comments 
CREATE OR ALTER TRIGGER [TR_AuditProductCommentTrigger] on [dbo].[ProductComment]
AFTER UPDATE, INSERT
AS
BEGIN
	DECLARE @Action as char(6);
    SET @Action = (CASE WHEN EXISTS(SELECT * FROM INSERTED) AND EXISTS(SELECT * FROM DELETED)
							THEN 'Update'  -- Set updated action
                        WHEN EXISTS(SELECT * FROM INSERTED)
							THEN 'Create'  -- Set created action
                    END)

	INSERT INTO [eCommerceAudit].[Audit].[AuditProductComment]
		(ProductCommentId, ProductId, UserNameComment, ProductRating, Comment, CommentDate, CommentTitle, ActionType, ModifiedBy, ModifiedOn)
	SELECT m.ProductCommentId, m.ProductId, m.UserNameComment, m.ProductRating, m.Comment, m.CommentDate, m.CommentTitle, @Action, SUSER_SNAME(), GETDATE()
	FROM [eCommerce].[dbo].[ProductComment] u
	inner join inserted m on u.ProductCommentId = m.ProductCommentId
END
GO

USE [eCommerceAudit]
GO


---------------------
-- Create table that stores the information for auditing the server
IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'DbLoginAuditing'))
BEGIN
	CREATE TABLE [Audit].[DbLoginAuditing] 
	(
		SessionId int,
		DbLoginTime datetime,
		HostName varchar(50),
		ProgramName varchar(500),
		LoginName varchar(50),
		ClientHost varchar(50)
	);
END
GO

-- Trigger for automatically logging logins
CREATE OR ALTER TRIGGER [TR_DbLoginAuditTrigger]
ON ALL SERVER
FOR LOGON
AS
	BEGIN
		DECLARE @DbLoginTriggerData xml,
		@EventTime datetime,
		@LoginName varchar(50),
		@ClientHost varchar(50),
		@LoginType varchar(50),
		@HostName varchar(50),
		@AppName varchar(500)

		SET @DbLoginTriggerData = eventdata()

		SET @EventTime = @DbLoginTriggerData.value('(/EVENT_INSTANCE/PostTime)[1]', 'datetime')
		SET @LoginName = @DbLoginTriggerData.value('(/EVENT_INSTANCE/LoginName)[1]', 'varchar(50)')
		SET @ClientHost = @DbLoginTriggerData.value('(/EVENT_INSTANCE/ClientHost)[1]', 'varchar(50)')
		SET @HostName = HOST_NAME()
		SET @AppName = APP_NAME()

		INSERT INTO [eCommerceAudit].[Audit].[DbLoginAuditing]
			(
				SessionId,
				DbLoginTime,
				HostName,
				ProgramName,
				LoginName,
				ClientHost
			)
		SELECT
			@@spid,
			@EventTime,
			@HostName,
			@AppName,
			@LoginName,
			@ClientHost
	END
GO

USE [eCommerce];
GO