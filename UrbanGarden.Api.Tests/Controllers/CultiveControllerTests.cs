using Xunit;
using UrbanGarden.Api.Services;
using UrbanGarden.Api.Models.Entities;
using UrbanGarden.Api.Models.Dtos;
using Moq;
using Microsoft.AspNetCore.Mvc;
using UrbanGarden.Api.Controllers;

public class CultiveControllerTests
{
    [Fact]
    public void GetById_WhenExists_ReturnsOk()
    {
        var mockService = new Mock<ICultiveService>();

        mockService
            .Setup(s => s.GetById(1))
            .Returns(new Cultive { ID = 1, Name = "Carrot" });

        var controller = new CultiveController(mockService.Object);

        var result = controller.GetById(1);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var cultive = Assert.IsType<CultiveDto>(okResult.Value);

        Assert.Equal("Carrot", cultive.Name);
    }
}