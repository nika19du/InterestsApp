using Microsoft.AspNetCore.Mvc;

namespace InYourInterest.Areas.Administration.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
