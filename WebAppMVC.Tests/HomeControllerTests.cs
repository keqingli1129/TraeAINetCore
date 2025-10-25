using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using WebAppMVC.Controllers;
using Xunit;

namespace WebAppMVC.Tests;

public class HomeControllerTests
{
    [Fact]
    public void Index_Returns_ViewResult_With_Default_View()
    {
        var controller = new HomeController(NullLogger<HomeController>.Instance);

        var result = controller.Index();

        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.True(string.IsNullOrEmpty(viewResult.ViewName));
    }

    [Fact]
    public void Privacy_Returns_ViewResult_With_Default_View()
    {
        var controller = new HomeController(NullLogger<HomeController>.Instance);

        var result = controller.Privacy();

        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.True(string.IsNullOrEmpty(viewResult.ViewName));
    }
}