using System;
using System.Collections.Generic;

namespace Tt.Framework.Models
{
    public class TransactionDto
    {
        #region Properties

        public DateTime CreatedOn { get; set; }

        public int ExchangeId { get; set; }

        public IEnumerable<FileInfoDto> FileInfoDtos { get; set; }

        public Guid Id { get; set; }

        public int NetworkId { get; set; }

        public string Product { get; set; }

        public int Quantity { get; set; }

        public DateTime TransactionDate { get; set; }

        public string TransactionKey { get; set; }

        public int TransactionType { get; set; }

        public string Username { get; set; }

        #endregion Properties

        #region Methods

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            return Equals((TransactionDto) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (TransactionKey != null ? TransactionKey.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ TransactionDate.GetHashCode();
                hashCode = (hashCode*397) ^ NetworkId;
                hashCode = (hashCode*397) ^ ExchangeId;
                hashCode = (hashCode*397) ^ TransactionType;
                hashCode = (hashCode*397) ^ (Username != null ? Username.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Product != null ? Product.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ Quantity;
                return hashCode;
            }
        }

        public override string ToString()
        {
            return
                string.Format(
                    "Id: {0}, TransactionKey: {1}, TransactionDate: {2}, NetworkId: {3}, ExchangeId: {4}, TransactionType: {5}, Username: {6}, Product: {7}, Quantity: {8}, CreatedOn: {9}, FileInfoDtos: {10}",
                    Id, TransactionKey ?? string.Empty, TransactionDate, NetworkId, ExchangeId,
                    TransactionType, Username ?? string.Empty, Product ?? string.Empty,
                    Quantity, CreatedOn, FileInfoDtos);
        }

        protected bool Equals(TransactionDto other)
        {
            return string.Equals(TransactionKey, other.TransactionKey) && TransactionDate.Equals(other.TransactionDate) &&
                   NetworkId == other.NetworkId && ExchangeId == other.ExchangeId &&
                   TransactionType == other.TransactionType && string.Equals(Username, other.Username) &&
                   string.Equals(Product, other.Product) && Quantity == other.Quantity;
        }

        #endregion Methods
    }
}