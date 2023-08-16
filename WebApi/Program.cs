using Autofac.Core;
using Business;
using Business.ServiceRegistrations;
using Core.Aspects;
using DataAccess.Context;
using Entities;
using Entities.JWTEntity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();



#region JWT CONFIGURATION & SWAGGER

builder.Services.AddSwaggerGen(setup =>
{
    setup.SwaggerDoc("v1", new OpenApiInfo { Title = "Web.Api", Version = "v1" });
    var jwtSecuritySheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Token'ý direkt olarak buraya yapýþtýrýnýz.",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    setup.AddSecurityDefinition(jwtSecuritySheme.Reference.Id, jwtSecuritySheme);
    setup.AddSecurityRequirement(new OpenApiSecurityRequirement { { jwtSecuritySheme, Array.Empty<string>() } });
});

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
//builder.Services.AddScoped(resolver => resolver.GetRequiredService<IOptions<JwtSettings>>().Value);
builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})

.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration.GetSection("JwtSettings")["Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration.GetSection("JwtSettings")["Audience"],
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero, // Token ömrü. Token süresi bittiði gibi unauthorize olsun dedim. Default hali 5 dakika.
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JwtSettings")["SigningKey"]))
    };
});

#endregion

builder.Services.AddServices(builder.Configuration);

#region Private Funcion

void AutoMigrateDatabase(IApplicationBuilder app)
{
    using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
    scope.ServiceProvider.GetRequiredService<AppDBContext>().Database.Migrate();
}
void InsertUser(IApplicationBuilder app)
{
    using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppDBContext>();
    if (context.Users.Any()) return;
    context.Users.Add(
        new User
        {
            Id = Guid.Parse("21161328-8d3a-4d81-811b-2f16aa5d14db"),
            Name = "Admin",
            Surname = "Developer",
            Username = "Admin",
            Password = "1",
            Email = "Admin@Admin.com",
            CreatedDate = DateTime.UtcNow,
            WithDeleted = false
        });
    context.SaveChanges();
}
void InsertRole(IApplicationBuilder app)
{
    using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppDBContext>();
    if (context.Roles.Any()) return;
    context.Roles.Add(
        new Role
        {
            Id = Guid.Parse("3689e930-8411-4e31-98d7-05d2f941d099"),
            Name = "Admin",
            Description = "Full Yetki",
            CreatedDate = DateTime.UtcNow,

            WithDeleted = false
        });
    context.SaveChanges();
}
void InsertUserRole(IApplicationBuilder app)
{
    using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppDBContext>();
    if (context.UserRoles.Any()) return;
    context.UserRoles.Add(
        new UserRole
        {
            RoleId = Guid.Parse("3689e930-8411-4e31-98d7-05d2f941d099"),
            UserId = Guid.Parse("21161328-8d3a-4d81-811b-2f16aa5d14db"),
            CreatedDate = DateTime.UtcNow,
            WithDeleted = false
        });
    context.SaveChanges();
}

void SeedData(IApplicationBuilder app)
{
    InsertUser(app);
    InsertRole(app);
    InsertUserRole(app);
}

#endregion

var app = builder.Build();

AppStatic.ServiceProviderInstance = app.Services.CreateScope().ServiceProvider;
AutoMigrateDatabase(app);
SeedData(app);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.ConfigureCustomExceptionMiddleware();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.MapControllers();

app.Run();
