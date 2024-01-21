using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FarmCentral__task_2_.Models;

public partial class FramerCentralDbContext : DbContext
{
    public FramerCentralDbContext()
    {
    }

    public FramerCentralDbContext(DbContextOptions<FramerCentralDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Farmer> Farmers { get; set; }

    public virtual DbSet<Produce> Produces { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=FramerCentralDB;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmpId).HasName("PK__tmp_ms_x__AFB3EC0DA1FE9693");

            entity.ToTable("employees");

            entity.Property(e => e.EmpId)
                .HasMaxLength(350)
                .IsUnicode(false)
                .HasColumnName("empId");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Surname)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("surname");
            entity.Property(e => e.Tel)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tel");
        });

        modelBuilder.Entity<Farmer>(entity =>
        {
            entity.HasKey(e => e.FarmerId).HasName("PK__tmp_ms_x__EC6F8828764B12E0");

            entity.ToTable("farmer");

            entity.Property(e => e.FarmerId)
                .HasMaxLength(350)
                .IsUnicode(false)
                .HasColumnName("farmerId");
            entity.Property(e => e.Active)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("active");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.EmpId)
                .HasMaxLength(350)
                .IsUnicode(false)
                .HasColumnName("empId");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Surname)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("surname");
            entity.Property(e => e.Tel)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tel");

            entity.HasOne(d => d.Emp).WithMany(p => p.Farmers)
                .HasForeignKey(d => d.EmpId)
                .HasConstraintName("FK__farmer__empId__71D1E811");
        });

        modelBuilder.Entity<Produce>(entity =>
        {
            entity.HasKey(e => e.ProduceId).HasName("PK__tmp_ms_x__391407765869692B");

            entity.ToTable("produce");

            entity.Property(e => e.ProduceId).HasColumnName("produceId");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("amount");
            entity.Property(e => e.FarmrId)
                .HasMaxLength(350)
                .IsUnicode(false)
                .HasColumnName("farmrId");
            entity.Property(e => e.Grade)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("grade");
            entity.Property(e => e.IsRotten)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("isRotten");
            entity.Property(e => e.PackedDate)
                .HasColumnType("date")
                .HasColumnName("packedDate");
            entity.Property(e => e.ProduceName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("produceName");
            entity.Property(e => e.Producetype)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("producetype");
            entity.Property(e => e.ShelfLife).HasColumnName("shelfLife");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.Unit)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("unit");

            entity.HasOne(d => d.Farmr).WithMany(p => p.Produces)
                .HasForeignKey(d => d.FarmrId)
                .HasConstraintName("FK__produce__farmrId__72C60C4A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
