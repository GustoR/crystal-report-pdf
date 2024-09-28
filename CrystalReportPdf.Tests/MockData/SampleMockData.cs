using CrystalReportPdf.Api.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CrystalReportPdf.Tests.MockData
{
    public class SampleMockData
    {
        public static IEnumerable<SampleItem> GetSampleItems()
        {
            return new List<SampleItem>
            {
                new SampleItem
                {
                    ItemId = 1,
                    ItemCode = "A001",
                    ItemName = "Item 1",
                    ItemType = "Type 1",
                    UnitPrice = 100.00m,
                    ItemImage = null,
                },
                new SampleItem
                {
                    ItemId = 2,
                    ItemCode = "A002",
                    ItemName = "Item 2",
                    ItemType = "Type 2",
                    UnitPrice = 200.00m,
                    ItemImage = null,
                },
                new SampleItem
                {
                    ItemId = 3,
                    ItemCode = "A003",
                    ItemName = "Item 3",
                    ItemType = "Type 3",
                    UnitPrice = 300.00m,
                    ItemImage = null,
                }
            };
        }
        public static Dictionary<string, object> GetSampleSubReportsDatasource()
        {
            return new Dictionary<string, object>
            {
                {
                    "SampleNoteReport",
                    JsonConvert.SerializeObject(new List<SampleNote>
                    {
                        new SampleNote { Seq = 1, Note = "Note 1" },
                        new SampleNote { Seq = 2, Note = "Note 2" },
                    })
                },
                {
                    "SamplePaymentTermseReport",
                    JsonConvert.SerializeObject(new List<SamplePaymentTerms>
                    {
                        new SamplePaymentTerms { Seq = 1, PaymentTerm = "Term 1" },
                        new SamplePaymentTerms { Seq = 2, PaymentTerm = "Term 2" },
                    })
                }
            };
        }
    }
}
