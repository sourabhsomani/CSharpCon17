**Steps to build a Sample Project**
-------------------------------
*Step 1 : Install following Packages*
```C#
Microsoft.AspNetCore.StaticFiles
Microsoft.AspNetCore.Mvc
Microsoft.AspNetCore.Razor  
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
Microsoft.Extensions.Configuration.Json
```
*Step 2 : Configure MVC Services: For that just write following code in startup.cs*
```C#
services.AddMvc();
```
*Now add Following line of code for middleware*
```C#
loggerFactory.AddConsole();  

if (env.IsDevelopment())  
{  
  app.UseDeveloperExceptionPage();  
}  
app.UseStatusCodePages();//extaintion Method  
app.UseStaticFiles();//extaintion Method  
app.UseMvcWithDefaultRoute();//extaintion Method  
```
*Step 3 : Create 3 Folders Models, Views, Controllers*
*Step 4 : Create Item Class*
```C#
public class Item  
{  
  public int ItemId { get; set; }  
  public string ItemName { get; set; }  
  public string ShortDescription { get; set; }  
  public string LongDescription { get; set; }  
  public decimal Price { get; set; }  
  public string ImageUrl { get; set; }  
  public int CategoryId { get; set; }  
  public virtual Category Category{ get; set; }  
} 
```
*Step 5 : Create Category Class*
```C#
public class Category  
{  
  public int CategoryId { get; set; }  
  public string CategoryName { get; set; }  
  public string Description { get; set; }  
  public List<Item> Items{ get; set; }  
}  

```
*Step 6 : Creating Repository Classes*
```C#
interface IItemRepository  
{  
  IEnumerable<Item> Items { get; }  
  Item GetItemById(int itemId);  
}  
public interface ICategoryRepository  
{  
  IEnumerable<Category> Categories { get; }  
}  
```
*Step 7 : Create AppDbContext File*
```C#
public class AppDbContext : DbContext  
{  
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)  
  {  
  }  
  public DbSet<Category> Categories { get; set; }  
  public DbSet<Item> Items { get; set; }  	  
}
```
*Step 8 : Adding appSetting.JSON File and Change DB Connection*
*Step 9 : Create Constructor in Startup.cs*
```C#
public Startup(IHostingEnvironment hostingEnvironment)  
{  
  _configurationRoot = new ConfigurationBuilder()  
      .SetBasePath(hostingEnvironment.ContentRootPath)  
      .AddJsonFile("appsettings.json")  
      .Build();  
}  
```
*Step 10 : Add ItemRepository*
```C#
public class ItemRepository:IItemRepository  
{  
    private readonly AppDbContext _appDbContext;  

    public ItemRepository(AppDbContext appDbContext)  
    {  
        _appDbContext = appDbContext;  
    }  
    public IEnumerable<Item> Items  
    {  
        get  
        {  
            return _appDbContext.Items.Include(c => c.Category);  
        }  
    }  
      
    public Item GetItemById(int itemId)  
    {  
        return _appDbContext.Items.FirstOrDefault(p => p.ItemId == itemId);  
    }  
}  
```
*Step 11 : Add CategoryRepository*
```C#
public class CategoryRepository : ICategoryRepository  
{  
    private readonly AppDbContext _appDbContext;  

    public CategoryRepository(AppDbContext appDbContext)  
    {  
        _appDbContext = appDbContext;  
    }  
    public IEnumerable<Category> Categories => _appDbContext.Categories;  
}  
```
*Step 12 : Configure services in Startup.cs*
```C#
services.AddDbContext<AppDbContext>(options=>  
        options.UseSqlServer(_configurationRoot.GetConnectionString("DefaultConnection")));  
services.AddTransient<ICategoryRepository, CategoryRepository>();  
services.AddTransient<IItemRepository, ItemRepository>();  
```
*Step 13 : Update Database using following commands*
```C#
Add-Migration Initial
Update-database
```
*Step 14 : Insert data in database*
```sql
INSERT INTO dbo.Categories  
        ( CategoryName, Description )  
values('Soup','Soup'),  
('South Indian','South Indian'),  
('North Indian','North Indian'),  
('Chinese','Noodle'),  
('Snakes','Snakes')  
  
GO  
  
INSERT INTO dbo.Items  
        (ItemName,  
ShortDescription,  
LongDescription,  
ImageUrl,  
CategoryId,  
Price)  
VALUES  
('Tomato Soup','Tomato Soup','Tomato Soup','/Images/ts.jpg',1,60.00)  
,('Paper Dosa','Paper Dosa','Paper Dosa','/Images/pd.jpg',2,70.00)  
,('Matar Paneer','Matar Paneer','Matar Paneer','/Images/mp.jpg',3,150.00)  
,('Hakka Noodles','Hakka Noodles','Hakka Noodles','/Images/hn.jpg',4,200.00)  
,('Manchurian','Manchurian','Manchurian','/Images/m.jpg',4,60.00)  
,('Chilli Potato','Chilli Potato','Chilli Potato','/Images/cp.jpg',5,200.00)  
,('Chilli Paneer','Chilli Paneer','Chilli Paneer','/Images/pc.jpg',5,60.00)  
```
*Step 15 : Create Item Controller*
```C#
public class ItemController : Controller  
{  
    private readonly IItemRepository _itemRepository;  
    private readonly ICategoryRepository _categoryRepository;  
    public ItemController(ICategoryRepository categoryRepository, IItemRepository itemRepository)  
    {  
        _itemRepository = itemRepository;  
        _categoryRepository = categoryRepository;  
    }  
    public ActionResult List()  
    {  
        return View(_itemRepository.Items);  
    }  
}  
```
*Step 16 : Now add following dotnet lib in .csproj to generate scaffolding template*
```xml
<DotNetCliToolReference Include="Microsoft.VisualStudio.Web.CodeGeneration.Tools" Version="1.0.0-msbuild3-final" />
```
*Step 17 : Finally Generate View using Scaffolding*

**Thank You**
**, We have done**
**,Happy Coding :)**

*for more info just email : sourabh_somani2010@hotmail.com*
