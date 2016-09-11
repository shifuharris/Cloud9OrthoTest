using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Cloud9OrthoTest.DAL;
using Cloud9OrthoTest.Models;

namespace Cloud9OrthoTest.Controllers
{
    public class PeopleController : Controller
    {
        private PeopleContext db = new PeopleContext();
        private List<Person> people;

        public ActionResult Index()
        {
            //return View(db.People.ToList());
            GetAddresses();
            return View(people);
        }
        public void GetAddresses()
        {
            people = db.People.OrderBy(p => p.LastName).ToList();
            people.ForEach(pA => pA.Addresses = (from a in db.Addresses
                                                 where a.PersonID == pA.ID
                                                 select a).ToList());
        }
        [HttpPost]
        public ActionResult AddAddress(string args)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            var newAddr = js.Deserialize<Addresses>(args);
            Guid pId = newAddr.PersonID;
            newAddr.ID = Guid.NewGuid();
            db.Addresses.Add(newAddr);
            db.SaveChanges();
            GetAddresses();
            return RedirectToAction("Edit", new { id = pId });
        }
        [HttpPost]
        public ActionResult UpdateAddress(string args)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            var newAddr = js.Deserialize<Addresses>(args);
            Guid pId = newAddr.PersonID;
            Addresses address = db.Addresses.Find(newAddr.ID);
            db.Addresses.Remove(address);
            db.SaveChanges();

            db.Addresses.Add(newAddr);
            db.SaveChanges();
            GetAddresses();
            return RedirectToAction("Edit", new { id = pId });
        }

        public ActionResult DeleteAddress(Guid id)
        {
            Addresses address = db.Addresses.Find(id);
            Guid pID = address.PersonID;
            db.Addresses.Remove(address);
            db.SaveChanges();
            GetAddresses();
            return RedirectToAction("Edit", new { id = pID });
        }

        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

         public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,Birthdate")] Person person)
        {
            if (ModelState.IsValid)
            {
                person.ID = Guid.NewGuid();
                db.People.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(person);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            GetAddresses();
            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,Birthdate")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(person);
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Person person = db.People.Find(id);
            db.People.Remove(person);
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
