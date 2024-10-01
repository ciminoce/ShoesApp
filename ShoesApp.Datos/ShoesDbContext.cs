using Microsoft.EntityFrameworkCore;
using ShoesApp.Entidades.Entities;

namespace ShoesApp.Datos
{
    public class ShoesDbContext:DbContext
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Colour> Colours { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Shoe> Shoes { get; set; }
        public DbSet<ShoeColour> ShoeColours { get; set; }
        public ShoesDbContext(DbContextOptions<ShoesDbContext> options):
            base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Colour>(entity =>
            {
                entity.ToTable("Colors");
                entity.Property(e => e.ColourId).HasColumnName("ColorId");
                entity.Property(e => e.ColourName).HasColumnName("ColorName");
                  
                
            });
            modelBuilder.Entity<Size>(entity =>
            {
                entity.Property(s => s.SizeNumber).HasColumnType("decimal(3,1)");
            });
            modelBuilder.Entity<ShoeColour>(entity =>
            {
                entity.ToTable("ShoeColors");
                entity.Property(e => e.ShoeColourId).HasColumnName("ShoeColorId");
                entity.Property(e => e.ColourId).HasColumnName("ColorId");

            });
        }
    }
}
