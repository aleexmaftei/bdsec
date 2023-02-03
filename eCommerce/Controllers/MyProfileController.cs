using System.Linq;
using AutoMapper;
using eCommerce.BusinessLogic;
using eCommerce.BusinessLogic.ProductServices;
using eCommerce.DataAccess;
using eCommerce.Models.MyProfileVM;
using eCommerce.Models.MyProfileVM.DeliveryLocation;
using eCommerce.Models.MyProfileVM.Invoice;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class MyProfileController : Controller
    {
        private readonly ProductService ProductService;
        private readonly DeliveryLocationService DeliveryLocationService;
        private readonly UserInvoiceService UserInvoiceService;
        private readonly IMapper Mapper;

        public MyProfileController(ProductService productService,
                                   DeliveryLocationService deliveryLocationService,
                                   UserInvoiceService userInvoiceService,
                                   IMapper mapper)
        {
            ProductService = productService;
            DeliveryLocationService = deliveryLocationService;
            UserInvoiceService = userInvoiceService;
            Mapper = mapper;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var model = new MyProfileVm();

            var lastInvoice = UserInvoiceService.LastInvoiceEmmitedForCurrentUser();
            if(lastInvoice != null)
            {
                var userInvoiceModel = new UserInvoiceVm()
                {
                    ProductId = lastInvoice.ProductId,
                    DateBuy = lastInvoice.DateBuy,
                    QuantityBuy = lastInvoice.QuantityBuy,
                    AdressDetail = lastInvoice.DeliveryLocation.AddressDetail,
                    CityName = lastInvoice.DeliveryLocation.CityName,
                    RegionName = lastInvoice.DeliveryLocation.RegionName
                };

                model.LastInvoice = userInvoiceModel;
            }

            var deliveryLocations = DeliveryLocationService.GetDeliveryLocationsCurrentUser();
            if(deliveryLocations != null)
            {
                model.DeliveryLocations = deliveryLocations.Select(c => Mapper.Map<DeliveryLocation, DeliveryLocationVm>(c)).ToList();
            }
            return View("MyProfile", model);
        }

        [HttpGet]
        public IActionResult Invoices()
        {
            var allInvoices = UserInvoiceService.GetInvoices();
            if(allInvoices == null)
            {
                return NotFound();
            }

            var allInvoicesModel = new AllInvoicesVM();
            foreach(var oneInvoice in allInvoices)
            {
                var invoiceList = new InvoiceListVM();
                
                var invoicesById = UserInvoiceService.GetInvoicesById(oneInvoice.UserInvoiceId).ToList();
                if(invoicesById == null)
                {
                    return NotFound();
                }

                var isAlreadyOnInvoice = false;
                foreach (var value in allInvoicesModel.AllInvoices)
                {
                    if (value.UserInvoiceId == oneInvoice.UserInvoiceId)
                    {
                        isAlreadyOnInvoice = true;
                        break;
                    }
                }

                if(isAlreadyOnInvoice == true)
                {
                    continue;
                }

                foreach(var currentOrderInvoice in invoicesById)
                {
                    var currentProduct = ProductService.GetProductById(currentOrderInvoice.ProductId);
                    if(currentProduct == null)
                    {
                        return NotFound();
                    }

                    var oneInvoiceModel = new OneInvoicePropertiesVM()
                    {
                        ProductName = currentProduct.ProductName,
                        QuantityBuy = currentOrderInvoice.QuantityBuy
                    };

                    invoiceList.UserInvoiceId = currentOrderInvoice.UserInvoiceId;
                    invoiceList.DateBuy = currentOrderInvoice.DateBuy;

                    invoiceList.InvoiceList.Add(oneInvoiceModel);
                }

                allInvoicesModel.AllInvoices.Add(invoiceList);
            }

            return View("Invoices", allInvoicesModel);
        }

        [HttpGet]
        public IActionResult InsertDeliveryLocation()
        {
            var model = new AddDeliveryLocationVM();
            return View("../MyProfile/DeliveryLocation/AddDeliveryLocation", model);
        }

        [HttpPost]
        public IActionResult InsertDeliveryLocation(DeliveryLocation deliveryLocation)
        {
            if(!ModelState.IsValid)
            {
                return Json(new {
                    flag = false
                });
            }

            DeliveryLocationService.InsertDeliveryLocation(deliveryLocation);

            return RedirectToAction("InsertDeliveryLocation", "MyProfile");
        }

        [HttpGet]
        public IActionResult ViewDeliveryLocations()
        {
            var listDeliveryLocation = DeliveryLocationService.GetDeliveryLocationsCurrentUser().ToList();
            if(listDeliveryLocation == null)
            {
                return NotFound();
            }

            var model = new ListDeliveryLocationVm();
            foreach(var deliveryLocation in listDeliveryLocation)
            {
                var modelMapped = Mapper.Map <DeliveryLocationVm>(deliveryLocation);
                model.DeliveryLocations.Add(modelMapped);
            }

            return View("DeliveryLocation/DeliveryLocations", model);
        }
        
        [HttpGet]
        public IActionResult ModifyOneDeliveryLocation(int deliveryLocationId)
        {
            var deliveryLocation = DeliveryLocationService.GetDeliveryLocationById(deliveryLocationId);
            if(deliveryLocation == null)
            {
                return NotFound();
            }
            
            var modelMapped = Mapper.Map<DeliveryLocationVm>(deliveryLocation);

            return View("DeliveryLocation/ModifyOneDeliveryLocation", modelMapped);
        }

        [HttpPost]
        public IActionResult ModifyOneDeliveryLocation(int deliveryLocationId, DeliveryLocation newDeliveryLocation)
        {
            var oldDeliveryLocation = DeliveryLocationService.GetDeliveryLocationById(deliveryLocationId);
            if(oldDeliveryLocation == null)
            {
                return NotFound();
            }

            DeliveryLocationService.UpdateDeliveryLocation(oldDeliveryLocation, newDeliveryLocation);
         
            return RedirectToAction("ViewDeliveryLocations", "MyProfile");
        }

        [HttpPost]
        public IActionResult DeleteDeliveryAdress(int deliveryLocationId)
        {
            var deliveryLocationToDelete = DeliveryLocationService.GetDeliveryLocationById(deliveryLocationId);
            if(deliveryLocationToDelete == null)
            {
                return Json(new {
                    flag = false
                });
            }

            if(DeliveryLocationService.DeleteDeliveryLocation(deliveryLocationToDelete) == null)
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
    }
}
