using Hello.Common.Utils;
using System;

namespace Hello.Core.Model
{
    public partial class Marker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EName { get; set; }
        public string KName { get; set; }
        public string Address { get; set; }
        public string EAddress { get; set; }
        public int CountryID { get; set; }
        public int ProvinceID { get; set; }
        public int DistrictID { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int FloorCount { get; set; }
        public string Thumbnail { get; set; }
        public Status Status { get; set; }
    }
}
