using Microsoft.EntityFrameworkCore;
using PropertySystem.Data;

var builder = WebApplication.CreateBuilder(args);

// 添加服务
builder.Services.AddControllersWithViews();

// 配置Entity Framework (SQL Server版本)
builder.Services.AddDbContext<PropertyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 配置CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// 配置HTTP请求管道
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors();
app.UseAuthorization();

// 配置路由
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// 自动应用数据库迁移 (生产环境中建议手动执行)
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PropertyContext>();
    context.Database.Migrate();
}

app.Run("http://0.0.0.0:12000");