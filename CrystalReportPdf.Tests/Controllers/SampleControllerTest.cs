using CrystalReportPdf.Api.Controllers;
using CrystalReportPdf.Api.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CrystalReportPdf.Tests.Controllers
{
    [TestClass]
    public class SampleControllerTest
    {
        private readonly SampleController _controller;

        public SampleControllerTest()
        {
            // Arrange (initialize the controller and mock request)
            _controller = new SampleController();
        }

        [TestMethod]
        public void SampleReport_WithValidData_ShouldReturnOkResponseWithPdfContent()
        {
            // Arrange
            var body = new RequestBody
            {
                TemplatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "SampleReport.rpt"),
                FileName = "SampleReport.pdf",
                Enumurable = new List<SampleModel>
                {
                    new SampleModel()
                    {
                        ItemId = 1,
                        ItemCode = "FG0001",
                        ItemName = Guid.NewGuid().ToString(),
                        ItemType = "Finished Goods",
                        ItemImage = null,
                    },
                },
            };

            // Act
            var result = _controller.SampleReport(body, download: false);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Content.Should().BeOfType<StreamContent>();
            result.Content.Headers.ContentType.Should().Be(new MediaTypeHeaderValue("application/pdf"));

            var contentDisposition = result.Content.Headers.ContentDisposition;
            contentDisposition.Should().NotBeNull();
            contentDisposition.DispositionType.Should().Be("inline"); // Or "attachment" if download is true
            contentDisposition.FileName.Should().MatchRegex(@"SampleReport.pdf");

            // Optional: If you can, verify the PDF stream content somehow.
        }

        [TestMethod]
        public void SampleReport_WithEmptyData_ShouldReturnBadRequest()
        {
            // Arrange
            var body = new RequestBody
            {
                TemplatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "SampleReport.rpt"),
                FileName = "SampleReport.pdf",
                Enumurable = null,
            };

            // Act
            var result = _controller.SampleReport(body, download: false);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
