using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Tt.Framework.Models;
using Tt.Framework.Service;

namespace Tt.Tests.Service
{
    [TestFixture]
    public class CsvFileReaderTests
    {
        [SetUp]
        public void Init()
        {
            File.WriteAllLines(Filename, Lines);
        }

        [TearDown]
        public void Cleanup()
        {
            File.Delete(Filename);
        }

        public static readonly string[] Lines =
        {
            "TransactionKey,TransactionDate,NetworkId,ExchangeId,TransactionType,Username,Product,Quantity",
            "000CH5,04/10/2014 05:17:50.04,555,37,21825,Ronnie,CAM,87",
            "000KVB92JM1MIW3WYV8,04/10/2014 09:50:02.69,555,34,89181,Brooke,ZB,60"
        };

        public static readonly List<TransactionDto> DataItems = new List<TransactionDto>
        {
            new TransactionDto
            {
                TransactionKey = "000CH5",
                TransactionDate = DateTime.Parse("04/10/2014 05:17:50.04"),
                NetworkId = 555,
                ExchangeId = 37,
                TransactionType = 21825,
                Username = "Ronnie",
                Product = "CAM",
                Quantity = 87
            },
            new TransactionDto
            {
                TransactionKey = "000KVB92JM1MIW3WYV8",
                TransactionDate = DateTime.Parse("04/10/2014 09:50:02.69"),
                NetworkId = 555,
                ExchangeId = 34,
                TransactionType = 89181,
                Username = "Brooke",
                Product = "ZB",
                Quantity = 60
            }
        };

        private const string Filename = "test.csv";

        [Test]
        public void ReadFileTest()
        {
            var items = new CsvFileReader().ReadFile(Filename);
            foreach (var item in DataItems.Zip(items, (d, i) => new {Data = d, Item = i}))
                Assert.AreEqual(item.Data, item.Item);
        }
    }
}