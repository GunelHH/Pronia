﻿using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProniaApp.Models;

namespace ProniaApp.DAL
{
	public class ApplicationDbContext:IdentityDbContext<AppUser>
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {

        }

        public DbSet<Slider> Sliders { get; set; }

        public DbSet<Plant> Plants { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<PlantCategory> PlantCategories { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<PlantInformation> PlantInformations { get; set; }

        public DbSet<Setting> Settings { get; set; }

        public DbSet<Color> Colors { get; set; }

        public DbSet<Size> Sizes { get; set; }



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

            modelBuilder.Entity<Size>()
                .HasIndex(i => i.Name)
                .IsUnique();

            modelBuilder.Entity<Color>()
                .HasIndex(i => i.ColorName)
                .IsUnique();

            modelBuilder.Entity<Setting>()
                .HasIndex(i => i.Key)
                .IsUnique();
            base.OnModelCreating(modelBuilder);
        }
    }
}

