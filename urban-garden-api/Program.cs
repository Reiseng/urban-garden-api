using System.Text.Json.Serialization;
using UrbanGarden.Api.Repositories;
using UrbanGarden.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

// Repositories
builder.Services.AddSingleton<ICropTypeRepository, CropTypeRepository>();
builder.Services.AddSingleton<IGardenPlotRepository, GardenPlotRepository>();
builder.Services.AddSingleton<IPlantedCropRepository, PlantedCropRepository>();
builder.Services.AddSingleton<IHarvestRepository, HarvestRepository>();

// Services
builder.Services.AddScoped<ICropTypeService, CropTypeService>();
builder.Services.AddScoped<IGardenPlotService, GardenPlotService>();
builder.Services.AddScoped<IPlantedCropService, PlantedCropService>();
builder.Services.AddScoped<IHarvestService, HarvestService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
