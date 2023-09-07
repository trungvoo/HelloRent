using Hello.Common.Utils;
using System;

namespace Hello.Core.Model
{
   public partial class Notify
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Thumbnail { get; set; }
        public string Url { get; set; }
        public DateTime CreateDate { get; set; }
        public NotifyType Type { get; set; }
        public Status Status { get; set; }
    }
}
