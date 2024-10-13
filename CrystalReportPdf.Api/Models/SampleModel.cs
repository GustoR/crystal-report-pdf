using System;
using System.Collections.Generic;

namespace CrystalReportPdf.Api.Models
{
    public class SampleModel
    {
        public int SampleId { get; set; }
        public string SampleNo { get; set; }
        public DateTime SampleDate { get; set; }
        public string SampleSales { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal NetAmount { get; set; }
        public List<SampleItem> Items { get; set; } = new List<SampleItem>();
        public List<SamplePaymentTerm> PaymentTerms { get; set; } = new List<SamplePaymentTerm>();
        public List<SampleNote> Notes { get; set; } = new List<SampleNote>();
    }
    public class SampleItem
    {
        public int SampleId { get; set; }
        public int ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }
        public decimal UnitPrice { get; set; }
        public byte[] ItemImage { get; set; }
    }
    public class SampleNote
    {
        public int SampleId { get; set; }
        public string Note { get; set; }
    }
    public class SamplePaymentTerm
    {
        public int SampleId { get; set; }
        public string PaymentTerm { get; set; }
    }
    public class SampleResult
    {
        public int SampleId { get; set; }
        public string SampleNo { get; set; }
        public DateTime SampleDate { get; set; }
        public string SampleSales { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal NetAmount { get; set; }

        public int ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }
        public decimal UnitPrice { get; set; }
        public byte[] ItemImage { get; set; }
    }
}