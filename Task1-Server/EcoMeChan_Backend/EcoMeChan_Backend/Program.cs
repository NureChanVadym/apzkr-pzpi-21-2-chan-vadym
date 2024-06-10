// Program.cs
using EcoMeChan.Database;
using EcoMeChan.Repositories.Interfaces;
using EcoMeChan.Repositories;
using EcoMeChan.Services.Interfaces;
using EcoMeChan.Services;
using Microsoft.EntityFrameworkCore;
using EcoMeChan_Backend.Services.ConsumptionNorms;
using EcoMeChan.Services.ConsumptionNorms;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IConsumptionRepository, ConsumptionRepository>();
builder.Services.AddScoped<IIoTDeviceRepository, IoTDeviceRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<ISensorDataRepository, SensorDataRepository>();
builder.Services.AddScoped<ITariffRepository, TariffRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IConsumptionCostService, ConsumptionCostService>();
builder.Services.AddScoped<IConsumptionNormRepository, ConsumptionNormRepository>();
builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
builder.Services.AddScoped<IResourceTypeRepository, ResourceTypeRepository>();

builder.Services.AddScoped<IConsumptionService, ConsumptionService>();
builder.Services.AddScoped<IIoTDeviceService, IoTDeviceService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<ISensorDataService, SensorDataService>();
builder.Services.AddScoped<ITariffService, TariffService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IConsumptionNormService, ConsumptionNormService>();
builder.Services.AddScoped<ICurrencyService, CurrencyService>();
builder.Services.AddScoped<IResourceTypeService, ResourceTypeService>();

builder.Services.AddScoped<ConsumptionNormCalculator, StandardDeviationCalculator>(); // alternatively you can use AverageDeviationCalculator
//builder.Services.AddScoped<ConsumptionNormCalculator, AverageDeviationCalculator>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
