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
    }
}

  


