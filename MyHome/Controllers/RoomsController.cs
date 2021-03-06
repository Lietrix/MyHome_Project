﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyHome.Models;
using Microsoft.AspNet.Identity;
using MyHome.ViewModels;

namespace MyHome.Controllers
{
    [Authorize]
    public class RoomsController : Controller
    {
        private MyHomeEntities db = new MyHomeEntities();

        private string LoggedInUser() => User.Identity.GetUserId();
        
        // GET: Rooms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Value,Name,RoomID,User")] Room room)
        {
            if (ModelState.IsValid)
            {
                room.User = LoggedInUser();
                db.Rooms.Add(room);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Dashboard");
            }
            
            return View(room);
        }

        [HttpGet]
        public PartialViewResult ItemList()
        {
            return PartialView();
        }


        // GET: Rooms/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string currentUser = LoggedInUser();
            Room room = await db.Rooms.FindAsync(id);
            IEnumerable<Room> rooms = db.Rooms.Where(x => x.User == currentUser);
        
            if (!rooms.Contains(room))
            {
                return HttpNotFound();
            }

            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Value,Name,RoomID,User")] Room room)
        {
            if (ModelState.IsValid)
            {
                db.Entry(room).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Dashboard");
            }
            return View(room);
        }

        // GET: Rooms/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string currentUser = LoggedInUser();
            Room room = await db.Rooms.FindAsync(id);
            IEnumerable<Room> rooms = db.Rooms.Where(x => x.User == currentUser);

            if (!rooms.Contains(room))
            {
                return HttpNotFound();
            }

            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Room room = await db.Rooms.FindAsync(id);
            db.Rooms.Remove(room);
            await db.SaveChangesAsync();
            return RedirectToAction("Index","Dashboard");
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
