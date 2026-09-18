using BolosDoJacquim1.BdContextEvent;
// Imports do projeto BolosDoJacquin
using BolosDoJacquin.Interfaces;
using BolosDoJacquin.Models;
using BolosDoJacquin.Repositories;
using BolosDoJacquin.Service;
using BolosDoJacquin.Services;
using BolosDoJacquin.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

using System;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "Insira o token JWT desta maneira: Bearer {seu token}"


    });

    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = []
    });
});


// 1. Configuração do Swagger com suporte a autenticação JWT


// 2. Configuração do EF Core com a classe Context
builder.Services.AddDbContext<EventContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 3. Ignorar ciclos de serialização JSON nos relacionamentos
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// 4. Injeção de Dependência dos Repositórios
builder.Services.AddScoped<ITipoUsuario, TipoUsuarioRepository>();
builder.Services.AddScoped<ICategoria, CategoriaRepository>();
builder.Services.AddScoped<IUsuario, UsuarioRepository>();
builder.Services.AddScoped<IProduto, ProdutoRepository>();
builder.Services.AddScoped<IAvaliacao, AvaliacaoRepository>();

// 5. Injeção de Dependência dos Serviços Externos
builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();

builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("Cloudinary"));
builder.Services.Configure<SightengineSettings>(builder.Configuration.GetSection("Sightengine"));



// 6. Autenticação JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = "BolosDoJacquin1.WebAPI",

        ValidateAudience = true,
        ValidAudience = "BolosDoJacquin1.WebAPI",

        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(5),

        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? "bolosdojacquin-chave-autenticacao-jwt-segura-2026")
        )
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Pipeline HTTP
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