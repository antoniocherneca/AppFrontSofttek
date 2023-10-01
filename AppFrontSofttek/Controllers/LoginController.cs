using Data.Base;
using Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
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

        public async Task<IActionResult> Ingresar(LoginDto loginDto)
        {
            var baseApi = new BaseApi(_httpClient);
            var token = await baseApi.PostToApi("Login", loginDto);
            var loginResult = token as OkObjectResult;
            var objectResult = JsonConvert.DeserializeObject<Models.Login>(loginResult.Value.ToString());

            // return RedirectToAction("Index", "Home");
            return View("~/Views/Home/Index.cshtml", objectResult);
        }
    }
}
