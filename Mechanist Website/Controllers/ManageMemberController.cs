using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mechanist_Website.Data;
using Mechanist_Website.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Mechanist_Website.Controllers
{
    [Authorize(Policy = "IsAdmin")]
    public class ManageMemberController : Controller
    {
        private readonly DataContext _context;

        public ManageMemberController(DataContext context)
        {
            _context = context;
        }

        // GET: ManageMember
        public async Task<IActionResult> Index()
        {
            return View(await _context.Members.ToListAsync());
        }

        // GET: ManageMember/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membersInfo = await _context.Members
                .FirstOrDefaultAsync(m => m.MemberID == id);
            if (membersInfo == null)
            {
                return NotFound();
            }

            return View(membersInfo);
        }

        // GET: ManageMember/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ManageMember/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberID,DiscordID,IsAdmin,DiscordName,MCName,Name,DateAccepted,Twitch,Youtube,Website,Retired")] MembersInfo membersInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(membersInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(membersInfo);
        }

        // GET: ManageMember/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membersInfo = await _context.Members.FindAsync(id);
            if (membersInfo == null)
            {
                return NotFound();
            }
            return View(membersInfo);
        }

        // POST: ManageMember/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemberID,DiscordID,IsAdmin,DiscordName,MCName,Name,DateAccepted,Twitch,Youtube,Website,Retired")] MembersInfo membersInfo)
        {
            if (id != membersInfo.MemberID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(membersInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MembersInfoExists(membersInfo.MemberID))
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
            return View(membersInfo);
        }

        // GET: ManageMember/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membersInfo = await _context.Members
                .FirstOrDefaultAsync(m => m.MemberID == id);
            if (membersInfo == null)
            {
                return NotFound();
            }

            return View(membersInfo);
        }

        // POST: ManageMember/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var membersInfo = await _context.Members.FindAsync(id);
            _context.Members.Remove(membersInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MembersInfoExists(int id)
        {
            return _context.Members.Any(e => e.MemberID == id);
        }
    }
}
