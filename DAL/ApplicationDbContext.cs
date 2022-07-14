using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProniaApp.Models;

namespace ProniaApp.DAL
{
	public class ApplicationDbContext:DbContext
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {

        }

        public DbSet<Slider> Sliders { get; set; }

        public DbSet<Plant> Plants { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<PlantCategory> PlantCategories { get; set; }

        public DbSet<Image> Images { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var item in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e=>e.GetProperties()
                .Where(p=>p.ClrType==typeof(decimal) || p.ClrType==typeof(decimal?))
                )
                )
            {
                item.SetColumnType("decimal(6,2)");
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}

