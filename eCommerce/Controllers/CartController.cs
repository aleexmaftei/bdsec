using AutoMapper;
using Braintree;
using eCommerce.BusinessLogic;
using eCommerce.BusinessLogic.ProductServices;
using eCommerce.DataAccess;
using eCommerce.Models.CartVM;
using eCommerce.Models.MyProfileVM.DeliveryLocation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace eCommerce.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService CartService;
        private readonly ProductService ProductService;
        private readonly DeliveryLocationService DeliveryLocationService;
        private readonly PaymentService PaymentService;

        private readonly IMapper Mapper;
        public CartController(CartService cartService,
                              ProductService productService,
                              DeliveryLocationService deliveryLocationService,
                              PaymentService paymentService,
                              IMapper mapper)
        {
            CartService = cartService;
            ProductService = productService;
            DeliveryLocationService = deliveryLocationService;
            PaymentService = paymentService;
            Mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var cartProducts = CartService.GetAllCartProductsNotDeletedNotOrderPlaced();
            if (cartProducts == null)
            {
                return NotFound();
            }

            double totalSum = 0;
            foreach (var cart in cartProducts)
            {
                totalSum += (cart.Product.ProductPrice * cart.QuantityBuy);
            }

            var deliveryLocations = DeliveryLocationService.GetDeliveryLocationsCurrentUser();
            var model = new CartListVM()
            {
                TotalSum = totalSum,
                CartList = cartProducts.Select(c => Mapper.Map<Cart, CartVM>(c)).ToList(),
                DeliveryLocations = deliveryLocations.Select(c => Mapper.Map<DeliveryLocation, DeliveryLocationVm>(c)).ToList()
            };

            var clientToken = PaymentService.GenerateClientToken();
            ViewBag.ClientToken = clientToken;
            ViewBag.TotalSum = totalSum;

            return View("../Cart/Cart", model);
        }

        [HttpGet]
        public IActionResult JsCartModalView()
        {
            var cartProducts = CartService.GetAllCartProductsNotDeletedNotOrderPlaced();
            if (cartProducts == null)
            {
                return NotFound();
            }
            
            double totalSum = 0;
            foreach (var cart in cartProducts)
            {
                
                totalSum += (cart.Product.ProductPrice * cart.QuantityBuy);
            }

            var model = new CartListVM()
            {
                TotalSum = totalSum,
                CartList = cartProducts.Select(c => Mapper.Map<Cart, CartVM>(c)).ToList()
            };

            return Json(new { 
                cartModelList = model.CartList,
                flag = true
            });
        }

        [HttpPost]
        public IActionResult PlaceOrder(int deliveryLocationId)
        {
            var cartList = CartService.GetAllCartProductsNotDeletedNotOrderPlaced();
            if (cartList == null)
            {
                return Json(new {
                    flag = false
                });
            }

            CartService.PlaceOrder(cartList.ToList(), deliveryLocationId);

            return Json(new {
                flag = true
            });
        }

        [HttpPost]
        public IActionResult AddProductToCart(int productId, int quantityToBuy)
        {
            var isAlreadyInCart = false;

            var productFromCart = CartService.GetCartByProductIdAndUser(productId);
            if (productFromCart != null)
            {
                isAlreadyInCart = true;
                productFromCart.QuantityBuy += quantityToBuy;
                
                var isUpdated = CartService.UpdateQuantityToBuy(productFromCart);
                if(isUpdated == false)
                {
                    return Json(new { 
                        flag = false
                    });
                }
            }

            if(isAlreadyInCart == false)
            {
                var product = ProductService.GetProductById(productId);
                if (product == null)
                {
                    return NotFound();
                }

                var model = new CartVM()
                {
                    ProductId = productId,
                    ProductPrice = product.ProductPrice,
                    ProductName = product.ProductName,
                    ProductImage = product.ProductImage,
                    QuantityBuy = quantityToBuy
                };
                var modelMappedToEntity = Mapper.Map<Cart>(model);

                CartService.InsertToCart(modelMappedToEntity);
            }
            

            return Json(new { 
                flag = true
            });
        }

        [HttpPost]
        public IActionResult UpdateQuantityToBuy(int productId, int quantityToBuy)
        {
            var productFromCart = CartService.GetCartByProductIdAndUser(productId);
            if(productFromCart == null)
            {
                return Json(new{
                    flag = false
                });
            }

            productFromCart.QuantityBuy = quantityToBuy;
            
            var isUpdated = CartService.UpdateQuantityToBuy(productFromCart);
            if (isUpdated == false)
            {
                return Json(new
                {
                    flag = false
                });
            }

            return Json(new { 
                flag = true
            });
        }

        [HttpPost]
        public IActionResult RemoveProductFromCart(int productId)
        {
            var cartToDelete = CartService.GetCartByProductIdAndUser(productId);
            if(cartToDelete == null)
            {
                return NotFound();
            }
            
            CartService.DeleteProductFromCart(cartToDelete);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult JsRemoveProductFromCart(int productId)
        {
            var cartToDelete = CartService.GetCartByProductIdAndUser(productId);
            if(cartToDelete == null)
            {
                return NotFound();
            }
            CartService.DeleteProductFromCart(cartToDelete);

            return Json(new{
                flag = true
            });
        }
    }
}
