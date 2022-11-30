using BaiTapDoAn.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace BaiTapDoAn.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly db_bigexamContext _context;
        private readonly HttpContextAccessor _httpContext;

        public LoginController(db_bigexamContext context, HttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string username,string password)
        {
            var member = _context.Members.FindAsync(username);
            if(member == null)
            {
                return NotFound();
            }

            if(member.Result.Password != password) {
                return NotFound();
            }
            return RedirectToAction("Index","Home");
        }
    }
}
