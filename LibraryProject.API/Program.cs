using AutoMapper;
using LibraryProject.BLL.Profiles;
using LibraryProject.BLL.Services;
using LibraryProject.DAL;
using LibraryProject.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// OpenAPI (встроенный, без Swashbuckle)
builder.Services.AddOpenApi();

// База данных
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// AutoMapper
var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new MappingProfile());
});

builder.Services.AddSingleton<IMapper>(mapperConfig.CreateMapper());

// Репозитории и сервисы
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IBookService, BookService>();

var app = builder.Build();

// Scalar UI вместо Swagger UI
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();                  // генерирует /openapi/v1.json
    app.MapScalarApiReference();       // UI на /scalar/v1
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();