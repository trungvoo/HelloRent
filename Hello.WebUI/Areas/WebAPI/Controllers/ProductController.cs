using Hello.Common.Helper;
using Hello.Common.Utils;
using Hello.Core.Interface.Service;
using Hello.Core.Model;
using Hello.WebUI.Areas.WebAPI.Models;
using Hello.WebUI.Infrastructure;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebGrease;

namespace Hello.WebUI.Areas.WebAPI.Controllers
{
    [RoutePrefix("api/Product")]
    public class ProductController : ApiController
    {
        private readonly IProductService ProductService;
        private readonly IAccountService AccountService;
        private readonly IFeaturesService FeaturesService;

        public ProductController(IProductService productService, IFeaturesService FeaturesService, IAccountService AccountService)
        {
            this.ProductService = productService;
            this.FeaturesService = FeaturesService;
            this.AccountService = AccountService;
        }

        [Route("Create")]
        [HttpPost]
        public async Task<HttpResponseMessage> Create()
        {
            long result = -1;
            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            string root = HttpContext.Current.Server.MapPath("~/" + AppSettings.ProductPath);
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {

                Stream reqStream = Request.Content.ReadAsStreamAsync().Result;
                MemoryStream tempStream = new MemoryStream();
                reqStream.CopyTo(tempStream);



                tempStream.Seek(0, SeekOrigin.End);
                StreamWriter writer = new StreamWriter(tempStream);
                writer.WriteLine();
                writer.Flush();
                tempStream.Position = 0;


                StreamContent streamContent = new StreamContent(tempStream);
                foreach (var header in Request.Content.Headers)
                {
                    streamContent.Headers.Add(header.Key, header.Value);
                }

                // Read the form data and return an async task.
                await streamContent.ReadAsMultipartAsync(provider);

                //await Request.Content.ReadAsMultipartAsync(provider);

                string address = provider.FormData.GetValues("Address").SingleOrDefault();

                decimal lat = 0;
                decimal.TryParse(provider.FormData.GetValues("Latitude").SingleOrDefault(), out lat);

                decimal lng = 0;
                decimal.TryParse(provider.FormData.GetValues("Longitude").SingleOrDefault(), out lng);

                int country = 0;
                int.TryParse(provider.FormData.GetValues("CountryID").SingleOrDefault(), out country);

                int province = 0;
                int.TryParse(provider.FormData.GetValues("ProvinceID").SingleOrDefault(), out province);

                int district = 0;
                int.TryParse(provider.FormData.GetValues("DistrictID").SingleOrDefault(), out district);

                int property = 0;
                int.TryParse(provider.FormData.GetValues("PropertyID").SingleOrDefault(), out property);

                int building = 0;
                int.TryParse(provider.FormData.GetValues("BuildingID").SingleOrDefault(), out building);

                decimal deposit = 0;
                decimal.TryParse(provider.FormData.GetValues("Deposit").SingleOrDefault(), out deposit);

                decimal price = 0;
                decimal.TryParse(provider.FormData.GetValues("Price").SingleOrDefault(), out price);

                int floor = 0;
                int.TryParse(provider.FormData.GetValues("Floor").SingleOrDefault(), out floor);

                int floorCount = 0;
                int.TryParse(provider.FormData.GetValues("FloorCount").SingleOrDefault(), out floorCount);

                decimal siteArea = 0;
                decimal.TryParse(provider.FormData.GetValues("SiteArea").SingleOrDefault(), out siteArea);

                decimal grossArea = 0;
                decimal.TryParse(provider.FormData.GetValues("GrossFloorArea").SingleOrDefault(), out grossArea);

                int beds = 0;
                int.TryParse(provider.FormData.GetValues("Bedroom").SingleOrDefault(), out beds);

                int baths = 0;
                int.TryParse(provider.FormData.GetValues("Bathroom").SingleOrDefault(), out baths);

                int direction = 0;
                int.TryParse(provider.FormData.GetValues("DirectionID").SingleOrDefault(), out direction);

                decimal serviceFee = 0;
                decimal.TryParse(provider.FormData.GetValues("ServiceFee").SingleOrDefault(), out serviceFee);

                string featureList = provider.FormData.GetValues("FeatureList").SingleOrDefault();
                string furnitureList = provider.FormData.GetValues("FurnitureList").SingleOrDefault();

                int elevator = 0;
                int.TryParse(provider.FormData.GetValues("Elevator").SingleOrDefault(), out elevator);

                int pets = 0;
                int.TryParse(provider.FormData.GetValues("Pets").SingleOrDefault(), out pets);

                int numPerson = 0;
                int.TryParse(provider.FormData.GetValues("NumberPerson").SingleOrDefault(), out numPerson);

                string title = provider.FormData.GetValues("Title").SingleOrDefault();
                string content = provider.FormData.GetValues("Content").SingleOrDefault();
                string note = provider.FormData.GetValues("Note").SingleOrDefault();
                string contactName = provider.FormData.GetValues("ContactName").SingleOrDefault();
                string contactPhone = provider.FormData.GetValues("ContactPhone").SingleOrDefault();

                long accID = 0;
                long.TryParse(provider.FormData.GetValues("AccountID").SingleOrDefault(), out accID);

                AccountRole accountRole = AccountRole.Undefined;
                AccountRole.TryParse(provider.FormData.GetValues("AccountRole").SingleOrDefault(), out accountRole);

                string nonUnicode = AppHelper.RemoveUnicode(title) + " | " + AppHelper.RemoveUnicode(note);

                long newID = await ProductService.Insert(new Product
                {
                    Address = address,
                    Latitude = lat,
                    Longitude = lng,
                    CountryID = country,
                    ProvinceID = province,
                    DistrictID = district,
                    PropertyID = property,
                    BuildingID = building,
                    Thumbnail = "",
                    HostIndex = HostIndex.CurrentHost,
                    Deposit = deposit,
                    Price = price,
                    Floor = floor,
                    FloorCount = floorCount,
                    SiteArea = siteArea,
                    GrossFloorArea = grossArea,
                    Bedroom = beds,
                    Bathroom = baths,
                    DirectionID = direction,
                    ServiceFee = serviceFee,
                    FeatureList = featureList,
                    FurnitureList = furnitureList,
                    Elevator = elevator != 0,
                    Pets = pets != 0,
                    NumPerson = numPerson,
                    Title = title,
                    Content = content,
                    Note = note,
                    AccountID = accID,
                    CreateDate = DateTime.Now,
                    ContactName = contactName,
                    ContactPhone = contactPhone,
                    NonUnicode = nonUnicode
                });

                List<string> pictureList = new List<string>();
                for (int i = 0; i < provider.FileData.Count; i++)
                {
                    MultipartFileData file = provider.FileData[i];
                    if (!string.IsNullOrEmpty(file.Headers.ContentDisposition.FileName.Replace("\"", string.Empty)))
                    {
                        string imageName = FileHelper.ProductImageName(newID, i);
                        pictureList.Add(FileHelper.SaveImageFile(file, imageName, root));
                    }

                }

                if (accountRole == AccountRole.Member || accountRole == AccountRole.Undefined)
                    await AccountService.UpdateRole(accID, accID, AccountRole.Owner);

                result = await ProductService.InsertPicture(newID, accID, pictureList);

            }
            catch (Exception ex)
            {
                ProductService.WriteError("Error in ProductController at Insert() Method", ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, new JsonResponse
            {
                DataList = result > 0,
                TotalRows = result
            });
        }

        [Route("Search")]
        [HttpPost]
        public async Task<HttpResponseMessage> Search()
        {
            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            List<ProductViewModel> productList = new List<ProductViewModel>();
            int totalRows = 0;

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                float startLat = 0;
                float.TryParse(provider.FormData.GetValues("StartLat").SingleOrDefault(), out startLat);
                float startLng = 0;
                float.TryParse(provider.FormData.GetValues("StartLng").SingleOrDefault(), out startLng);

                float endLat = 0;
                float.TryParse(provider.FormData.GetValues("EndLat").SingleOrDefault(), out endLat);
                float endLng = 0;
                float.TryParse(provider.FormData.GetValues("EndLng").SingleOrDefault(), out endLng);

                float distance = 0;
                float.TryParse(provider.FormData.GetValues("Distance").SingleOrDefault(), out distance);

                PropertyType type = PropertyType.Undefined;
                PropertyType.TryParse(provider.FormData.GetValues("PropertyType").SingleOrDefault(), out type);
                int propertyID = 0;
                int.TryParse(provider.FormData.GetValues("PropertyID").SingleOrDefault(), out propertyID);

                float minPrice = 0;
                float.TryParse(provider.FormData.GetValues("MinPrice").SingleOrDefault(), out minPrice);
                float maxPrice = 0;
                float.TryParse(provider.FormData.GetValues("MaxPrice").SingleOrDefault(), out maxPrice);

                float minArea = 0;
                float.TryParse(provider.FormData.GetValues("MinArea").SingleOrDefault(), out minArea);
                float maxArea = 0;
                float.TryParse(provider.FormData.GetValues("MaxArea").SingleOrDefault(), out maxArea);

                int bed = 0;
                int.TryParse(provider.FormData.GetValues("Bedroom").SingleOrDefault(), out bed);
                int bath = 0;
                int.TryParse(provider.FormData.GetValues("Bathroom").SingleOrDefault(), out bath);

                ProductStatus status = ProductStatus.Certified;
                ProductStatus.TryParse(provider.FormData.GetValues("Status").SingleOrDefault(), out status);

                int pageNo = 0;
                int.TryParse(provider.FormData.GetValues("PageNo").SingleOrDefault(), out pageNo);
                if (pageNo == 0)
                {
                    pageNo = 1;
                }

                LanguageType language = LanguageType.Vietnamese;
                LanguageType.TryParse(provider.FormData.GetValues("LanguageType").SingleOrDefault(), out language);

                int beginRow = (pageNo - 1) * AppSettings.ItemPerPageMobile;

                IEnumerable<Product> result = Enumerable.Empty<Product>();
                if (distance != 0 && endLat == 0 && endLng == 0)
                    result = await ProductService.SearchNearArea(startLat, startLng, distance, type, propertyID, minPrice, maxPrice, minArea, maxArea, bed, bath, status, beginRow, AppSettings.ItemPerPageMobile);
                else if (distance == 0 && endLat != 0 && endLng != 0)
                    result = await ProductService.SearchBounds(startLat, startLng, endLat, endLng, type, propertyID, minPrice, maxPrice, minArea, maxArea, bed, bath, status, beginRow, pageNo > 0 ? AppSettings.ItemPerPageMobile : 0);

                if (result.Count() > 0)
                    totalRows = result.ElementAt(0).TotalRows;

                productList = ToProductList(result, language);

            }
            catch (Exception ex)
            {
                ProductService.WriteError("Error in ProductController at Search() Method", ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, new JsonResponse
            {
                TotalRows = totalRows,
                DataList = productList
            });
        }

        [Route("SearchByAccount")]
        [HttpPost]
        public async Task<HttpResponseMessage> SearchByAccount()
        {

            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            List<ProductViewModel> productList = new List<ProductViewModel>();
            int totalRows = 0;

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                long accountID = 0;
                long.TryParse(provider.FormData.GetValues("AccountID").SingleOrDefault(), out accountID);

                ProductStatus status = ProductStatus.Certified;
                ProductStatus.TryParse(provider.FormData.GetValues("Status").SingleOrDefault(), out status);

                string keyword = provider.FormData.GetValues("Keyword").SingleOrDefault();
                long productID = Convert.ToInt64(AppHelper.StringToNumber(keyword));

                int pageNo = 0;
                int.TryParse(provider.FormData.GetValues("PageNo").SingleOrDefault(), out pageNo);

                LanguageType language = LanguageType.Vietnamese;
                LanguageType.TryParse(provider.FormData.GetValues("LanguageType").SingleOrDefault(), out language);

                int beginRow = (pageNo - 1) * AppSettings.ItemPerPageMobile;

                var result = await ProductService.SearchByAccount(accountID, status, productID, (productID > 0 ? string.Empty : keyword), beginRow, AppSettings.ItemPerPageMobile);

                if (result.Count() > 0)
                    totalRows = result.ElementAt(0).TotalRows;

                foreach (Product obj in result)
                {
                    string title = obj.Title;
                    if (language == LanguageType.English)
                        title = obj.ETitle;
                    else if (language == LanguageType.Korean)
                        title = obj.KTitle;

                    productList.Add(new ProductViewModel
                    {
                        Id = obj.Id,
                        Address = obj.Address,
                        Images = PathHelper.ProductImage(obj.Thumbnail, obj.HostIndex),
                        Price = obj.Price,
                        GrossFloorArea = obj.GrossFloorArea,
                        Title = title,
                        Note = obj.Note,
                        Status = obj.Status,
                        NumView = obj.NumView,
                        NumLike = obj.NumLike
                    });
                }
            }
            catch (Exception ex)
            {
                ProductService.WriteError("Error in ProductController at SearchByAccount() Method", ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, new JsonResponse
            {
                DataList = productList,
                TotalRows = totalRows
            });
        }

        [Route("GetByID")]
        [HttpPost]
        public async Task<HttpResponseMessage> GetByID()
        {

            List<ProductViewModel> model = new List<ProductViewModel>();

            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                long id = 0;
                long.TryParse(provider.FormData.GetValues("ProductID").SingleOrDefault(), out id);

                long accId = 0;
                long.TryParse(provider.FormData.GetValues("AccountID").SingleOrDefault(), out accId);

                int isMeViewThis = 0;
                int.TryParse(provider.FormData.GetValues("IsMeViewThis").SingleOrDefault(), out isMeViewThis);

                LanguageType language = LanguageType.Vietnamese;
                LanguageType.TryParse(provider.FormData.GetValues("LanguageType").SingleOrDefault(), out language);


                var result = await ProductService.GetById(id, accId);

                if (result.Count() > 0)
                {

                    if (isMeViewThis == 0 && result.ElementAt(0).AccountID != accId)
                        await ProductService.UserView(id);

                    var featureList = await FeaturesById(result.ElementAt(0).FeatureList, language, ThumbType.Feature);
                    var furnitureList = await FeaturesById(result.ElementAt(0).FurnitureList, language, ThumbType.Furniture);

                    List<string> listImage = new List<string>();
                    foreach (Product obj in result)
                        listImage.Add(PathHelper.ProductImage(obj.ImageName, obj.ImageHostIndex));


                    string addr = result.ElementAt(0).Address;
                    string title = result.ElementAt(0).Title;
                    string content = result.ElementAt(0).Content;
                    long accountID = result.ElementAt(0).AccountID;

                    if(accountID == 301) // Phat Loc Real
                        listImage.Add(PathHelper.Adv("phat_loc_real.jpg"));

                    model.Add(new ProductViewModel
                    {
                        Id = result.ElementAt(0).Id,
                        Address = addr,
                        Latitude = result.ElementAt(0).Latitude,
                        Longitude = result.ElementAt(0).Longitude,
                        CountryID = result.ElementAt(0).CountryID,
                        ProvinceID = result.ElementAt(0).ProvinceID,
                        DistrictID = result.ElementAt(0).DistrictID,
                        PropertyID = result.ElementAt(0).PropertyID,
                        BuildingID = result.ElementAt(0).BuildingID,
                        DirectionID = result.ElementAt(0).DirectionID,
                        Images = string.Join(", ", listImage),
                        Deposit = result.ElementAt(0).Deposit,
                        Price = result.ElementAt(0).Price,
                        Floor = result.ElementAt(0).Floor,
                        FloorCount = result.ElementAt(0).FloorCount,
                        SiteArea = result.ElementAt(0).SiteArea,
                        GrossFloorArea = result.ElementAt(0).GrossFloorArea,
                        Bedroom = result.ElementAt(0).Bedroom,
                        Bathroom = result.ElementAt(0).Bathroom,
                        ServiceFee = result.ElementAt(0).ServiceFee,
                        FeatureList = featureList,
                        FurnitureList = furnitureList,
                        Elevator = result.ElementAt(0).Elevator,
                        Pets = result.ElementAt(0).Pets,
                        NumPerson = result.ElementAt(0).NumPerson,
                        Title = title,
                        Content = content,
                        Note = result.ElementAt(0).Note,
                        AccountID = accountID,
                        ContactName = result.ElementAt(0).ContactName,
                        ContactPhone = result.ElementAt(0).ContactPhone,
                        IsMeLikeThis = result.ElementAt(0).IsMeLikeThis,
                        Status = result.ElementAt(0).Status,
                        CreateDate = result.ElementAt(0).AccountID == accId ? result.ElementAt(0).CreateDate : DateTime.Now,
                        PropertyName = result.ElementAt(0).PropertyName,
                        DirectionName = result.ElementAt(0).DirectionName,
                        BuildingName = result.ElementAt(0).BuildingName

                    });

                }

            }
            catch (Exception ex)
            {
                ProductService.WriteError("Error in ProductController at GetByID() Method", ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, new JsonResponse
            {
                DataList = model,
                TotalRows = model.Count
            });
        }

        [Route("Favorite")]
        [HttpPost]
        public async Task<HttpResponseMessage> Favorite()
        {
            long result = -1;

            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                long id = 0;
                long.TryParse(provider.FormData.GetValues("ProductID").SingleOrDefault(), out id);

                long accId = 0;
                long.TryParse(provider.FormData.GetValues("AccountID").SingleOrDefault(), out accId);

                if (id > 0 && accId > 0)
                    result = await ProductService.Favorite(id, accId);

            }
            catch (Exception ex)
            {
                ProductService.WriteError("Error in ProductController at Favorite() Method", ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, new JsonResponse
            {
                DataList = result > 0,
                TotalRows = result
            });
        }


        [Route("ListUserLike/UserID={userID}/Language={language}")]
        [HttpGet]
        public async Task<HttpResponseMessage> Favorite(long userID, LanguageType language)
        {
            List<ProductViewModel> productList = ToProductList(await ProductService.GetByUserLikes(userID), language);

            return Request.CreateResponse(HttpStatusCode.OK, new JsonResponse
            {
                DataList = productList,
                TotalRows = productList.Count
            });
        }

        [Route("UpdateNote")]
        [HttpPost]
        public async Task<HttpResponseMessage> UpdateNote()
        {
            long result = -1;

            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                long id = 0;
                long.TryParse(provider.FormData.GetValues("ProductID").SingleOrDefault(), out id);

                long accId = 0;
                long.TryParse(provider.FormData.GetValues("AccountID").SingleOrDefault(), out accId);

                string note = provider.FormData.GetValues("Note").SingleOrDefault();

                var products = await ProductService.GetById(id, accId);
                if (products.Count() > 0 && products.ElementAt(0).AccountID == accId)
                {
                    string nonUnicode = products.ElementAt(0).NonUnicode.Split('|')[0].Trim() + " | " + AppHelper.RemoveUnicode(note);
                    result = await ProductService.UpdateNote(accId, id, note, nonUnicode);
                }

            }
            catch (Exception ex)
            {
                ProductService.WriteError("Error in ProductController at Favorite() Method", ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, new JsonResponse
            {
                DataList = result > 0,
                TotalRows = result
            });
        }

        [Route("UpdateProductStatus")]
        [HttpPost]
        public async Task<HttpResponseMessage> UpdateProductStatus()
        {
            long result = -1;

            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                long id = 0;
                long.TryParse(provider.FormData.GetValues("ProductID").SingleOrDefault(), out id);

                long accId = 0;
                long.TryParse(provider.FormData.GetValues("AccountID").SingleOrDefault(), out accId);

                ProductStatus status = ProductStatus.Undefined;
                ProductStatus.TryParse(provider.FormData.GetValues("Status").SingleOrDefault(), out status);

                var products = await ProductService.GetById(id, accId);
                if (products.Count() > 0 && products.ElementAt(0).AccountID == accId)
                {
                    switch (status)
                    {
                        case ProductStatus.Activated:
                            result = await ProductService.UpdateStatus(accId, id, ProductStatus.Recertified);
                            break;
                        case ProductStatus.Certified:
                            // Update End Date Certified 
                            break;
                        case ProductStatus.Disabled:
                        case ProductStatus.Completed:
                        case ProductStatus.EndCertified:
                            result = await ProductService.UpdateStatus(accId, id, status);
                            break;
                        default:
                            break;
                    }
                }


            }
            catch (Exception ex)
            {
                ProductService.WriteError("Error in ProductController at Favorite() Method", ex.Message);


            }

            return Request.CreateResponse(HttpStatusCode.OK, new JsonResponse
            {
                DataList = result > 0,
                TotalRows = result
            });
        }

