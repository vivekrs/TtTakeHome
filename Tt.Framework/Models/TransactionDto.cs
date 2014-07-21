using System.Collections.Generic;

namespace Tt.Framework.Models
{
    public class TransactionDto
    {
        public System.Guid Id { get; set; }
        public string TransactionKey { get; set; }
        public System.DateTime TransactionDate { get; set; }
        public int NetworkId { get; set; }
        public int ExchangeId { get; set; }
        public int TransactionType { get; set; }
        public string Username { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public IEnumerable<FileInfoDto> FileInfoDtos { get; set; }
    }
}
