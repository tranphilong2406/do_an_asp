using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BaiTapDoAn.Areas.Admin.Models;
using BaiTapDoAn.Areas.Admin.Filter;

namespace BaiTapDoAn.Areas.Admin.Controllers
{
    [Area("Admin")]
    [TypeFilter(typeof(AuthenticationFilter))]
    public class PostsController : Controller
    {
        private readonly db_bigexamContext _context;

        public PostsController(db_bigexamContext context)
        {
            _context = context;
        }

        // GET: Admin/Posts
        public async Task<IActionResult> Index()
        {
            var db_bigexamContext = _context.Posts.Include(p => p.AuthorNavigation).Include(p => p.Cat);
            return View(await db_bigexamContext.ToListAsync());
        }

        // GET: Admin/Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.AuthorNavigation)
                .Include(p => p.Cat)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Admin/Posts/Create
        public IActionResult Create()
        {
            List<Status> ListStatus = new List<Status>();
            ListStatus.Add(new Status(0, "Không hoạt động"));
            ListStatus.Add(new Status(1, "Hoạt động"));
            ViewData["listStatus"] = new SelectList(ListStatus, "id", "name");
             
            ViewData["Author"] = new SelectList(_context.Members, "Username", "Username");
            ViewData["CatId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: Admin/Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ShortDecription,FullContent,Img,Status,CatId,Author")] Post post, IFormFile ful_img)
        {
            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", post.Id + Path.GetExtension(ful_img.FileName));
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await ful_img.CopyToAsync(stream);
                }
                post.Img = post.Id.ToString() + Path.GetExtension(ful_img.FileName);
                _context.Update(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Author"] = new SelectList(_context.Members, "Username", "Username", post.Author);
            ViewData["CatId"] = new SelectList(_context.Categories, "Name", "Id", post.CatId);
            return View(post);
        }

        // GET: Admin/Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            string? img = _context.Posts.Where(p => p.Id == post.Id).Select(x => new { x.Img }).Single().Img;

            ViewData["img"] = img;
            List<Status> ListStatus = new List<Status>();
            ListStatus.Add(new Status(0, "Không hoạt động"));
            ListStatus.Add(new Status(1, "Hoạt động"));
            ViewData["listStatus"] = new SelectList(ListStatus, "id", "name");

            ViewData["Author"] = new SelectList(_context.Members, "Username", "Username", post.Author);
            ViewData["CatId"] = new SelectList(_context.Categories, "Id", "Name", post.CatId);
            return View(post);
        }

        // POST: Admin/Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ShortDecription,FullContent,Img,Status,CatId,Author")] Post post,IFormFile? ful_img,string image_temp)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (ful_img != null)
                    {
                        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", post.Id + Path.GetExtension(ful_img.FileName));
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await ful_img.CopyToAsync(stream);
                        }
                        post.Img = post.Id.ToString() + Path.GetExtension(ful_img.FileName);
                    }
                    else
                    {
                        post.Img = image_temp;
                    }
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Author"] = new SelectList(_context.Members, "Username", "Username", post.Author);
            ViewData["CatId"] = new SelectList(_context.Categories, "Id", "Id", post.CatId);
            return View(post);
        }

        // GET: Admin/Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.AuthorNavigation)
                .Include(p => p.Cat)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Admin/Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Posts == null)
            {
                return Problem("Entity set 'db_bigexamContext.Posts'  is null.");
            }
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                post.Status = 0;
                _context.Posts.Update(post);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
          return _context.Posts.Any(e => e.Id == id);
        }
    }
}
