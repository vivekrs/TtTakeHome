using System.Collections.Generic;
using Tt.Framework.Models;

namespace Tt.Framework.Service
{
    public interface IFileReader
    {
        IEnumerable<TransactionDto> ReadFile(string filepath);
    }
}
