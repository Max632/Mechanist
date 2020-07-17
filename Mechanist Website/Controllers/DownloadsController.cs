using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mechanist_Website.Models;
using Mechanist_Website.Data;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Mechanist_Website.Controllers
{
    public class DownloadsController : Controller
    {
        private readonly DataContext _context;

        public DownloadsController(DataContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            List<DownloadInfo> downloadInfo = _context.Downloads.ToList();
            downloadInfo.ForEach(d => d.Image = null);
            return View(downloadInfo);
        }

        public ActionResult Download(DownloadInfo dl)
        {
            return View(dl);
        }

        public FileResult FileDownload(string fileName)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(@"wwwroot/Downloads/" + Path.GetFileName(fileName));
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public FileContentResult getImg(int id)
        {
            try
            {
                byte[] byteArray = _context.Downloads.First(d => d.DownloadID == id).Image;
                return byteArray != null
                    ? new FileContentResult(byteArray, "image/jpeg")
                    : null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}
