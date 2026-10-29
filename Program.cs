using TpApi.Abstracts;
using TpApi.Application;
using TpApi.DataAccess;
using TpApi.Repository;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddScoped(typeof(IApplication<>), typeof(Application<>));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IDBContext<>), typeof(DBContext<>));
builder.Services.AddDbContext<APIDBContext>(options =>
{
    options.UseSqlServer(ConfigurationExtensions.GetConnectionString(builder.Configuration, "DefaultConnectionString"), b => b.MigrationsAssembly("TpApi"));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var dbContextOptions = new DbContextOptionsBuilder<APIDBContext>()
    .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"), b => b.MigrationsAssembly("TpApi"))
    .Options;

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
