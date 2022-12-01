using Microsoft.AspNetCore.Mvc;

namespace BaiTapDoAn.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DeniedController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
