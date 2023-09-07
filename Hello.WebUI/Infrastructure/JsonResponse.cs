using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hello.WebUI.Infrastructure
{
    public class JsonResponse
    {
        public long TotalRows { get; set; }
        public object DataList { get; set; }
    }
}