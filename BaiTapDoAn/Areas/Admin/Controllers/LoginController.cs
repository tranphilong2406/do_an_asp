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
            return RedirectToAction("Index","Home");
        }
    }
}
