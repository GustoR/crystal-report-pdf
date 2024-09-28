using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalReportPdf.Api.Models;
using System.IO;
using System.Net;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Linq;
using System.Data;
using Newtonsoft.Json;

namespace CrystalReportPdf.Api
{
    public class AppUtils
    {
        public static byte[] ExportPdf<T>(RequestBody<T> body)
        {
            // Validate the input
            if (body == null)
            {
                // If the body is null, throw an argument null exception
                throw new ArgumentException("Request body is required");
            }
            if (string.IsNullOrWhiteSpace(body.FileName))
            {
                // If the file name is null or empty, throw an argument exception
                throw new ArgumentException("File name is required");
            }
            if (body.Enumurable == null)
            {
                // If the enumerable is null, throw an argument exception
                throw new ArgumentException("Data source is required");
            }
            if (body.SubReportsDatasource != null && body.SubReportsDatasource.Any(s => s.Value == null))
            {
                // If any sub report data source is null, throw an argument exception
                throw new ArgumentException("Sub report data source is required");
            }

            // Load the report
            using (ReportDocument report = new ReportDocument())
            {
                // Combine the template path
                var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppConstants.TemplatesFolder, body.FileName);

                // Check if the template file exists
                if (!File.Exists(filePath))
                {
                    // Return a not found, throw an exception
                    throw new FileNotFoundException("Report template not found", filePath);
                }

                // Load the report template
                report.Load(filePath);

                // Set the data source
                report.SetDataSource(body.Enumurable);

                // Set the sub report data source
                foreach (ReportDocument subReport in report.Subreports)
                {
                    if (body.SubReportsDatasource.ContainsKey(subReport.Name))
                    {
                        subReport.SetDataSource(ConvertJsonToDataTable(body.SubReportsDatasource[subReport.Name]));
                    }
                }

                // Export the report to byte array with stream
                using (var stream = report.ExportToStream(ExportFormatType.PortableDocFormat))
                {
                    // Reset the stream position
                    stream.Seek(0, SeekOrigin.Begin);

                    // Read the stream to byte array
                    using (var reader = new BinaryReader(stream))
                    {
                        return reader.ReadBytes((int)stream.Length);
                    }
                }
            }
        }
        public static DataTable ConvertJsonToDataTable(object json)
        {
            return JsonConvert.DeserializeObject<DataTable>(json.ToString());
        }
        public static HttpResponseMessage CreateResponse<T>(RequestBody<T> body, bool download)
        {
            try
            {
                // Create the response message
                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(ExportPdf(body)),
                };

                // Set the content type
                response.Content.Headers.ContentType = new MediaTypeHeaderValue(AppConstants.PdfContentType);

                // Prepare output file name
                var outputFileName = Path.GetFileNameWithoutExtension(body.FileName) + AppConstants.PdfFileExtension;

                // Set the content disposition
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue(download ? AppConstants.AttachmentDisposition : AppConstants.InlineDisposition)
                {
                    Name = outputFileName,
                    FileName = outputFileName,
                };

                // Return the response message
                return response;
            }
            catch (ArgumentException ex)
            {
                // Return a bad request
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(ex.Message),
                };
            }
            catch (FileNotFoundException ex)
            {
                // Return a not found
                return new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(ex.Message),
                };
            }
            catch (Exception ex)
            {
                // Return an internal server error
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.Message),
                };
            }
        }
    }
}