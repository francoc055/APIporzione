using System;
using System.Collections.Generic;
using backendAPIPorzione.Models;
using Microsoft.EntityFrameworkCore;

namespace backendAPIPorzione.Datos;

public partial class PorzioneapiContext : DbContext
{
    public PorzioneapiContext()
    {
    }

    public PorzioneapiContext(DbContextOptions<PorzioneapiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Detalle> Detalles { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Detalle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Detalle__3214EC077021286A");

            entity.ToTable("Detalle");

            entity.HasOne(d => d.IdMenuNavigation).WithMany(p => p.Detalles)
                .HasForeignKey(d => d.IdMenu)
                .HasConstraintName("FK__Detalle__IdMenu__3E52440B");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Detalles)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__Detalle__IdProdu__3F466844");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.ToTable("Menu");

            entity.Property(e => e.Categoria)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("money");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Menus)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_Menu_IdUsuario");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.ToTable("Producto");

            entity.Property(e => e.NombreProducto)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("Usuario");

            entity.Property(e => e.Clave)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.ClaveHash).HasMaxLength(64);
            entity.Property(e => e.ClaveSalt).HasMaxLength(64);
            entity.Property(e => e.Correo)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Rol)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
