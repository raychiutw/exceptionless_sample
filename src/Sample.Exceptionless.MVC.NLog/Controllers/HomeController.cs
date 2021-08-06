using System;
using System.Diagnostics;
using Exceptionless;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.Exceptionless.MVC.NLog.Models;

namespace Sample.Exceptionless.MVC.NLog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            this._logger.LogInformation("log info");
            this._logger.LogTrace("log trace");

            this._logger.LogWarning("log warning");

            this._logger.LogError("log error");

            try
            {
                var s = 0;
                var a = 3;
                var d = a / s;
            }
            catch (Exception ex)
            {
                this._logger.LogError($"{HttpContext.Connection.RemoteIpAddress} 錯誤");
                ex.ToExceptionless().Submit();
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}