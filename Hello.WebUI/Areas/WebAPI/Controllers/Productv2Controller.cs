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
using System.Web.Http.Results;
using System.Web.UI.WebControls;
using WebGrease;

namespace Hello.WebUI.Areas.WebAPI.Controllers
{
    [RoutePrefix("api/v2.0/Product")]
    public class Productv2Controller : ApiController
    {
        private readonly IProductService ProductService;
        private readonly IProductsService ProductsService;
        private readonly IAccountService AccountService;
        private readonly IFeaturesService FeaturesService;
        private readonly IFavouriteService _favouriteService;

        public Productv2Controller(IProductService productService, IFeaturesService FeaturesService, IAccountService AccountService, IProductsService productsService, IFavouriteService favouriteService)
        {
            this.ProductService = productService;
            this.FeaturesService = FeaturesService;
            this.AccountService = AccountService;
            this.ProductsService = productsService;
            this._favouriteService = favouriteService;
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
            var result = new List<DatasNhaTot>();

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

                double minPrice = 0;
                double.TryParse(provider.FormData.GetValues("MinPrice").SingleOrDefault(), out minPrice);
                double maxPrice = 0;
                double.TryParse(provider.FormData.GetValues("MaxPrice").SingleOrDefault(), out maxPrice);
                string price = "";
                if (maxPrice > 0)
                {
                    price = minPrice.ToString() + "-" + (maxPrice * 1000000).ToString();
                }

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

                LanguageType language = LanguageType.Vietnamese;
                LanguageType.TryParse(provider.FormData.GetValues("LanguageType").SingleOrDefault(), out language);

                //int beginRow = (pageNo - 1) * AppSettings.ItemPerPageMobile;

                //IEnumerable<Product> result = Enumerable.Empty<Product>();
                if (distance != 0 && endLat == 0 && endLng == 0)
                {
                    result = await DatasNhaTotByDistance(startLat, startLng, distance, price, pageNo, bed, propertyID);
                    foreach (var item in result)
                    {
                        var product = new ProductViewModel();
                        product.FromModel(item);
                        productList.Add(product);
                    }
                }
                else if (distance == 0 && endLat != 0 && endLng != 0)
                {
                    result = await DatasNhaTot(price, propertyID);

                    foreach (var item in result)
                    {
                        if ((Convert.ToDouble(item.latitude) > startLat && Convert.ToDouble(item.latitude) < endLat) && (Convert.ToDouble(item.longitude) > startLng && Convert.ToDouble(item.longitude) < endLng))
                        {
                            var product = new ProductViewModel();
                            product.FromModel(item);
                            productList.Add(product);
                        }
                    }
                }

                totalRows = productList.Count();

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

        [Route("Delete")]
        [HttpPost]
        public async Task<HttpResponseMessage> Delete()
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


                if (id > 0)
                {
                    result = await ProductsService.Delete(id);
                }
            }
            catch (Exception ex)
            {
                ProductService.WriteError("Error in ProductController at Delete() Method", ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, new JsonResponse
            {
                DataList = result > 0,
                TotalRows = result
            });
        }

        [Route("GetFavourite")]
        [HttpPost]
        public async Task<HttpResponseMessage> GetFavourite()
        {
            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            List<ProductsViewModel> productList = new List<ProductsViewModel>();
            int totalRows = 0;

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                long accId = 0;
                long.TryParse(provider.FormData.GetValues("AccountID").SingleOrDefault(), out accId);

                if (accId > 0)
                {
                    var result = await ProductsService.GetFavourite(accId);
                    foreach (var item in result)
                    {
                        var product = new ProductsViewModel();
                        product.ProductID = item.ProductID;
                        product.AccountID = item.AccountID;
                        product.Avatar = item.Avatar;
                        product.Title = item.Title;
                        product.Address = item.Address;
                        product.GrossFloorArea = item.GrossFloorArea;
                        product.Price = item.Price;
                        product.Status = item.Status;
                        product.CreateDate = item.CreateDate;
                        product.ModifiedDate = item.ModifiedDate;

                        productList.Add(product);
                    }
                }

                totalRows = productList.Count();

            }
            catch (Exception ex)
            {
                ProductService.WriteError("Error in ProductController at GetFavourite() Method", ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, new JsonResponse
            {
                TotalRows = totalRows,
                DataList = productList
            });
        }

