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
    [Authorize]
    public class DashboardController : Controller
    {
        private MyHomeEntities db = new MyHomeEntities();
        // GET: Dashboard
        public async Task<ActionResult> Index()
        {
            string CurrentUser = User.Identity.GetUserId();
            return View(await db.Rooms.Where(x => x.User == CurrentUser).ToListAsync());
        }

        //public async PartialViewResult _Sidebar(string id)
        //{
        //  TODO: Make sidebar display all rooms, change dashboard afterwards.
        //}
    }
}