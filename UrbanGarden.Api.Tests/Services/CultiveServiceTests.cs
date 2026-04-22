using Xunit;
using UrbanGarden.Api.Services;
using UrbanGarden.Api.Models.Entities;
using UrbanGarden.Api.Models.Enums;
using Moq;

public class CultiveServiceTests
{
[Fact]
    public void GetAll_WhenEmpty_ReturnsEmptyList()
    {

        var mockRepository = new Mock<ICultiveRepository>();

        mockRepository
            .Setup(r => r.GetAll())
            .Returns(new List<Cultive>());

        var service = new CultiveService(mockRepository.Object);

        var result = service.GetAll();

        Assert.NotNull(result);
        Assert.Empty(result);
    }
    [Fact]
    public void GetAll_WhenRepositoryHasData_ReturnsAllCultives()
    {
        var mockRepository = new Mock<ICultiveRepository>();

        var data = new List<Cultive>
        {
            new Cultive { Name = "Carrot" },
            new Cultive { Name = "Lettuce" }
        };

        mockRepository
            .Setup(r => r.GetAll())
            .Returns(data);

        var service = new CultiveService(mockRepository.Object);

        var result = service.GetAll();

        Assert.Equal(2, result.Count());
    }
    [Fact]
    public void GetById_ReturnsCultive()
    {
        var mockRepository = new Mock<ICultiveRepository>();
        var data = new List<Cultive>
        {
            new Cultive {ID = 1, Name = "Carrot", Season = Season.Spring },
            new Cultive {ID = 2, Name = "Lettuce", Season = Season.Spring }
        };

        mockRepository
            .Setup(r => r.GetById(It.IsAny<int>()))
            .Returns((int id) => data.FirstOrDefault(c => c.ID == id));
        var service = new CultiveService(mockRepository.Object);
        
        var result = service.GetById(1);
        Assert.Equal("Carrot", result?.Name);
        var result2 = service.GetById(2);
        Assert.Equal("Lettuce", result2?.Name);
    }
    [Fact]
    public void GetById_ReturnsNullForNonExistingCultive()
    {
        var mockRepository = new Mock<ICultiveRepository>();

        mockRepository
            .Setup(r => r.GetById(999))
            .Returns((Cultive?)null);
    
        var service = new CultiveService(mockRepository.Object);
        
        var result = service.GetById(999);
        Assert.Null(result);
    }
    [Fact]
    public void Update_CallsRepositoryUpdate()
    {
        var mockRepository = new Mock<ICultiveRepository>();
        var data = new List<Cultive>
        {
            new Cultive { ID = 1, Name = "Pepper" }
        };
        mockRepository
            .Setup(r => r.GetById(It.IsAny<int>()))
            .Returns((int id) => data.FirstOrDefault(c => c.ID == id));
        mockRepository
            .Setup(r => r.Update(It.IsAny<Cultive>()))
            .Callback((Cultive c) =>
            {
                var existing = data.FirstOrDefault(x => x.ID == c.ID);
                if (existing != null)
                    existing.Name = c.Name;
            });

        var service = new CultiveService(mockRepository.Object);
        var cultive = new Cultive { ID = 1, Name = "Bell Pepper" };
        service.Update(cultive);
        var result = service.GetById(1);

        Assert.Equal("Bell Pepper", result?.Name);    
    }
    [Fact]
    public void Update_NonExistentId_ThrowsException()
    {
        var mockRepository = new Mock<ICultiveRepository>();

        mockRepository
            .Setup(r => r.GetById(999))
            .Returns((Cultive?)null);

        var service = new CultiveService(mockRepository.Object);

        var cultive = new Cultive { ID = 999, Name = "NonExisting" };

        Assert.Throws<KeyNotFoundException>(() => service.Update(cultive));
    }
    [Fact]
    public void Delete_DeletesCultive()
    {
        var mockRepository = new Mock<ICultiveRepository>();
        var data = new List<Cultive>
        {
            new Cultive {ID = 1, Name = "Cucumber", Season = Season.Summer },
            new Cultive {ID = 2, Name = "Lettuce", Season = Season.Spring }
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
        
        var service = new CultiveService(mockRepository.Object);
        
        service.Delete(1);
        
        var result = service.GetById(1);
        Assert.Null(result);
    }

    [Fact]
    public void Delete_NonExistentId_ThrowsException()
    {
        var mockRepository = new Mock<ICultiveRepository>();

        mockRepository
            .Setup(r => r.GetById(999))
            .Returns((Cultive?)null);

        var service = new CultiveService(mockRepository.Object);

        Assert.Throws<KeyNotFoundException>(() => service.Delete(999));

        mockRepository.Verify(r => r.Delete(It.IsAny<int>()), Times.Never);
    }
    [Fact]
    public void Add_CallsRepositoryAdd()
    {
        var mockRepository = new Mock<ICultiveRepository>();

        var service = new CultiveService(mockRepository.Object);

        var cultive = new Cultive { Name = "Tomato" };

        service.Add(cultive);

        mockRepository.Verify(r => r.Add(cultive), Times.Once);
    }
    [Fact]
    public void Update_NoChanges_DoesNotFail()
    {
        var mockRepository = new Mock<ICultiveRepository>();

        var cultive = new Cultive { ID = 1, Name = "Pepper", Season = Season.Summer, Disponible = true };

        mockRepository
            .Setup(r => r.GetById(1))
            .Returns(cultive);

        var service = new CultiveService(mockRepository.Object);

        service.Update(cultive);

        mockRepository.Verify(r => r.Update(cultive), Times.Once);
    }

}