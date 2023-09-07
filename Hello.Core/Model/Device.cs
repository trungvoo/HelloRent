using Hello.Common.Utils;
using System;

namespace Hello.Core.Model
{
    public partial class Device
    {
        public long Id { get; set; }
        public string Token { get; set; }
        public DeviceType DeviceType { get; set; }
        public DateTime RegisteredDate { get; set; }
    }
}
