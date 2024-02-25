using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // create new value for id
            modelBuilder.Entity<Owner>().Property(i => i.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<PortfolioItem>().Property(i => i.Id).HasDefaultValueSql("NEWID()");
            // initial insert in table has created
            //modelBuilder.Entity<Owner>().HasData(
            //    new Owner
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "Ashraf Hesham Mohamed",
            //        Avatar = "avatar.jpg",
            //        Profil = ".Net FullStack Developer",
            //    }
            //    );
        }

        public DbSet<Owner> Owners { get; set; }
        public DbSet<PortfolioItem> PortfolioItems { get; set; }


    }
}
