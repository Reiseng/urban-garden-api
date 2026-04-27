using Xunit;
using UrbanGarden.Api.Services;
using UrbanGarden.Api.Models.Entities;
using UrbanGarden.Api.Models.Dtos;
using UrbanGarden.Api.Models.Enums;
using Moq;
using Microsoft.AspNetCore.Mvc;
using UrbanGarden.Api.Controllers;

public class CropTypeControllerTests
{
    [Fact]
    public void GetAll_ReturnsOkWithList()
    {
        var mockService = new Mock<ICropTypeService>();

        mockService
            .Setup(s => s.GetAll())
            .Returns(new List<CropType>
            {
                new CropType { ID = 1, Name = "Carrot" },
                new CropType { ID = 2, Name = "Lettuce" }
            });

        var controller = new CropTypeController(mockService.Object);

        var result = controller.GetAll();

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var list = Assert.IsAssignableFrom<IEnumerable<CropTypeDto>>(okResult.Value);

        Assert.Equal(2, list.Count());
    }
    [Fact]
    public void GetAll_WhenEmpty_ReturnsOkWithEmptyList()
    {
        var mockService = new Mock<ICropTypeService>();

        mockService
            .Setup(s => s.GetAll())
            .Returns(new List<CropType>());

        var controller = new CropTypeController(mockService.Object);

        var result = controller.GetAll();

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var list = Assert.IsAssignableFrom<IEnumerable<CropTypeDto>>(okResult.Value);

        Assert.Empty(list);
    }
    [Fact]
    public void GetById_WhenExists_ReturnsOk()
    {
        var mockService = new Mock<ICropTypeService>();

        mockService
            .Setup(s => s.GetById(1))
            .Returns(new CropType { ID = 1, Name = "Carrot" });

        var controller = new CropTypeController(mockService.Object);

        var result = controller.GetById(1);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var cropType = Assert.IsType<CropTypeDto>(okResult.Value);

        Assert.Equal("Carrot", cropType.Name);
    }
    [Fact]
    public void GetById_WhenNotExists_ReturnsNotFound()
    {
        var mockService = new Mock<ICropTypeService>();

        mockService
            .Setup(s => s.GetById(1))
            .Returns((CropType?)null);

        var controller = new CropTypeController(mockService.Object);

        var result = controller.GetById(1);

        Assert.IsType<NotFoundResult>(result.Result);
    }
    [Fact]
    public void Create_WhenValid_ReturnsCreated()
    {
        var mockService = new Mock<ICropTypeService>();

        var createDto = new CreateCropTypeDto
        { 
            Name = "Carrot", 
            Season = Season.Spring 
        };

        mockService
            .Setup(s => s.Add(It.IsAny<CropType>()))
            .Callback<CropType>(c => c.ID = 1);

        var controller = new CropTypeController(mockService.Object);
        controller.ControllerContext = new ControllerContext
        {
            RouteData = new Microsoft.AspNetCore.Routing.RouteData()
        };

        controller.RouteData.Values["version"] = "v1";
        var result = controller.Create(createDto);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result);

        var returnedCropType = Assert.IsType<CropType>(createdResult.Value);

        Assert.Equal(1, returnedCropType.ID);
        Assert.Equal("Carrot", returnedCropType.Name);

        mockService.Verify(s => s.Add(It.IsAny<CropType>()), Times.Once);
    }
    [Fact]
    public void Create_WhenInvalid_ReturnsBadRequest()
    {
        var mockService = new Mock<ICropTypeService>();

        var createDto = new CreateCropTypeDto 
        { 
            Name = "", 
            Season = Season.Spring 
        };

        var controller = new CropTypeController(mockService.Object);
        controller.ModelState.AddModelError("Name", "The Name field is required.");

        var result = controller.Create(createDto);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.IsType<SerializableError>(badRequestResult.Value);

        mockService.Verify(s => s.Add(It.IsAny<CropType>()), Times.Never);
    }
    [Fact]
    public void Update_WhenValid_ReturnsNoContent()
    {
        var mockService = new Mock<ICropTypeService>();

        var updateDto = new CropTypeDto 
        { 
            ID = 1, 
            Name = "Carrot", 
            Season = Season.Spring, 
            Disponible = true 
        };

        mockService
            .Setup(s => s.GetById(1))
            .Returns(new CropType { ID = 1, Name = "Carrot", Season = Season.Spring, Disponible = true });

        var controller = new CropTypeController(mockService.Object);

        var result = controller.Update(1, updateDto);
        mockService.Verify(s => s.Update(It.IsAny<CropType>()), Times.Once);
        Assert.IsType<NoContentResult>(result);
    }
    [Fact]
    public void Update_WhenNotExists_ReturnsNotFound()
    {
        var mockService = new Mock<ICropTypeService>();

        var updateDto = new CropTypeDto 
        { 
            ID = 1, 
            Name = "Carrot", 
            Season = Season.Spring, 
            Disponible = true 
        };

        mockService
            .Setup(s => s.GetById(1))
            .Returns((CropType?)null);

        var controller = new CropTypeController(mockService.Object);

        var result = controller.Update(1, updateDto);
        mockService.Verify(s => s.Update(It.IsAny<CropType>()), Times.Never);

        Assert.IsType<NotFoundResult>(result);
    }
    [Fact]
    public void Update_WhenInvalid_ReturnsBadRequest()
    {
        var mockService = new Mock<ICropTypeService>();

        var updateDto = new CropTypeDto 
        { 
            ID = 1, 
            Name = "", 
            Season = Season.Spring, 
            Disponible = true 
        };

        mockService
            .Setup(s => s.GetById(1))
            .Returns(new CropType { ID = 1, Name = "Carrot", Season = Season.Spring, Disponible = true });

        var controller = new CropTypeController(mockService.Object);
        controller.ModelState.AddModelError("Name", "The Name field is required.");

        var result = controller.Update(1, updateDto);
        mockService.Verify(s => s.Update(It.IsAny<CropType>()), Times.Never);
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.IsType<SerializableError>(badRequestResult.Value);
    }
    [Fact]
    public void Delete_WhenExists_ReturnsNoContent()
    {
        var mockService = new Mock<ICropTypeService>();

        mockService
            .Setup(s => s.GetById(1))
            .Returns(new CropType { ID = 1, Name = "Carrot" });

        var controller = new CropTypeController(mockService.Object);

        var result = controller.Delete(1);
        mockService.Verify(s => s.Delete(1), Times.Once);
        Assert.IsType<NoContentResult>(result);
    }
    [Fact]
    public void Delete_WhenNotExists_ReturnsNotFound()
    {
        var mockService = new Mock<ICropTypeService>();

        mockService
            .Setup(s => s.GetById(1))
            .Returns((CropType?)null);

        var controller = new CropTypeController(mockService.Object);

        var result = controller.Delete(1);
        mockService.Verify(s => s.Delete(It.IsAny<int>()), Times.Never);
        Assert.IsType<NotFoundResult>(result);
    }
}