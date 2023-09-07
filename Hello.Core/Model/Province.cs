using Hello.Common.Utils;
using System;

namespace Hello.Core.Model
{
    public partial class Province
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PostalCode { get; set; }
        public int CountryID { get; set; }
        public Status Status { get; set; }
    }
}
