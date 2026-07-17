using System.Text.Json.Serialization;
using UrbanGarden.Api.Repositories;
using UrbanGarden.Api.Services;
using UrbanGarden.Api.Infrastructure.MQTT.Services;
using UrbanGarden.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var mqttBrokerIp = builder.Configuration["MQTT:BrokerIP"];
var mqttBrokerPort = builder.Configuration["MQTT:BrokerPort"];
var deviceRegistrationKey = builder.Configuration["DeviceRegistration:Key"];

builder.WebHost.UseUrls("http://0.0.0.0:8080");

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactFrontend",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
builder.Services.AddDbContext<UrbanGardenDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

// Repositories
builder.Services.AddScoped<ICropTypeRepository, CropTypeRepository>();
builder.Services.AddScoped<IGardenPlotRepository, GardenPlotRepository>();
builder.Services.AddScoped<IPlantedCropRepository, PlantedCropRepository>();
builder.Services.AddScoped<IHarvestRepository, HarvestRepository>();
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<ISensorDataRepository, SensorDataRepository>();

// Services
builder.Services.AddScoped<ICropTypeService, CropTypeService>();
builder.Services.AddScoped<IGardenPlotService, GardenPlotService>();
builder.Services.AddScoped<IPlantedCropService, PlantedCropService>();
builder.Services.AddScoped<IHarvestService, HarvestService>();
builder.Services.AddScoped<IDeviceService, DeviceService>(provider => new DeviceService(provider.GetRequiredService<IDeviceRepository>(), deviceRegistrationKey));
builder.Services.AddScoped<ISensorDataService, SensorDataService>();

// MQTT
builder.Services.AddSingleton<IMqttService, MqttClientService>(provider => new MqttClientService(mqttBrokerIp, mqttBrokerPort)
);builder.Services.AddScoped<ISensorsService, SensorsService>();
builder.Services.AddHostedService<MqttHostedService>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseCors("ReactFrontend");
app.UseAuthorization();
app.MapControllers();

app.Run();
