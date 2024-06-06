﻿using CodeLuckManagement.DbFirst.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//config context trong program
//var connectionString = builder.Configuration.GetConnectionString("CodLuck");

//// Đăng ký ApplicationDbContext với chuỗi kết nối
//builder.Services.AddDbContext<CodLuckContext>(options =>
//    options.UseSqlServer(connectionString));

//neu config trong file Context
builder.Services.AddDbContext<CodLuckContext>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
