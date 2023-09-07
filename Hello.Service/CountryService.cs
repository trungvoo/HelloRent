using Hello.Core.Interface.Data;
using Hello.Core.Interface.Service;
using Hello.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.Service
{
    public partial class CountryService : BaseService<Country>, ICountryService
    {
        public CountryService(IRepository<Country> repository) : base(repository) { }
    }

    public partial class CountryService : ICountryService
    {

        public async Task<IEnumerable<Country>> GetAll()
        {
            try
            {
                return await Task.FromResult(base.SqlQuery("pro_Country_GetAll").ToList());
            }
            catch (Exception ex)
            {
                base.WriteError("Error in CountryService at GetAll() Method", ex.Message);
            }
            return Enumerable.Empty<Country>();
        }
    }
}
