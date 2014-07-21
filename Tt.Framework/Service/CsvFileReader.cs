using System;
using System.Collections.Generic;
using AutoMapper;
using DataAccess;
using Tt.Framework.Models;

namespace Tt.Framework.Service
{
    /// <summary>
    /// Read CSV Files. This will ignore any line that will not conform to the CollectorDataItem syntax
    /// </summary>
    public class CsvFileReader : IFileReader
    {
        static CsvFileReader()
        {
            Mapper.CreateMap<CollectorDataItem, TransactionDto>();
        }

        public IEnumerable<TransactionDto> ReadFile(string filepath)
        {
            var dataTable = DataTable.New.ReadLazy(filepath);
            var dataItems = dataTable.RowsAs<CollectorDataItem>();
            foreach (var item in dataItems)
            {
                var transaction = Mapper.Map<CollectorDataItem, TransactionDto>(item);
                transaction.Id=Guid.NewGuid();
                transaction.CreatedOn = DateTime.Now;
                yield return transaction;
            }
        }
    }
}