        [Route("UpdateInfo")]
        [HttpPost]
        public async Task<HttpResponseMessage> UpdateInfo()
        {
            long result = -1;
            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {

                await Request.Content.ReadAsMultipartAsync(provider);

                string address = provider.FormData.GetValues("Address").SingleOrDefault();

                decimal lat = 0;
                decimal.TryParse(provider.FormData.GetValues("Latitude").SingleOrDefault(), out lat);

                decimal lng = 0;
                decimal.TryParse(provider.FormData.GetValues("Longitude").SingleOrDefault(), out lng);

                int province = 0;
                int.TryParse(provider.FormData.GetValues("ProvinceID").SingleOrDefault(), out province);

                int district = 0;
                int.TryParse(provider.FormData.GetValues("DistrictID").SingleOrDefault(), out district);

                int property = 0;
                int.TryParse(provider.FormData.GetValues("PropertyID").SingleOrDefault(), out property);

                int building = 0;
                int.TryParse(provider.FormData.GetValues("BuildingID").SingleOrDefault(), out building);

                decimal deposit = 0;
                decimal.TryParse(provider.FormData.GetValues("Deposit").SingleOrDefault(), out deposit);

                decimal price = 0;
                decimal.TryParse(provider.FormData.GetValues("Price").SingleOrDefault(), out price);

                int floor = 0;
                int.TryParse(provider.FormData.GetValues("Floor").SingleOrDefault(), out floor);

                int floorCount = 0;
                int.TryParse(provider.FormData.GetValues("FloorCount").SingleOrDefault(), out floorCount);

                decimal siteArea = 0;
                decimal.TryParse(provider.FormData.GetValues("SiteArea").SingleOrDefault(), out siteArea);

                decimal grossArea = 0;
                decimal.TryParse(provider.FormData.GetValues("GrossFloorArea").SingleOrDefault(), out grossArea);

                int beds = 0;
                int.TryParse(provider.FormData.GetValues("Bedroom").SingleOrDefault(), out beds);

                int baths = 0;
                int.TryParse(provider.FormData.GetValues("Bathroom").SingleOrDefault(), out baths);

                int direction = 0;
                int.TryParse(provider.FormData.GetValues("DirectionID").SingleOrDefault(), out direction);

                decimal serviceFee = 0;
                decimal.TryParse(provider.FormData.GetValues("ServiceFee").SingleOrDefault(), out serviceFee);

                string featureList = provider.FormData.GetValues("FeatureList").SingleOrDefault();
                string furnitureList = provider.FormData.GetValues("FurnitureList").SingleOrDefault();

                int elevator = 0;
                int.TryParse(provider.FormData.GetValues("Elevator").SingleOrDefault(), out elevator);

                int pets = 0;
                int.TryParse(provider.FormData.GetValues("Pets").SingleOrDefault(), out pets);

                int numPerson = 0;
                int.TryParse(provider.FormData.GetValues("NumberPerson").SingleOrDefault(), out numPerson);

                string title = provider.FormData.GetValues("Title").SingleOrDefault();
                string content = provider.FormData.GetValues("Content").SingleOrDefault();
                string contactName = provider.FormData.GetValues("ContactName").SingleOrDefault();
                string contactPhone = provider.FormData.GetValues("ContactPhone").SingleOrDefault();

                long accID = 0;
                long.TryParse(provider.FormData.GetValues("AccountID").SingleOrDefault(), out accID);


                long productID = 0;
                long.TryParse(provider.FormData.GetValues("ProductID").SingleOrDefault(), out productID);

                result = await ProductService.UpdateInfo(new Product
                {
                    Id = productID,
                    Address = address,
                    Latitude = lat,
                    Longitude = lng,
                    ProvinceID = province,
                    DistrictID = district,
                    PropertyID = property,
                    BuildingID = building,
                    Deposit = deposit,
                    Price = price,
                    Floor = floor,
                    FloorCount = floorCount,
                    SiteArea = siteArea,
                    GrossFloorArea = grossArea,
                    Bedroom = beds,
                    Bathroom = baths,
                    DirectionID = direction,
                    ServiceFee = serviceFee,
                    FeatureList = featureList,
                    FurnitureList = furnitureList,
                    Elevator = elevator != 0,
                    Pets = pets != 0,
                    NumPerson = numPerson,
                    Title = title,
                    Content = content,
                    AccountID = accID,
                    ContactName = contactName,
                    ContactPhone = contactPhone
                });

            }
            catch (Exception ex)
            {
                ProductService.WriteError("Error in ProductController at UpdateInfo() Method", ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, new JsonResponse
            {
                DataList = result > 0,
                TotalRows = result
            });
        }

        #region Private Method
        private List<ProductViewModel> ToProductList(IEnumerable<Product> result, LanguageType language)
        {
            List<ProductViewModel> productList = new List<ProductViewModel>();

            foreach (Product obj in result)
            {
                string title = obj.Title;
                if (language == LanguageType.English)
                    title = obj.ETitle;
                else if (language == LanguageType.Korean)
                    title = obj.KTitle;

                productList.Add(new ProductViewModel
                {
                    Id = obj.Id,
                    Address = obj.Address,
                    Latitude = obj.Latitude,
                    Longitude = obj.Longitude,
                    Images = PathHelper.ProductImage(obj.Thumbnail, obj.HostIndex),
                    Price = obj.Price,
                    GrossFloorArea = obj.GrossFloorArea,
                    Title = title,
                    AccountID = obj.AccountID,
                    Status = obj.Status
                });
            }

            return productList;
        }

        private async Task<List<FeaturesViewModel>> FeaturesById(string listID, LanguageType language, ThumbType type)
        {
            List<FeaturesViewModel> featureList = new List<FeaturesViewModel>();

            if (String.IsNullOrEmpty(listID.Trim()))
                return featureList;

            var result = await FeaturesService.ListById(listID.Split(',').ToList(), type);

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
                    Thumbnail = PathHelper.Thumbnail(obj.Thumbnail, type)
                });
            }

