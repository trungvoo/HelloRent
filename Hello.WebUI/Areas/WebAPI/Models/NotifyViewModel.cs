using System;


namespace Hello.WebUI.Areas.WebAPI.Models
{
    public class NotifyViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Thumbnail { get; set; }
        public string Url { get; set; }
        public DateTime CreateDate { get; set; }
    }
}