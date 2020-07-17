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
    public class ManageDownloadController : Controller
    {
        private readonly DataContext _context;

        public ManageDownloadController(DataContext context)
        {
            _context = context;
        }

        // GET: ManageDownload
        public async Task<IActionResult> Index()
        {
            return View(await _context.Downloads.ToListAsync());
        }

        // GET: ManageDownload/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var downloadInfo = await _context.Downloads
                .FirstOrDefaultAsync(m => m.DownloadID == id);
            if (downloadInfo == null)
            {
                return NotFound();
            }

            return View(downloadInfo);
        }

        // GET: ManageDownload/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ManageDownload/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DownloadID,Name,FileName,Image,Credit,Description,DateUploaded,Depreciated")] DownloadInfo downloadInfo, [FromForm(Name = "File")] IFormFile File, [FromForm(Name = "Image")] IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                if (File != null && File.Length > 0)
                {
                    var fileName = Path.GetFileName(File.FileName);
                    downloadInfo.FileName = fileName;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\downloads", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await File.CopyToAsync(fileStream);
                    }
                }
                else
                {
                    ViewBag["FileError"] = true;
                    return View(downloadInfo);
                }
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
                        downloadInfo.Image = p1;

                    }
                }
                _context.Add(downloadInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(downloadInfo);
        }

        // GET: ManageDownload/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var downloadInfo = await _context.Downloads.FindAsync(id);
            if (downloadInfo == null)
            {
                return NotFound();
            }
            return View(downloadInfo);
        }

        // POST: ManageDownload/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DownloadID,Name,FileName,Image,Credit,Description,DateUploaded,Depreciated")] DownloadInfo downloadInfo, [FromForm(Name = "Image")] IFormFile Image)
        {
            if (id != downloadInfo.DownloadID)
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
                        {
                            byte[] p1 = null;
                            using (var fs1 = Image.OpenReadStream())
                            using (var ms1 = new MemoryStream())
                            {
                                fs1.CopyTo(ms1);
                                p1 = ms1.ToArray();
                            }
                            downloadInfo.Image = p1;

                        }
                    }
                    _context.Update(downloadInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DownloadInfoExists(downloadInfo.DownloadID))
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
            return View(downloadInfo);
        }

        // GET: ManageDownload/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var downloadInfo = await _context.Downloads
                .FirstOrDefaultAsync(m => m.DownloadID == id);
            if (downloadInfo == null)
            {
                return NotFound();
            }

            return View(downloadInfo);
        }

        // POST: ManageDownload/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var downloadInfo = await _context.Downloads.FindAsync(id);
            _context.Downloads.Remove(downloadInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DownloadInfoExists(int id)
        {
            return _context.Downloads.Any(e => e.DownloadID == id);
        }
    }
}
