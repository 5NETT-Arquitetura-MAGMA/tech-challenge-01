using Microsoft.EntityFrameworkCore;
using Prometheus;
using RegionalContactsAPI.Core.Repository;
using RegionalContactsAPI.Core.Service;
using RegionalContactsAPI.Core.Service.Interface;
using RegionalContactsAPI.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ContactDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ContactDbContext"));
});

builder.Services.AddScoped(typeof(IEntityBase<>), typeof(EntityRepository<>));
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<ICidadeService, CidadeService>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<ICidadeRepository, CidadeRepository>();
builder.Services.AddScoped<ICacheService, CacheService>();

builder.Services.AddMemoryCache(); // Add IMemoryCache service

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region Prometheus

var counter = Metrics.CreateCounter("webapimetric", "Conta os requests da API.", new CounterConfiguration
{
    LabelNames = new[] { "method", "endpoint" }
});

app.Use((context, next) =>
{
    counter.WithLabels(context.Request.Method, context.Request.Path).Inc();
    return next();
});

app.UseMetricServer();
app.UseHttpMetrics();

#endregion

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{
}