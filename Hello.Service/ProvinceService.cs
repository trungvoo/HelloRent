using Hello.Common.Parameter;
using Hello.Core.Interface.Data;
using Hello.Core.Interface.Service;
using Hello.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hello.Service
{
    public partial class ProvinceService : BaseService<Province>, IProvinceService
    {
        public ProvinceService(IRepository<Province> repository) : base(repository) { }
    }

    public partial class ProvinceService : IProvinceService
    {

        public async Task<IEnumerable<Province>> ListByCountry(int countryID)
        {
            try
            {
                ParamItem[] arr = new ParamItem[] { new ParamItem("CountryID", SqlDbType.Int, countryID) };
                return await Task.FromResult(base.SqlQuery("pro_Province_ListByCountry", Params.Create(arr)).ToList());
            }
            catch (Exception ex)
            {
                base.WriteError("Error in ProvinceService at ListByCountry() Method", ex.Message);
            }

            return Enumerable.Empty<Province>();
        }
    }
}
