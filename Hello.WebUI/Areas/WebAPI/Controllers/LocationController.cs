using Hello.Common.Utils;
using Hello.Core.Interface.Service;
using Hello.Core.Model;
using Hello.WebUI.Areas.WebAPI.Models;
using Hello.WebUI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Hello.WebUI.Areas.WebAPI.Controllers
{
    [RoutePrefix("api/Location")]
    public class LocationController : ApiController
    {
        private readonly ICountryService CountryService;
        private readonly IProvinceService ProvinceService;
        private readonly IDistrictService DistrictService;
        private readonly IMarkerService MarkerService;

        public LocationController(ICountryService CountryService, IProvinceService ProvinceService, IDistrictService DistrictService, IMarkerService MarkerService)
        {
            this.CountryService = CountryService;
            this.ProvinceService = ProvinceService;
            this.DistrictService = DistrictService;
            this.MarkerService = MarkerService;
        }

        [Route("ListCountry")]
        [HttpGet]
        public async Task<HttpResponseMessage> ListCountry()
        {
            List<CountryInfo> countryList = new List<CountryInfo>();
            var result = await CountryService.GetAll();

            foreach (Country obj in result)
            {
                countryList.Add(new CountryInfo
                {
                    Id = obj.Id,
                    Name = obj.Name,
                    Code = obj.Code,
                    Thumbnail = PathHelper.Thumbnail(obj.Thumbnail, ThumbType.Country)
                });
            }

            return Request.CreateResponse(HttpStatusCode.OK, new JsonResponse
            {
                DataList = countryList,
                TotalRows = countryList.Count
            });
        }

        [Route("ListProvince/CountryID={countryID}")]
        [HttpGet]
        public async Task<HttpResponseMessage> ListProvince(int countryID)
        {
            List<ProvinceInfo> proList = new List<ProvinceInfo>();
            var result = await ProvinceService.ListByCountry(countryID);

            foreach (Province obj in result)
            {
                proList.Add(new ProvinceInfo
                {
                    Id = obj.Id,
                    Name = obj.Name,
                    PostalCode = obj.PostalCode,
                    CountryID = obj.CountryID
                });
            }

            return Request.CreateResponse(HttpStatusCode.OK, new JsonResponse
            {
                DataList = proList,
                TotalRows = proList.Count
            });
        }

        [Route("ListDistrict/ProvinceID={provinceID}")]
        [HttpGet]
        public async Task<HttpResponseMessage> ListDistrict(int provinceID)
        {
            List<DistrictInfo> distList = new List<DistrictInfo>();
            var result = await DistrictService.ListByProvince(provinceID);

            foreach (District obj in result)
            {
                distList.Add(new DistrictInfo
                {
                    Id = obj.Id,
                    Name = obj.Name,
                    Latitude = obj.Latitude,
                    Longitude = obj.Longitude,
                    ProvinceID = obj.ProvinceID
                });
            }

            return Request.CreateResponse(HttpStatusCode.OK, new JsonResponse
            {
                DataList = distList,
                TotalRows = distList.Count
            });
        }

        [Route("ListAttractionByLocation/Latitude={lat}/Longitude={lng}/NumItems={numItems}/Language={language}")]
        [HttpGet]
        public async Task<HttpResponseMessage> ListAttractionByLocation(double lat, double lng, int numItems, int language)
        {
            var result = await MarkerService.ListAttractionByLocation(lat, lng, numItems);

            if (result.Count() == 0)
            {
                string[] location = AppSettings.DefaultLocation.Split(',');
                result = await MarkerService.ListAttractionByLocation(double.Parse(location[0].Trim()), double.Parse(location[1].Trim()), numItems);
            }

            List<MarkerInfo> attrList = ToMarkerInfoList(result, (LanguageType)language);

            return Request.CreateResponse(HttpStatusCode.OK, new JsonResponse
            {
                DataList = attrList,
                TotalRows = attrList.Count
            });
        }

        [Route("ListMarkerByKeyword")]
        [HttpPost]
        public async Task<HttpResponseMessage> ListMarkerByKeyword()
        {
            List<MarkerInfo> markerList = new List<MarkerInfo>();

            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                string keyword = provider.FormData.GetValues("Keyword").SingleOrDefault();

                int provinceID = 0;
                int.TryParse(provider.FormData.GetValues("ProvinceID").SingleOrDefault(), out provinceID);

                MarkerType markerType = MarkerType.Undefined;
                MarkerType.TryParse(provider.FormData.GetValues("MarkerType").SingleOrDefault(), out markerType);

                LanguageType language = LanguageType.Vietnamese;
                LanguageType.TryParse(provider.FormData.GetValues("LanguageType").SingleOrDefault(), out language);

                if (markerType == MarkerType.Attraction)
                    markerList = ToMarkerInfoList(await MarkerService.ListAttractionByKeyword(keyword, provinceID), language);
                else
                    markerList = ToMarkerInfoList(await MarkerService.ListMarkerByKeyword(keyword, provinceID), language);
            }
            catch (Exception ex)
            {
                MarkerService.WriteError("Error in LocationController at ListAttractionByLocation() Method", ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, new JsonResponse
            {
                DataList = markerList,
                TotalRows = markerList.Count
            });
        }

        [Route("ListBuildingByDistrict/DistrictID={districtID}/Language={language}")]
        [HttpGet]
        public async Task<HttpResponseMessage> ListBuildingByDistrict(int districtID, int language)
        {
            List<MarkerInfo> markerList = ToMarkerInfoList(await MarkerService.ListMarkerByDistrict(districtID, MarkerType.Building), (LanguageType)language);

            return Request.CreateResponse(HttpStatusCode.OK, new JsonResponse
            {
                DataList = markerList,
                TotalRows = markerList.Count
            });
        }

        [Route("ListUniversityInKorea/NumItems={numItems}/Language={language}")]
        [HttpGet]
        public async Task<HttpResponseMessage> ListUniversityInKorea(int numItems, int language)
        {
            List<MarkerInfo> markerList = ToMarkerInfoList(await MarkerService.ListMarkerByCountry(3, MarkerType.University, numItems), (LanguageType)language);

            return Request.CreateResponse(HttpStatusCode.OK, new JsonResponse
            {
                DataList = markerList,
                TotalRows = markerList.Count
            });
        }


        #region Private Method
        private List<MarkerInfo> ToMarkerInfoList(IEnumerable<Marker> result, LanguageType language)
        {
            List<MarkerInfo> markerList = new List<MarkerInfo>();

            foreach (Marker obj in result)
            {
                string name = obj.Name;
                if (language == LanguageType.English)
                    name = obj.EName;
                else if (language == LanguageType.Korean)
                    name = obj.KName;

                markerList.Add(new MarkerInfo
                {
                    Id = obj.Id,
                    Name = name,
                    Address = obj.Address,
                    Latitude = obj.Latitude,
                    Longitude = obj.Longitude,
                    Thumbnail = PathHelper.Thumbnail(obj.Thumbnail, ThumbType.Marker),
                    FloorCount = obj.FloorCount
                });
            }

            return markerList;

        }
        #endregion
    }
}
