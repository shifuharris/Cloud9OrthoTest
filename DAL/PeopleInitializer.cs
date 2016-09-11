using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Cloud9OrthoTest.Models;

namespace Cloud9OrthoTest.DAL
{
    public class PeopleInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<PeopleContext>
    {
        protected override void Seed(PeopleContext context)
        {
            //base.InitializeDatabase(context);

            for (int i = 0; i <= 5; i++)
            {
                var p = new Person()
                {
                    ID = Guid.NewGuid(),
                    FirstName = "FirstName " + i.ToString(),
                    LastName = "LastName " + i.ToString(),
                    Birthdate = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString())
                };
                context.People.Add(p);

                var aH = new Addresses()
                {
                    ID = Guid.NewGuid(),
                    PersonID = p.ID,
                    Address = i.ToString() + " Home Address",
                    City = "Atlanta",
                    State = "GA",
                    Zip = 30338
                };
                context.Addresses.Add(aH);

                var aW = new Addresses()
                {
                    ID = Guid.NewGuid(),
                    PersonID = p.ID,
                    Address = i.ToString() + " Work Address",
                    City = "Atlanta",
                    State = "GA",
                    Zip = 30338
                };
                context.Addresses.Add(aW);

                context.SaveChanges();

            }
        }
    }

}