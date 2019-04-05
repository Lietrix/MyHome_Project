using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MyHome.Models;
using Microsoft.AspNet.Identity;

namespace MyHome.Controllers
{
    public class DashboardController : Controller
    {
        private MyHomeModels db = new MyHomeModels();
        // GET: Dashboard
        public async Task<ActionResult> Index()
        {
            string CurrentUser = User.Identity.GetUserId();
            return View(await db.Rooms.Where(x => x.User == CurrentUser).ToListAsync());
        }
    }
}