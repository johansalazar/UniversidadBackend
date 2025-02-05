using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using UBack.Aplication.Interfaces;
using UBack.Aplication.Services;
using UBack.Aplication.UseCases;
using UBack.Infraestructure.Infraestructura;
using UBack.Infraestructure.Persistence.Contexts;
using UBack.Services.WebApi.Modules.Versioning;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Universidad API", Version = "v1" });

    // Configurar esquema de seguridad JWT
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese el token JWT en el formato: Bearer {token}"
    });

    // Requerir el esquema de seguridad en todas las operaciones
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] {}
        }
    });
});

builder.Services.AddVersioning();
builder.Services.AddApplicationServices();

// agregar los repositorios
builder.Services.AddScoped<IInscripcionRepository, InscripcionRepository>();
builder.Services.AddScoped<IMateriaRepository, MateriaRepository>();
builder.Services.AddScoped<IRolRepository, RolRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

// Agregar los casos de uso
builder.Services.AddScoped<InscripcionUseCases>();
builder.Services.AddScoped<MateriaUseCases>();
builder.Services.AddScoped<RolUseCases>();
builder.Services.AddScoped<UsuarioUseCases>();
builder.Services.AddScoped<AuthService>();

builder.Services.AddDbContext<UniversidadDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuración de JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])),      
        };
        // Logs adicionales para depuración
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine("Token successfully validated.");
                return Task.CompletedTask;
            }
        };
    });


// Agregar políticas de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowHost",
        builder => builder
            .WithOrigins("http://localhost:4200") // Dominio del frontend             
            .AllowAnyMethod()
            .AllowAnyHeader());
});



var app = builder.Build();

// Usar CORS
app.UseCors("AllowHost");
EnsureDatabaseCreatedAndMigrate(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        // build a swagger endpoint for each discovered API version

        foreach (var description in provider.ApiVersionDescriptions)
        {
            c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
        }
    });
}
else
{
    app.UseDeveloperExceptionPage();
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        // build a swagger endpoint for each discovered API version

        foreach (var description in provider.ApiVersionDescriptions)
        {
            c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthentication(); 

app.UseAuthorization();

app.MapControllers();

app.Run();

// Método para asegurar que la base de datos esté creada y migrada
static void EnsureDatabaseCreatedAndMigrate(IApplicationBuilder app)
{
    using var scope = app.ApplicationServices.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<UniversidadDbContext>();

    // Verificar si la base de datos existe
    if (!context.Database.CanConnect())
    {
        try
        {
            // Si no existe, aplicar migraciones
            context.Database.Migrate();
            Console.WriteLine("Base de datos migrada correctamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al migrar la base de datos: {ex.Message}");
        }
    }
    else
    {
        Console.WriteLine("La base de datos ya existe y está conectada.");
    }
}