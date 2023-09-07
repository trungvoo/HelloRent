using Hello.Common.Utils;
using System;

namespace Hello.Core.Model
{
    public partial class Warning
    {
        public int Id { get; set; }
        public long AccountID { get; set; }
        public long EmployeeID { get; set; }
        public long ObjectID { get; set; }
        public ObjectType ObjectType { get; set; }
        public WarningType Type { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ExecutedDate { get; set; }
        public string Note { get; set; }
        public Status Status { get; set; }
    }
}
