using Hello.Common.Disposable;
using Hello.Data.Interface;
using System;

namespace Hello.Data
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private IDataContext dataContext;

        public IDataContext Get()
        {
            return dataContext ?? (dataContext = new DatabaseContext());
        }

        protected override void DisposeCore()
        {
            base.DisposeCore();
        }
    }
}
