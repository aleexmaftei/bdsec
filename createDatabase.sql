IF (NOT EXISTS (SELECT name FROM master.sys.databases WHERE name = N'eCommerce'))
BEGIN 
	CREATE DATABASE [eCommerce];
END
GO

USE [eCommerce]
GO

CREATE TABLE [dbo].[DeliveryLocation](
	[DeliveryLocationId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[CountryName] [nvarchar](100) NOT NULL,
	[RegionName] [nvarchar](25) NOT NULL,
	[CityName] [nvarchar](25) NOT NULL,
	[AddressDetail] [nvarchar](max) NOT NULL,
	[PostalCode] [int] NOT NULL,
	[SysStartTime] [datetime2](7) GENERATED ALWAYS AS ROW START NOT NULL,
	[SysEndTime] [datetime2](7) GENERATED ALWAYS AS ROW END NOT NULL,
PRIMARY KEY CLUSTERED 
(
[DeliveryLocationId] ASC) ON [PRIMARY], PERIOD FOR SYSTEM_TIME ([SysStartTime], [SysEndTime])
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
WITH
(
SYSTEM_VERSIONING = ON ( HISTORY_TABLE = [dbo].[DeliveryLocation_History] )
)
GO

CREATE TABLE [dbo].[Manufacturer](
	[ManufacturerId] [int] IDENTITY(1,1) NOT NULL,
	[ManufacturerName] [nvarchar](15) NOT NULL,
	[ManufacturerLogo] [nvarchar](max) NULL,
	[SysStartTime] [datetime2](7) GENERATED ALWAYS AS ROW START NOT NULL,
	[SysEndTime] [datetime2](7) GENERATED ALWAYS AS ROW END NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ManufacturerId] ASC
) ON [PRIMARY], PERIOD FOR SYSTEM_TIME ([SysStartTime], [SysEndTime])
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
WITH
(
SYSTEM_VERSIONING = ON ( HISTORY_TABLE = [dbo].[Manufacturer_History] )
)
GO

CREATE TABLE [dbo].[Product](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[ManufacturerId] [int] NOT NULL,
	[ProductName] [nvarchar](100) NOT NULL,
	[ProductImage] [nvarchar](max) NOT NULL,
	[ProductPrice] [float] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Quantity] [int] NOT NULL,
	[SysStartTime] [datetime2](7) GENERATED ALWAYS AS ROW START NOT NULL,
	[SysEndTime] [datetime2](7) GENERATED ALWAYS AS ROW END NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[ProductName] ASC
) ON [PRIMARY], PERIOD FOR SYSTEM_TIME ([SysStartTime], [SysEndTime])
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
WITH
(
SYSTEM_VERSIONING = ON ( HISTORY_TABLE = [dbo].[Product_History] )
)
GO

CREATE TABLE [dbo].[ProductCategory](
	[ProductCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[ProductCategoryName] [nvarchar](35) NOT NULL,
	[ProductCategoryImage] [nvarchar](max) NOT NULL,
	[ParentProductCategoryId] [int] NULL,
	[SysStartTime] [datetime2](7) GENERATED ALWAYS AS ROW START NOT NULL,
	[SysEndTime] [datetime2](7) GENERATED ALWAYS AS ROW END NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductCategoryId] ASC
) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[ProductCategoryName] ASC
)ON [PRIMARY], PERIOD FOR SYSTEM_TIME ([SysStartTime], [SysEndTime])
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
WITH
(
SYSTEM_VERSIONING = ON ( HISTORY_TABLE = [dbo].[ProductCategory_History] )
)
GO

CREATE TABLE [dbo].[ProductComment](
	[ProductCommentId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[UserNameComment] [nvarchar](35) NOT NULL,
	[ProductRating] [int] NULL,
	[Comment] [nvarchar](max) NOT NULL,
	[CommentDate] [datetime] NOT NULL,
	[CommentTitle] [nvarchar](50) NOT NULL,
	[SysStartTime] [datetime2](7) GENERATED ALWAYS AS ROW START NOT NULL,
	[SysEndTime] [datetime2](7) GENERATED ALWAYS AS ROW END NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductCommentId] ASC
) ON [PRIMARY], PERIOD FOR SYSTEM_TIME ([SysStartTime], [SysEndTime])
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
WITH
(
SYSTEM_VERSIONING = ON ( HISTORY_TABLE = [dbo].[ProductComment_History] )
)
GO

CREATE TABLE [dbo].[ProductDetail](
	[ProductId] [int] NOT NULL,
	[ProductCategoryId] [int] NOT NULL,
	[PropertyId] [int] NOT NULL,
	[DetailValue] [nvarchar](max) NOT NULL,
	[SysStartTime] [datetime2](7) GENERATED ALWAYS AS ROW START NOT NULL,
	[SysEndTime] [datetime2](7) GENERATED ALWAYS AS ROW END NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC,
	[ProductCategoryId] ASC,
	[PropertyId] ASC
) ON [PRIMARY], PERIOD FOR SYSTEM_TIME ([SysStartTime], [SysEndTime])
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
WITH
(
SYSTEM_VERSIONING = ON ( HISTORY_TABLE = [dbo].[ProductDetail_History] )
)
GO

