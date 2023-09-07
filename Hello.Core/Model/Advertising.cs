using Hello.Common.Utils;
using System;

namespace Hello.Core.Model
{
    public partial class Advertising
    {
        public int Id { get; set; }
        public string Thumbnail { get; set; }
        public string ImageDetails { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public AdvLevel Level { get; set; }
        public byte Priority { get; set; }
        public DateTime CreatedDate { get; set; }
        public Status Status { get; set; }

        public string CompanyName { get; set; }
        public string ContactPhone { get; set; }
    }
}
