using Hello.Common.Utils;
using System;

namespace Hello.Core.Model
{
    public partial class Features
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EName { get; set; }
        public string KName { get; set; }
        public string Thumbnail { get; set; }
        public Status Status { get; set; }
    }
}
