using Xunit;
using UrbanGarden.Api.Services;
using UrbanGarden.Api.Models.Entities;
using UrbanGarden.Api.Models.Enums;
using Moq;

public class PlantedCropServiceTests
{
    private readonly PlantedCropService _service;
    private readonly Mock<IPlantedCropRepository> _repositoryMock;
    private readonly Mock<IHarvestRepository> _harvestRepositoryMock;

    public PlantedCropServiceTests()
    {
        _repositoryMock = new Mock<IPlantedCropRepository>();
        _harvestRepositoryMock = new Mock<IHarvestRepository>();
        _service = new PlantedCropService(_repositoryMock.Object, _harvestRepositoryMock.Object);
    }

    [Fact]
    public void GetAll_ShouldReturnAllPlantedCrops()
    {

        var plantedCrops = new List<PlantedCrop>
        {
            new PlantedCrop { Id = 1, CropTypeId = 1, PlantedAt = DateTime.Now },
            new PlantedCrop { Id = 2, CropTypeId = 2, PlantedAt = DateTime.Now }
        };
        _repositoryMock.Setup(r => r.GetAll()).Returns(plantedCrops);

        var result = _service.GetAll();

        Assert.Equal(2, result.Count());
        Assert.Contains(result, pc => pc.Id == 1 && pc.CropTypeId == 1);
        Assert.Contains(result, pc => pc.Id == 2 && pc.CropTypeId == 2);
    }
    [Fact]
    public void GetById_ShouldReturnPlantedCrop_WhenExists()
    {
        var plantedCrop = new PlantedCrop { Id = 1, CropTypeId = 1, PlantedAt = DateTime.Now };
        _repositoryMock.Setup(r => r.GetById(1)).Returns(plantedCrop);

        var result = _service.GetById(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal(1, result.CropTypeId);
    }
    [Fact]
    public void GetById_ShouldReturnNull_WhenNotExists()
    {
        _repositoryMock.Setup(r => r.GetById(1)).Returns((PlantedCrop?)null);

        var result = _service.GetById(1);

        Assert.Null(result);
    }
    [Fact]
    public void Add_ShouldAddPlantedCrop()
    {
        var plantedCrop = new PlantedCrop { Id = 1, CropTypeId = 1, PlantedAt = DateTime.Now };

        _service.Add(plantedCrop);

        _repositoryMock.Verify(r => r.Add(plantedCrop), Times.Once);
    }
    [Fact]
    public void Update_ShouldUpdatePlantedCrop()
    {
        var plantedCrop = new PlantedCrop { Id = 1, CropTypeId = 1, PlantedAt = DateTime.Now };

        _service.Update(plantedCrop.Id, CropStatus.ReadyForHarvest);

        _repositoryMock.Verify(r => r.Update(plantedCrop), Times.Once);
    }
    [Fact]
    public void Delete_ShouldDeletePlantedCrop()
    {
        var plantedCrop = new PlantedCrop { Id = 1, CropTypeId = 1, PlantedAt = DateTime.Now };

        _service.Delete(plantedCrop.Id);

        _repositoryMock.Verify(r => r.Delete(plantedCrop.Id), Times.Once);
    }
}