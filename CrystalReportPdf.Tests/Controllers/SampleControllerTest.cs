using CrystalReportPdf.Api;
using CrystalReportPdf.Api.Controllers;
using CrystalReportPdf.Api.Models;
using CrystalReportPdf.Tests.MockData;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CrystalReportPdf.Tests.Controllers
{
    [TestClass]
    public class SampleControllerTest
    {
        private readonly SampleController _sut;

        public SampleControllerTest()
        {
            // Arrange (initialize the controller and mock request)
            _sut = new SampleController();
        }
        [TestMethod]
        public async Task SampleReportRequestBody_ShouldReturnsOkWithPdfContent()
        {
            // Arrange
            var models = SampleMockData.Samples().ToArray();
            bool download = false;

            // Act
            var response = _sut.SampleReport(models, download);

            // Assert
            response.Should().NotBeNull().And.BeOfType<HttpResponseMessage>();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.MediaType.Should().Be(AppConstants.PdfContentType);
            var content = await response.Content.ReadAsByteArrayAsync();
            content.Should().NotBeNullOrEmpty();

            var contentDisposition = response.Content.Headers.ContentDisposition;
            contentDisposition.Should().NotBeNull();
            contentDisposition.DispositionType.Should().Be(AppConstants.InlineDisposition);
            contentDisposition.Name.Should().Contain(AppConstants.PdfFileExtension);
            contentDisposition.FileName.Should().Contain(AppConstants.PdfFileExtension);

            // Optional: If you can, verify the PDF stream content somehow.
        }

        [TestMethod]
        public async Task SampleReportRequestBody_Download_ShouldReturnsOkWithAttachmentDisposition()
        {
            // Arrange
            var models = SampleMockData.Samples().ToArray();
            bool download = true;

            // Act
            var response = _sut.SampleReport(models, download);

            // Assert
            response.Should().NotBeNull().And.BeOfType<HttpResponseMessage>();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.MediaType.Should().Be(AppConstants.PdfContentType);
            var content = await response.Content.ReadAsByteArrayAsync();
            content.Should().NotBeNullOrEmpty();

            var contentDisposition = response.Content.Headers.ContentDisposition;
            contentDisposition.Should().NotBeNull();
            contentDisposition.DispositionType.Should().Be(AppConstants.AttachmentDisposition);
            contentDisposition.Name.Should().Contain(AppConstants.PdfFileExtension);
            contentDisposition.FileName.Should().Contain(AppConstants.PdfFileExtension);

            // Optional: If you can, verify the PDF stream content somehow.
        }

        [TestMethod]
        public async Task SampleReportRequestBody_WithNullBody_ShouldReturnsBadRequst()
        {
            // Arrange
            SampleModel[] models = null;
            bool download = false;

            // Act
            var response = _sut.SampleReport(models, download);

            // Assert
            response.Should().NotBeNull().And.BeOfType<HttpResponseMessage>();
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeNull().And.Be("Datasource is required");
        }
    }
}
