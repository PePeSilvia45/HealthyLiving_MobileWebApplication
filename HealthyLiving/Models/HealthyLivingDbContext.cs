using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HealthyLiving.Models
{

    public class HealthyLivingDbContext : IdentityDbContext<User>
    {

        public DbSet<Message> Messages { get; set; }

        public DbSet<FoodGroup> FoodGroups { get; set; }

        public DbSet<FoodItem> FoodItems { get; set; }

        public DbSet<Intake> Intakes { get; set; }

        public HealthyLivingDbContext()
            : base("HealthyLiving_2022", throwIfV1Schema: false)
        {
            Database.SetInitializer(new DatabaseInitializer());
        }

        public static HealthyLivingDbContext Create()
        {
            return new HealthyLivingDbContext();
        }
    }
}