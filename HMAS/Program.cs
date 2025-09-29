using HMAS.Data;
using HMAS.Helper;
using HMAS.Middleware;
using HMAS.Profiles;
using HMAS.Repositories;
using HMAS.Repositories.Interface;
using HMAS.Services;
using HMAS.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddScoped<LoggingMiddleware>();

builder.Services.AddDbContext<ApplicationDbContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("DbString"),
        sqlOptions => sqlOptions.EnableRetryOnFailure());
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
            ValidAudience = builder.Configuration["JwtConfig:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:Key"]))
        };
    });
builder.Services.AddAuthorization();

//Repos 
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IPatientRepo, PatientRepo>();
builder.Services.AddScoped<IDoctorRepo, DoctorRepo>();
builder.Services.AddScoped<IDepartmentRepo, DepartmentRepo>();
builder.Services.AddScoped<IAppointRepo, AppointRepo>();
builder.Services.AddScoped<IMedicalRepo, MedicalRepo>();
builder.Services.AddScoped<IDashRepo, DashRepo>();

//Services
builder.Services.AddScoped<IAuthService, AuthServiceImple>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IDeptService, DepartmentService>();
builder.Services.AddScoped<IAppointService, AppointService>();
builder.Services.AddScoped<IMedicalService, MedicalService>();
builder.Services.AddScoped<IDashService, DashService>();

//AutoMapper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<AutoMapperProfile>();
});

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<IEmailService, EmailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseSwagger();
//app.UseSwaggerUI();

//using var serviceScope = app.Services.CreateScope();
//using var dbContext = serviceScope.ServiceProvider.GetRequiredService<DbContext>();
//dbContext.Database.Migrate();

app.UseHttpsRedirection();

app.UseMiddleware<LoggingMiddleware>();

app.UseAuthorization();


app.MapControllers();

app.Run();
