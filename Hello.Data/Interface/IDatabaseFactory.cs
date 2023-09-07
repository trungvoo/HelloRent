using System;

namespace Hello.Data.Interface
{
    public interface IDatabaseFactory : IDisposable
    {
        IDataContext Get();
    }
}
