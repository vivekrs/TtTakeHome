using System;

namespace Tt.Framework.Service
{
    public interface ICollector
    {
        bool AddCollectorFile(string customer, string filename, string username, string localFilePath);
        Guid GetNextUnprocessedFile();
        void ProcessFile(Guid fileInfoId);
    }
}