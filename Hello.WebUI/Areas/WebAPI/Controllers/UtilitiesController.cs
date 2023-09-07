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
    [RoutePrefix("api/Utilities")]
    public class UtilitiesController : ApiController
    {
        private readonly IAdvService AdvService;
        private readonly IPropertyService CategoryService;
        private readonly IFeaturesService FeaturesService;
        private readonly IDirectionService DirectionService;
        private readonly IWarningService WarningService;
        private readonly IInfoService InforService;
        private readonly IGiftCardService GiftCardService;

        public UtilitiesController(IAdvService AdvService, IPropertyService CategoryService, IFeaturesService FeaturesService, IDirectionService DirectionService, IWarningService WarningService,
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

        [Route("ListMainBanner")]
        [HttpGet]
        public async Task<HttpResponseMessage> ListMainBanner()
        {
            List<AdvViewModel> listAdv = new List<AdvViewModel>();
            var result = await AdvService.ListByLevel(AdvLevel.MainBanner, Status.Activated, 1, 5);

            foreach (Advertising adv in result)
            {
                listAdv.Add(new AdvViewModel
                {
                    Id = adv.Id,
                    Title = adv.Title,
                    Thumbnail = PathHelper.Adv(adv.Thumbnail),
                    Url = adv.Url
                });
            }

            return Request.CreateResponse(HttpStatusCode.OK, new JsonResponse
            {
                DataList = listAdv,
                TotalRows = listAdv.Count
            });
        }

        [Route("ListPowerLink/ProvinceID={provinceID}")]
        [HttpGet]
        public async Task<HttpResponseMessage> ListPowerLink(int provinceID)
        {
            List<AdvViewModel> listAdv = new List<AdvViewModel>();
            var result = await AdvService.ListByLevel(AdvLevel.Powerlink, Status.Activated, 1, 3);

            foreach (Advertising adv in result)
            {
                listAdv.Add(new AdvViewModel
                {
                    Id = adv.Id,
                    Title = adv.Title,
                    Thumbnail = PathHelper.Adv(adv.Thumbnail),
                    ImageDetails = PathHelper.Adv(adv.ImageDetails),
                    Url = adv.Url,
                    CompanyName = adv.Title,
                    ContactPhone = adv.Url
                });
            }

            return Request.CreateResponse(HttpStatusCode.OK, new JsonResponse
            {
                DataList = listAdv,
                TotalRows = listAdv.Count
            });
        }

        [Route("ListProperty/LanguageType={language}")]
        [HttpGet]
        public async Task<HttpResponseMessage> ListProperty(LanguageType language)
        {
            List<PropertyViewModel> listCate = new List<PropertyViewModel>();

            var result = await CategoryService.ListProperty();
            foreach (Property obj in result)
            {
                string name = obj.Name;
                if (language == LanguageType.English)
                    name = obj.EName;
                else if (language == LanguageType.Korean)
                    name = obj.KName;

                listCate.Add(new PropertyViewModel
                {
                    Id = obj.Id,
                    Name = name,
                    Type = obj.Type
                });
            }

            return Request.CreateResponse(HttpStatusCode.OK, new JsonResponse
            {
                DataList = listCate,
                TotalRows = listCate.Count
            });

        }

        [Route("ListFeature/LanguageType={language}")]
        [HttpGet]
        public async Task<HttpResponseMessage> ListFeature(LanguageType language)
        {
            List<FeaturesViewModel> featureList = new List<FeaturesViewModel>();
            var result = await FeaturesService.ListFeature();

            foreach (Features obj in result)
            {
                string name = obj.Name;
                if (language == LanguageType.English)
                    name = obj.EName;
                else if (language == LanguageType.Korean)
                    name = obj.KName;

                featureList.Add(new FeaturesViewModel
                {
                    Id = obj.Id,
                    Name = name,
                    Thumbnail = PathHelper.Thumbnail(obj.Thumbnail, ThumbType.Feature)
                });
            }

            return Request.CreateResponse(HttpStatusCode.OK, new JsonResponse
            {
                DataList = featureList,
                TotalRows = featureList.Count
            });
        }

        [Route("ListDirection/LanguageType={language}")]
        [HttpGet]
        public async Task<HttpResponseMessage> ListDirection(LanguageType language)
        {
            List<InfoViewModel> listDirection = new List<InfoViewModel>();

            var result = await DirectionService.ListDirection();

            foreach (Direction obj in result)
            {
                string name = obj.Name;
                if (language == LanguageType.English)
                    name = obj.EName;
                else if (language == LanguageType.Korean)
                    name = obj.KName;

                listDirection.Add(new InfoViewModel
                {
                    Id = obj.Id,
                    Name = name
                });
            }

            return Request.CreateResponse(HttpStatusCode.OK, new JsonResponse
            {
                DataList = listDirection,
                TotalRows = listDirection.Count
            });
        }

        [Route("ListFurniture/LanguageType={language}")]
        [HttpGet]
        public async Task<HttpResponseMessage> ListFurniture(LanguageType language)
        {
            List<FeaturesViewModel> featureList = new List<FeaturesViewModel>();
            var result = await FeaturesService.ListFurniture();

            foreach (Features obj in result)
            {
                string name = obj.Name;
                if (language == LanguageType.English)
                    name = obj.EName;
                else if (language == LanguageType.Korean)
                    name = obj.KName;

                featureList.Add(new FeaturesViewModel
                {
                    Id = obj.Id,
                    Name = name,
                    Thumbnail = PathHelper.Thumbnail(obj.Thumbnail, ThumbType.Furniture)
                });
            }

            return Request.CreateResponse(HttpStatusCode.OK, new JsonResponse
            {
                DataList = featureList,
                TotalRows = featureList.Count
            });
        }
        
        [Route("GetInfoByType/InfoType={type}/LanguageType={language}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetInfoByType(InfoType type, LanguageType language)
        {
            List<InfoViewModel> infoList = new List<InfoViewModel>();

            var result = await InforService.GetInfoByType(type);

            if (result != null)
            {
                string title = result.Title;
                string content = result.Content;
                if (language == LanguageType.English)
                {
                    title = result.ETitle;
                    content = result.EContent;
                }
                else if (language == LanguageType.Korean)
                {
                    title = result.KTitle;
                    content = result.KContent;
                }

                infoList.Add(new InfoViewModel
                {
                    Id = result.Id,
                    Name = title,
                    Content = content
                });
            }

            return Request.CreateResponse(HttpStatusCode.OK, new JsonResponse
            {
                DataList = infoList,
                TotalRows = infoList.Count
            });
        }

        [Route("SendWarning")]
        [HttpPost]
        public async Task<HttpResponseMessage> SendWarning()
        {
            long result = 1;

            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                long accountID = 0;
                long.TryParse(provider.FormData.GetValues("AccountID").SingleOrDefault(), out accountID);

                long productID = 0;
                long.TryParse(provider.FormData.GetValues("ProductID").SingleOrDefault(), out productID);

                WarningType type = 0;
                WarningType.TryParse(provider.FormData.GetValues("WarningType").SingleOrDefault(), out type);

                string content = provider.FormData.GetValues("Content").SingleOrDefault();

                result = await WarningService.Insert(new Warning
                {
                    AccountID = accountID,
                    ObjectID = productID,
                    ObjectType = ObjectType.Product,
                    Type = type,
                    Content = content,
                    CreateDate = DateTime.Now
                });

            }
            catch (Exception e)
            {
                CategoryService.WriteError("Error in UtilitiesController at SendEmail() Method", e.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, new JsonResponse
            {
                DataList = result > 0,
                TotalRows = result
            });
        }

        [Route("GetGiftCard/AccountID={accountID}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetGiftCard(long accountID)
        {
            List<GiftCard> model = new List<GiftCard>();

            var result = await GiftCardService.GetByAccount(accountID);
            if (result != null)
            {
                model.Add(new GiftCard
                {
                    Id = result.Id,
                    Name = result.Name,
                    Content = result.Content,
                    Thumbnail = PathHelper.Adv(result.Thumbnail),
                    PictureCard  = PathHelper.Adv(result.PictureCard)
                });
            }

            return Request.CreateResponse(HttpStatusCode.OK, new JsonResponse
            {
                DataList = model,
                TotalRows = model.Count
            });
        }

        [Route("UpdateAccountGift")]
        [HttpPost]
        public async Task<HttpResponseMessage> GetGiftCard()
        {
            long result = 1;

            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                long accountID = 0;
                long.TryParse(provider.FormData.GetValues("AccountID").SingleOrDefault(), out accountID);

                int giftID = 0;
                int.TryParse(provider.FormData.GetValues("GiftID").SingleOrDefault(), out giftID);

                result = await GiftCardService.UpdateAccountGift(accountID, giftID);

            }
            catch (Exception e)
            {
                CategoryService.WriteError("Error in UtilitiesController at UpdateAccountGift() Method", e.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, new JsonResponse
            {
                DataList = result > 0,
                TotalRows = result
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
