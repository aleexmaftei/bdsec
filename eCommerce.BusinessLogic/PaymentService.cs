using Braintree;
using Braintree.Exceptions;
using eCommerce.BusinessLogic.Base;
using eCommerce.Data;
using eCommerce.Entities.DTOs;
using System.Linq;

namespace eCommerce.BusinessLogic
{
    public class PaymentService : BaseService
    {
        private readonly BraintreeGateway Gateway;
        private readonly CurrentUserDto CurrentUserDto;
        private readonly UserService UserService;
        private readonly DeliveryLocationService DeliveryLocationService;

        private static readonly TransactionStatus[] TransactionSuccessStatuses =
                                                    {
                                                        TransactionStatus.AUTHORIZED,
                                                        TransactionStatus.AUTHORIZING,
                                                        TransactionStatus.SETTLED,
                                                        TransactionStatus.SETTLING,
                                                        TransactionStatus.SETTLEMENT_CONFIRMED,
                                                        TransactionStatus.SETTLEMENT_PENDING,
                                                        TransactionStatus.SUBMITTED_FOR_SETTLEMENT
                                                    };

        public PaymentService(UnitOfWork uow,
                              CurrentUserDto currentUserDto,
                              UserService userService,
                              DeliveryLocationService deliveryLocationService)
            :base(uow)
        {
            CurrentUserDto = currentUserDto;
            UserService = userService;
            DeliveryLocationService = deliveryLocationService;

            Gateway = new BraintreeGateway
            {
                Environment = Braintree.Environment.SANDBOX,
                MerchantId = "h4fcbb49ytxv4cdz",
                PublicKey = "cqw8bjp523gjwkzx",
                PrivateKey = "d5b06a4d3befb10fd4e0118bc4655b92"
            };
        }

        public string GenerateClientToken()
        {
            try
            {
                Customer customer = Gateway.Customer.Find(CurrentUserDto.Id);
                return Gateway.ClientToken.Generate(
                            new ClientTokenRequest
                            {
                                CustomerId = customer.Id
                            });
            } 
            catch(NotFoundException)
            {
                var currentUser = UserService.GetCurrentUser();
                if(currentUser == null)
                {
                    return null;
                }

                var customerRequest = new CustomerRequest
                {
                    FirstName = currentUser.FirstName,
                    LastName = currentUser.LastName,
                    Email = currentUser.Email,
                    Id = currentUser.UserId.ToString()
                };
                

                Result<Customer> resultCustomer = Gateway.Customer.Create(customerRequest);

                if (resultCustomer.IsSuccess() == true)
                {
                    return Gateway.ClientToken.Generate(
                                new ClientTokenRequest
                                {
                                    CustomerId = resultCustomer.Target.Id
                                });
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        public Result<Transaction> CreateTransaction(decimal amount, string nonceFromTheClient, int deliveryLocationId)
        {
            var currentUser = UserService.GetCurrentUser();
            if (currentUser == null)
            {
                return null;
            }

            var currentSelectedAddress = DeliveryLocationService.GetDeliveryLocationById(deliveryLocationId);
            if (currentSelectedAddress == null)
            {
                return null;
            }
            
            var addressRequest = new AddressRequest
            {
                FirstName = currentUser.FirstName,
                LastName = currentUser.LastName,
                StreetAddress = currentSelectedAddress.AddressDetail,
                Locality = currentSelectedAddress.CityName,
                Region = currentSelectedAddress.RegionName,
                PostalCode = currentSelectedAddress.PostalCode.ToString(),
                CountryName = currentSelectedAddress.CountryName
                
            };

            var transactionRequest = new TransactionRequest
            {
                Amount = amount,
                PaymentMethodNonce = nonceFromTheClient,
                BillingAddress = addressRequest,
                BillingAddressId = currentSelectedAddress.DeliveryLocationId.ToString(),

                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true
                }
            };

            Result<Transaction> result = Gateway.Transaction.Sale(transactionRequest);
            return result;
        }

        public Result<Address> CreateAddress(int deliveryLocationId)
        {
            var currentUser = UserService.GetCurrentUser();
            if(currentUser == null)
            {
                return null;
            }

            var currentSelectedAddress = DeliveryLocationService.GetDeliveryLocationById(deliveryLocationId);
            if(currentSelectedAddress == null)
            {
                return null;
            }

            var addressRequest = new AddressRequest
            {
                FirstName = currentUser.FirstName,
                LastName = currentUser.LastName,
                StreetAddress = currentSelectedAddress.AddressDetail,
                Locality = currentSelectedAddress.CityName,
                Region = currentSelectedAddress.RegionName,
                PostalCode = currentSelectedAddress.PostalCode.ToString(),
                CountryName = currentSelectedAddress.CountryName
            };

            Result<Address> resultAddress = Gateway.Address.Create(currentUser.UserId.ToString(), addressRequest);

            if(resultAddress.IsSuccess() == true)
            {
                return resultAddress;
            }
            
            return null;
        }

        public bool CheckTransactionStatus(string transactionId)
        {
            try
            {
                Transaction transaction = Gateway.Transaction.Find(transactionId);
                if(TransactionSuccessStatuses.Contains(transaction.Status))
                {
                    return true;
                }
                
            }
            catch
            {
                return false;
            }
            return false;
        }
    }
}
