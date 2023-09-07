using Hello.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Hello.WebUI.Areas.WebAPI.Models
{
    public class ProductViewModel
    {
        public long Id { get; set; }
        public string Address { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int CountryID { get; set; }
        public int ProvinceID { get; set; }
        public int DistrictID { get; set; }
        public int PropertyID { get; set; }
        public int BuildingID { get; set; }
        public int DirectionID { get; set; }
        public string Images { get; set; }
        public decimal Deposit { get; set; }
        public decimal Price { get; set; }
        public int Floor { get; set; }
        public int FloorCount { get; set; }
        public decimal SiteArea { get; set; }
        public decimal GrossFloorArea { get; set; }
        public int Bedroom { get; set; }
        public int Bathroom { get; set; }
        public decimal ServiceFee { get; set; }
        public bool Elevator { get; set; }
        public bool Pets { get; set; }
        public int NumPerson { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Note { get; set; }
        public long AccountID { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public int IsMeLikeThis { get; set; }
        public ProductStatus Status { get; set; }
        public int NumView { get; set; }
        public int NumLike { get; set; }
        public DateTime CreateDate { get; set; }


        public string PropertyName { get; set; }
        public string DirectionName { get; set; }
        public string BuildingName { get; set; }
        public List<FeaturesViewModel> FeatureList { get; set; }
        public List<FeaturesViewModel> FurnitureList { get; set; }
    }

    public class ProductsNhaTot
    {
        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("ads")]
        public List<DatasNhaTot> Data { get; set; }
    }

    public class DatasNhaTot
    {
        [JsonProperty("ad_id")]
        public int ad_id { get; set; }

        [JsonProperty("list_id")]
        public int list_id { get; set; }

        [JsonProperty("date")]
        public string date { get; set; }

        [JsonProperty("account_name")]
        public string account_name { get; set; }

        [JsonProperty("subject")]
        
        public string subject { get; set; }

        [JsonProperty("body")]
        public string body { get; set; }

        [JsonProperty("category")]
        public int category { get; set; }

        [JsonProperty("category_name")]
        public string category_name { get; set;}

        [JsonProperty("area")]
        public int area { get; set; }

        [JsonProperty("area_name")]
        public string area_name { get; set; }

        [JsonProperty("region")]
        public int region { get; set; }

        [JsonProperty("region_name")]
        public string region_name { get; set; }

        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("price")]
        public int price { get; set; }

        [JsonProperty("price_string")]
        public string price_string { get; set; }

        [JsonProperty("image")]
        public string image { get; set; }

        [JsonProperty("rooms")]
        public int rooms { get; set;}

        [JsonProperty("size")]
        public float size { get; set; }

        [JsonProperty("toilets")]
        public int toilets { get; set; }

        [JsonProperty("floors")]
        public int floors { get; set; }

        [JsonProperty("longitude")]
        public string longitude { get; set; }

        [JsonProperty("latitude")]
        public string latitude { get; set; }

        [JsonProperty("street_name")]
        public string street_name { get; set; }

        [JsonProperty("ward_name")]
        public string ward_name { get; set; }

    }

    public class DetailNhatot
    {
        [JsonProperty("ad")]
        public DatasNhaTot_Detail Data { get; set; }

        [JsonProperty("parameters")]
        public List<DatasNhaTot_Detail_Parameters> parameters { get; set; }
    }

    public class DatasNhaTot_Detail
    {
        [JsonProperty("ad_id")]
        public int ad_id { get; set; }

        [JsonProperty("list_id")]
        public long list_id { get; set; }

        [JsonProperty("date")]
        public string date { get; set; }

        [JsonProperty("account_name")]
        public string account_name { get; set ; }

        [JsonProperty("state")]
        public string state { get; set; }

        [JsonProperty("subject")]
        public string subject { get; set; }

        [JsonProperty("body")] 
        public string body { get; set; }

        [JsonProperty("category_name")]
        public string category_name { get; set; }

        [JsonProperty("area_name")]
        public string area_name { get; set; }

        [JsonProperty("region_name")]
        public string region_name { get; set; }

        [JsonProperty("type")] 
        public string type { get; set; }

        [JsonProperty("price")]
        public int price { get; set; }

        [JsonProperty("price_string")]
        public string price_string { get; set; }

        [JsonProperty("images")]
        public string[] images { get; set; }

        [JsonProperty("longitude")]
        public string longitude { get; set; }

        [JsonProperty("latitude")]
        public string latitude { get ; set; }

        [JsonProperty("type_name")]
        public string type_name { get; set; }

        [JsonProperty("reviewer_nickname")] 
        public string reviewer_nickname { get; set; }

        [JsonProperty("thumbnail_image")] 
        public string thumbnail_image { get; set; }

        [JsonProperty("rooms")]
        public string rooms { get; set; }

        [JsonProperty("size")]
        public int size { get; set; }

        [JsonProperty("toilets")]
        public int toilets { get; set; }

        [JsonProperty("detail_address")]
        public string detail_address { get; set; }

    }

    public class DatasNhaTot_Detail_Parameters
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("value")]
        public string value { get; set; }

        [JsonProperty("label")]
        public string label { get; set; }

    }

    public static partial class MapperHelper
    {
        public static void FromModel(this ProductViewModel obj, DatasNhaTot model)
        {
            obj.Id = model.list_id;
            obj.Address = model.ward_name + ", " + model.area_name + ", " + model.region_name;
            obj.Latitude = Convert.ToDecimal(model.latitude);
            obj.Longitude = Convert.ToDecimal(model.longitude);
            obj.Images = model.image;
            obj.Price = model.price / 1000000;
            obj.Title = model.subject;
            obj.AccountID = 1;
            obj.GrossFloorArea = Convert.ToDecimal(model.size);
        }
    }
}