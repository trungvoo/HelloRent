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
    [RoutePrefix("api/Notify")]
    public class NotifyController : ApiController
    {
        private readonly INotifyService NotifyService;

        public NotifyController(INotifyService NotifyService)
        {
            this.NotifyService = NotifyService;
        }

        [Route("GetMainLauncher")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetMainLauncher()
        { 
            List<NotifyViewModel> model = new List<NotifyViewModel>();

            var result = await NotifyService.GetMainLauncher();

            if (result != null)
            {
                model.Add(new NotifyViewModel
                {
                    Id = result.Id,
                    Name = result.Name,
                    Content = result.Content,
                    Thumbnail = PathHelper.Thumbnail(result.Thumbnail, ThumbType.Notify),
                    CreateDate = result.CreateDate
                });
            }

            return Request.CreateResponse(HttpStatusCode.OK, new JsonResponse
            {
                DataList = model,
                TotalRows = model.Count
            });
        } 
    }
}
