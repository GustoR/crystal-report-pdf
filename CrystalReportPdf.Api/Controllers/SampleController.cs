using CrystalReportPdf.Api.Models;
using System.Net.Http;
using System.Web.Http;

namespace CrystalReportPdf.Api.Controllers
{
    [RoutePrefix("api/sample")]
    public class SampleController : ApiController
    {
        [Route("")]
        [HttpPost]
        public HttpResponseMessage SampleReport(RequestBody<SampleItem> body, [FromUri] bool download = false)
        {
            return AppUtils.CreateResponse(body, download);
        }

        [Route("datasource-only")]
        [HttpPost]
        public HttpResponseMessage SampleReportDatasource(SampleItem[] data, [FromUri] bool download = false)
        {
            return AppUtils.CreateResponse(new RequestBody<SampleItem>
            {
                FileName = "SampleReport.rpt",
                Enumurable = data
            }, download);
        }

        [Route("data")]
        [HttpPost]
        public HttpResponseMessage SampleReportDatasource(ExportModel<SampleItem> data, [FromUri] bool download = false)
        {
            return AppUtils.CreateResponse(new RequestBody<SampleItem>
            {
                FileName = "SampleReport.rpt",
                Enumurable = data.Enumurable,
                SubReportsDatasource = data.SubReportsDatasource,
            }, download);
        }
    }
}
