using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PatikaLMSCoreProject.Business.DataProtection;
using PatikaLMSCoreProject.Business.Operations.Course;
using PatikaLMSCoreProject.Business.Operations.Feature;
using PatikaLMSCoreProject.Business.Operations.Setting;
using PatikaLMSCoreProject.Business.Operations.User;
using PatikaLMSCoreProject.Data.Context;
using PatikaLMSCoreProject.Data.Repositories;
using PatikaLMSCoreProject.Data.UnitOfWork;
using PatikaLMSCoreProject.WebApi.Middlewares;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Modified to allow authentication within Swagger
builder.Services.AddSwaggerGen(options =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Name = "Jwt Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Put **_ONLY_** your JWT Bearer Token on Textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});

// Protection
builder.Services.AddScoped<IDataProtection, DataProtection>();
var keysDirectory = new DirectoryInfo(Path.Combine(builder.Environment.ContentRootPath, "App_Data", "Keys"));
builder.Services.AddDataProtection()
                .SetApplicationName("PatikaLMSCoreProject")
                .PersistKeysToFileSystem(keysDirectory);

//JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],

                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["Jwt:Audience"],

                        ValidateLifetime = true, // To prevent the expired token from being used

                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!))
                    };
                });

// Database Connection
var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<PatikaLMSCoreProjectDbContext>(options => options.UseSqlServer(connectionString));

// Service Lifetimes for Repository & UnitOfWork 
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Service Lifetimes for Operations
builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<IFeatureService, FeatureManager>();
builder.Services.AddScoped<ICourseService, CourseManager>();
builder.Services.AddScoped<ISettingService, SettingManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseExceptionHandling();
//app.UseMiddleware<MaintenenceMiddleware>();
app.UseMaintenenceMode();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();