            return featureList;

        }


        private async Task<List<DatasNhaTot>> DatasNhaTot(string price)
        {
            var listDatas = new List<DatasNhaTot>();
            bool isHCM = true;
            bool isHaNoi = true;
            bool isDaNang = true;
            bool isCanTho = true;
            bool isHaiPhong = true;

            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                if (isHCM)
                {
                    string data = string.Empty;
                    string url = "https://" + "gateway.chotot.com/v1/public/ad-listing?";
                    string prams = string.Format("cg={0}&protection_entitlement={1}&price={2}&longitude={3}&latitude={4}&distance={5}&st={6}&limit={7}&key_param_included={8}", 1010, "true", price, 106.7011391, 10.7763897, 1000, "u,h", 500, "true");
                    string WEBSERVICE_URL = url + prams;
                    var webRequest = System.Net.WebRequest.Create(WEBSERVICE_URL);
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

                        var model = JsonConvert.DeserializeObject<ProductsNhaTot>(data);

                        if (model.Data != null)
                        {
                            foreach (var product in model.Data)
                            {
                                listDatas.Add(product);
                            }
                        }
                    }
                }

                if (isHaNoi)
                {
                    string data = string.Empty;
                    string url = "https://" + "gateway.chotot.com/v1/public/ad-listing?";
                    string prams = string.Format("cg={0}&protection_entitlement={1}&price={2}&longitude={3}&latitude={4}&distance={5}&st={6}&limit={7}&key_param_included={8}", 1010, "true", price, 105.8544441, 21.0294498, 1500, "u,h", 500, "true");
                    string WEBSERVICE_URL = url + prams;
                    var webRequest = System.Net.WebRequest.Create(WEBSERVICE_URL);
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

                        var model = JsonConvert.DeserializeObject<ProductsNhaTot>(data);

                        if (model.Data != null)
                        {
                            foreach (var product in model.Data)
                            {
                                listDatas.Add(product);
                            }
                        }
                    }
                }

                if (isDaNang)
                {
                    string data = string.Empty;
                    string url = "https://" + "gateway.chotot.com/v1/public/ad-listing?";
                    string prams = string.Format("cg={0}&protection_entitlement={1}&price={2}&longitude={3}&latitude={4}&distance={5}&st={6}&limit={7}&key_param_included={8}", 1010, "true", price, 108.212, 16.068, 600, "u,h", 100, "true");
                    string WEBSERVICE_URL = url + prams;
                    var webRequest = System.Net.WebRequest.Create(WEBSERVICE_URL);
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

                        var model = JsonConvert.DeserializeObject<ProductsNhaTot>(data);

                        if (model.Data != null)
                        {
                            foreach (var product in model.Data)
                            {
                                listDatas.Add(product);
                            }
                        }
                    }
                }

                if (isCanTho)
                {
                    string data = string.Empty;
                    string url = "https://" + "gateway.chotot.com/v1/public/ad-listing?";
                    string prams = string.Format("cg={0}&protection_entitlement={1}&price={2}&longitude={3}&latitude={4}&distance={5}&st={6}&limit={7}&key_param_included={8}", 1010, "true", price, 105.7875219, 10.0364216, 700, "u,h", 50, "true");
                    string WEBSERVICE_URL = url + prams;
                    var webRequest = System.Net.WebRequest.Create(WEBSERVICE_URL);
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

                        var model = JsonConvert.DeserializeObject<ProductsNhaTot>(data);

                        if (model.Data != null)
                        {
                            foreach (var product in model.Data)
                            {
                                listDatas.Add(product);
                            }
                        }
                    }
                }

                if (isHaiPhong)
                {
                    string data = string.Empty;
                    string url = "https://" + "gateway.chotot.com/v1/public/ad-listing?";
                    string prams = string.Format("cg={0}&protection_entitlement={1}&price={2}&longitude={3}&latitude={4}&distance={5}&st={6}&limit={7}&key_param_included={8}", 1010, "true", price, 106.6749591, 20.858864, 750, "u,h", 50, "true");
                    string WEBSERVICE_URL = url + prams;
                    var webRequest = System.Net.WebRequest.Create(WEBSERVICE_URL);
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

                        var model = JsonConvert.DeserializeObject<ProductsNhaTot>(data);

                        if (model.Data != null)
                        {
                            foreach (var product in model.Data)
                            {
                                listDatas.Add(product);
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

        #endregion
    }
}