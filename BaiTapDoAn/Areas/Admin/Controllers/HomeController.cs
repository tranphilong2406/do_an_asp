using BaiTapDoAn.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace BaiTapDoAn.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly db_bigexamContext _context;

        public HomeController(db_bigexamContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.NumPost = _context.Posts.Where(m=>m.Status == 1).Count();
            ViewBag.NumCat = _context.Categories.Where(m=>m.Status == 1).Count();
            ViewBag.NumUser = _context.Members.Where(m => m.Status == 1).Count();
            return View();
        }
    }
}
