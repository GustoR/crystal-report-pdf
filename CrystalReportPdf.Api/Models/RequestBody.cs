using System.Collections.Generic;

namespace CrystalReportPdf.Api.Models
{
    public class RequestBody<T>
    {
        /// <summary>
        /// Crystal Report template file name with extension
        /// </summary>
        public string FileName { get; set; }
        public IEnumerable<T> Enumurable { get; set; } = new List<T>();
        public Dictionary<string, object> SubReportsDatasource { get; set; } = new Dictionary<string, object>();
    }
}