using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mechanist_Website.Data;
using Mechanist_Website.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Mechanist_Website.Controllers
{
    public class ManageProjectController : Controller
    {
        private readonly DataContext _context;

        public ManageProjectController(DataContext context)
        {
            _context = context;
        }

        // GET: ManageProject
        public async Task<IActionResult> Index()
        {
            return View(await _context.Projects.ToListAsync());
        }

        // GET: ManageProject/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectInfo = await _context.Projects
                .FirstOrDefaultAsync(m => m.ProjectID == id);
            if (projectInfo == null)
            {
                return NotFound();
            }

            return View(projectInfo);
        }

        // GET: ManageProject/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ManageProject/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectID,Name,Description,DateUpdated,Depreciated")] ProjectInfo projectInfo, [FromForm(Name = "Image")] IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)

                {
                    if (Image.Length > 0)

                    //Convert Image to byte and save to database

                    {

                        byte[] p1 = null;
                        using (var fs1 = Image.OpenReadStream())
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                        projectInfo.Image = p1;

                    }
                }
                _context.Add(projectInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(projectInfo);
        }

        // GET: ManageProject/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectInfo = await _context.Projects.FindAsync(id);
            if (projectInfo == null)
            {
                return NotFound();
            }
            return View(projectInfo);
        }

        // POST: ManageProject/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectID,Name,Description,DateUpdated,Depreciated")] ProjectInfo projectInfo, [FromForm(Name = "Image")] IFormFile Image)
        {
            if (id != projectInfo.ProjectID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Image != null)

                    {
                        if (Image.Length > 0)

                        //Convert Image to byte and save to database

                        {

                            byte[] p1 = null;
                            using (var fs1 = Image.OpenReadStream())
                            using (var ms1 = new MemoryStream())
                            {
                                fs1.CopyTo(ms1);
                                p1 = ms1.ToArray();
                            }
                            projectInfo.Image = p1;

                        }
                    }
                    _context.Update(projectInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectInfoExists(projectInfo.ProjectID))
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
            return View(projectInfo);
        }

        // GET: ManageProject/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectInfo = await _context.Projects
                .FirstOrDefaultAsync(m => m.ProjectID == id);
            if (projectInfo == null)
            {
                return NotFound();
            }

            return View(projectInfo);
        }

        // POST: ManageProject/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projectInfo = await _context.Projects.FindAsync(id);
            _context.Projects.Remove(projectInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectInfoExists(int id)
        {
            return _context.Projects.Any(e => e.ProjectID == id);
        }
    }
}
