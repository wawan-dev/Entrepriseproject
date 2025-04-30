using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Entrepriseproject.Models;

namespace Applicationhackathon
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Entreprise> Entreprise
        {
            get; set;
        }

        public DbSet<Commentaire> Commentaire
        {
            get; set;
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Commentaire>()
            .Property(c => c.IdEntreprise)
            .HasColumnName("id_entreprise");  // Correspondance explicite

        }




    }
}

  


