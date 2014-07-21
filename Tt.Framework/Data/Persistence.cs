using System.Collections.Generic;
using System.Linq;

namespace Tt.Framework.Data
{
    partial class PersistenceDataContext
    {
    }

    partial class FileInfo
    {
        public IEnumerable<Transaction> Transactions
        { get { return FileTransactions.Select(ft => ft.Transaction); } }
    }

    partial class Transaction
    {
        public IEnumerable<FileInfo> FileInfos
        { get { return FileTransactions.Select(ft => ft.FileInfo); } }
    }
}
