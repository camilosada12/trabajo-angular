using System.Text;
using Business.Interfaces;
using Business.Services;
using Data.Interfaces;
using Data.Services;
using Entity.Context;
using Entity.DTOs;
using Entity.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Web.Custom;

var builder = WebApplication.CreateBuilder(args);

// Configurar CORS
var origenPermitido = builder.Configuration.GetValue<string>("Cors:OrigenPermitido");

builder.Services.AddCors(options =>
{
    options.AddPolicy("PoliticaCors", policy =>
    {
        policy.WithOrigins(origenPermitido)  // Lee el valor desde appSettings.json
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(config =>
    {
        config.RequireHttpsMetadata = false;  // Cambia a true en producción
        config.SaveToken = true;
        config.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JWT:key"])),
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:Audience"],
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(5)  // Añade un pequeño margen para diferencias de reloj
        };
    });

builder.Services.AddAuthorization();

// REGISTRO DE DEPENDENCIAS GENERALES
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Servicios genéricos
builder.Services.AddScoped(typeof(IGenericService<FormDto>), typeof(GenericService<FormDto, Form>));
builder.Services.AddScoped(typeof(IGenericService<FormModuleDto>), typeof(GenericService<FormModuleDto, FormModule>));
builder.Services.AddScoped(typeof(IGenericService<ModuleDto>), typeof(GenericService<ModuleDto, Module>));
builder.Services.AddScoped(typeof(IGenericService<UserDto>), typeof(GenericService<UserDto, User>));
builder.Services.AddScoped(typeof(IGenericService<RolUserDto>), typeof(GenericService<RolUserDto, RolUser>));
builder.Services.AddScoped(typeof(IGenericService<rolDto>), typeof(GenericService<rolDto, rol>));
builder.Services.AddScoped(typeof(IGenericService<PersonDto>), typeof(GenericService<PersonDto, Person>));
builder.Services.AddScoped(typeof(IGenericService<PermissionDto>), typeof(GenericService<PermissionDto, Permission>));
builder.Services.AddScoped(typeof(IGenericService<RolFormPermissionDto>), typeof(GenericService<RolFormPermissionDto, RolFormPermission>));

// Automapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// 📌 SERVICIO EXTENDIDO (importante si quieres que el RolUserController pueda usarlo)
builder.Services.AddScoped<RolUserRepository>();
builder.Services.AddScoped<RolFormPermissionRepository>();
builder.Services.AddScoped<FormModuleRepository>();
builder.Services.AddScoped<UserRepository>();


// ⬅️ Agrega esta línea para registrar utilidades
builder.Services.AddScoped<utilidades>();

// Configuración de Base de Datos
string databaseProvider = builder.Configuration["DatabaseProvider"];
string connectionString = builder.Configuration.GetConnectionString(databaseProvider);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    switch (databaseProvider)
    {
        case "SqlServer":
            options.UseSqlServer(connectionString);
            break;
        case "PostgreSql":
            options.UseNpgsql(connectionString);
            break;
        case "MySql":
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            break;
        default:
            throw new InvalidOperationException("Proveedor de base de datos no soportado");
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Usar CORS
app.UseCors("PoliticaCors");

// Habilitar HTTPS Redirection si lo necesitas
app.UseHttpsRedirection();

// Autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

// Rutas
app.MapControllers();

app.Run();
