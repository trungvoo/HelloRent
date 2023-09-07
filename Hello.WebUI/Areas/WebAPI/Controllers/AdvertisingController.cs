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
using System.Web.Http;

namespace Hello.WebUI.Areas.WebAPI.Controllers
{
    [RoutePrefix("api/Advertising")]
    public class AdvertisingController : ApiController
    {
        private readonly IAdvertisingService AdvertisingService;

        public AdvertisingController(IAdvertisingService advertisingService)
        {
            this.AdvertisingService = advertisingService;
        }

        [Route("ListActivated")]
        [HttpGet]
        public async Task<HttpResponseMessage> ListActivated()
        {

            var result = await AdvertisingService.ListByStatus(Status.Activated);

            List<AdvViewModel> advList = new List<AdvViewModel>();
            foreach (Advertising obj in result)
            {
                advList.Add(new AdvViewModel
                {
                    Id = obj.Id,
                    Thumbnail = PathHelper.AdvPath(obj.Thumbnail, obj.HostIndex),
                    ImageDetails = PathHelper.AdvPath(obj.ImageDetails, obj.HostIndex),
                    Title = obj.Title,
                    Description = obj.Description,
                    Url = obj.Url,
                    Level = obj.Level,
                    Priority = obj.Level,
                    AgencyID = obj.AgencyID,
                    NoofClicked = obj.NoofClicked,
                    NoofAppearance = obj.NoofAppearance
                });
            }

            return Request.CreateResponse(HttpStatusCode.OK, new JsonResponse
            {
                Datas = advList,
                TotalRows = advList.Count
            });
        }
    }
}
