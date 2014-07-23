using System;
using System.Collections.Generic;

namespace Tt.Framework.Models
{
    public class FileInfoDto
    {
        #region Properties

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Customer { get; set; }

        public string Filename { get; set; }

        public Guid Id { get; set; }

        public string LocalFilePath { get; set; }

        public IEnumerable<TransactionDto> Transactions { get; set; }

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
            return Equals((FileInfoDto) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id.GetHashCode();
                hashCode = (hashCode*397) ^ (Customer != null ? Customer.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Filename != null ? Filename.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (LocalFilePath != null ? LocalFilePath.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ CreatedOn.GetHashCode();
                hashCode = (hashCode*397) ^ (CreatedBy != null ? CreatedBy.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Transactions != null ? Transactions.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override string ToString()
        {
            return
                string.Format(
                    "Id: {0}, Customer: {1}, Filename: {2}, LocalFilePath: {3}, CreatedOn: {4}, CreatedBy: {5}, Transactions: {6}",
                    Id, Customer ?? string.Empty, Filename ?? string.Empty, LocalFilePath ?? string.Empty, CreatedOn,
                    CreatedBy ?? string.Empty, Transactions);
        }

        protected bool Equals(FileInfoDto other)
        {
            return string.Equals(Customer, other.Customer) && string.Equals(Filename, other.Filename);
        }

        #endregion Methods
    }
}