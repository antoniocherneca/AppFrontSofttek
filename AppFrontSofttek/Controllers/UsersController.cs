using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppFrontSofttek.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UsersAddPartial()
        {
            return PartialView("~/Views/Users/Partial/UsersAddPartial.cshtml");
        }
    }
}
