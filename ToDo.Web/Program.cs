using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToDoAPI.Business.Abstract;
using ToDoAPI.Business.Concrete;
using ToDoAPI.Data;
using ToDoAPI.Data.IRepositories;
using TodoApp.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

// AutoMapper registration
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddHttpClient<ITodoService, TodoService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:44305/api");
});
var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
