using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using eCommerce.BusinessLogic;
using eCommerce.DataAccess;
using eCommerce.Entities.DTOs;
using eCommerce.Models.MyProfileVM.ChangePassword;
using eCommerce.Models.UserAccountVM;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers
{
    public class UserAccountController : Controller
    {
        private readonly UserAccountService UserAccountService;
        private readonly IMapper Mapper;

        public UserAccountController(UserAccountService userAccountService, 
                                     IMapper mapper)
        { 
            UserAccountService = userAccountService;
            Mapper = mapper;
        }


        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterVM();
            return View("Register", model);
        }

        [HttpPost]
        public IActionResult Register(RegisterVM model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var user = Mapper.Map<UserT>(model);
            var deliveryLocation = Mapper.Map<DeliveryLocation>(model);

            UserAccountService.RegisterNewUser(user, deliveryLocation);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            var model = new LoginVM();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = UserAccountService.Login(model.Email, model.Password);

            if (user == null)
            {
                model.AreCredentialsInvalid = true;

                return View(model);
            }

            await LogIn(user);

            return Json(new{
                flag = true
            }) ;
        }

        private async Task LogIn(UserT user)
        {
            var claims = new List<Claim>
            {
                new Claim("Id", user.UserId.ToString()),
                new Claim("Email", user.Email.ToString()),
                new Claim("FirstName", user.FirstName.ToString()),
                new Claim("LastName", user.LastName.ToString()),
            };

            user.UserRole
                .ToList()
                .ForEach(r => claims.Add(new Claim(ClaimTypes.Role, r.Role.RoleName)));

            var identity = new ClaimsIdentity(claims, "Cookies");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                    scheme: "eCommerceCookies",
                    principal: principal);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await LogOut();

            return RedirectToAction("Index", "Home");
        }

        private async Task LogOut()
        {
            await HttpContext.SignOutAsync(scheme: "eCommerceCookies");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMyAccount()
        {
            await LogOut();
            UserAccountService.DeleteUser();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            var model = new ChangePasswordVm();
            return View("../MyProfile/ChangePassword", model);
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordVm model)
        {
            if (!ModelState.IsValid)
            {
                return View("../MyProfile/ChangePassword", model);
            }
            
            if(model.ConfirmNewPassword != model.NewPassword)
            {
                return View("../MyProfile/ChangePassword", model);
            }

            bool isChanged = UserAccountService.UpdateUserPassword(model.NewPassword, model.OldPassword);
            if(isChanged == false)
            {
                return View("../MyProfile/ChangePassword", model);
            } 
            
            return View("../MyProfile/MyProfile", model);
        }

    }
}
