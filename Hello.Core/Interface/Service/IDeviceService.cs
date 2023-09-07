using Hello.Common.Utils;
using Hello.Core.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hello.Core.Interface.Service
{
    public partial interface IDeviceService : IBaseService<Device>
    {
        Task<long> Insert(string token, long accountID, DeviceType type, DateTime date, string iPAddress, string version, string address, decimal latitude, decimal logitude);
    }
}
