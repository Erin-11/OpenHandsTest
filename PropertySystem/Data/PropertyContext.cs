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

            // 添加示例数据
            modelBuilder.Entity<Property>().HasData(
                new Property
                {
                    Id = 1,
                    Title = "豪华别墅",
                    Description = "位于市中心的豪华别墅，环境优美，交通便利",
                    Region = "北京",
                    District = "朝阳区",
                    PropertyType = "别墅",
                    SalePrice = 8000000,
                    ImageUrl = "/images/villa1.jpg",
                    Bedrooms = 4,
                    Bathrooms = 3,
                    Area = 300.5,
                    Address = "朝阳区建国路88号",
                    CreatedDate = DateTime.Now.AddDays(-30)
                },
                new Property
                {
                    Id = 2,
                    Title = "现代公寓",
                    Description = "精装修现代公寓，设施齐全",
                    Region = "北京",
                    District = "海淀区",
                    PropertyType = "公寓",
                    SalePrice = 3500000,
                    ImageUrl = "/images/apartment1.jpg",
                    Bedrooms = 2,
                    Bathrooms = 2,
                    Area = 120.0,
                    Address = "海淀区中关村大街123号",
                    CreatedDate = DateTime.Now.AddDays(-25)
                },
                new Property
                {
                    Id = 3,
                    Title = "商业办公楼",
                    Description = "黄金地段商业办公楼，投资首选",
                    Region = "上海",
                    District = "浦东新区",
                    PropertyType = "商业",
                    SalePrice = 12000000,
                    ImageUrl = "/images/office1.jpg",
                    Bedrooms = 0,
                    Bathrooms = 4,
                    Area = 500.0,
                    Address = "浦东新区陆家嘴金融街1号",
                    CreatedDate = DateTime.Now.AddDays(-20)
                },
                new Property
                {
                    Id = 4,
                    Title = "温馨住宅",
                    Description = "温馨舒适的家庭住宅，适合居住",
                    Region = "上海",
                    District = "徐汇区",
                    PropertyType = "住宅",
                    SalePrice = 4500000,
                    ImageUrl = "/images/house1.jpg",
                    Bedrooms = 3,
                    Bathrooms = 2,
                    Area = 150.0,
                    Address = "徐汇区淮海中路456号",
                    CreatedDate = DateTime.Now.AddDays(-15)
                },
                new Property
                {
                    Id = 5,
                    Title = "高档公寓",
                    Description = "高档装修公寓，视野开阔",
                    Region = "广州",
                    District = "天河区",
                    PropertyType = "公寓",
                    SalePrice = 2800000,
                    ImageUrl = "/images/apartment2.jpg",
                    Bedrooms = 2,
                    Bathrooms = 1,
                    Area = 95.0,
                    Address = "天河区珠江新城CBD核心区",
                    CreatedDate = DateTime.Now.AddDays(-10)
                },
                new Property
                {
                    Id = 6,
                    Title = "花园别墅",
                    Description = "带花园的独栋别墅，环境清幽",
                    Region = "深圳",
                    District = "南山区",
                    PropertyType = "别墅",
                    SalePrice = 15000000,
                    ImageUrl = "/images/villa2.jpg",
                    Bedrooms = 5,
                    Bathrooms = 4,
                    Area = 400.0,
                    Address = "南山区科技园南区",
                    CreatedDate = DateTime.Now.AddDays(-5)
                }
            );
        }
    }
}