using Hello.Common.Utils;
using Hello.Core.Interface.Service;
using Hello.Core.Model;
using Hello.WebUI.Areas.WebAPI.Models;
using Hello.WebUI.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Hello.WebUI.Areas.WebAPI.Controllers
{
    [RoutePrefix("api/v2.0/Utilities")]
    public class Utilitiesv2Controller : ApiController
    {
        private readonly IAdvService AdvService;
        private readonly IPropertyService CategoryService;
        private readonly IFeaturesService FeaturesService;
        private readonly IDirectionService DirectionService;
        private readonly IWarningService WarningService;
        private readonly IInfoService InforService;
        private readonly IGiftCardService GiftCardService;

        public Utilitiesv2Controller(IAdvService AdvService, IPropertyService CategoryService, IFeaturesService FeaturesService, IDirectionService DirectionService, IWarningService WarningService,
            IInfoService InforService, IGiftCardService GiftCardService)
        {
            this.AdvService = AdvService;
            this.CategoryService = CategoryService;
            this.FeaturesService = FeaturesService;
            this.DirectionService = DirectionService;
            this.WarningService = WarningService;
            this.InforService = InforService;
            this.GiftCardService = GiftCardService;
        }

        [Route("ListProperty")]
        [HttpGet]
        public async Task<HttpResponseMessage> ListProperty()
        {
            SelectData listCate = new SelectData();

            var result = await DatasNhaTot_Property();

            listCate = result;

            int totalRow = listCate.Options.Count();

            return Request.CreateResponse(HttpStatusCode.OK, new JsonResponse
            {
                DataList = listCate,
                TotalRows = totalRow
            });

        }

        private async Task<SelectData> DatasNhaTot_Property()
        {
            var listDatas = new SelectData();

            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;


                string data = string.Empty;
                string url = "https://" + "gateway.chotot.com/v1/public/nav-conf/filter?cg=1000";
                var webRequest = System.Net.WebRequest.Create(url);
                webRequest.ContentType = "application/json";
                webRequest.Method = "GET";

                if (webRequest != null)
                {
                    var httpResponse = (HttpWebResponse)await webRequest.GetResponseAsync();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var rawResponse = streamReader.ReadToEnd();
                        data = rawResponse;
                    }

                    var model = JsonConvert.DeserializeObject<PropertyNhaTotModel>(data);

                    if (model.Datas != null)
                    {
                        foreach (var property in model.Datas)
                        {
                            if (property.Key == "cg")
                            {
                                listDatas = property.Select;
                            }
                        }
                    }

                }

                return listDatas;
            }
            catch (Exception)
            {
                return listDatas;
            }

        }
    }
}
