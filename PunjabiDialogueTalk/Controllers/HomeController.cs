﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations.Schema;


using PunjabiDialogueTalk.Models;


namespace PunjabiDialogueTalk.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        // GET: Home/Index
        public ActionResult Index()
        {
            return View();
        }

        // POST: Home/Index
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([Bind(Include = "Id,Content")] Dialogue dialogue)
        {
            if (ModelState.IsValid)
            {
                dialogue.CreatedAt = DateTime.Now;

                // http://stackoverflow.com/questions/19936433/asp-net-mvc-5-identity-application-user-as-foreign-key
                dialogue.User = db.Users.Find(User.Identity.GetUserId());

                db.Dialogues.Add(dialogue);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(dialogue);
        }
    }
}