using Hello.Common.Utils;
using System;
using System.Reflection;

namespace Hello.Core.Model
{
    public partial class ProductsViewModel
    {

        public long ProductID { get; set; }

        public long AccountID { get; set; }

        public string Title { get; set; }

        public string Avatar { get; set; }

        public string Address { get; set; }

        public decimal Price { get; set; }

        public decimal GrossFloorArea { get; set; }

        public ProductStatus Status { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifiedDate { get; set; }

    }

    public partial class ProductsModel
    {
        public long ProductID { get; set;}
        public string Title { get; set; }
        public string Avatar { get; set; }
        public string Address { get; set; }
        public ProductStatus Status { get; set; }
        public decimal Price { get; set; }

        public decimal GrossFloorArea { get; set; }
    }
}
