# 房产搜索系统

这是一个基于.NET Core、jQuery和Bootstrap构建的房产搜索系统。

## 功能特性

- 🏠 房产信息展示
- 🔍 多条件搜索过滤
- 📱 响应式设计
- 📄 分页显示
- 💰 价格范围筛选

## 技术栈

- **后端**: .NET Core 8.0
- **前端**: jQuery + Bootstrap 5
- **数据库**: Entity Framework Core (支持SQL Server和内存数据库)
- **样式**: CSS3 + Bootstrap

## 搜索功能

系统提供以下搜索条件：

1. **地区选择**: 北京、上海、广州、深圳等
2. **区域选择**: 朝阳区、海淀区、浦东新区等
3. **房产类型**: 别墅、公寓、住宅、商业等
4. **价格范围**: 
   - 100万以下
   - 100万-300万
   - 300万-500万
   - 500万-1000万
   - 1000万以上

## 房产信息展示

每个房产卡片包含：
- 房产图片
- 标题和价格
- 地区和区域信息
- 房产类型和面积
- 卧室和卫生间数量
- 详细地址

## 运行方式

### 开发环境 (内存数据库)
```bash
cd PropertySystem
dotnet restore
dotnet run
```

### 生产环境 (SQL Server)
1. 修改 `appsettings.json` 中的连接字符串
2. 将 `Program.cs` 中的 `UseInMemoryDatabase` 改为 `UseSqlServer`
3. 运行数据库迁移：
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## 数据库配置

### SQL Server 配置示例

在 `appsettings.json` 中添加：
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=PropertySystemDB;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

在 `Program.cs` 中修改：
```csharp
builder.Services.AddDbContext<PropertyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
```

## 项目结构

```
PropertySystem/
├── Controllers/
│   └── HomeController.cs          # 主控制器
├── Models/
│   ├── Property.cs                # 房产模型
│   ├── PagedResult.cs             # 分页结果模型
│   └── PropertySearchFilter.cs    # 搜索过滤器模型
├── Data/
│   └── PropertyContext.cs         # 数据库上下文
├── Views/
│   └── Home/
│       └── Index.cshtml           # 主页视图
├── wwwroot/
│   ├── css/
│   │   └── site.css               # 样式文件
│   ├── js/
│   │   └── property-search.js     # JavaScript功能
│   └── images/                    # 图片资源
└── Program.cs                     # 应用程序入口
```

## API 接口

### 搜索房产
- **URL**: `/Home/SearchProperties`
- **方法**: POST
- **参数**: PropertySearchFilter对象
- **返回**: PagedResult<Property>

### 获取过滤选项
- **URL**: `/Home/GetFilterOptions`
- **方法**: GET
- **返回**: 地区、区域、房产类型列表

## 扩展功能

可以进一步扩展的功能：
- 用户登录和收藏
- 房产详情页面
- 地图集成
- 图片上传和管理
- 高级搜索功能
- 房产比较功能

## 部署说明

1. 发布应用程序：
```bash
dotnet publish -c Release -o ./publish
```

2. 配置IIS或其他Web服务器

3. 确保SQL Server连接正常

4. 运行数据库迁移

## 注意事项

- 当前使用内存数据库进行演示，重启后数据会丢失
- 生产环境请使用SQL Server或其他持久化数据库
- 图片文件需要实际上传到服务器
- 建议添加数据验证和错误处理