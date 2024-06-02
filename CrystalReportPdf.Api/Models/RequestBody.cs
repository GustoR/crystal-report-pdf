using System.Collections;

namespace CrystalReportPdf.Api.Models
{
    public class RequestBody
    {
        public string TemplatePath { get; set; }
        public string FileName { get; set; }
        public IEnumerable Enumurable { get; set; }
    }
}