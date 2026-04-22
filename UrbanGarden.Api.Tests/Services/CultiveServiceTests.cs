using Xunit;
using UrbanGarden.Api.Services;
using UrbanGarden.Api.Models.Entities;
using UrbanGarden.Api.Models.Enums;
using Moq;

public class CropTypeServiceTests
{
[Fact]
    public void GetAll_WhenEmpty_ReturnsEmptyList()
    {

        var mockRepository = new Mock<ICropTypeRepository>();

        mockRepository
            .Setup(r => r.GetAll())
            .Returns(new List<CropType>());

        var service = new CropTypeService(mockRepository.Object);

        var result = service.GetAll();

        Assert.NotNull(result);
        Assert.Empty(result);
    }
    [Fact]
    public void GetAll_WhenRepositoryHasData_ReturnsAllCultives()
    {
        var mockRepository = new Mock<ICropTypeRepository>();

        var data = new List<CropType>
        {
            new CropType { Name = "Carrot" },
            new CropType { Name = "Lettuce" }
        };

        mockRepository
            .Setup(r => r.GetAll())
            .Returns(data);

        var service = new CropTypeService(mockRepository.Object);

        var result = service.GetAll();

        Assert.Equal(2, result.Count());
    }
    [Fact]
    public void GetById_ReturnsCultive()
    {
        var mockRepository = new Mock<ICropTypeRepository>();
        var data = new List<CropType>
        {
            new CropType {ID = 1, Name = "Carrot", Season = Season.Spring },
            new CropType {ID = 2, Name = "Lettuce", Season = Season.Spring }
        };

        mockRepository
            .Setup(r => r.GetById(It.IsAny<int>()))
            .Returns((int id) => data.FirstOrDefault(c => c.ID == id));
        var service = new CropTypeService(mockRepository.Object);
        
        var result = service.GetById(1);
        Assert.Equal("Carrot", result?.Name);
        var result2 = service.GetById(2);
        Assert.Equal("Lettuce", result2?.Name);
    }
    [Fact]
    public void GetById_ReturnsNullForNonExistingCultive()
    {
        var mockRepository = new Mock<ICropTypeRepository>();

        mockRepository
            .Setup(r => r.GetById(999))
            .Returns((CropType?)null);
    
        var service = new CropTypeService(mockRepository.Object);
        
        var result = service.GetById(999);
        Assert.Null(result);
    }
    [Fact]
    public void Update_CallsRepositoryUpdate()
    {
        var mockRepository = new Mock<ICropTypeRepository>();
        var data = new List<CropType>
        {
            new CropType { ID = 1, Name = "Pepper" }
        };
        mockRepository
            .Setup(r => r.GetById(It.IsAny<int>()))
            .Returns((int id) => data.FirstOrDefault(c => c.ID == id));
        mockRepository
            .Setup(r => r.Update(It.IsAny<CropType>()))
            .Callback((CropType c) =>
            {
                var existing = data.FirstOrDefault(x => x.ID == c.ID);
                if (existing != null)
                    existing.Name = c.Name;
            });

        var service = new CropTypeService(mockRepository.Object);
        var cropType = new CropType { ID = 1, Name = "Bell Pepper" };
        service.Update(cropType);
        var result = service.GetById(1);

        Assert.Equal("Bell Pepper", result?.Name);    
    }
    [Fact]
    public void Update_NonExistentId_ThrowsException()
    {
        var mockRepository = new Mock<ICropTypeRepository>();

        mockRepository
            .Setup(r => r.GetById(999))
            .Returns((CropType?)null);

        var service = new CropTypeService(mockRepository.Object);

        var cropType = new CropType { ID = 999, Name = "NonExisting" };

        Assert.Throws<KeyNotFoundException>(() => service.Update(cropType));
    }
    [Fact]
    public void Delete_DeletesCultive()
    {
        var mockRepository = new Mock<ICropTypeRepository>();
        var data = new List<CropType>
        {
            new CropType {ID = 1, Name = "Cucumber", Season = Season.Summer },
            new CropType {ID = 2, Name = "Lettuce", Season = Season.Spring }
        };

        mockRepository
            .Setup(r => r.GetById(It.IsAny<int>()))
            .Returns((int id) => data.FirstOrDefault(c => c.ID == id));
        mockRepository
            .Setup(r => r.Delete(It.IsAny<int>()))
            .Callback((int id) =>
            {
                var existing = data.FirstOrDefault(x => x.ID == id);
                if (existing != null)
                    data.Remove(existing);
            });
        
        var service = new CropTypeService(mockRepository.Object);
        
        service.Delete(1);
        
        var result = service.GetById(1);
        Assert.Null(result);
    }

    [Fact]
    public void Delete_NonExistentId_ThrowsException()
    {
        var mockRepository = new Mock<ICropTypeRepository>();

        mockRepository
            .Setup(r => r.GetById(999))
            .Returns((CropType?)null);

        var service = new CropTypeService(mockRepository.Object);

        Assert.Throws<KeyNotFoundException>(() => service.Delete(999));

        mockRepository.Verify(r => r.Delete(It.IsAny<int>()), Times.Never);
    }
    [Fact]
    public void Add_CallsRepositoryAdd()
    {
        var mockRepository = new Mock<ICropTypeRepository>();

        var service = new CropTypeService(mockRepository.Object);

        var cropType = new CropType { Name = "Tomato" };

        service.Add(cropType);

        mockRepository.Verify(r => r.Add(cropType), Times.Once);
    }
    [Fact]
    public void Update_NoChanges_DoesNotFail()
    {
        var mockRepository = new Mock<ICropTypeRepository>();

        var cropType = new CropType { ID = 1, Name = "Pepper", Season = Season.Summer };

        mockRepository
            .Setup(r => r.GetById(1))
            .Returns(cropType);

        var service = new CropTypeService(mockRepository.Object);

        service.Update(cropType);

        mockRepository.Verify(r => r.Update(cropType), Times.Once);
    }

}