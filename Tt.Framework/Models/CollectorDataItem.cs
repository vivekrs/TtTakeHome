using System;

namespace Tt.Framework.Models
{
    class CollectorDataItem
    {
        public string TransactionKey { get; set; }
        public DateTime TransactionDate { get; set; }
        public int NetworkId { get; set; }
        public int TransactionType { get; set; }
        public string Username { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
    }
}