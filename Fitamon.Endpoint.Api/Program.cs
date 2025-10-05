using Fitamon.Endpoint.Api.Dtos;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.ComponentModel;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.Negotiate; // ⚠️ الزامی برای NegotiateDefaults


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

    //// 🔹 همچنان نگه داشته شد (بدون حذف)
    //opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    //{
    //    In = ParameterLocation.Header,
    //    Description = "Please enter token",
    //    Name = "Authorization",
    //    Type = SecuritySchemeType.Http,
    //    BearerFormat = "JWT",
    //    Scheme = "bearer"
    //});
    //opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    //{
    //    {
    //        new OpenApiSecurityScheme
    //        {
    //            Reference = new OpenApiReference
    //            {
    //                Type = ReferenceType.SecurityScheme,
    //                Id = "Bearer"
    //            }
    //        },
    //        new string[] { }
    //    }
    //});
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