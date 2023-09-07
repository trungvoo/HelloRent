using Hello.Core.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hello.Core.Interface.Service
{
    public partial interface IWarningService : IBaseService<Warning>
    {
        Task<long> Insert(Warning model);
    }
}
