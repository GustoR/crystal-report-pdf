namespace CrystalReportPdf.Api.Models
{
    public class SampleModel
    {
        public int ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }
        public decimal UnitPrice { get; set; }
        public byte[] ItemImage { get; set; }
    }
}