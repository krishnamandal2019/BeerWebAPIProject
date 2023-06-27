using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BeerWebAPI.DataAccess.DbModel;

namespace BeerWebAPI.DataAccess.DatabaseContext
{
    /// <summary>
    /// This class contain the property and methods that interect with database.
    /// </summary>
    public class AppDBContext : DbContext
    {
        protected readonly IConfiguration _configuration;
        public AppDBContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        }
        public virtual DbSet<BreweryDBModel> DbBrewery { get; set; }
        public virtual DbSet<BeerDBModel> DbBeerModels { get; set; }
        public virtual DbSet<BarDBModel> DbBarModels { get; set; }
        public virtual DbSet<BreweryBeerDBModel> DbBreweryBeerModels { get; set; }
        public virtual DbSet<BarBeerDBModel> DbBarBeerModels { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BreweryDBModel>(entity =>
            {
                entity.ToTable("brewery");
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });
            modelBuilder.Entity<BeerDBModel>(entity =>
            {
                entity.ToTable("beer");
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
                entity.Property(e => e.PercentageAlcoholByVolume)
                  .HasColumnName("percentage_alcoholby_volume")
                  .IsRequired();
            });
            modelBuilder.Entity<BarDBModel>(entity =>
            {
                entity.ToTable("bar");
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
                entity.Property(e => e.Address)
                  .HasColumnName("address")
                  .IsRequired()
                  .HasMaxLength(500);
            });
            modelBuilder.Entity<BreweryBeerDBModel>(entity =>
            {
                entity.ToTable("brewery_beer");
                entity.Property(e => e.BreweryId)
                .HasColumnName("brewery_id")
                    .IsRequired()
                    .HasMaxLength(200);
                entity.Property(e => e.BeerId)
                  .HasColumnName("beer_id")
                  .IsRequired()
                  .HasMaxLength(500);
            });
            modelBuilder.Entity<BarBeerDBModel>(entity =>
            {
                entity.ToTable("bar_beers");
                entity.Property(e => e.BarId)
                .HasColumnName("bar_id")
                    .IsRequired()
                    .HasMaxLength(200);
                entity.Property(e => e.BeerId)
                  .HasColumnName("beer_id")
                  .IsRequired()
                  .HasMaxLength(500);
            });
        }
    }
}