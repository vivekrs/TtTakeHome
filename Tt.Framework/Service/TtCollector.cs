using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using AutoMapper;
using Ninject;
using Tt.Framework.Models;

namespace Tt.Framework.Service
{
    public class TtCollector : ICollector
    {
        private readonly IPersistence _persistence;
        private readonly IFileReader _fileReader;
        private readonly string _basePath;

        /// <summary>
        /// Creates a new instance of the TtCollector service
        /// </summary>
        /// <param name="persistence"></param>
        /// <param name="fileReader"></param>
        /// <param name="collectorBasePath"></param>
        [Inject]
        public TtCollector(IPersistence persistence, IFileReader fileReader, string collectorBasePath)
        {
            _persistence = persistence;
            _fileReader = fileReader;
            _basePath = collectorBasePath;
        }

        /// <summary>
        /// Add a new file submitted (usually from the web) to the Collector repository and record it in the database
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="filename"></param>
        /// <param name="username"></param>
        /// <param name="localFilePath"></param>
        /// <returns></returns>
        public bool AddCollectorFile(string customer, string filename, string username, string localFilePath)
        {
            if (_persistence.ContainsFile(customer, filename))
            {
                Trace.WriteLine(string.Format("File {0} already added/processed.", localFilePath));
                return false;
            }

            var newGuid = Guid.NewGuid();
            var newFilePath = Path.Combine(_basePath, newGuid+new FileInfo(filename).Extension);
            File.Move(localFilePath, newFilePath);

            _persistence.AddFile(new FileInfoDto
            {
                Id = newGuid,
                Customer = customer,
                Filename = filename,
                LocalFilePath = newFilePath,
                CreatedOn = DateTime.Now,
                CreatedBy = username
            });
            Trace.WriteLine(string.Format("File {0} saved for processing.", localFilePath));
            return true;
        }

        /// <summary>
        /// Get the next unprocessed transaction file from the persistence service
        /// </summary>
        /// <returns>The Guid of the File Info associated with the unprocessed file. Guid.Empty if not found</returns>
        public Guid GetNextUnprocessedFile()
        {
            return _persistence.GetNextUnprocessedFile();
        }

        /// <summary>
        /// Process transactions in a collector file and upsert into the database
        /// </summary>
        /// <param name="fileInfoId"></param>
        public void ProcessFile(Guid fileInfoId)
        {
            var fileInfo = _persistence.GetFile(fileInfoId);

            //Here, we could inject different File Reader objects depending on the file type
            foreach (var item in _fileReader.ReadFile(fileInfo.LocalFilePath))
                _persistence.AddTransaction(fileInfoId, item);

            Trace.WriteLine(string.Format("File {0} processed.", fileInfo.LocalFilePath));
        }
    }
}
