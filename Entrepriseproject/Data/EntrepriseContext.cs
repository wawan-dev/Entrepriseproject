using System;
using System.Collections.Generic;
using Entrepriseproject.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Entrepriseproject.Data;

public partial class EntrepriseContext : DbContext
{
    public EntrepriseContext()
    {
    }

    public EntrepriseContext(DbContextOptions<EntrepriseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Commentaire> Commentaires { get; set; }

    public virtual DbSet<EfmigrationsHistory> EfmigrationsHistories { get; set; }

    public virtual DbSet<Entreprise> Entreprises { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=192.168.10.16;port=3306;user=girard_erwan;password=6rEtw3VB;database=girard_erwan_entreprise", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.4.1-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Commentaire>(entity =>
        {
            entity.HasKey(e => e.IdCommentaire).HasName("PRIMARY");

            entity.ToTable("Commentaire");

            entity.HasIndex(e => e.IdUser, "User_ibfk_1");

            entity.HasIndex(e => e.IdEntreprise, "id_entreprise");

            entity.Property(e => e.IdCommentaire).HasColumnName("id_commentaire");
            entity.Property(e => e.Commentaire1)
                .HasColumnType("text")
                .HasColumnName("commentaire");
            entity.Property(e => e.DateCreation)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("date_creation");
            entity.Property(e => e.IdEntreprise).HasColumnName("id_entreprise");

            entity.HasOne(d => d.IdEntrepriseNavigation).WithMany(p => p.Commentaires)
                .HasForeignKey(d => d.IdEntreprise)
                .HasConstraintName("Commentaire_ibfk_1");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Commentaires)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("User_ibfk_1");
        });

        modelBuilder.Entity<EfmigrationsHistory>(entity =>
        {
            entity.HasKey(e => e.MigrationId).HasName("PRIMARY");

            entity.ToTable("__EFMigrationsHistory");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<Entreprise>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Entreprise");

            entity.HasIndex(e => e.Siren, "siren").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activite)
                .HasMaxLength(255)
                .HasColumnName("activite");
            entity.Property(e => e.Adresse)
                .HasMaxLength(255)
                .HasColumnName("adresse");
            entity.Property(e => e.CategorieEntreprise).HasMaxLength(20);
            entity.Property(e => e.CodeApe)
                .HasMaxLength(10)
                .HasColumnName("code_ape");
            entity.Property(e => e.CodePostal)
                .HasMaxLength(10)
                .HasColumnName("code_postal");
            entity.Property(e => e.Coordonnees).HasMaxLength(50);
            entity.Property(e => e.DateCreation).HasColumnName("date_creation");
            entity.Property(e => e.Departement)
                .HasMaxLength(100)
                .HasColumnName("departement");
            entity.Property(e => e.Dirigeants)
                .HasMaxLength(255)
                .HasColumnName("dirigeants");
            entity.Property(e => e.Nom)
                .HasMaxLength(255)
                .HasColumnName("nom");
            entity.Property(e => e.Pays)
                .HasMaxLength(50)
                .HasDefaultValueSql("'France'")
                .HasColumnName("pays");
            entity.Property(e => e.Siren)
                .HasMaxLength(9)
                .HasColumnName("siren");
            entity.Property(e => e.Siret)
                .HasMaxLength(14)
                .HasColumnName("siret");
            entity.Property(e => e.Ville)
                .HasMaxLength(100)
                .HasColumnName("ville");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("User");

            entity.Property(e => e.Motdepasse)
                .HasMaxLength(200)
                .HasColumnName("motdepasse");
            entity.Property(e => e.Psedo)
                .HasMaxLength(100)
                .HasColumnName("psedo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
