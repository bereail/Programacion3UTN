
using Application.Data.Implementations;
using Application.Data.Services;
using Domain.Interfaces;
using Infraestructure;
using Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.AddSecurityDefinition("BookApiBearerAuth", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Admin: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIzIiwiZ2l2ZW5fbmFtZSI6ImFkbWluIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiQWRtaW4iLCJuYmYiOjE2ODk5NDk1NTYsImV4cCI6MTY4OTk1MzE1NiwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzE2OSIsImF1ZCI6ImNvbnN1bHRhYWx1bW5vc2FwaSJ9.0X3JKM20FCImAFkR3IYNmiFLLr_wLyS4ROGmxwuZWok" +
        "    " +
        " Client: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxIiwiZ2l2ZW5fbmFtZSI6IkNsYXJhUmF6emV0dG8iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJDbGllbnQiLCJuYmYiOjE2ODk5NjU2NjEsImV4cCI6MTY4OTk2OTI2MSwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzE2OSIsImF1ZCI6ImNvbnN1bHRhYWx1bW5vc2FwaSJ9.9C8D-3bxA2bRef0QU3pVA_l81JLXZxJxj-Cdi2mhKIM"
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "BookApiBearerAuth"
                }
            }, new List<string>()
        }
    });
});

// Add DbContext
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlite(builder.Configuration["ConnectionStrings:BookDBConnectionString"], b => b.MigrationsAssembly("BookAPI")));

//Automapper
//Busca en los perfiles que vamos a crear
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



//Auth
builder.Services.AddAuthentication("Bearer")  //"Bearer" es el tipo de auntenticación que tenemos que elegir después en PostMan para pasarle el token
    .AddJwtBearer(options =>  //Acá definimos la configuración de la autenticación. le decimos qué cosas queremos comprobar. La fecha de expiración se valida por defecto.
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
        };
    }
);

//Services
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ClientServices>();



//Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>(); 
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IUserComparisonRepository, UserComparisonService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();