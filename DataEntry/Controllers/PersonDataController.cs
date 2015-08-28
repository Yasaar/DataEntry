using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataEntry.DAL;
using DataEntry.Models;

namespace DataEntry.Controllers
{
    [Authorize]
    public class PersonDataController : Controller
    {
        private PersonContext db = new PersonContext();

        // GET: PersonData
        public ActionResult Index(string sortOrder)
        {
            ViewBag.LastNameSortParm = String.IsNullOrEmpty(sortOrder) ? "LastName_desc" : "";
            ViewBag.FirstMidName = sortOrder == "FirstMidName" ? "FirstMidName_desc" : "FirstMidName";
            ViewBag.EmailSortParm = sortOrder == "Email" ? "Email_desc" : "Email";
            ViewBag.AgeSortParm = sortOrder == "Age" ? "Age_desc" : "Age";

             var Person = from s in db.Persons
                         select s;
            switch (sortOrder)
            {
                case "name_desc":
                    Person = Person.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    Person = Person.OrderBy(s => s.FirstMidName);
                    break;
                case "date_desc":
                    Person = Person.OrderByDescending(s => s.Email);
                    break;
                default:
                    Person = Person.OrderBy(s => s.LastName);
                    break;
            }
            return View(db.Persons.ToList());
        }

        // GET: PersonData/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonData personData = db.Persons.Find(id);
            if (personData == null)
            {
                return HttpNotFound();
            }
            return View(personData);
        }

        [AllowAnonymous]
        // GET: PersonData/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonData/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,LastName,FirstMidName,Email,Age")] PersonData personData)
        {
            if (ModelState.IsValid)
            {
                db.Persons.Add(personData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(personData);
        }

        // GET: PersonData/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonData personData = db.Persons.Find(id);
            if (personData == null)
            {
                return HttpNotFound();
            }
            return View(personData);
        }

        // POST: PersonData/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,LastName,FirstMidName,Email,Age")] PersonData personData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(personData);
        }

        // GET: PersonData/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonData personData = db.Persons.Find(id);
            if (personData == null)
            {
                return HttpNotFound();
            }
            return View(personData);
        }

        // POST: PersonData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PersonData personData = db.Persons.Find(id);
            db.Persons.Remove(personData);
            db.SaveChanges();
            return RedirectToAction("Index");
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
