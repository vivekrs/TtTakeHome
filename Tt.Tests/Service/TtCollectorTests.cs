using System;
using System.IO;
using Moq;
using NUnit.Framework;
using Tt.Framework.Models;
using Tt.Framework.Service;

namespace Tt.Tests.Service
{
    [TestFixture]
    public class TtCollectorTests
    {
        private Mock<IPersistence> _persistence;
        private Mock<IFileReader> _fileReader;
        private Mock<TtCollector> _collector;

        [SetUp]
        public void TtCollectorTest()
        {
            File.WriteAllLines(File2, CsvFileReaderTests.Lines);
            Directory.CreateDirectory(Folder);

            _persistence = new Mock<IPersistence>();
            _persistence.Setup(m => m.ContainsFile(Customer, File1)).Returns(true);
            _persistence.Setup(m => m.ContainsFile(Customer, File2)).Returns(false);
            _persistence.Setup(m => m.GetFile(Guid.Empty)).Returns(new FileInfoDto {LocalFilePath = File2});

            _fileReader = new Mock<IFileReader>();
            _fileReader.Setup(m => m.ReadFile(File2)).Returns(CsvFileReaderTests.DataItems);

            _collector = new Mock<TtCollector>(_persistence.Object, _fileReader.Object, Folder);
        }

        public string Customer = "TT";
        public string Username = "Vivek";
        public string Folder = "TestFolder";
        public string File1 = "test1.csv";
        public string File2 = "test2.csv";

        [Test]
        public void AddCollectorFileTest()
        {
            Assert.IsFalse(_collector.Object.AddCollectorFile(Customer, File1, Username, File1));
            Assert.IsTrue(_collector.Object.AddCollectorFile(Customer, File2, Username, File2));
        }

        [Test]
        public void GetNextUnprocessedFileTest()
        {
            Assert.AreEqual(_collector.Object.GetNextUnprocessedFile(), Guid.Empty);
            _persistence.Verify(m => m.GetUnprocessedFileFromDb(), Times.Once());
        }

        [Test]
        public void ProcessFileTest()
        {
            _collector.Object.ProcessFile(Guid.Empty);
            _persistence.Verify(m => m.GetFile(Guid.Empty), Times.Once());
            _persistence.Verify(m => m.AddTransaction(Guid.Empty, CsvFileReaderTests.DataItems[0]), Times.Once());
            _persistence.Verify(m => m.AddTransaction(Guid.Empty, CsvFileReaderTests.DataItems[1]), Times.Once());
            _persistence.Verify(m => m.UpdateProcessedOnForFile(Guid.Empty), Times.Once());
        }
    }
}
