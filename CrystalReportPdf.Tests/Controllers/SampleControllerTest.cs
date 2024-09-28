using CrystalReportPdf.Api;
using CrystalReportPdf.Api.Controllers;
using CrystalReportPdf.Api.Models;
using CrystalReportPdf.Tests.MockData;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CrystalReportPdf.Tests.Controllers
{
    [TestClass]
    public class SampleControllerTest
    {
        private readonly SampleController _controller;
        private readonly string _SampleFileName = "SampleReport.rpt";

        public SampleControllerTest()
        {
            // Arrange (initialize the controller and mock request)
            _controller = new SampleController();
        }

        [TestMethod]
        public async Task SampleReport_ShouldReturnsOkWithPdfContent()
        {
            // Arrange
            var body = new RequestBody<SampleItem>
            {
                FileName = _SampleFileName,
                Enumurable = SampleMockData.GetSampleItems(),
                SubReportsDatasource = SampleMockData.GetSampleSubReportsDatasource(),
            };
            bool download = false;

            // Act
            var response = _controller.SampleReport(body, download);

            // Assert
            response.Should().NotBeNull().And.BeOfType<HttpResponseMessage>();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.MediaType.Should().Be(AppConstants.PdfContentType);
            var content = await response.Content.ReadAsByteArrayAsync();
            content.Should().NotBeNullOrEmpty();

            var contentDisposition = response.Content.Headers.ContentDisposition;
            contentDisposition.Should().NotBeNull();
            contentDisposition.DispositionType.Should().Be(AppConstants.InlineDisposition);
            string fileNameBase = Regex.Match(body.FileName, @"^[^\.]+").Value;
            string fileOutputBase = Regex.Match(contentDisposition.FileName, @"^[^\.]+").Value;
            fileOutputBase.Should().MatchRegex($"^{Regex.Escape(fileNameBase)}$");

            // Optional: If you can, verify the PDF stream content somehow.
        }

        [TestMethod]
        public async Task SampleReport_Download_ShouldReturnsOkWithAttachmentDisposition()
        {
            // Arrange
            var body = new RequestBody<SampleItem>
            {
                FileName = _SampleFileName,
                Enumurable = SampleMockData.GetSampleItems(),
                SubReportsDatasource = SampleMockData.GetSampleSubReportsDatasource(),
            };
            bool download = true;

            // Act
            var response = _controller.SampleReport(body, download);

            // Assert
            response.Should().NotBeNull().And.BeOfType<HttpResponseMessage>();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.MediaType.Should().Be(AppConstants.PdfContentType);
            var content = await response.Content.ReadAsByteArrayAsync();
            content.Should().NotBeNullOrEmpty();

            var contentDisposition = response.Content.Headers.ContentDisposition;
            contentDisposition.Should().NotBeNull();
            contentDisposition.DispositionType.Should().Be(AppConstants.AttachmentDisposition);
            string fileNameBase = Regex.Match(body.FileName, @"^[^\.]+").Value;
            string fileOutputBase = Regex.Match(contentDisposition.FileName, @"^[^\.]+").Value;
            fileOutputBase.Should().MatchRegex($"^{Regex.Escape(fileNameBase)}$");

            // Optional: If you can, verify the PDF stream content somehow.
        }

        [TestMethod]
        public async Task SampleReport_WithNullBody_ShouldReturnsBadRequst()
        {
            // Arrange
            RequestBody<SampleItem> body = null;
            bool download = false;

            // Act
            var response = _controller.SampleReport(body, download);

            // Assert
            response.Should().NotBeNull().And.BeOfType<HttpResponseMessage>();
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeNull().And.Be("Request body is required");
        }

        [TestMethod]
        public async Task SampleReport_WithEmptyFileName_ShouldReturnsBadRequest()
        {
            // Arrange
            var body = new RequestBody<SampleItem>
            {
                FileName = string.Empty,
                Enumurable = SampleMockData.GetSampleItems(),
                SubReportsDatasource = SampleMockData.GetSampleSubReportsDatasource(),
            };
            bool download = false;

            // Act
            var response = _controller.SampleReport(body, download);

            // Assert
            response.Should().NotBeNull().And.BeOfType<HttpResponseMessage>();
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeNull().And.Be("File name is required");
        }

        [TestMethod]
        public async Task SampleReport_WithNotExistsFile_ShouldReturnsNotFound()
        {
            // Arrange
            var body = new RequestBody<SampleItem>
            {
                FileName = Guid.NewGuid().ToString(),
                Enumurable = SampleMockData.GetSampleItems(),
                SubReportsDatasource = SampleMockData.GetSampleSubReportsDatasource(),
            };
            bool download = false;

            // Act
            var response = _controller.SampleReport(body, download);

            // Assert
            response.Should().NotBeNull().And.BeOfType<HttpResponseMessage>();
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeNull().And.Be("Report template not found");
        }

        [TestMethod]
        public async Task SampleReport_WithNullData_ShouldReturnsBadRequest()
        {
            // Arrange
            var body = new RequestBody<SampleItem>
            {
                FileName = _SampleFileName,
                Enumurable = null,
            };
            bool download = false;

            // Act
            var response = _controller.SampleReport(body, download);

            // Assert
            response.Should().NotBeNull().And.BeOfType<HttpResponseMessage>();
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeNull().And.Be("Data source is required");
        }

        [TestMethod]
        public async Task SampleReport_WithNullSubReportData_ShouldReturnsBadRequest()
        {
            // Arrange
            var body = new RequestBody<SampleItem>
            {
                FileName = _SampleFileName,
                Enumurable = SampleMockData.GetSampleItems(),
                SubReportsDatasource = new Dictionary<string, object>
                {
                    { "SampleNoteReport", null }
                }
            };
            bool download = false;

            // Act
            var response = _controller.SampleReport(body, download);

            // Assert
            response.Should().NotBeNull().And.BeOfType<HttpResponseMessage>();
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeNull().And.Be("Sub report data source is required");
        }
    }
}
