using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hello.WebUI.Areas.WebAPI.Models
{
    public abstract class LocationInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CountryInfo : LocationInfo
    {
        public string Code { get; set; }
        public string Thumbnail { get; set; }
    }

    public class ProvinceInfo : LocationInfo
    {
        public string PostalCode { get; set; }
        public int CountryID { get; set; }
    }

    public class DistrictInfo : LocationInfo
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int ProvinceID { get; set; }
    }
}