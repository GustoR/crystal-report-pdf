using CrystalReportPdf.Api.Models;
using System;
using System.Collections.Generic;

namespace CrystalReportPdf.Tests.MockData
{
    public class SampleMockData
    {
        public static List<SampleModel> Samples() => new List<SampleModel>()
        {
            new SampleModel()
            {
                SampleId = 1,
                SampleNo = "SAMPLE-001",
                SampleDate = DateTime.Today,
                Items = new List<SampleItem>()
                {
                    new SampleItem()
                    {
                        SampleId = 1,
                        ItemId = 1,
                        ItemCode = "ITEM-001",
                        ItemName = "Item 1",
                        ItemType = "Product",
                        UnitPrice = 1000,
                    },
                    new SampleItem()
                    {
                        SampleId = 1,
                        ItemId = 2,
                        ItemCode = "ITEM-002",
                        ItemName = "Item 2",
                        ItemType = "Service",
                        UnitPrice = 500,
                    },
                },
                PaymentTerms = new List<SamplePaymentTerm>()
                {
                    new SamplePaymentTerm()
                    {
                        SampleId = 1,
                        PaymentTerm = "Cash on Delivery",
                    },
                    new SamplePaymentTerm()
                    {
                        SampleId = 1,
                        PaymentTerm = "Net 30 Days",
                    },
                },
                Notes = new List<SampleNote>()
                {
                    new SampleNote()
                    {
                        SampleId = 1,
                        Note = "Sample note 1",
                    },
                    new SampleNote()
                    {
                        SampleId = 1,
                        Note = "Sample note 2",
                    },
                },
            },
            new SampleModel()
            {
                SampleId = 2,
                SampleNo = "SAMPLE-002",
                SampleDate = DateTime.Today.AddDays(1),
                Items = new List<SampleItem>()
                {
                    new SampleItem()
                    {
                        SampleId = 2,
                        ItemId = 3,
                        ItemCode = "ITEM-003",
                        ItemName = "Item 3",
                        ItemType = "Product",
                        UnitPrice = 2000,
                    },
                    new SampleItem()
                    {
                        SampleId = 2,
                        ItemId = 4,
                        ItemCode = "ITEM-004",
                        ItemName = "Item 4",
                        ItemType = "Service",
                        UnitPrice = 1000,
                    },
                },
                PaymentTerms = new List<SamplePaymentTerm>()
                {
                    new SamplePaymentTerm()
                    {
                        SampleId = 2,
                        PaymentTerm = "Cash on Delivery",
                    },
                    new SamplePaymentTerm()
                    {
                        SampleId = 2,
                        PaymentTerm = "Net 30 Days",
                    },
                },
                Notes = new List<SampleNote>()
                {
                    new SampleNote()
                    {
                        SampleId = 2,
                        Note = "Sample note 3",
                    },
                    new SampleNote()
                    {
                        SampleId = 2,
                        Note = "Sample note 4",
                    },
                },
            },
        };
    }
}
