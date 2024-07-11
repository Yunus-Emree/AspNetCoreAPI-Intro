using AspNetCoreAPI_Intro.Entities;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreAPI_Intro.Data
{
    public class CityDbContext : DbContext
    {
        public CityDbContext(DbContextOptions<CityDbContext> options) : base(options) { }

        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent API
            modelBuilder.Entity<Country>().HasMany<City>(c => c.Cities).WithOne(c => c.Country).HasForeignKey(c => c.CountryId);

            //Seed Data
            modelBuilder.Entity<City>().HasData(
                new City() { Id = 1, Name = "İstanbul", Region = "Marmara", Code = "34", CountryId = 1},
                new City() { Id = 2, Name = "İzmir", Region = "Ege", Code = "35", CountryId = 1 },
                new City() { Id = 3, Name = "Ankara", Region = "İç Anadolu", Code = "06", CountryId = 1 },
                new City() { Id = 4, Name = "Antalya", Region = "Akdeniz", Code = "07", CountryId = 1 },
                new City() { Id = 5, Name = "Adana", Region = "Akdeniz", Code = "01", CountryId = 1 },
                new City() { Id = 6, Name = "Edirne", Region = "Marmara", Code = "22", CountryId = 1 }
                );
            modelBuilder.Entity<Country>().HasData(
                new Country() { Id = 1, Name ="Türkiye",}
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
