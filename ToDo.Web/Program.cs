using Microsoft.EntityFrameworkCore;
using ToDoAPI.Business.Abstract;
using ToDoAPI.Data;
using ToDoAPI.Data.IRepositories;
using ToDoAPI.Data.Repositories;
using TodoApp.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

// AutoMapper registration
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// DbContext kaydýný yapalým
builder.Services.AddDbContext<TodoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // SQL Server kullanýmý örneði

// Repositori ve servislerin DI'ye eklenmesi
builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<ITodoService, TodoService>();

builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseCors();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ToDo}/{action=ToDoList}/{id?}");

app.Run();
