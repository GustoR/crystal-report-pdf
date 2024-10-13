using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;

namespace CrystalReportPdf.Api
{
    public static class AppExtensions
    {
        public static byte[] GetBytes(this ReportDocument report)
        {
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
        public static HttpResponseMessage CreateResponse(this byte[] bytes, string fileName, bool download)
        {
            // Create the response message
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(bytes),
            };

            // Set the content type
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(AppConstants.PdfContentType);

            // Prepare output file name
            var outputFileName = Path.GetFileNameWithoutExtension(fileName) + AppConstants.PdfFileExtension;

            // Set the content disposition
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue(download ? AppConstants.AttachmentDisposition : AppConstants.InlineDisposition)
            {
                Name = outputFileName,
                FileName = outputFileName,
            };

            // Return the response message
            return response;
        }
        public static HttpResponseMessage GeneratePdfResponse(this ReportDocument report, string fileName, bool download)
        {
            return report.GetBytes().CreateResponse(fileName, download);
        }
        public static void SetDataSource(this ReportDocument report, Dictionary<string, IEnumerable> dataSources, Dictionary<string, IEnumerable> subDataSources = default)
        {
            foreach (Table table in report.Database.Tables)
            {
                if (dataSources.ContainsKey(table.Name))
                {
                    table.SetDataSource(dataSources[table.Name]);
                }
            }

            if (subDataSources.Count > 0 && report.Subreports.Count > 0)
            {
                foreach (ReportDocument subReport in report.Subreports)
                {
                    if (subDataSources.ContainsKey(subReport.Name))
                    {
                        subReport.SetDataSource(subDataSources[subReport.Name]);
                    }
                }
            }
        }
    }
}