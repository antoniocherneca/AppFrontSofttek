using Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;

namespace AppFrontSofttek.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Ingresar(LoginDto loginDto)
        {
            return View("~/Views/Home/Index.cshtml");
        }
    }
}
