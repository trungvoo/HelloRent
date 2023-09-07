using Hello.Common.Utils;
using System;

namespace Hello.Core.Model
{
    public partial class Product
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
        public string Thumbnail { get; set; }
        public HostIndex HostIndex { get; set; }
        public decimal Deposit { get; set; }
        public decimal Price { get; set; }
        public int Floor { get; set; }
        public int FloorCount { get; set; }
        public decimal SiteArea { get; set; }
        public decimal GrossFloorArea { get; set; }
        public int Bedroom { get; set; }
        public int Bathroom { get; set; }
        public int DirectionID { get; set; }
        public decimal ServiceFee { get; set; }
        public string FeatureList { get; set; }
        public string FurnitureList { get; set; }
        public bool Elevator { get; set; }
        public bool Pets { get; set; }
        public int NumPerson { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ETitle { get; set; }
        public string EContent { get; set; }
        public string KTitle { get; set; }
        public string KContent { get; set; }
        public string Note { get; set; }
        public long AccountID { get; set; }
        public DateTime CreateDate { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public ProductStatus Status { get; set; }
        public int NumView { get; set; }
        public int NumLike { get; set; }
        public string NonUnicode { get; set; }

    }

    public partial class Product {

        public string PropertyName { get; set; }
        public string DirectionName { get; set; }
        public string BuildingName { get; set; }
        public int IsMeLikeThis { get; set; }
        public string ImageName { get; set; }
        public HostIndex ImageHostIndex { get; set; }

        public int TotalRows { get; set; }

    }
}