CREATE TABLE [dbo].[Properties](
	[PropertyId] [int] IDENTITY(1,1) NOT NULL,
	[ProductBaseCategoryId] [int] NOT NULL,
	[PropertyName] [nvarchar](100) NOT NULL,
	[SysStartTime] [datetime2](7) GENERATED ALWAYS AS ROW START NOT NULL,
	[SysEndTime] [datetime2](7) GENERATED ALWAYS AS ROW END NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PropertyId] ASC
) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[PropertyName] ASC
) ON [PRIMARY], PERIOD FOR SYSTEM_TIME ([SysStartTime], [SysEndTime])
) ON [PRIMARY]
WITH
(
SYSTEM_VERSIONING = ON ( HISTORY_TABLE = [dbo].[Properties_History] )
)
GO

CREATE TABLE [dbo].[Role](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](6) NOT NULL,
	[SysStartTime] [datetime2](7) GENERATED ALWAYS AS ROW START NOT NULL,
	[SysEndTime] [datetime2](7) GENERATED ALWAYS AS ROW END NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[RoleName] ASC
) ON [PRIMARY], PERIOD FOR SYSTEM_TIME ([SysStartTime], [SysEndTime])
) ON [PRIMARY]
WITH
(
SYSTEM_VERSIONING = ON ( HISTORY_TABLE = [dbo].[Role_History] )
)
GO

CREATE TABLE [dbo].[UserInvoice](
	[UserInvoiceId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[DeliveryLocationId] [int] NOT NULL,
	[DateBuy] [datetime] NOT NULL,
	[QuantityBuy] [int] NOT NULL,
	[SysStartTime] [datetime2](7) GENERATED ALWAYS AS ROW START NOT NULL,
	[SysEndTime] [datetime2](7) GENERATED ALWAYS AS ROW END NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserInvoiceId] ASC,
	[ProductId] ASC,
	[DeliveryLocationId] ASC
) ON [PRIMARY], PERIOD FOR SYSTEM_TIME ([SysStartTime], [SysEndTime])
) ON [PRIMARY]
WITH
(
SYSTEM_VERSIONING = ON ( HISTORY_TABLE = [dbo].[UserInvoice_History] )
)
GO

CREATE TABLE [dbo].[UserRole](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	[SysStartTime] [datetime2](7) GENERATED ALWAYS AS ROW START NOT NULL,
	[SysEndTime] [datetime2](7) GENERATED ALWAYS AS ROW END NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
) ON [PRIMARY], PERIOD FOR SYSTEM_TIME ([SysStartTime], [SysEndTime])
) ON [PRIMARY]
WITH
(
SYSTEM_VERSIONING = ON ( HISTORY_TABLE = [dbo].[UserRole_History] )
)
GO

CREATE TABLE [dbo].[UserT](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](50) MASKED WITH (FUNCTION = 'email()') NULL,
	[PasswordHash] [nvarchar](max) NOT NULL,
	[FirstName] [nvarchar](35) NOT NULL,
	[LastName] [nvarchar](35) NOT NULL,
	[SysStartTime] [datetime2](7) GENERATED ALWAYS AS ROW START NOT NULL,
	[SysEndTime] [datetime2](7) GENERATED ALWAYS AS ROW END NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Email] ASC
) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Email] ASC
) ON [PRIMARY],	PERIOD FOR SYSTEM_TIME ([SysStartTime], [SysEndTime])
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
WITH
(
SYSTEM_VERSIONING = ON ( HISTORY_TABLE = [dbo].[UserT_History] )
)
GO

CREATE TABLE [dbo].[Cart](
	[UserId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[QuantityBuy] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[ProductId] ASC
) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Salt](
	[SaltId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[SaltPass] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SaltId] ASC
) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[UserId] ASC
) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Cart] ADD  DEFAULT ((1)) FOR [QuantityBuy]
GO

ALTER TABLE [dbo].[Product] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO

ALTER TABLE [dbo].[Product] ADD  DEFAULT ((1)) FOR [Quantity]
GO

ALTER TABLE [dbo].[ProductComment] ADD  DEFAULT ((5)) FOR [ProductRating]
GO

ALTER TABLE [dbo].[ProductComment] ADD  DEFAULT (getdate()) FOR [CommentDate]
GO

ALTER TABLE [dbo].[UserInvoice] ADD  DEFAULT (getdate()) FOR [DateBuy]
GO

ALTER TABLE [dbo].[UserInvoice] ADD  DEFAULT ((1)) FOR [QuantityBuy]
GO

ALTER TABLE [dbo].[Cart]  WITH CHECK ADD FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Cart]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[UserT] ([UserId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[DeliveryLocation]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[UserT] ([UserId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Product]  WITH CHECK ADD FOREIGN KEY([ManufacturerId])
REFERENCES [dbo].[Manufacturer] ([ManufacturerId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[ProductCategory]  WITH CHECK ADD FOREIGN KEY([ParentProductCategoryId])
REFERENCES [dbo].[ProductCategory] ([ProductCategoryId])
GO

ALTER TABLE [dbo].[ProductComment]  WITH CHECK ADD FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[ProductDetail]  WITH CHECK ADD FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[ProductDetail]  WITH CHECK ADD FOREIGN KEY([ProductCategoryId])
REFERENCES [dbo].[ProductCategory] ([ProductCategoryId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[ProductDetail]  WITH CHECK ADD FOREIGN KEY([PropertyId])
REFERENCES [dbo].[Properties] ([PropertyId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Salt]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[UserT] ([UserId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[UserInvoice]  WITH CHECK ADD FOREIGN KEY([DeliveryLocationId])
REFERENCES [dbo].[DeliveryLocation] ([DeliveryLocationId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[UserInvoice]  WITH CHECK ADD FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([RoleId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[UserT] ([UserId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[ProductComment]  WITH CHECK ADD CHECK  (((1)<=[ProductRating] AND [ProductRating]<=(5)))
GO
