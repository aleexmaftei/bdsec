using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers
{
    [Route("Error")]
    public class ErrorController : Controller
    {
        [Route("NotFoundView")]
        public IActionResult NotFoundView()
        {
            return View("NotFound");
        }
    }
}
