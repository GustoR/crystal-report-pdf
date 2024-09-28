using System.Collections.Generic;

namespace CrystalReportPdf.Api.Models
{
    public class ExportModel<T>
    {
        public IEnumerable<T> Enumurable { get; set; } = new List<T>();
        public Dictionary<string, object> SubReportsDatasource { get; set; } = new Dictionary<string, object>();
    }
}