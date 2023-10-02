using AppFrontSofttek.ViewModels;
using Data.Base;
using Data.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text.Json;

namespace AppFrontSofttek.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClient;

        public LoginController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LogIn(LoginDto loginDto)
        {
            var baseApi = new BaseApi(_httpClient);
            var token = await baseApi.PostToApi("Login", loginDto);
            var loginResult = token as OkObjectResult;

            if (loginResult != null)
            {
                var objectResult = JsonConvert.DeserializeObject<Models.Login>(loginResult.Value.ToString());

                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);

                Claim claimUserName = new(ClaimTypes.Name, objectResult.UserName);
                Claim claimEmail = new(ClaimTypes.Email, objectResult.Email);
                Claim claimRole = new(ClaimTypes.Role, "Administrador");

                identity.AddClaim(claimUserName);
                identity.AddClaim(claimEmail);
                identity.AddClaim(claimRole);

                var principalClaim = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principalClaim, new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.Now.AddMinutes(60),
                });

                var homeViewModel = new HomeViewModel();
                homeViewModel.Token = objectResult.Token;

                return View("~/Views/Home/Index.cshtml", homeViewModel);
            }

            return View("~/Views/Shared/Error.cshtml");
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("index", "Login");
        }
    }
}
