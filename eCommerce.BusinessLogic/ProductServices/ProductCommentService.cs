using eCommerce.BusinessLogic.Base;
using eCommerce.Data;
using eCommerce.DataAccess;
using eCommerce.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;

namespace eCommerce.BusinessLogic.ProductServices
{
    public class ProductCommentService : BaseService
    {
        private readonly CurrentUserDto CurrentUserDto;

        public ProductCommentService(UnitOfWork uow,
                                     CurrentUserDto currentUserDto)
            : base(uow)
        {
            CurrentUserDto = currentUserDto;
        }

        public IEnumerable<ProductComment> GetAllProductCommentsByProductId(int productId)
        {
            return UnitOfWork.ProductComments.Get()
                .Where(cnd => cnd.ProductId == productId)
                .ToList();
        }

        public ProductComment GetProductCommentById(int productCommentId)
        {
            return UnitOfWork.ProductComments.Get()
                .FirstOrDefault(cnd => cnd.ProductCommentId == productCommentId);
        }


        [DllImport("utils.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr cleanStrings(string str);

        public bool InsertComment(ProductComment productComment)
        {
            if (CurrentUserDto.IsAuthenticated == false || productComment == null)
            {
                return false;
            }

            return ExecuteInTransaction(uow =>
            {

                string userName = $"{CurrentUserDto.FirstName} {CurrentUserDto.LastName}";

                productComment.UserNameComment = userName;

                productComment.Comment = Marshal.PtrToStringAnsi(cleanStrings(productComment.Comment));

                uow.ProductComments.Insert(productComment);
                uow.SaveChanges();

                return true;
            });
        }

        public bool DeleteProductComment(ProductComment productComment)
        {
            if (productComment == null)
            {
                return false;
            }

            return ExecuteInTransaction(uow =>
            {
                uow.ProductComments.Delete(productComment);
                uow.SaveChanges();

                return true;
            });
        }
    }
}
