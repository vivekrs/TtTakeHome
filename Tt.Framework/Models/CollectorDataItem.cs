using System;

namespace Tt.Framework.Models
{
    public class CollectorDataItem
    {
        #region Properties

        public int ExchangeId { get; set; }

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
            return Equals((CollectorDataItem) obj);
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
                    "TransactionKey: {0}, TransactionDate: {1}, NetworkId: {2}, ExchangeId: {3}, TransactionType: {4}, Username: {5}, Product: {6}, Quantity: {7}",
                    TransactionKey ?? string.Empty, TransactionDate, NetworkId, ExchangeId, TransactionType,
                    Username ?? string.Empty, Product ?? string.Empty, Quantity);
        }

        protected bool Equals(CollectorDataItem other)
        {
            return string.Equals(TransactionKey, other.TransactionKey) && TransactionDate.Equals(other.TransactionDate) &&
                   NetworkId == other.NetworkId && ExchangeId == other.ExchangeId &&
                   TransactionType == other.TransactionType && string.Equals(Username, other.Username) &&
                   string.Equals(Product, other.Product) && Quantity == other.Quantity;
        }

        #endregion Methods
    }
}