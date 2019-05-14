using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using System.Collections.Generic;
using MyHome.Models;
using Microsoft.AspNet.Identity;

namespace MyHome.Controllers
{
    [Authorize]
    public class ItemsController : Controller
    {
        private MyHomeEntities db = new MyHomeEntities();

        // GET: Items
        public async Task<ActionResult> Index()
        {
            
            string currentUser = User.Identity.GetUserId();
            var temp = await db.Items.ToListAsync();
            var items = temp.Where(x => x.Room.User == currentUser);

            decimal? value = 0;
            foreach(var x in items)
            {
                value += x.Value;
            }

            var trueValue = value.ToString();
            // Returns the total value of all items belonging to the user
            ViewBag.TotalValue = "$" + trueValue.TrimEnd('0');

            return View(items);
        }

        // GET: Items/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string currentUser = User.Identity.GetUserId();
            Item item = await db.Items.FindAsync(id);
            IEnumerable<Item> items = db.Items.Where(x => x.Room.User == currentUser);

            if (!items.Contains(item))
            {
                return HttpNotFound();
            }

            if (item == null)
            {
                return HttpNotFound();
            }

            return View(item);
        }

        // GET: Items/Create         Unused at the moment, partial view is what is being used to act as the UI, see _Additem in Views/Shared
        //public ActionResult Create()
        //{
        //    ViewBag.RoomID = new SelectList(db.Rooms, "RoomID", "Name");
        //    return View();
        //}

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name,Value,ExpirationDate,RoomID,ItemID")] Item item)
        {
            if (ModelState.IsValid)
            {  
                db.Items.Add(item);
                await db.SaveChangesAsync();
                return RedirectToAction("Edit", "Rooms", new { id = item.RoomID });
            }

            ViewBag.RoomID = new SelectList(db.Rooms, "RoomID", "Name", item.RoomID);
            return RedirectToAction("Index", "Dashboard");
        }

        // GET: Items/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string currentUser = User.Identity.GetUserId();
            Item item = await db.Items.FindAsync(id);
            IEnumerable<Item> items = db.Items.Where(x => x.Room.User == currentUser);

            if (!items.Contains(item))
            {
                return HttpNotFound();
            }
            if (item == null)
            {
                return HttpNotFound();
            }

            ViewBag.RoomID = new SelectList(db.Rooms, "RoomID", "Name", item.RoomID);

            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Name,Value,ExpirationDate,RoomID,ItemID")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Edit", "Rooms", new { id = item.RoomID });
            }
            ViewBag.RoomID = new SelectList(db.Rooms, "RoomID", "Name", item.RoomID);
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string currentUser = User.Identity.GetUserId();
            Item item = await db.Items.FindAsync(id);
            IEnumerable<Item> items = db.Items.Where(x => x.Room.User == currentUser);

            if (!items.Contains(item))
            {
                return HttpNotFound();
            }
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Item item = await db.Items.FindAsync(id);
            int? temp = item.RoomID;
            db.Items.Remove(item);
            await db.SaveChangesAsync();
            return RedirectToAction("Edit", "Rooms", new { id = temp });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        
    }
}
