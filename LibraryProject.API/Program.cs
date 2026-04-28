using AutoMapper;
using AutoMapper;
using LibraryProject.BLL.Profiles;
using LibraryProject.BLL.Services;
using LibraryProject.DAL;
using LibraryProject.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

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
builder.Services.AddScoped<IAuthorService, AuthorService>();

// --- ДОДАТИ ЦЕЙ БЛОК ДЛЯ РЕЄСТРАЦІЇ ТА ТОКЕНІВ ---
builder.Services.AddIdentity<Microsoft.AspNetCore.Identity.IdentityUser, Microsoft.AspNetCore.Identity.IdentityRole>()
    .AddEntityFrameworkStores<LibraryProject.DAL.AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "LibraryAPI",
        ValidAudience = "LibraryClients",
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes("SuperSecretKeyThatIsAtLeast32BytesLong!!!")) // Секретний ключ для шифрування
    };
});
builder.Services.AddAuthorization();
// --------------------------------------------------

var app = builder.Build();

// Scalar UI вместо Swagger UI
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();                  // генерирует /openapi/v1.json
    app.MapScalarApiReference();       // UI на /scalar/v1
}

app.UseHttpsRedirection();
app.UseAuthentication(); 
app.UseAuthorization();  
app.MapControllers();

app.Run();