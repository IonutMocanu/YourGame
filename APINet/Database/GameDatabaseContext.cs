using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using APINet.Database.Models;

namespace APINet.Database;

public partial class GameDatabaseContext : DbContext
{
     public GameDatabaseContext()
    {
    }

    public GameDatabaseContext(DbContextOptions<GameDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       // Configurare User
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Email).IsUnique();
        });

        // Configurare Car
        modelBuilder.Entity<Car>(entity =>
        {
            entity.ToTable("Car");
            entity.HasKey(e => e.Id);

            // RELAȚIA IMPORTANTA:
            // O mașină are un User -> Un User are multe mașini
            entity.HasOne(d => d.User)
                  .WithMany(p => p.Cars)
                  .HasForeignKey(d => d.UserId)
                  .OnDelete(DeleteBehavior.Cascade); // Dacă ștergi Userul, dispar și mașinile
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}