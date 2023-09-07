using Hello.Core.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hello.Core.Interface.Service
{
    public partial interface IDistrictService : IBaseService<District>
    {
        Task<IEnumerable<District>> ListByProvince(int provinceID);
    }
}
