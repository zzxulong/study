﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebTest.DAL
{
    public partial class NopMallContext : DbContext
    {
        public NopMallContext()
        {
        }

        public NopMallContext(DbContextOptions<NopMallContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> User { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=NopMall;uid=sa;pwd=123456;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user").HasKey(e => e.Id) ;


                entity.Property(e => e.Email).HasMaxLength(1000);

                entity.Property(e => e.LastIpAddress).HasMaxLength(50);

                entity.Property(e => e.SystemName).HasMaxLength(50);

                entity.Property(e => e.UserName).HasMaxLength(1000);
            });
        }
    }
}