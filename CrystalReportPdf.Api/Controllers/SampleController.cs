using CrystalReportPdf.Api.Models;
using System.Net.Http;
using System.Web.Http;

namespace CrystalReportPdf.Api.Controllers
{

    public class SampleController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage SampleReport(RequestBody<SampleItem> body, [FromUri] bool download = false)
        {
            return AppUtils.CreateResponse(body, download);
        }
    }
}
