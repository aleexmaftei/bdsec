using eCommerce.BusinessLogic.Base;
using eCommerce.Data;
using eCommerce.DataAccess;
using eCommerce.Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eCommerce.BusinessLogic.ProductServices
{
    public class CartService : BaseService
    {
        private readonly CurrentUserDto CurrentUserCartDto;
        private readonly UserService UserService;
        private readonly ProductService ProductService;
        private readonly UserInvoiceService UserInvoiceService;

        public CartService(UnitOfWork uow, 
                           CurrentUserDto currentUserCartDto, 
                           UserService userService,
                           ProductService productService,
                           UserInvoiceService userInvoiceService)
            :base(uow)
        {
            CurrentUserCartDto = currentUserCartDto;
            UserService = userService;
            ProductService = productService;
            UserInvoiceService = userInvoiceService;
        }

        public IEnumerable<Cart> GetAllCartProductsNotDeletedNotOrderPlaced()
        {
            return UnitOfWork.Carts.Get()
                .Include(c => c.Product)
                .Where(cnd => cnd.UserId == Int32.Parse(CurrentUserCartDto.Id) && 
                              cnd.Product.IsDeleted == false)
                .ToList();
        }

        public Cart GetCartByProductIdAndUser(int productId)
        {
            return UnitOfWork.Carts.Get()
                .FirstOrDefault(cnd => cnd.UserId == Int32.Parse(CurrentUserCartDto.Id) && 
                                       cnd.ProductId == productId);
        }

        public Cart InsertToCart(Cart cart)
        {
            var currentUserId = Int32.Parse(CurrentUserCartDto.Id);

            var currentUser = UserService.GetUserByUserId(currentUserId);
            if (currentUser == null)
            {
                return cart;
            }
            
            return ExecuteInTransaction(uow =>
            {
                cart.UserId = currentUser.UserId;

                uow.Carts.Insert(cart);
                uow.SaveChanges();

                return cart;
            });
        }

        public bool UpdateQuantityToBuy(Cart cart)
        {
            if(CurrentUserCartDto == null)
            {
                return false;
            }

            return ExecuteInTransaction(uow => {

                uow.Carts.Update(cart);
                uow.SaveChanges();

                return true;
            });
            
        }

        public IEnumerable<Cart> PlaceOrder(List<Cart> carts, int deliveryLocationId)
        {
            var currentUserId = Int32.Parse(CurrentUserCartDto.Id);

            var currentUser = UserService.GetUserByUserId(currentUserId);
            if (currentUser == null)
            {
                return carts;
            }

            return ExecuteInTransaction(uow =>
            {
                var userInvoicesList = new List<UserInvoice>();

                var currentInvoiceNumber = UserInvoiceService.GetNumberOfInvoices() + 1;
                foreach (var cart in carts)
                {
                    var currentProduct = ProductService.GetProductById(cart.ProductId);
                    if(currentProduct == null)
                    {
                        return carts;
                    }

                    var userInvoice = new UserInvoice()
                    {
                        UserInvoiceId = currentInvoiceNumber,
                        DeliveryLocationId = deliveryLocationId,
                        QuantityBuy = cart.QuantityBuy,
                        ProductId = currentProduct.ProductId
                    };

                    userInvoicesList.Add(userInvoice);
                }

                uow.Carts.DeleteList(carts);

                uow.UserInvoices.InsertList(userInvoicesList);
                uow.SaveChanges();
                
                return carts;
            });
        }

        public void DeleteProductFromCart(Cart cart)
        {
            UnitOfWork.Carts.Delete(cart);
            UnitOfWork.SaveChanges();
        }

    }
}
