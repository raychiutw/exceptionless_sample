using System;
using System.Diagnostics;
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

            try
            {
                throw new Exception("發生了未知的異常");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{HttpContext.Connection.RemoteIpAddress}呼叫了productapi/product/exceptiontest介面返回了失敗");
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