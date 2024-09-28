namespace CrystalReportPdf.Api.Models
{
    public class SampleItem
    {
        public int ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }
        public decimal UnitPrice { get; set; }
        public byte[] ItemImage { get; set; }
    }
    public class SampleNote
    {
        public int Seq { get; set; }
        public string Note { get; set; }
    }
    public class SamplePaymentTerms
    {
        public int Seq { get; set; }
        public string PaymentTerm { get; set; }
    }
}