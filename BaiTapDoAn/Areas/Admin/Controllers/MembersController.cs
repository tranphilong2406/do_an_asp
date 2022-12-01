using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BaiTapDoAn.Areas.Admin.Models;
using NuGet.Protocol;

namespace BaiTapDoAn.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MembersController : Controller
    {
        private readonly db_bigexamContext _context;

        public MembersController(db_bigexamContext context)
        {
            _context = context;
        }

        // GET: Admin/Members
        public IActionResult Index()
        {
            List<Status> ListStatus = new List<Status>();
            ListStatus.Add(new Status(1, "Hoạt động"));
            ListStatus.Add(new Status(0, "Không hoạt động"));
            ViewData["listStatus"] = new SelectList(ListStatus, "id", "name");

            List<Roles> ListRoles = new List<Roles>();
            ListRoles.Add(new Roles(1, "Admin"));
            ListRoles.Add(new Roles(0, "Editor"));
            ViewData["listRoles"] = new SelectList(ListRoles, "id", "name");

            ViewBag.listMem = _context.Members.ToJson();


            return View();
        }

        // GET: Admin/Members/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Members == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .FirstOrDefaultAsync(m => m.Username == id);
            if (member == null)
            {
                return NotFound();
            }

            return Json(member.ToJson());
        }

        // GET: Admin/Members/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username,Password,Role,Status")] Member member)
        {
            if (ModelState.IsValid)
            {
                _context.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        // GET: Admin/Members/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Members == null)
            {
                return NotFound();
            }

            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            return Json(member.ToJson());
        }

        // POST: Admin/Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Username,Password,Role,Status")] Member member)
        {
            if (id != member.Username)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(member);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.Username))
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
            return View(member);
        }

        // GET: Admin/Members/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Members == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .FirstOrDefaultAsync(m => m.Username == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Admin/Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Members == null)
            {
                return Problem("Entity set 'db_bigexamContext.Members'  is null.");
            }
            var member = await _context.Members.FindAsync(id);
            if (member != null)
            {
                member.Status = 0;
                _context.Members.Update(member);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(string id)
        {
          return _context.Members.Any(e => e.Username == id);
        }
    }
}
