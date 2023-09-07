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
    public partial class DeviceService : BaseService<Device>, IDeviceService
    {
        public DeviceService(IRepository<Device> repository) : base(repository) { }
    }

    public partial class DeviceService : IDeviceService
    {

        public async Task<long> Insert(string token, long accountID, DeviceType type, DateTime date, string iPAddress, string version, string address, decimal latitude, decimal logitude)
        {
            try
            {
                ParamItem[] arr = new ParamItem[] { new ParamItem("Token", SqlDbType.VarChar, token),
                                                    new ParamItem("AccountID", SqlDbType.BigInt, accountID),
                                                    new ParamItem("Type", SqlDbType.TinyInt, (int)type),
                                                    new ParamItem("Date", SqlDbType.DateTime, date),
                                                    new ParamItem("IPAddress", SqlDbType.VarChar, iPAddress),
                                                    new ParamItem("Version", SqlDbType.VarChar, version),
                                                    new ParamItem("Address", SqlDbType.NVarChar, address),
                                                    new ParamItem("Latitude", SqlDbType.Decimal, latitude),
                                                    new ParamItem("Longitude", SqlDbType.Decimal, logitude)};

                var result = await Task.FromResult(SqlQuery("pro_Device_Insert", Params.Create(arr)).SingleOrDefault());

                return result != null ? result.Id : -1;
            }
            catch (Exception ex)
            {
                base.WriteError("Error in DeviceService at Insert() Method", ex.Message);
            }

            return -1;
        }
    }
}
