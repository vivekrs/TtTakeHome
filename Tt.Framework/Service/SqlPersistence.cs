using System;
using System.Linq;
using AutoMapper;
using Ninject;
using Tt.Framework.Data;
using Tt.Framework.Models;

namespace Tt.Framework.Service
{
    public class SqlPersistence : IPersistence
    {
        private readonly string _cs;

        [Inject]
        public SqlPersistence(string connectionString)
        {
            _cs = connectionString;
        }
        
        static SqlPersistence()
        {
            Mapper.CreateMap<FileInfo, FileInfoDto>();
            Mapper.CreateMap<FileInfoDto, FileInfo>();
            Mapper.CreateMap<Transaction, TransactionDto>();
            Mapper.CreateMap<TransactionDto, Transaction>();
        }

        public bool ContainsFile(string customer, string filename)
        {
            using (var db = new PersistenceDataContext(_cs))
            {
                return db.FileInfos.Any(fi => fi.Customer == customer && fi.Filename == filename);
            }
        }

        public bool ContainsTransaction(string transactionKey, DateTime transactionDate, int networkId, int exchangeId)
        {
            using (var db = new PersistenceDataContext(_cs))
            {
                return
                    db.Transactions.Any(
                        tr =>
                            tr.TransactionKey == transactionKey && tr.TransactionDate == transactionDate &&
                            tr.NetworkId == networkId && tr.ExchangeId == exchangeId);
            }
        }

        public void AddFile(FileInfoDto file)
        {
            using (var db = new PersistenceDataContext(_cs))
            {
                db.FileInfos.InsertOnSubmit(Mapper.Map<FileInfoDto, FileInfo>(file));
                db.SubmitChanges();
            }
        }

        public FileInfoDto GetFile(Guid fileInfoId)
        {
            using (var db = new PersistenceDataContext(_cs))
            {
                var fileInfo = db.FileInfos.FirstOrDefault(fi => fi.Id == fileInfoId);
                return Mapper.Map<FileInfo, FileInfoDto>(fileInfo);
            }
        }

        public void AddTransaction(Guid fileInfoId, TransactionDto transaction)
        {
            using (var db = new PersistenceDataContext(_cs))
            {
                db.Transactions.InsertOnSubmit(Mapper.Map<TransactionDto, Transaction>(transaction));
                db.FileTransactions.InsertOnSubmit(new FileTransaction
                {
                    Id=Guid.NewGuid(),
                    FileId = fileInfoId,
                    TransactionId = transaction.Id
                });
                db.SubmitChanges();
            }
        }
    }
}