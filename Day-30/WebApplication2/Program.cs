using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Interfaces;
using WebApplication2.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

// reflection
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddAutoMapper(_ => { }, Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IFileUpload, FileUpload>();
builder.Services.AddHttpContextAccessor();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();