using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using MVC03.PL.Models;
using MVC03.PL.Services;

namespace MVC03.PL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IScopedService scopedService01;
        private readonly IScopedService scopedService02;

        public ITransentService TransentService01 { get; }
        public ITransentService TransentService02 { get; }
        public ISingletonService SingletonService01 { get; }
        public ISingletonService SingletonService02 { get; }

        public HomeController(
            ILogger<HomeController> logger,
            IScopedService scopedService01,
            IScopedService scopedService02,
            ITransentService transentService01,
            ITransentService transentService02,
            ISingletonService singletonService01,
            ISingletonService singletonService02

            )
        {
            _logger = logger;
            this.scopedService01 = scopedService01;
            this.scopedService02 = scopedService02;
            this.TransentService01 = transentService01;
            this.TransentService02 = transentService02;
            this.SingletonService01 = singletonService01;
            this.SingletonService02 = singletonService02;
        }

        public string TestLifeTime()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"scopedService01 : {scopedService01.GetGuid()}\n");
            builder.Append($"scopedService02 : {scopedService02.GetGuid()}\n\n");
            builder.Append($"TransentService01 : {TransentService01.GetGuid()}\n");
            builder.Append($"TransentService02 : {TransentService02.GetGuid()}\n\n");
            builder.Append($"SingletonService01 : {SingletonService01.GetGuid()}\n");
            builder.Append($"SingletonService02 : {SingletonService02.GetGuid()}\n\n");

            return builder.ToString();
        }
        public IActionResult Index()
        {
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
