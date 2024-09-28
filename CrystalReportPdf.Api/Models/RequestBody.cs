namespace CrystalReportPdf.Api.Models
{
    public class RequestBody<T> : ExportModel<T>
    {
        /// <summary>
        /// Crystal Report template file name with extension
        /// </summary>
        public string FileName { get; set; }
    }
}