using Hello.Common.Utils;
using System;

namespace Hello.Core.Model
{
    public partial class District
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int ProvinceID { get; set; }
        public Status Status { get; set; }
    }
}
