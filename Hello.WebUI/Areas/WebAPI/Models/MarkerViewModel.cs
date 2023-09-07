using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hello.WebUI.Areas.WebAPI.Models
{
    public class MarkerInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Thumbnail { get; set; }
        public int FloorCount { get; set; }
    }
}