using System;
using System.Collections.Generic;
using TestAsp.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TestAsp.DAL
{
    public class EFDbContext : IdentityDbContext<ApplicationUser>
    {
        public EFDbContext() : base("EFDbContext") { }
        public static EFDbContext Create()
        {
            return new EFDbContext();
        }
        public DbSet<New> News { get; set; }
        public DbSet<NewsContent> NewsContents { get; set; }

        public DbSet<Material> Materials { get; set; }
        public DbSet<MaterialsContent> MaterialsContents { get; set; }

        public DbSet<Event> Events { get; set; }
        public DbSet<EventsContent> EventsContents { get; set; }

        public DbSet<Student> Students { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Schedule> Schedules { get; set; }

        public DbSet<Picture> Pictures { get; set; }
    }
}