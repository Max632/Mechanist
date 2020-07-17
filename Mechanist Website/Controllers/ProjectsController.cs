using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mechanist_Website.Models;
using Mechanist_Website.Data;

namespace Mechanist_Website.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly DataContext _context;

        public ProjectsController(DataContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            List<ProjectInfo> projectInfo = _context.Projects.ToList();
            projectInfo.ForEach(p => p.Image = null);
            return View(projectInfo);
        }

        public ActionResult Project(ProjectInfo project)
        {
            return View(project);
        }

        public FileContentResult getImg(int id)
        {
            try
            {
                byte[] byteArray = _context.Projects.First(d => d.ProjectID == id).Image;
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
