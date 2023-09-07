using System;
using System.Data.Entity;

namespace Hello.Data.Interface
{
    public interface IDataContext : IDisposable
    {
        Database GetDbContext();
    }
}
