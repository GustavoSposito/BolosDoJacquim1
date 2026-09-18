using System;
using System.Collections.Generic;
using BolosDoJacquin.Models;
using Microsoft.EntityFrameworkCore;

namespace BolosDoJacquim1.BdContextEvent;

public partial class EventContext : DbContext
{
    public EventContext()
    {
    }

    public EventContext(DbContextOptions<EventContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Avaliacao> Avaliacao { get; set; }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Produto> Produto { get; set; }

    public virtual DbSet<TipoUsuario> TipoUsuario { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=D03S20-1252903\\SQLEXPRESS;Database=BolosDoJacquin;User Id=sa;Password=123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Avaliacao>(entity =>
        {
            entity.Property(e => e.IdAvaliacao).HasDefaultValueSql("(newid())");
            entity.Property(e => e.DataCriacao).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdProdutoNavigation).WithMany(p => p.Avaliacao)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Avaliacao_Produto");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Avaliacao)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Avaliacao_Usuario");
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.Property(e => e.IdCategoria).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<Produto>(entity =>
        {
            entity.Property(e => e.IdProduto).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Disponibilidade).HasDefaultValue(true);

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Produto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Produto_Categoria");
        });

        modelBuilder.Entity<TipoUsuario>(entity =>
        {
            entity.Property(e => e.IdTipoUsuario).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.Property(e => e.IdUsuario).HasDefaultValueSql("(newid())");
            entity.Property(e => e.DataCadastro).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdTipoUsuarioNavigation).WithMany(p => p.Usuario)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Usuario_TipoUsuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
