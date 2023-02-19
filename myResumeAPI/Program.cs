using Microsoft.EntityFrameworkCore;
using myResumeAPI.Contracts;
using myResumeAPI.Manager;
using myResumeAPI.Models;
using myResumeAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Logging.f();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ResumeAPIContext>(c => c.UseSqlServer(builder.Configuration.GetConnectionString("myResumedb"),d => d.EnableRetryOnFailure())) ;
//builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ResumeAPIContext>();
builder.Services.AddTransient<IRepository<AboutMe>, AboutMeRepository>();
builder.Services.AddTransient<IRepository<Education>, EducationRepository>();
builder.Services.AddTransient<IRepository<ContactReference>, ContactReferenceRepository>();
builder.Services.AddTransient<IRepository<WorkHistory>, WorkHistoryRepository>();
builder.Services.AddTransient<IManagerRepository, ManagerRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
