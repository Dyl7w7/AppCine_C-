using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AppCine.Models
{
    public partial class AppCineDBContext : IdentityDbContext
    {
        public AppCineDBContext()
        {
        }

        public AppCineDBContext(DbContextOptions<AppCineDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Calificacion> Calificacions { get; set; } = null!;
        public virtual DbSet<Pelicula> Peliculas { get; set; } = null!;




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=localhost;Initial Catalog=AppCineDB;Integrated Security=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Calificacion>(entity =>
            {
                entity.HasKey(e => e.Idcalificacion)
                    .HasName("PK__Califica__CB9F892CD536979E");

                entity.ToTable("Calificacion");

                entity.Property(e => e.Idcalificacion)
                    .ValueGeneratedNever()
                    .HasColumnName("IDCalificacion");

                entity.Property(e => e.Calificacion1).HasColumnName("Calificacion1");

                entity.Property(e => e.Idpelicula).HasColumnName("IDPelicula");

                entity.HasOne(d => d.IdpeliculaNavigation)
                    .WithMany(p => p.Calificacions)
                    .HasForeignKey(d => d.Idpelicula)
                    .HasConstraintName("FK__Calificac__IDPel__267ABA7A");
            });

            modelBuilder.Entity<Pelicula>(entity =>
            {
                entity.HasKey(e => e.Idpelicula)
                    .HasName("PK__Pelicula__E060873A4DD67760");

                entity.ToTable("Pelicula");

                entity.Property(e => e.Idpelicula)
                    .ValueGeneratedNever()
                    .HasColumnName("IDPelicula");

                entity.Property(e => e.Director)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Genero)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Poster)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Sipnosis)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Titulo)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }

}

