using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mechanist_Website.Data;
using Microsoft.AspNetCore.Mvc;
using Mechanist_Website.Models;

namespace Mechanist_Website.Controllers
{
    public class AboutUsController : Controller
    {
        private readonly DataContext _context;

        public AboutUsController(DataContext context)
        {
            _context = context;
        }
        public ActionResult Index()
        {
            List<MembersInfo> membersInfo = _context.Members.ToList();
            return View(membersInfo);
        }
    }
}
