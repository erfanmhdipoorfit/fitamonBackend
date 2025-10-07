using Fitamon.Endpoint.Api.Dtos;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.ComponentModel;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.Negotiate;

// 🔹 فقط رفرنس به لایه‌های اصلی (نه کلاس‌های داخلی!)
using Fitamon.Application;
using Fitamon.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var swaggerConfig = configuration.GetSection("Swagger");
var docs = swaggerConfig.GetSection("Docs").Get<List<SwaggerDocConfig>>();

// Authentication
builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
   .AddNegotiate();

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = options.DefaultPolicy;
});

builder.Services.AddControllers();

// 🔹 رجیستر سرویس‌ها از لایه‌های مختلف (تمیز و مرتب!)
builder.Services.AddApplicationServices(); // از لایه Application
var conn = builder.Configuration.GetConnectionString("FitamonDb"); 
Console.WriteLine($"🔍 Connection String: {(string.IsNullOrEmpty(conn) ? "NULL!" : "OK")}"); //تست کانکشن استرینگ
builder.Services.AddPersistenceServices(configuration.GetConnectionString("FitamonDb")); // از لایه Persistence
// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.CustomSchemaIds(x =>
        x.GetCustomAttributes(false).OfType<DisplayNameAttribute>().FirstOrDefault()?.DisplayName ?? x.Name);
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);

    if (File.Exists(xmlPath))
    {
        opt.IncludeXmlComments(xmlPath);
    }

    docs?.ForEach(doc =>
        opt.SwaggerDoc(doc.Name, new OpenApiInfo { Title = doc.Title, Version = doc.Version }));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        docs?.ForEach(doc =>
            c.SwaggerEndpoint(doc.Url, $"{doc.Title} - {doc.Version}"));
        c.DocExpansion(DocExpansion.None);
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
app.Run();