using System;

namespace Tt.Framework.Service
{
    public interface ICollector
    {
        bool AddCollectorFile(string customer, string filename, string username, string localFilePath);
        void ProcessFile(Guid fileInfoId);
    }
}