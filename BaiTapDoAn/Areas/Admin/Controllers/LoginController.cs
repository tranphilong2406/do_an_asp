using BaiTapDoAn.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BaiTapDoAn.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly db_bigexamContext _context;
        private readonly IHttpContextAccessor _httpContext;

        public LoginController(db_bigexamContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            if(_httpContext.HttpContext.Session.GetInt32("role") != null)
            {
                if(_httpContext.HttpContext.Session.GetInt32("role") == 0)
                {
                    return RedirectToAction("Index","Posts");
                }
                return RedirectToAction("Index","Home");
            }
            return View("Login");
        }
        [HttpPost]
        public async Task<IActionResult> Index(string username,string password)
        {
            var member = await _context.Members
                .FirstOrDefaultAsync(m => m.Username == username);
            if (member == null)
            {
                return NotFound();
            }

            if(member.Password != password) {
                return NotFound();
            }

            _httpContext.HttpContext.Session.SetInt32("role", member.Role);
            if(member.Role == 1) { 
                return RedirectToAction("Index","Home");
            }
            return RedirectToAction("Index", "Posts");
        }

        public IActionResult Logout()
        {
            _httpContext.HttpContext.Session.Remove("role");
            return RedirectToAction("Index");
        }
    }
}
