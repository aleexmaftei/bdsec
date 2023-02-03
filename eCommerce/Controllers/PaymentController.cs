using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Braintree;
using eCommerce.BusinessLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class PaymentController : Controller
    {
        private readonly PaymentService PaymentService;

        public PaymentController(PaymentService paymentService)
        {
            PaymentService = paymentService;
        }

        
        [HttpPost]
        public IActionResult CheckoutPayment(string paymentMethodNonce, double totalSum, int deliveryLocationId)
        {
            string nonceFromTheClient = paymentMethodNonce;
            decimal amount;

            try
            {
                amount = Convert.ToDecimal(totalSum);
            }
            catch (FormatException)
            {
                return Json(new { flag = false });
            }

            var result = PaymentService.CreateTransaction(amount, nonceFromTheClient, deliveryLocationId);
            
            if (result.IsSuccess())
            {
                Transaction transaction = result.Target;
                return Json(new { 
                    flag = true,
                    transactionId = transaction.Id
                });

            }
            else if (result.Transaction != null)
            {
                return Json(new { 
                    flag = true,
                    transactionId = result.Transaction.Id
                });
            }
            else
            {
                string errorMessages = "";
                foreach (ValidationError error in result.Errors.DeepAll())
                {
                    errorMessages += "Error: " + (int)error.Code + " - " + error.Message + "\n";
                }
                return Json(new { flag = false });

            }
        }

        [HttpPost]
        public IActionResult AddPaymentAddress(int deliveryLocationId)
        {
            var result = PaymentService.CreateAddress(deliveryLocationId);
            if(result == null)
            {
                return Json(new { flag = false });
            }

            return Json(new { flag = true });
        }

        [HttpGet]
        public IActionResult CheckTransactionStatus(string transactionId)
        {
            var isAccepted = PaymentService.CheckTransactionStatus(transactionId);
            if (isAccepted == true)
            {
                return Json(new { flag = true });
            }
            
            return Json(new { flag = false });
        }

    }
}
