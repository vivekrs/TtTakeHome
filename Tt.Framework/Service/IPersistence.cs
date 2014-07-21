using System;
using Tt.Framework.Models;

namespace Tt.Framework.Service
{
    public interface IPersistence
    {
        bool ContainsFile(string customer, string filename);
        bool ContainsTransaction(string transactionKey, DateTime transactionDate, int networkId, int exchangeId);

        FileInfoDto GetFile(Guid fileInfoId);

        void AddFile(FileInfoDto fileInfoDto);
        void AddTransaction(Guid fileInfoId, TransactionDto item);
    }
}
