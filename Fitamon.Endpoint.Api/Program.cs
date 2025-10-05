using Fitamon.Endpoint.Api.Dtos;
//using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.ComponentModel;
using System.Reflection;



var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var swaggerConfig = configuration.GetSection("Swagger");
var docs = swaggerConfig.GetSection("Docs").Get<List<SwaggerDocConfig>>();

builder.Services.AddControllers();

// ✅ فقط Swashbuckle (بدون Microsoft.AspNetCore.OpenApi)
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Title = "My API",
//        Version = "v1"
//    });

//    // فقط اگر نسخه 6.5.0+ نصب باشد، این خط کار می‌کند
//    //c.EnableAnnotations();
//});

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

    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        docs?.ForEach(doc =>
            c.SwaggerEndpoint(doc.Url, $"{doc.Title} - {doc.Version}"));
        c.DocExpansion(DocExpansion.None);
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();