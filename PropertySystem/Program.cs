using Microsoft.EntityFrameworkCore;
using PropertySystem.Data;

var builder = WebApplication.CreateBuilder(args);

// 添加服务
builder.Services.AddControllersWithViews();

// 配置Entity Framework (使用内存数据库进行演示)
builder.Services.AddDbContext<PropertyContext>(options =>
    options.UseInMemoryDatabase("PropertyDB"));

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

// 初始化数据库
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PropertyContext>();
    context.Database.EnsureCreated();
}

app.Run("http://0.0.0.0:12000");
