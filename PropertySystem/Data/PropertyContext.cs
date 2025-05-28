using Microsoft.EntityFrameworkCore;
using PropertySystem.Models;

namespace PropertySystem.Data
{
    public class PropertyContext : DbContext
    {
        public PropertyContext(DbContextOptions<PropertyContext> options) : base(options)
        {
        }

        public DbSet<Property> Properties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Property>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.SalePrice).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Area).HasColumnType("float");
            });

            // Add sample data
            modelBuilder.Entity<Property>().HasData(
                new Property
                {
                    Id = 1,
                    Title = "Luxury Villa",
                    Description = "Luxury villa located in city center with beautiful environment and convenient transportation",
                    Region = "Beijing",
                    District = "Chaoyang",
                    PropertyType = "Villa",
                    SalePrice = 8000000,
                    ImageUrl = "/images/villa1.jpg",
                    Bedrooms = 4,
                    Bathrooms = 3,
                    Area = 300.5,
                    Address = "88 Jianguo Road, Chaoyang District",
                    CreatedDate = DateTime.Now.AddDays(-30)
                },
                new Property
                {
                    Id = 2,
                    Title = "Modern Apartment",
                    Description = "Fully renovated modern apartment with complete facilities",
                    Region = "Beijing",
                    District = "Haidian",
                    PropertyType = "Apartment",
                    SalePrice = 3500000,
                    ImageUrl = "/images/apartment1.jpg",
                    Bedrooms = 2,
                    Bathrooms = 2,
                    Area = 120.0,
                    Address = "123 Zhongguancun Street, Haidian District",
                    CreatedDate = DateTime.Now.AddDays(-25)
                },
                new Property
                {
                    Id = 3,
                    Title = "Commercial Office Building",
                    Description = "Prime location commercial office building, ideal for investment",
                    Region = "Shanghai",
                    District = "Pudong",
                    PropertyType = "Commercial",
                    SalePrice = 12000000,
                    ImageUrl = "/images/office1.jpg",
                    Bedrooms = 0,
                    Bathrooms = 4,
                    Area = 500.0,
                    Address = "1 Lujiazui Financial Street, Pudong District",
                    CreatedDate = DateTime.Now.AddDays(-20)
                },
                new Property
                {
                    Id = 4,
                    Title = "Cozy Residence",
                    Description = "Warm and comfortable family residence, perfect for living",
                    Region = "Shanghai",
                    District = "Xuhui",
                    PropertyType = "House",
                    SalePrice = 4500000,
                    ImageUrl = "/images/house1.jpg",
                    Bedrooms = 3,
                    Bathrooms = 2,
                    Area = 150.0,
                    Address = "456 Huaihai Middle Road, Xuhui District",
                    CreatedDate = DateTime.Now.AddDays(-15)
                },
                new Property
                {
                    Id = 5,
                    Title = "Luxury Apartment",
                    Description = "High-end renovated apartment with panoramic views",
                    Region = "Guangzhou",
                    District = "Tianhe",
                    PropertyType = "Apartment",
                    SalePrice = 2800000,
                    ImageUrl = "/images/apartment2.jpg",
                    Bedrooms = 2,
                    Bathrooms = 1,
                    Area = 95.0,
                    Address = "Zhujiang New Town CBD Core Area, Tianhe District",
                    CreatedDate = DateTime.Now.AddDays(-10)
                },
                new Property
                {
                    Id = 6,
                    Title = "Garden Villa",
                    Description = "Detached villa with garden, peaceful environment",
                    Region = "Shenzhen",
                    District = "Nanshan",
                    PropertyType = "Villa",
                    SalePrice = 15000000,
                    ImageUrl = "/images/villa2.jpg",
                    Bedrooms = 5,
                    Bathrooms = 4,
                    Area = 400.0,
                    Address = "Hi-Tech Park South, Nanshan District",
                    CreatedDate = DateTime.Now.AddDays(-5)
                },
                new Property
                {
                    Id = 7,
                    Title = "Premium Apartment",
                    Description = "Fully furnished apartment, move-in ready",
                    Region = "Beijing",
                    District = "Chaoyang",
                    PropertyType = "Apartment",
                    SalePrice = 4200000,
                    ImageUrl = "/images/apartment3.jpg",
                    Bedrooms = 3,
                    Bathrooms = 2,
                    Area = 135.0,
                    Address = "Guomao CBD, Chaoyang District",
                    CreatedDate = DateTime.Now.AddDays(-3)
                },
                new Property
                {
                    Id = 8,
                    Title = "School District House",
                    Description = "Quality school district property with excellent educational resources",
                    Region = "Beijing",
                    District = "Haidian",
                    PropertyType = "House",
                    SalePrice = 6800000,
                    ImageUrl = "/images/house2.jpg",
                    Bedrooms = 3,
                    Bathrooms = 2,
                    Area = 110.0,
                    Address = "Wudaokou, Haidian District",
                    CreatedDate = DateTime.Now.AddDays(-2)
                },
                new Property
                {
                    Id = 9,
                    Title = "Riverside Luxury Home",
                    Description = "Huangpu River view luxury residence with stunning panoramic views",
                    Region = "Shanghai",
                    District = "Pudong",
                    PropertyType = "House",
                    SalePrice = 18000000,
                    ImageUrl = "/images/luxury1.jpg",
                    Bedrooms = 4,
                    Bathrooms = 3,
                    Area = 280.0,
                    Address = "Binjiang Avenue, Pudong District",
                    CreatedDate = DateTime.Now.AddDays(-1)
                },
                new Property
                {
                    Id = 10,
                    Title = "Duplex Apartment",
                    Description = "Stylish duplex apartment with exceptional design",
                    Region = "Shanghai",
                    District = "Xuhui",
                    PropertyType = "Apartment",
                    SalePrice = 5200000,
                    ImageUrl = "/images/duplex1.jpg",
                    Bedrooms = 2,
                    Bathrooms = 2,
                    Area = 160.0,
                    Address = "Hengshan Road, Xuhui District",
                    CreatedDate = DateTime.Now
                },
                new Property
                {
                    Id = 11,
                    Title = "Business Apartment",
                    Description = "Core business district apartment",
                    Region = "Guangzhou",
                    District = "Tianhe",
                    PropertyType = "Apartment",
                    SalePrice = 3200000,
                    ImageUrl = "/images/business1.jpg",
                    Bedrooms = 1,
                    Bathrooms = 1,
                    Area = 80.0,
                    Address = "Tiyu West Road, Tianhe District",
                    CreatedDate = DateTime.Now.AddDays(-7)
                },
                new Property
                {
                    Id = 12,
                    Title = "Oceanview Villa",
                    Description = "Beachfront detached villa with private beach",
                    Region = "Shenzhen",
                    District = "Nanshan",
                    PropertyType = "Villa",
                    SalePrice = 25000000,
                    ImageUrl = "/images/seavilla1.jpg",
                    Bedrooms = 6,
                    Bathrooms = 5,
                    Area = 500.0,
                    Address = "Sea World, Shekou, Nanshan District",
                    CreatedDate = DateTime.Now.AddDays(-12)
                },
                new Property
                {
                    Id = 13,
                    Title = "Modern Residence",
                    Description = "Modern design residence with smart home features",
                    Region = "Beijing",
                    District = "Chaoyang",
                    PropertyType = "House",
                    SalePrice = 7500000,
                    ImageUrl = "/images/modern1.jpg",
                    Bedrooms = 4,
                    Bathrooms = 3,
                    Area = 200.0,
                    Address = "Wangjing SOHO, Chaoyang District",
                    CreatedDate = DateTime.Now.AddDays(-8)
                }
            );
        }
    }
}