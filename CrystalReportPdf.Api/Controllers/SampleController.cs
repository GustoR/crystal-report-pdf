using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalReportPdf.Api.Models;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace CrystalReportPdf.Api.Controllers
{

    public class SampleController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage SampleReport(RequestBody body, [FromUri] bool download = false)
        {
            // Validate the input
            if (body == null || string.IsNullOrWhiteSpace(body.TemplatePath) || body.Enumurable == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            try
            {
                // Load the report
                using (ReportDocument report = new ReportDocument())
                {
                    // Load the report template
                    report.Load(body.TemplatePath);
                    // Set the data source
                    report.SetDataSource(body.Enumurable);

                    // Export the report to a stream
                    using (var stream = report.ExportToStream(ExportFormatType.PortableDocFormat))
                    {
                        // Reset the stream position
                        stream.Seek(0, SeekOrigin.Begin);

                        // Create the response message
                        var result = new HttpResponseMessage(HttpStatusCode.OK)
                        {
                            Content = new StreamContent(stream)
                        };
                        // Set the content type
                        result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                        // Set the content disposition
                        result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue(download ? "attachment" : "inline")
                        {
                            Name = body.FileName,
                            FileName = body.FileName,
                        };

                        // Return the response message
                        return result;
                    }
                }
            }
            catch (Exception)
            {
                // Log the exception
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
    }
}
