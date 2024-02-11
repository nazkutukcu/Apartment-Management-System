using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SiteYonetimSistemi.API.Models;
using SiteYonetimSistemi.API.Models.Users;
using SiteYonetimSistemi.API.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IdentityService>();
builder.Services.AddScoped<TokenService>();

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

// Enable user authentication and roles
builder.Services.AddIdentity<User, AppRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<AppRole>() 
.AddEntityFrameworkStores<AppDbContext>();


// Token validation setup
builder.Services.AddAuthentication(options =>
{
    //schema
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
{
    var signatureKey = builder.Configuration.GetSection("TokenOptions")["SignatureKey"]!;

    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateLifetime = true,

        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false,        
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signatureKey))
    };
});


// add behavior to the DI container
builder.Services.AddDIContainer();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program)); // IMapper

var app = builder.Build();

// Seed data initialization
//await SeedData.Initialize(app.Services);

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

// Seed data initialization
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
    var roles = new[] { "Admin", "User" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new AppRole(role));
    }
}
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    string email = "admin@admin.com";
    string password = "Test1234,";

    if(await userManager.FindByEmailAsync(email)== null)
    {
        var user = new User();
        user.UserName = email;
        user.Email = email;
        user.FirstName = "Admin";
        user.LastName = "Admin";
        user.IdentityNumber = "1234567890";
        user.PhoneNumber = "1234567890";

        await userManager.CreateAsync(user, password);

        await userManager.AddToRoleAsync(user, "Admin");
    }
}


app.Run();



