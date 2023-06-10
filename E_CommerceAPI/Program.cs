using E_CommerceCore.IRepositories;
using E_CommerceInfrastructure.Base;
using E_CommerceInfrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SharedDTO;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
// using testuser is wrong but for quick testing
var TestUser = new List<User> {
            new User{UserName="admin",Password="admin"},
            new User{UserName="testUser",Password="testUser"}
        };
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


// Register the DbContext with the connection string
builder.Services.AddDbContext<E_CommerceDbContext>(options =>
    options.UseSqlServer(connectionString));
var assembly = System.AppDomain.CurrentDomain.Load("E-CommerceApplication");
builder.Services.AddMediatR(assembly);

builder.Services.AddScoped<ICategoriesRepository,CateorigesRepository>();
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();

builder.Services.AddAutoMapper(assembly); 

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization();
//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("RequireAdmin", policy =>
//    {
//        policy.RequireAuthenticatedUser();
//        policy.RequireRole("admin");
//    });

//    options.AddPolicy("RequireUserRole", policy =>
//    {
//        policy.RequireAuthenticatedUser();
//        policy.RequireRole("user");
//    });
//});

builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapPost("/Security/CreateToken",
[AllowAnonymous] (User user) =>
{
    if (TestUser.Any(x=>x.UserName==user.UserName && x.Password == user.Password))
    {
        string role = user.UserName == "admin" ? "admin" : "user";
        var issuer = builder.Configuration["Jwt:Issuer"];
        var audience = builder.Configuration["Jwt:Audience"];
        var key = Encoding.ASCII.GetBytes
        (builder.Configuration["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),               
                new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, role)
             }),
            Expires = DateTime.UtcNow.AddMinutes(30),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials
            (new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha512Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);
        var stringToken = tokenHandler.WriteToken(token);
        return Results.Ok(stringToken);
    }
    return Results.Unauthorized();
});
app.UseAuthentication();
app.UseAuthorization();




app.MapControllers();



app.Run();

