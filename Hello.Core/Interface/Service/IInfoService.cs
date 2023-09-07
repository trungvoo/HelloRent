using Hello.Common.Utils;
using Hello.Core.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hello.Core.Interface.Service
{
    public partial interface IInfoService : IBaseService<Information>
    {
        Task<Information> GetInfoByType(InfoType type);

    }
}
