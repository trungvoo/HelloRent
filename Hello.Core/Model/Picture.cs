using Hello.Common.Utils;
using System;

namespace Hello.Core.Model
{
    public partial class Picture
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public HostIndex HostIndex { get; set; }
        public string ProductID { get; set; }
        public long AccountID { get; set; }
        public string Note { get; set; }
        public DateTime CreateDate { get; set; }
        public Status Status { get; set; }
    }
}
