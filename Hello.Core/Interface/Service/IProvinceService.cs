using Hello.Core.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hello.Core.Interface.Service
{
    public partial interface IProvinceService : IBaseService<Province>
    {
        Task<IEnumerable<Province>> ListByCountry(int countryID);
    }
}
