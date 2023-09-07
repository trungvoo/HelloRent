using Hello.Common.Utils;
using System;
using System.Data;

namespace Hello.Core.Model
{
    public partial class Favourite
    {

        public long ProductID { get; set; }

        public long AccountID { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifiedDate { get; set; }

    }
}
