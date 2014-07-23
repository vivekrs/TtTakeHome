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

        /// <summary>
        ///     Initialize AutoMapper mappings (once statically)
        /// </summary>
        static SqlPersistence()
        {
            Mapper.CreateMap<FileInfo, FileInfoDto>();
            Mapper.CreateMap<FileInfoDto, FileInfo>();
            Mapper.CreateMap<Transaction, TransactionDto>();
            Mapper.CreateMap<TransactionDto, Transaction>();
        }

        /// <summary>
        ///     Init
        /// </summary>
        /// <param name="connectionString"></param>
        [Inject]
        public SqlPersistence(string connectionString)
        {
            _cs = connectionString;
        }

        /// <summary>
        ///     Check if the File has already been submitted into the database
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool ContainsFile(string customer, string filename)
        {
            using (var db = new PersistenceDataContext(_cs))
            {
                return db.FileInfos.Any(fi => fi.Customer == customer && fi.Filename == filename);
            }
        }

        /// <summary>
        ///     Add a new transaction file (info) to the database
        /// </summary>
        /// <param name="file"></param>
        public void AddFile(FileInfoDto file)
        {
            using (var db = new PersistenceDataContext(_cs))
            {
                db.FileInfos.InsertOnSubmit(Mapper.Map<FileInfoDto, FileInfo>(file));
                db.SubmitChanges();
            }
        }

        /// <summary>
        ///     Get File Info by ID
        /// </summary>
        /// <param name="fileInfoId"></param>
        /// <returns></returns>
        public FileInfoDto GetFile(Guid fileInfoId)
        {
            using (var db = new PersistenceDataContext(_cs))
            {
                var fileInfo = db.FileInfos.FirstOrDefault(fi => fi.Id == fileInfoId);
                return Mapper.Map<FileInfo, FileInfoDto>(fileInfo);
            }
        }

        /// <summary>
        ///     Add a new Transaction to the database
        /// </summary>
        /// <param name="fileInfoId"></param>
        /// <param name="transactionDto"></param>
        public void AddTransaction(Guid fileInfoId, TransactionDto transactionDto)
        {
            using (var db = new PersistenceDataContext(_cs))
            {
                //First, check if the transaction already exists
                var tId = db.Transactions.Where(
                    tr =>
                        tr.TransactionKey == transactionDto.TransactionKey &&
                        tr.TransactionDate == transactionDto.TransactionDate &&
                        tr.NetworkId == transactionDto.NetworkId && tr.ExchangeId == transactionDto.ExchangeId)
                    .Select(tr => tr.Id)
                    .FirstOrDefault();

                if (tId == Guid.Empty)
                {
                    //If not found, insert into database
                    db.Transactions.InsertOnSubmit(Mapper.Map<TransactionDto, Transaction>(transactionDto));
                    tId = transactionDto.Id;
                }

                //Check if the file-transaction exists
                if (!db.FileTransactions.Any(ft => ft.FileId == fileInfoId && ft.TransactionId == tId))
                {
                    //If not found, insert into database
                    db.FileTransactions.InsertOnSubmit(new FileTransaction
                    {
                        Id = Guid.NewGuid(),
                        FileId = fileInfoId,
                        TransactionId = tId
                    });
                }

                db.SubmitChanges();
            }
        }

        /// <summary>
        ///     Update ProcessedOn DateTime for File
        /// </summary>
        /// <param name="fileInfoId"></param>
        public void UpdateProcessedOnForFile(Guid fileInfoId)
        {
            using (var db = new PersistenceDataContext(_cs))
            {
                var fileInfo = db.FileInfos.FirstOrDefault(fi => fi.Id == fileInfoId);
                if (fileInfo == null)
                    return;

                fileInfo.ProcessedOn = DateTime.Now;
                db.SubmitChanges();
            }
        }

        /// <summary>
        ///     Get the next unprocessed transaction file (in the order it was received)
        /// </summary>
        /// <returns>Guid.Empty if there are no unprocessed files</returns>
        public Guid GetUnprocessedFileFromDb()
        {
            using (var db = new PersistenceDataContext(_cs))
            {
                return
                    db.FileInfos.Where(fi => fi.ProcessedOn == null)
                        .OrderBy(fi => fi.CreatedOn)
                        .Select(fi => fi.Id)
                        .FirstOrDefault();
            }
        }
    }
}