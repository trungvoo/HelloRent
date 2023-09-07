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
    public partial class DistrictService : BaseService<District>, IDistrictService
    {
        public DistrictService(IRepository<District> repository) : base(repository) { }
    }

    public partial class DistrictService : IDistrictService
    {

        public async Task<IEnumerable<District>> ListByProvince(int provinceID)
        {
            try
            {
                ParamItem[] arr = new ParamItem[] { new ParamItem("ProvinceID", SqlDbType.Int, provinceID) };
                return await Task.FromResult(base.SqlQuery("pro_District_ListByProvince", Params.Create(arr)).ToList());
            }
            catch (Exception ex)
            {
                base.WriteError("Error in DistrictService at ListByProvince() Method", ex.Message);
            }
            return Enumerable.Empty<District>();
        }
    }
}
