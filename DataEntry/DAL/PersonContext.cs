using DataEntry.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace DataEntry.DAL
{
    public class PersonContext : DbContext
    {
      
            public PersonContext() : base("DefaultConnection") { }

            public DbSet<PersonData> Persons { get; set; }


        
    }
}