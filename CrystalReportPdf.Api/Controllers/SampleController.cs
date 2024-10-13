using CrystalReportPdf.Api.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CrystalReportPdf.Api.Controllers
{
    [RoutePrefix("api/sample")]
    public class SampleController : ApiController
    {
        [Route("")]
        [HttpPost]
        public HttpResponseMessage SampleReport(SampleModel[] models, [FromUri] bool download = false)
        {
            try
            {
                if (models == null) throw new ArgumentException("Datasource is required");

                var report = AppUtils.GetReport("Sample\\SampleReport.rpt");
                report.SetDataSource(
                    new Dictionary<string, IEnumerable>
                    {
                        { "SampleModel", models.ToList() },
                        { "SampleItem", models.SelectMany(model => model.Items) },
                        { "SamplePaymentTerm", models.SelectMany(model => model.PaymentTerms) },
                        { "SampleNote", models.SelectMany(model => model.Notes) },
                    },
                    new Dictionary<string, IEnumerable>
                    {
                        { "SampleNoteReport", models.SelectMany(model => model.Notes) },
                    });
                return report.GeneratePdfResponse("SampleReport", download);
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
