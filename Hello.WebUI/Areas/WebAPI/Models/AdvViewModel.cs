﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hello.WebUI.Areas.WebAPI.Models
{
    public class AdvViewModel
    {
        public int Id { get; set; }
        public string Thumbnail { get; set; }
        public string ImageDetails { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string CompanyName { get; set; }
        public string ContactPhone { get; set; }
    }
}