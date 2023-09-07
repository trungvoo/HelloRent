﻿using Hello.Core.Interface.Data;
using Hello.Core.Interface.Service;
using Hello.Core.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Hello.Service
{
    public abstract class BaseService<T> : IBaseService<T> where T : class
    {
        protected readonly IRepository<T> Repository;

        protected BaseService(IRepository<T> repository)
        {
            this.Repository = repository;
        }

        public IEnumerable<T> SqlQuery(string procName)
        {
            return Repository.SqlQuery(procName);
        }

        public IEnumerable<T> SqlQuery(string procName, SqlParameter[] sqlParams)
        {
            return Repository.SqlQuery(procName, sqlParams);
        }

        public long ExecuteSql(string procName, SqlParameter[] sqlParams)
        {
            return Repository.ExecuteSql(procName, sqlParams);
        }

        public void WriteError(string title, string content)
        {
            Repository.WriteError(title, content);
        }
    }
}
