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
        /// <summary>
        /// Initialize AutoMapper mappings (once statically)
        /// </summary>
        static CsvFileReader()
        {
            Mapper.CreateMap<CollectorDataItem, TransactionDto>();
        }

        /// <summary>
        /// Read input file. This will not read the entire file into memory.
        /// Instead, the rows are read, mapped and yielded as requested by the consumer
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public IEnumerable<TransactionDto> ReadFile(string filepath)
        {
            var dataTable = DataTable.New.ReadLazy(filepath);
            var dataItems = dataTable.RowsAs<CollectorDataItem>();

            foreach (var item in dataItems)
            {
                var transaction = Mapper.Map<CollectorDataItem, TransactionDto>(item);

                //These two fields may (or may not) be initialized in the constructor of the object
                transaction.Id=Guid.NewGuid();
                transaction.CreatedOn = DateTime.Now;

                yield return transaction;
            }
        }
    }
}