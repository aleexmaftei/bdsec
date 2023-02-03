using AutoMapper;
using eCommerce.BusinessLogic.ProductServices;
using eCommerce.DataAccess;
using eCommerce.Models.ProductVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class ProductCommentsController : Controller
    {
        private readonly ProductCommentService ProductCommentService;
        private readonly IMapper Mapper;

        public ProductCommentsController(ProductCommentService productCommentService,
                                         IMapper mapper)
        {
            ProductCommentService = productCommentService;
            Mapper = mapper;
        }

        [HttpGet]
        public IActionResult AddComment(int productId)
        {
            var model = new ProductCommentVM()
            {
                ProductId = productId
            };

            return View("../Products/AddComment", model);
        }

        [HttpPost]
        public IActionResult AddComment(int productId, ProductCommentVM productCommentModel)
        {
            productCommentModel.ProductId = productId;

            var modelMapped = Mapper.Map<ProductComment>(productCommentModel);
            ProductCommentService.InsertComment(modelMapped);

            return RedirectToAction("ViewFullSpecsProduct", "ProductDetails", new { productId = productId });
        }
    }
}
