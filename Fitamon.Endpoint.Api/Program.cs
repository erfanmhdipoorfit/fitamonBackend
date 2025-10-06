using Fitamon.Endpoint.Api.Dtos;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.ComponentModel;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.Negotiate;
using MediatR;

// 🔹 اضافه شده: برای EF Core و سرویس‌ها
using Microsoft.EntityFrameworkCore;
using Fitamon.Persistence.EntityFramework.Bot; // مسیر BotDbContext
using Fitamon.Domain.Bot.Contracts;
using Fitamon.Persistence.EntityFramework.Bot.Services; // مسیر BotServices

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var swaggerConfig = configuration.GetSection("Swagger");
var docs = swaggerConfig.GetSection("Docs").Get<List<SwaggerDocConfig>>();

// Add services to the container.
builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
   .AddNegotiate();

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = options.DefaultPolicy;
});

builder.Services.AddControllers();

// 🔹 1. رجیستر DbContext
builder.Services.AddDbContext<BotDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

// 🔹 2. رجیستر سرویس‌های دامنه
builder.Services.AddScoped<IBotServices, BotServices>();

// 🔹 3. رجیستر MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Fitamon.Application.Bot.Query.AllBotQueryFilter).Assembly));

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