        [Route("GetRecentlyViewed")]
        [HttpPost]
        public async Task<HttpResponseMessage> GetRecentlyViewed()
        {
            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            List<ProductsViewModel> productList = new List<ProductsViewModel>();
            int totalRows = 0;

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                long accId = 0;
                long.TryParse(provider.FormData.GetValues("AccountID").SingleOrDefault(), out accId);

                if (accId > 0)
                {
                    var result = await ProductsService.GetRecentlyViewed(accId);
                    foreach (var item in result)
                    {
                        var product = new ProductsViewModel();
                        product.ProductID = item.ProductID;
                        product.AccountID = item.AccountID;
                        product.Avatar = item.Avatar;
                        product.Title = item.Title;
                        product.Price = item.Price;
                        product.GrossFloorArea = item.GrossFloorArea;
                        product.Address = item.Address;
                        product.Status = item.Status;
                        product.CreateDate = item.CreateDate;
                        product.ModifiedDate = item.ModifiedDate;

                        productList.Add(product);
                    }
                }

                totalRows = productList.Count();

            }
            catch (Exception ex)
            {
                ProductService.WriteError("Error in ProductController at GetRecentlyViewed() Method", ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, new JsonResponse
            {
                TotalRows = totalRows,
                DataList = productList
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


                var result = await DatasNhaTot_Detail(id);
                int isLike = 0;

                if (accId > 0)
                {
                    await ProductsService.Insert(new ProductsModel
                    {
                        ProductID = result.list_id,
                        Title = result.subject,
                        Avatar = result.images[0],
                        Address = result.detail_address,
                        Status = result.state.ToLower() == "accepted" ? ProductStatus.Activated : ProductStatus.Failed,
                        Price = Convert.ToDecimal(result.price) / 1000000,
                        GrossFloorArea = result.size,
                    }, Convert.ToInt32(accId));

                    var isLikeProduct = await _favouriteService.GetFavouriteByAccountProduct(accId, id);
                    if (isLikeProduct.Count() > 0)
                    {
                        isLike = 1;
                    }
                }

                model.Add(new ProductViewModel
                {
                    Id = result.list_id,
                    Address = result.detail_address,
                    Latitude = Convert.ToDecimal(result.latitude),
                    Longitude = Convert.ToDecimal(result.longitude),
                    Images = string.Join(", ", result.images),
                    Price = Convert.ToDecimal(result.price) / 1000000,
                    SiteArea = result.size,
                    GrossFloorArea = result.size,
                    Title = result.subject,
                    Content = result.body,
                    PropertyName = result.type_name,
                    ContactName = "Property Hero",
                    ContactPhone = "0971027021",
                    IsMeLikeThis = isLike,
                    Status = result.state == "accepted" ? ProductStatus.Activated : ProductStatus.Failed
                });

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
                {
                    result = await _favouriteService.Update(new Favourite
                    {
                        ProductID = id,
                        AccountID = accId
                    });
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

        [Route("Favorite/Delete")]
        [HttpPost]
        public async Task<HttpResponseMessage> FavoriteDelete()
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
                {
                    result = await _favouriteService.Delete(Convert.ToInt32(id), Convert.ToInt32(accId));
                }
            }
            catch (Exception ex)
            {
                ProductService.WriteError("Error in ProductController at FavoriteDelete() Method", ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, new JsonResponse
            {
                DataList = result > 0,
                TotalRows = result
            });
        }

        [Route("Favorite/Check")]
        [HttpPost]
        public async Task<HttpResponseMessage> FavoriteCheck()
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
                {
                    var islike = await _favouriteService.GetFavouriteByAccountProduct(accId, id);
                    if (islike.Count() > 0 )
                    {
                        result = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                ProductService.WriteError("Error in ProductController at FavoriteDelete() Method", ex.Message);
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


        private async Task<List<DatasNhaTot>> DatasNhaTot(string price, int propertyID)
        {
            var listDatas = new List<DatasNhaTot>();
            bool isHCM = true;
            bool isHaNoi = true;
            bool isDaNang = true;
            bool isCanTho = true;
            bool isHaiPhong = true;
            int cg = 1000;
            if (propertyID != 1000)
            {
                cg = propertyID;
            }

            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                if (isHCM)
                {
                    string data = string.Empty;
                    string url = "https://" + "gateway.chotot.com/v1/public/ad-listing?";
                    string prams = string.Format("cg={0}&protection_entitlement={1}&price={2}&longitude={3}&latitude={4}&distance={5}&st={6}&limit={7}&key_param_included={8}", cg, "true", price, 106.7011391, 10.7763897, 1000, "u,h", 500, "true");
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
                    string prams = string.Format("cg={0}&protection_entitlement={1}&price={2}&longitude={3}&latitude={4}&distance={5}&st={6}&limit={7}&key_param_included={8}", cg, "true", price, 105.8544441, 21.0294498, 1500, "u,h", 500, "true");
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
                    string prams = string.Format("cg={0}&protection_entitlement={1}&price={2}&longitude={3}&latitude={4}&distance={5}&st={6}&limit={7}&key_param_included={8}", cg, "true", price, 108.212, 16.068, 600, "u,h", 100, "true");
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
                    string prams = string.Format("cg={0}&protection_entitlement={1}&price={2}&longitude={3}&latitude={4}&distance={5}&st={6}&limit={7}&key_param_included={8}", cg, "true", price, 105.7875219, 10.0364216, 700, "u,h", 50, "true");
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
                    string prams = string.Format("cg={0}&protection_entitlement={1}&price={2}&longitude={3}&latitude={4}&distance={5}&st={6}&limit={7}&key_param_included={8}", cg, "true", price, 106.6749591, 20.858864, 750, "u,h", 50, "true");
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


        private async Task<List<DatasNhaTot>> DatasNhaTotByDistance(float lat, float lng, float distance, string price, int page, int rooms, int propertyID)
        {
            var listDatas = new List<DatasNhaTot>();

            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;


                string data = string.Empty;
                string url = "https://" + "gateway.chotot.com/v1/public/ad-listing?";
                string pramPrice = "";
                if (price.Length > 0)
                {
                    pramPrice = price;
                }
                int room = 0;
                if (rooms > 0)
                {
                    room = rooms;
                }
                int cg = 1000;
                if (propertyID != 1000)
                {
                    cg = propertyID;
                }
                string prams = string.Format("cg={0}&rooms={1}&protection_entitlement={2}&price={3}&longitude={4}&latitude={5}&distance={6}&o={7}&st={8}&page={9}&limit={10}&key_param_included={11}", cg, room, "true", pramPrice, lng, lat, distance, page * 10, "u,h", page, 20, "true");
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

                return listDatas;
            }
            catch (Exception)
            {
                return listDatas;
            }

        }


        private async Task<DatasNhaTot_Detail> DatasNhaTot_Detail(long productID)
        {
            var listDatas = new DatasNhaTot_Detail();

            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;


                string data = string.Empty;
                string url = "https://" + "gateway.chotot.com/v2/public/ad-listing/";
                string WEBSERVICE_URL = string.Format(url + "{0}", productID);
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

                    var model = JsonConvert.DeserializeObject<DetailNhatot>(data);

                    if (model.Data != null)
                    {
                        listDatas = model.Data;
                    }
                    foreach (var item in model.parameters)
                    {
                        if (item.id == "address")
                        {
                            listDatas.detail_address = item.value;
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