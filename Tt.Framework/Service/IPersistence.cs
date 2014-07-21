using System;
using Tt.Framework.Models;

namespace Tt.Framework.Service
{
    public interface IPersistence
    {
        bool ContainsFile(string customer, string filename);
        FileInfoDto GetFile(Guid fileInfoId);
        Guid GetUnprocessedFileFromDb();
        void AddFile(FileInfoDto fileInfoDto);

        void AddTransaction(Guid fileInfoId, TransactionDto item);
        void UpdateProcessedOnForFile(Guid fileInfoId);
    }
}
