using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SecurityMS.Presentation.Web.Models;
using System.Diagnostics;

namespace SecurityMS.Presentation.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Main");
            return Redirect("/identity/Account/Login");
        }

        public IActionResult Main()
        {
            return View();
        }

        public IActionResult HRIndex()
        {
            return View("EmployeesManagementIndexView");
        }

        public IActionResult ContractsManagementIndexView()
        {
            return View();
        }

        public IActionResult FinincialManagementIndexView()
        {
            return View();
        }

        public IActionResult OperationsManagementIndexView()
        {
            return View();
        }
        public IActionResult TreasuryIndex()
        {
            return View();
        }

        public IActionResult StoresManagementIndexView()
        {
            return View();
        }

        public IActionResult PurchasesIndexView()
        {
            return View();
        }
        [Authorize(Roles = "Administrator,Admin")]
        public IActionResult SystemSettingsIndexView()
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
