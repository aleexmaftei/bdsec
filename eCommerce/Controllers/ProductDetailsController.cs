using AutoMapper;
using eCommerce.BusinessLogic;
using eCommerce.BusinessLogic.ProductServices;
using eCommerce.DataAccess;
using eCommerce.Entities.Entities;
using eCommerce.Models.ProductVM;
using eCommerce.Models.ProductVM.ProductsWithValues;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eCommerce.Controllers
{
    public class ProductDetailsController : Controller
    {
        private readonly ProductDetailsService ProductDetailsService;
        private readonly ProductService ProductService;
        private readonly ProductCommentService ProductCommentService;
        private readonly ManufacturerService ManufacturerService;
        private readonly IMapper Mapper;

        public ProductDetailsController(ProductDetailsService productDetailsService,
                                        ProductService productService,
                                        ProductCommentService productCommentService,
                                        ManufacturerService manufacturerService,
                                        IMapper mapper)
        {
            ProductDetailsService = productDetailsService;
            ProductService = productService;
            ProductCommentService = productCommentService;
            ManufacturerService = manufacturerService;
            Mapper = mapper;
        }

        
        [HttpPost]
        public IActionResult Search(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return Json(new { flag = false });
            }
            
            var searchInProducts = ProductService.GetAllProductsNotDeleted();
            if(searchInProducts == null)
            {
                return Json(new { flag = false });
            }

            search = search.ToUpper();
            
            var productsFound = searchInProducts.Where(cnd => $"{cnd.Manufacturer.ManufacturerName.ToUpper()} {cnd.ProductName.ToUpper()}".Contains(search))
                                                .ToList();
            
            var allProducts = new AllProductsVM()
            {
                ProductsCount = productsFound.Count(),
            };

            foreach (var product in productsFound)
            {
                var propertiesList = new ProductDetailsVM()
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    ProductPrice = product.ProductPrice,
                    ProductImage = product.ProductImage,
                    ManufacturerLogo = product.Manufacturer.ManufacturerLogo,
                    ManufacturerName = product.Manufacturer.ManufacturerName,
                    Quantity = product.Quantity
                };

                var productsByProductId = ProductDetailsService.GetAllProductDetailsByProductId(product.ProductId);
                foreach (var productByProductId in productsByProductId)
                {
                    propertiesList.AllPropertiesWithValues.Add(new PropertyWithValueVM()
                    {
                        PropertyName = productByProductId.Property.PropertyName,
                        Value = productByProductId.DetailValue
                    });

                }

                allProducts.Allproducts.Add(propertiesList);
            }

            return View("../Products/SearchProducts", allProducts);
        }

        [HttpGet]
        public IActionResult ListProductDetailsByCategory(int currentPage, int categoryId)
        {
            if(currentPage <= 0)
            {
                return NotFound();
            }
            

            var products = ProductService.GetAllProductsNotDeleted(currentPage, categoryId);
            
            if (products == null)
            {
                return NotFound();
            }

            var allProducts = new AllProductsVM()
            {
                
                CategoryId = categoryId
            };

            foreach (var product in products)
            {
                var currentProductProperties = ProductService.GetProductById(product.ProductId);
                if(currentProductProperties == null)
                {
                    return NotFound();
                }

                var propertiesList = new ProductDetailsVM()
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    ProductPrice = product.ProductPrice,
                    ProductImage = product.ProductImage,
                    ManufacturerLogo = currentProductProperties.Manufacturer.ManufacturerLogo,
                    ManufacturerName = currentProductProperties.Manufacturer.ManufacturerName,
                    Quantity = product.Quantity
                };

                var productsByProductId = ProductDetailsService.GetAllProductDetailsNotDeleted(product.ProductId, categoryId);
                foreach (var productByProductId in productsByProductId)
                {
                    propertiesList.AllPropertiesWithValues.Add(new PropertyWithValueVM()
                    {
                        PropertyName = productByProductId.Property.PropertyName,
                        Value = productByProductId.DetailValue
                    });

                }

                allProducts.Allproducts.Add(propertiesList);
            }

            var manufacturers = ManufacturerService.GetManufacturersByProductCategory(categoryId);
            if (manufacturers == null)
            {
                return NotFound();
            }

            allProducts.Manufacturers = manufacturers.Select(c => Mapper.Map<Manufacturer, ManufacturerVM>(c)).ToList();

            return View("../Products/Products", allProducts);

        }

        [HttpPost]
        public IActionResult ListProductDetailsByCategory(int categoryId, FiltersVM filtersModel)
        {
            var filters = new Filters()
            {
                ManufacturersFilter = filtersModel.ManufacturersFilter,
                MinPriceFilter = filtersModel.MinPriceFilter,
                MaxPriceFilter = filtersModel.MaxPriceFilter
            };

            var products = ProductService.GetAllProductsFilteredBy(filters);
            if (products == null)
            {
                return NotFound();
            }

            var allProducts = new AllProductsVM()
            {
                ProductsCount = products.Count(),
                CategoryId = categoryId
            };

            foreach (var product in products)
            {
                var propertiesList = new ProductDetailsVM()
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    ProductPrice = product.ProductPrice,
                    ProductImage = product.ProductImage,
                    ManufacturerLogo = product.Manufacturer.ManufacturerLogo,
                    ManufacturerName = product.Manufacturer.ManufacturerName,
                    Quantity = product.Quantity
                };

                var productsByProductId = ProductDetailsService.GetAllProductDetailsNotDeleted(product.ProductId, categoryId);
                foreach (var productByProductId in productsByProductId)
                {
                    propertiesList.AllPropertiesWithValues.Add(new PropertyWithValueVM()
                    {
                        PropertyName = productByProductId.Property.PropertyName,
                        Value = productByProductId.DetailValue
                    });

                }

                allProducts.Allproducts.Add(propertiesList);
            }

            var manufacturers = ManufacturerService.GetAllManufacturers();
            if (manufacturers == null)
            {
                return NotFound();
            }

            allProducts.Manufacturers = manufacturers.Select(c => Mapper.Map<Manufacturer, ManufacturerVM>(c)).ToList();

            return View("../Products/_Products", allProducts);
        }


        [HttpGet]
        public IActionResult ViewFullSpecsProduct(int productId)
        {
            var currentProduct = ProductService.GetProductById(productId);
            if(currentProduct == null)
            {
                return NotFound();
            }

            var currentProductDetails = ProductDetailsService.GetAllProductDetailsByProductId(productId);
            if(currentProductDetails == null)
            {
                return NotFound();
            }

            var productModel = new ProductDetailsVM()
            {
                ManufacturerLogo = currentProduct.Manufacturer.ManufacturerLogo,
                ManufacturerName = currentProduct.Manufacturer.ManufacturerName,
                ProductId = currentProduct.ProductId,
                ProductName = currentProduct.ProductName,
                ProductPrice = currentProduct.ProductPrice,
                ProductImage = currentProduct.ProductImage,
                QuantityBuy = 1
            };

            foreach (var productByProductId in currentProductDetails)
            {
                productModel.AllPropertiesWithValues.Add(new PropertyWithValueVM()
                {
                    PropertyName = productByProductId.Property.PropertyName,
                    Value = productByProductId.DetailValue
                });
            }


            var modelProductComments = new List<ProductCommentVM>();
            var currentProductComments = ProductCommentService.GetAllProductCommentsByProductId(productId).ToList();

            foreach (var productComment in currentProductComments)
            {
                var modelMapped = Mapper.Map<ProductCommentVM>(productComment);
                modelProductComments.Add(modelMapped);
            }

            
            double starsSum = 0;
            foreach(var comment in modelProductComments)
            {
                starsSum += comment.ProductRating;
            }

            var numberOfEvaluations = currentProductComments.Count();
            var avgStars = starsSum / numberOfEvaluations;

            var model = new ProductInfoVM()
            {
                IsDeleted = currentProduct.IsDeleted,
                Quantity = currentProduct.Quantity,
                NumberOfEvaluations = numberOfEvaluations,
                ProductDetails = productModel,
                ProductComments = modelProductComments,
                AvgStars = avgStars
            };

            return View("../Products/ProductDetails", model);
        }
    }

}
