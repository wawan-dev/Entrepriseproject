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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Commentaire>()
            .Property(c => c.EntrepriseId)
            .HasColumnName("id_entreprise");  // Correspondance explicite

        }




    }
}

  


