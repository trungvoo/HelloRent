using Hello.Common.Helper;
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
using System.Web.Http.Results;

namespace Hello.WebUI.Areas.WebAPI.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private readonly IAccountService AccountService;
        private readonly IDeviceService DeviceService;

        public AccountController(IAccountService AccountService, IDeviceService DeviceService)
        {
            this.AccountService = AccountService;
            this.DeviceService = DeviceService;
        }


        [Route("Verify/UserName={userName}")]
        [HttpGet]
        public async Task<HttpResponseMessage> Verify(string userName)
        {
            List<AccountInfo> accountList = new List<AccountInfo>();

            var result = await AccountService.Verify(userName, AccountType.HelloRent);

            if (result != null)
            {
                accountList.Add(new AccountInfo
                {
                    Id = result.Id,
                    UserName = result.UserName,
                    FullName = result.FullName,
                    Gender = result.Gender,
                    BirthDate = result.BirthDate,
                    PhoneNumber = result.PhoneNumber,
                    Email = result.Email,
                    Address = result.Address,
                    CountryID = result.CountryID,
                    ProvinceID = result.ProvinceID,
                    DistrictID = result.DistrictID,
                    Avatar = result.Avatar,
                    IDCode = result.IDCode,
                    IssuedDate = result.IssuedDate,
                    IssuedPlace = result.IssuedPlace,
                    AccountRole = result.AccountRole,
                    AccountType = result.AccountType,
                    Status = result.Status
                });
            }

            return Request.CreateResponse(HttpStatusCode.OK, new JsonResponse
            {
                DataList = accountList,
                TotalRows = accountList.Count
            });
        }

        [Route("MemberRegistration")]
        [HttpPost]
        public async Task<HttpResponseMessage> MemberRegistration()
        {

            long result = -1;

            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                string userName = provider.FormData.GetValues("UserName").SingleOrDefault();
                string password = provider.FormData.GetValues("Password").SingleOrDefault();
                string fullName = provider.FormData.GetValues("FullName").SingleOrDefault();
                string phoneNumber = provider.FormData.GetValues("PhoneNumber").SingleOrDefault();

                bool isEmail = AppHelper.isEmail(userName);

                var account = new Account
                {
                    UserName = userName,
                    Password = password,
                    FullName = fullName,
                    Gender = Gender.Male,
                    BirthDate = AppSettings.DefaultDate,
                    PhoneNumber = phoneNumber,
                    Email = isEmail ? userName : "",
                    Address = "",
                    CountryID = (int)DefaultLocation.Country,
                    ProvinceID = (int)DefaultLocation.Province,
                    DistrictID = (int)DefaultLocation.Disttrict,
                    Avatar = "",
                    AvatarHostIndex = HostIndex.CurrentHost,
                    RegisterDate = DateTime.Now,
                    IDCode = "",
                    IssuedDate = AppSettings.DefaultDate,
                    IssuedPlace = "",
                    AccountRole = AccountRole.Member,
                    AccountType = AccountType.HelloRent,
                    Status = AccountStatus.New
                };

                result = await AccountService.Register(account);

            }
            catch (Exception ex)
            {
                AccountService.WriteError("Error in AccountController at AccountLogin() Method", ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, new JsonResponse
            {
                DataList = result > 0,
                TotalRows = result
            });
        }

        [Route("AccountLogin")]
        [HttpPost]
        public async Task<HttpResponseMessage> AccountLogin()
        {

            List<AccountInfo> accountList = new List<AccountInfo>();

            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                string userName = provider.FormData.GetValues("UserName").SingleOrDefault();
                string password = provider.FormData.GetValues("Password").SingleOrDefault();

                string token = provider.FormData.GetValues("Token").SingleOrDefault();

                DeviceType deviceType = 0;
                DeviceType.TryParse(provider.FormData.GetValues("DeviceType").SingleOrDefault(), out deviceType);

                string version = provider.FormData.GetValues("Version").SingleOrDefault();

                string addr = provider.FormData.GetValues("Address").SingleOrDefault();

                decimal lat = 0;
                decimal.TryParse(provider.FormData.GetValues("Latitude").SingleOrDefault(), out lat);

                decimal lng = 0;
                decimal.TryParse(provider.FormData.GetValues("Longitude").SingleOrDefault(), out lng);


                var result = await AccountService.Login(userName, password);

                accountList = await AsyncLogin(result, token, deviceType, version, addr, lat, lng);

            }
            catch (Exception ex)
            {
                AccountService.WriteError("Error in AccountController at AccountLogin() Method", ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, new JsonResponse
            {
                DataList = accountList,
                TotalRows = accountList.Count
            });
        }

        [Route("SocialLogin")]
        [HttpPost]
        public async Task<HttpResponseMessage> SocialLogin()
        {
            List<AccountInfo> accountList = new List<AccountInfo>();

            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                string userName = provider.FormData.GetValues("UserName").SingleOrDefault();
                string fullName = provider.FormData.GetValues("FullName").SingleOrDefault();
                string email = provider.FormData.GetValues("Email").SingleOrDefault();

                string strBirthDate = provider.FormData.GetValues("BirthDate").SingleOrDefault();

                Gender gender = 0;
                Gender.TryParse(provider.FormData.GetValues("Gender").SingleOrDefault(), out gender);

                AccountType accType = 0;
                AccountType.TryParse(provider.FormData.GetValues("AccountType").SingleOrDefault(), out accType);

                string token = provider.FormData.GetValues("Token").SingleOrDefault();

                DeviceType deviceType = 0;
                DeviceType.TryParse(provider.FormData.GetValues("DeviceType").SingleOrDefault(), out deviceType);

                string version = provider.FormData.GetValues("Version").SingleOrDefault();

                string addr = provider.FormData.GetValues("Address").SingleOrDefault();

                decimal lat = 0;
                decimal.TryParse(provider.FormData.GetValues("Latitude").SingleOrDefault(), out lat);

                decimal lng = 0;
                decimal.TryParse(provider.FormData.GetValues("Longitude").SingleOrDefault(), out lng);

                var result = await AccountService.SocialLogin(new Account
                {
                    UserName = userName,
                    FullName = fullName,
                    Gender = gender,
                    BirthDate = String.IsNullOrEmpty(strBirthDate) ? AppSettings.DefaultDate : AppHelper.StringToDate(strBirthDate),
                    Email = email,
                    CountryID = (int)DefaultLocation.Country,
                    ProvinceID = (int)DefaultLocation.Province,
                    DistrictID = (int)DefaultLocation.Disttrict,
                    RegisterDate = DateTime.Now,
                    AccountType = accType
                });

                if (String.IsNullOrEmpty(token) && result != null && result.Status != AccountStatus.Disabled)
                {
                    accountList.Add(new AccountInfo
                    {
                        Id = result.Id,
                        UserName = result.UserName,
                        FullName = result.FullName,
                        Gender = result.Gender,
                        BirthDate = result.BirthDate,
                        PhoneNumber = result.PhoneNumber,
                        Email = result.Email,
                        Address = result.Address,
                        CountryID = result.CountryID,
                        ProvinceID = result.ProvinceID,
                        DistrictID = result.DistrictID,
                        Avatar = PathHelper.Avatar(result.Avatar, result.AvatarHostIndex),
                        IDCode = result.IDCode,
                        IssuedDate = result.IssuedDate,
                        IssuedPlace = result.IssuedPlace,
                        AccountRole = result.AccountRole,
                        AccountType = result.AccountType,
                        Status = result.Status
                    });

                }
                else
                    accountList = await AsyncLogin(result, token, deviceType, version, addr, lat, lng);

            }
            catch (Exception ex)
            {
                AccountService.WriteError("Error in AccountController at AccountLogin() Method", ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, new JsonResponse
            {
                DataList = accountList,
                TotalRows = accountList.Count
            });
        }

        [Route("UpdatePhoneNumber/UserID={userID}/AccountID={accountID}/PhoneNumber={phoneNumber}")]
        [HttpGet]
        public async Task<HttpResponseMessage> UpdatePhoneNumber(long userID, long accountID, string phoneNumber)
        {
            var result = await AccountService.UpdatePhoneNumber(userID, accountID, phoneNumber);

            return Request.CreateResponse(HttpStatusCode.OK, new JsonResponse
            {
                DataList = result > 0,
                TotalRows = result
            });
        }

        [Route("GetDetails/AccountID={accountID}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetDetails(long accountID)
        {
            List<AccountInfo> accountList = new List<AccountInfo>();

            var result = await AccountService.GetDetails(accountID);
            if (result != null)
            {
                accountList.Add(new AccountInfo
                {
                    Id = result.Id,
                    UserName = result.UserName,
                    FullName = result.FullName,
                    Gender = result.Gender,
                    BirthDate = DateTime.Compare(result.BirthDate, AppSettings.DefaultDate) != 0 ? result.BirthDate : DateTime.Now,
                    PhoneNumber = result.PhoneNumber,
                    Email = result.Email,
                    Address = result.Address,
                    CountryID = result.CountryID,
                    ProvinceID = result.ProvinceID,
                    DistrictID = result.DistrictID,
                    Avatar = PathHelper.Avatar(result.Avatar, result.AvatarHostIndex),
                    IDCode = result.IDCode,
                    IssuedDate = result.IssuedDate,
                    IssuedPlace = result.IssuedPlace,
                    AccountRole = result.AccountRole,
                    AccountType = result.AccountType,
                    Status = result.Status
                });
            }

            return Request.CreateResponse(HttpStatusCode.OK, new JsonResponse
            {
                DataList = accountList,
                TotalRows = accountList.Count
            });
        }

        [Route("ChangeAvatar")]
        [HttpPost]
        public async Task<HttpResponseMessage> ChangeAvatar()
        {
            long result = -1;

            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            string root = HttpContext.Current.Server.MapPath("~/" + AppSettings.AvatarPath);
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                string userName = provider.FormData.GetValues("UserName").SingleOrDefault();

                long accountID = 0;
                long.TryParse(provider.FormData.GetValues("AccountID").SingleOrDefault(), out accountID);

                string avatar = String.Empty;
                foreach (MultipartFileData file in provider.FileData)
                {
                    if (!String.IsNullOrEmpty(file.Headers.ContentDisposition.FileName))
                        avatar = FileHelper.SaveImageFile(file, userName, root);
                }

                result = await AccountService.ChangeAvatar(accountID, avatar);

            }
            catch (Exception ex)
            {
                AccountService.WriteError("Error in AccountController at ChangeAvatar() Method", ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, new JsonResponse
            {
                DataList = result > 0,
                TotalRows = result
            });
        }

        [Route("ChangePassword/AccountID={accountID}/Password={password}")]
        [HttpGet]
        public async Task<HttpResponseMessage> ChangePassword(long accountID, string password)
        {
            var result = await AccountService.ChangePassword(accountID, password);

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

            string root = HttpContext.Current.Server.MapPath("~/" + AppSettings.AvatarPath);
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                long accountID = 0;
                long.TryParse(provider.FormData.GetValues("AccountID").SingleOrDefault(), out accountID);

                Gender gender = 0;
                Gender.TryParse(provider.FormData.GetValues("Gender").SingleOrDefault(), out gender);

                string fullName = provider.FormData.GetValues("FullName").SingleOrDefault();
                string strBirthDate = provider.FormData.GetValues("BirthDate").SingleOrDefault();
                string phoneNumber = provider.FormData.GetValues("PhoneNumber").SingleOrDefault();
                string email = provider.FormData.GetValues("Email").SingleOrDefault();
                string address = provider.FormData.GetValues("Address").SingleOrDefault();
                string iDCode = provider.FormData.GetValues("IDCode").SingleOrDefault();
                string issuedDate = provider.FormData.GetValues("IssuedDate").SingleOrDefault();
                string issuedPlace = provider.FormData.GetValues("IssuedPlace").SingleOrDefault();

                int countryID = 0;
                int.TryParse(provider.FormData.GetValues("CountryID").SingleOrDefault(), out countryID);

                int provinceID = 0;
                int.TryParse(provider.FormData.GetValues("ProvinceID").SingleOrDefault(), out provinceID);

                int districtID = 0;
                int.TryParse(provider.FormData.GetValues("DistrictID").SingleOrDefault(), out districtID);


                DateTime birthDate = String.IsNullOrEmpty(strBirthDate) ? AppSettings.DefaultDate : AppHelper.StringToDate(strBirthDate);

                result = await AccountService.UpdateInfo(new Account
                {
                    Id = accountID,
                    FullName = fullName,
                    Gender = gender,
                    BirthDate = birthDate,
                    PhoneNumber = phoneNumber,
                    Email = email,
                    Address = address,
                    CountryID = countryID,
                    ProvinceID = provinceID,
                    DistrictID = districtID,
                    IDCode = iDCode,
                    IssuedDate = AppHelper.StringToDate(issuedDate),
                    IssuedPlace = issuedPlace,
                }, accountID);

            }
            catch (Exception ex)
            {
                AccountService.WriteError("Error in AccountController at UpdateInfo() Method", ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, new JsonResponse
            {
                DataList = result > 0,
                TotalRows = result
            });
        }

        [Route("AccountDelete")]
        [HttpPost]
        public async Task<HttpResponseMessage> AccountDelete()
        {

            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                string userName = provider.FormData.GetValues("UserName").SingleOrDefault();
                string password = provider.FormData.GetValues("Password").SingleOrDefault();

                var result = await AccountService.Delete(userName, password, AccountStatus.Disabled);

                return Request.CreateResponse(HttpStatusCode.OK, new JsonResponse
                {
                    TotalRows = result,
                    DataList = result > 0
                });

            }
            catch (Exception ex)
            {
                AccountService.WriteError("Error in AccountController at AccountDelete() Method", ex.Message);
                return Request.CreateResponse(HttpStatusCode.OK, new JsonResponse
                {
                    DataList = false,
                    TotalRows = 0
                });
            }
        }

        #region Private Funcion
        private async Task<List<AccountInfo>> AsyncLogin(Account result, String token, DeviceType deviceType, string version, string addr, decimal lat, decimal lng)
        {
            List<AccountInfo> accountList = new List<AccountInfo>();
            if (result != null)
            {

                //await DeviceService.LoginHistory(result.Id, token, DateTime.Now, "0.0.0", deviceType, version, addr, lat, lng);

                accountList.Add(new AccountInfo
                {
                    Id = result.Id,
                    UserName = result.UserName,
                    FullName = result.FullName,
                    Gender = result.Gender,
                    BirthDate = result.BirthDate,
                    PhoneNumber = result.PhoneNumber,
                    Email = result.Email,
                    Address = result.Address,
                    CountryID = result.CountryID,
                    ProvinceID = result.ProvinceID,
                    DistrictID = result.DistrictID,
                    Avatar = PathHelper.Avatar(result.Avatar, result.AvatarHostIndex),
                    IDCode = result.IDCode,
                    IssuedDate = result.IssuedDate,
                    IssuedPlace = result.IssuedPlace,
                    AccountRole = result.AccountRole,
                    AccountType = result.AccountType,
                    Status = result.Status
                });
            }

            return accountList;
        }

        #endregion

    }
}
