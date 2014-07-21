using System;
using Tt.Framework.Models;

namespace Tt.Framework.Service
{
    public interface IPersistence
    {
        bool ContainsFile(string customer, string filename);
        FileInfoDto GetFile(Guid fileInfoId);
        Guid GetNextUnprocessedFile();
        void AddFile(FileInfoDto fileInfoDto);

        void AddTransaction(Guid fileInfoId, TransactionDto item);
    }
}
