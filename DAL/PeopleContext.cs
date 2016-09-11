using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Cloud9OrthoTest.Models;

namespace Cloud9OrthoTest.DAL
{
    public class PeopleContext: DbContext
    {
        public PeopleContext() : base("PeopleContext")
        {
            Database.SetInitializer<PeopleContext>(
            new DropCreateDatabaseAlways<PeopleContext>());
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Addresses> Addresses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}