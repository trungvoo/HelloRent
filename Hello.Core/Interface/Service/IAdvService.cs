using Hello.Common.Utils;
using Hello.Core.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hello.Core.Interface.Service
{
    public partial interface IAdvService : IBaseService<Advertising>
    {
        Task<IEnumerable<Advertising>> ListByLevel(AdvLevel level, Status status, int beginRow, int numRows);
    }
}
