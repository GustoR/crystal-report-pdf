using CrystalReportPdf.Api.Controllers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace CrystalReportPdf.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ActionResult response = controller.Index();

            // Assert
            response.Should().NotBeNull().And.BeOfType<ViewResult>();
            var result = response as ViewResult;
            var title = result.ViewBag.Title as string;
            title.Should().Be("Home Page");
        }
    }
}
