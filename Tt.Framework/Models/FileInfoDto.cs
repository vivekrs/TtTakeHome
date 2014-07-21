using System;
using System.Collections.Generic;

namespace Tt.Framework.Models
{
    public class FileInfoDto
    {
        public Guid Id { get; set; }
        public string Customer { get; set; }
        public string Filename { get; set; }
        public string LocalFilePath { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public IEnumerable<TransactionDto> Transactions { get;  set; }
    }
}