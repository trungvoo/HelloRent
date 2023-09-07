using Hello.Common.Parameter;
using Hello.Common.Utils;
using Hello.Core.Interface.Data;
using Hello.Core.Interface.Service;
using Hello.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.Service
{
    public partial class AdvertisingService : BaseService<Advertising>, IAdvertisingService
    {
        public AdvertisingService(IRepository<Advertising> repository) : base(repository) { }
    }

    public partial class AdvertisingService : IAdvertisingService
    {

        public async Task<IEnumerable<Advertising>> ListByStatus(Status status)
        {
            try
            {
                ParamItem[] arr = new ParamItem[] { new ParamItem("Status", SqlDbType.TinyInt, (int)status) };
                return await Task.FromResult(base.SqlQuery("pro_Advertising_GetByStatus", Params.Create(arr)).ToList());
            }
            catch (Exception ex)
            {
                base.WriteError("Error in AdvertisingService at ListActivated() Method", ex.Message);
            }

            return Enumerable.Empty<Advertising>();
        }
    }
}
