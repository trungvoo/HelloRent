using Hello.Common.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Hello.WebUI.Areas.WebAPI.Models
{

    public partial class InfoViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
    }

    public partial class PropertyViewModel : InfoViewModel
    {
        public PropertyType Type { get; set; }
    }

    public class PropertyNhaTotModel
    {
        [JsonProperty("filters")]
        public List<PropertyDatas> Datas { get; set; }
    }

    public class PropertyDatas
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("select")]
        public SelectData Select { get; set; }
    }

    public class SelectData
    {
        [JsonProperty("options")]
        public List<OptionsData> Options { get; set; } 
    }

    public class OptionsData
    {
        [JsonProperty("id")]
        public string Id { get; set; }


        [JsonProperty("value")]     
        public string Value { get; set; }
    }
}