# 本地运行指南 (Local Setup Guide)

## 前置要求 (Prerequisites)

### 1. 安装 .NET 8.0 SDK
从官方网站下载并安装：https://dotnet.microsoft.com/download/dotnet/8.0

验证安装：
```bash
dotnet --version
```
应该显示 8.0.x 版本

### 2. 安装 Git
从官方网站下载：https://git-scm.com/downloads

## 项目下载和运行步骤

### 步骤 1: 克隆项目
```bash
git clone https://github.com/Erin-11/OpenHandsTest.git
cd OpenHandsTest/PropertySystem
```

### 步骤 2: 还原依赖包
```bash
dotnet restore
```

### 步骤 3: 运行项目
```bash
dotnet run
```

项目将在以下地址启动：
- HTTP: http://localhost:5000
- HTTPS: https://localhost:5001

### 步骤 4: 访问应用
在浏览器中打开：http://localhost:5000

## 项目功能

### ✅ 已实现功能
- **分页显示**: 每页显示6条房产信息
- **搜索过滤**: 按地区、区域、房产类型、价格范围筛选
- **响应式设计**: 支持移动端和桌面端
- **英文界面**: 完整的英文用户界面
- **数据展示**: 13条示例房产数据

### 🔍 搜索功能
- **地区**: Beijing, Shanghai, Guangzhou, Shenzhen
- **区域**: Chaoyang, Haidian, Pudong, Xuhui, Tianhe, Nanshan
- **房产类型**: Villa, Apartment, Commercial, House
- **价格范围**: Under $1M, $1M-$3M, $3M-$5M, $5M-$10M, Over $10M

### 📄 分页功能
- 每页显示6条记录
- 支持上一页/下一页导航
- 页码直接跳转
- 总共3页（13条记录）

## 技术栈

- **后端**: ASP.NET Core 8.0
- **前端**: jQuery + Bootstrap 5
- **数据库**: Entity Framework Core InMemory Database
- **样式**: CSS3 + Bootstrap

## 开发模式

当前项目使用内存数据库，包含以下优势：
- ✅ 无需安装数据库软件
- ✅ 快速启动和测试
- ✅ 包含13条示例数据
- ⚠️ 重启后数据会重置

## 项目结构

```
PropertySystem/
├── Controllers/
│   └── HomeController.cs          # 主控制器，处理搜索和分页
├── Models/
│   ├── Property.cs                # 房产数据模型
│   ├── PagedResult.cs             # 分页结果模型
│   └── PropertySearchFilter.cs    # 搜索过滤器
├── Data/
│   └── PropertyContext.cs         # 数据库上下文和示例数据
├── Views/Home/
│   └── Index.cshtml               # 主页面（英文界面）
├── wwwroot/
│   ├── css/site.css               # 自定义样式
│   ├── js/property-search.js      # 前端交互逻辑
│   └── images/                    # 房产图片
└── Program.cs                     # 应用程序入口点
```

## 常见问题

### Q: 端口被占用怎么办？
A: 修改 `Properties/launchSettings.json` 中的端口号，或者使用：
```bash
dotnet run --urls="http://localhost:8080"
```

### Q: 如何添加更多房产数据？
A: 编辑 `Data/PropertyContext.cs` 文件中的 `SeedData()` 方法

### Q: 如何切换到真实数据库？
A: 
1. 修改 `Program.cs` 中的数据库配置
2. 更新 `appsettings.json` 中的连接字符串
3. 运行数据库迁移命令

### Q: 如何修改每页显示的记录数？
A: 修改 `wwwroot/js/property-search.js` 中的 `pageSize` 变量

## 开发建议

1. **代码编辑器**: 推荐使用 Visual Studio Code 或 Visual Studio
2. **浏览器开发工具**: 使用 F12 查看网络请求和调试
3. **热重载**: 使用 `dotnet watch run` 实现代码修改后自动重启

## 部署到生产环境

### 发布应用
```bash
dotnet publish -c Release -o ./publish
```

### 配置生产数据库
1. 安装 SQL Server
2. 修改连接字符串
3. 运行数据库迁移
4. 部署到 IIS 或其他 Web 服务器

## 联系信息

如有问题，请查看项目的 GitHub Issues 或提交新的 Issue。