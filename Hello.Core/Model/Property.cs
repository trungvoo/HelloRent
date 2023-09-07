using Hello.Common.Utils;
using System;

namespace Hello.Core.Model
{
    public partial class Property
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EName { get; set; }
        public string KName { get; set; }
        public PropertyType Type { get; set; }
        public Status Status { get; set; }
    }
}
