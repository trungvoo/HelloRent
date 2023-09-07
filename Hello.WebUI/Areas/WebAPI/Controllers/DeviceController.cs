using Hello.Common.Utils;
using Hello.Core.Interface.Service;
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
    [RoutePrefix("api/Device")]
    public class DeviceController : ApiController
    {
        private readonly IDeviceService DeviceService;

        public DeviceController(IDeviceService DeviceService)
        {
            this.DeviceService = DeviceService;
        }

        [Route("CreateDevice")]
        [HttpPost]
        public async Task<HttpResponseMessage> CreateDevice()
        {
            long result = -1;

            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                string token = provider.FormData.GetValues("Token").SingleOrDefault();

                long accountID = 0;
                long.TryParse(provider.FormData.GetValues("AccountID").SingleOrDefault(), out accountID);

                DeviceType type = 0;
                DeviceType.TryParse(provider.FormData.GetValues("DeviceType").SingleOrDefault(), out type);

                string version = provider.FormData.GetValues("Version").SingleOrDefault();
                string address = provider.FormData.GetValues("Address").SingleOrDefault();

                decimal lat = 0;
                decimal.TryParse(provider.FormData.GetValues("Latitude").SingleOrDefault(), out lat);

                decimal lng = 0;
                decimal.TryParse(provider.FormData.GetValues("Longitude").SingleOrDefault(), out lng);

                result = await DeviceService.Insert(token, accountID, type, DateTime.Now, "0.0.0", version, address, lat, lng);

            }
            catch (Exception e)
            {
                DeviceService.WriteError("Error in DeviceController at CreateDevice() Method", e.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, new JsonResponse
            {
                DataList = result != 0,
                TotalRows = result
            });
        }
    }
}
