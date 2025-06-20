using Microsoft.EntityFrameworkCore;
using MangaMechiApi.Core.Entities;

namespace MangaMechiApi.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    private readonly DatabaseSettings _settings;

    public ApplicationDbContext(DatabaseSettings settings)
    {
        _settings = settings;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_settings.ConnectionString);
        }
    }

    public DbSet<Manga> Mangas { get; set; }
    public DbSet<Prestamo> Prestamos { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Manga>(entity =>
        {
            entity.ToTable("mangas", "dbo");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .IsRequired();

            entity.Property(e => e.Title)
                .HasColumnName("title")
                .HasMaxLength(255);

            entity.Property(e => e.Authors)
                .HasColumnName("authors");

            entity.Property(e => e.Volumes)
                .HasColumnName("volumes");

            entity.Property(e => e.Chapters)
                .HasColumnName("chapters");

            entity.Property(e => e.Status)
                .HasColumnName("status")
                .HasMaxLength(50);

            entity.Property(e => e.Published)
                .HasColumnName("published")
                .HasMaxLength(100);

            entity.Property(e => e.Genres)
                .HasColumnName("genres");

            entity.Property(e => e.Synopsis)
                .HasColumnName("synopsis");

            entity.Property(e => e.ImageUrl)
                .HasColumnName("image_url")
                .HasMaxLength(500);
        });

        modelBuilder.Entity<Prestamo>(entity => 
        {
            entity.ToTable("prestamos", "dbo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MangaId).HasColumnName("manga_id");
            entity.Property(e => e.FechaPrestamo).HasColumnName("fecha_prestamo");
            entity.Property(e => e.FechaDevolucion).HasColumnName("fecha_devolucion");
            entity.Property(e => e.Prestatario).HasColumnName("prestatario").HasMaxLength(100);
            entity.Property(e => e.Estado).HasColumnName("estado").HasMaxLength(50);

            entity.HasOne(d => d.Manga)
                .WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.MangaId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
