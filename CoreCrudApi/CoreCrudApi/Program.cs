using CoreCrudApi.Db.EntityFramework;
using CoreCrudApi.Helpers;
using CoreCrudApi.Services;
using CoreCrudApi.Db.Services;
using CoreCrudApi.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(); // commented this and added below line to support jwt token pass in Authorize
//added to support jwt token in swagger
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "JWTToken_Auth_API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITenantService, TenantService>();
builder.Services.AddScoped<ICurrentTenantService, CurrentTenantService>();

builder.Services.AddDbContext<TenantDbContext>( options =>
    options.UseSqlite(@"Data Source=..\CoreCrudApi.Db\Database\Employee.db")
    );

builder.Services.AddDbContext<EmployeeDBContext>(options =>
    options.UseSqlite(@"Data Source=..\CoreCrudApi.Db\Database\Employee.db")
    );

builder.Services.AddMemoryCache();
builder.Services.AddScoped<JwtHandler>();

// CORS setup
string MyAllowSpecificOrigins = "MyAllowSpecificOriginsKey";
builder.Services.AddCors(options =>
{    
        options
            .AddPolicy(name: MyAllowSpecificOrigins,
                        policy =>
                        {
                            policy.WithOrigins("http://localhost:4200")
                                    .AllowAnyHeader()
                                    .AllowAnyMethod();
                        });
});

// JWT token authentication setup
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "AppAPI",
        ValidAudience = "https://localhost:7006",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes("AppSecretKey"))
    };
});
//var jwtSettings = builder.Configuration.GetSection("JwtSettings");
//builder.Services.AddAuthentication(opt =>
//{
//    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = jwtSettings["validIssuer"],
//        ValidAudience = jwtSettings["validAudience"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
//            .GetBytes(jwtSettings.GetSection("securityKey").Value))
//    };
//});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

//app.UseAuthentication();

app.UseAuthorization();

app.UseTenantResolverMiddleware();

app.MapControllers();

app.Run();




