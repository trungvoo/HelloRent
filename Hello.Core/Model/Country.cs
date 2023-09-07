using Hello.Common.Utils;
using System;

namespace Hello.Core.Model
{
    public partial class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Thumbnail { get; set; }
        public Status Status { get; set; }
    }
}
