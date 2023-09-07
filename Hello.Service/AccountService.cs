using Hello.Common.Parameter;
using Hello.Common.Utils;
using Hello.Core.Interface.Data;
using Hello.Core.Interface.Service;
using Hello.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.Service
{
    public partial class AccountService : BaseService<Account>, IAccountService
    {
        public AccountService(IRepository<Account> repository) : base(repository) { }
    }

    public partial class AccountService : IAccountService
    {

        public async Task<Account> Verify(string userName, AccountType type)
        {
            try
            {
                ParamItem[] arr = new ParamItem[] { new ParamItem("UserName", SqlDbType.VarChar, userName),
                                                    new ParamItem("AccountType", SqlDbType.TinyInt, (int)type) };

                return await Task.FromResult(base.SqlQuery("pro_Account_Verify", Params.Create(arr)).SingleOrDefault());

            }
            catch (Exception ex)
            {
                base.WriteError("Error in AccountService at Verify() Method", ex.Message);
            }

            return null;
        }

        public async Task<long> Register(Account account)
        {
            try
            {
                ParamItem[] arr = new ParamItem[] { new ParamItem("UserName", SqlDbType.VarChar, account.UserName),
                                                    new ParamItem("Password", SqlDbType.VarChar, Encrypt.MD5Encrypt(account.Password)),
                                                    new ParamItem("FullName", SqlDbType.NVarChar, account.FullName),
                                                    new ParamItem("Gender", SqlDbType.TinyInt, (int)account.Gender),
                                                    new ParamItem("BirthDate", SqlDbType.DateTime, account.BirthDate),
                                                    new ParamItem("PhoneNumber", SqlDbType.VarChar, account.PhoneNumber),
                                                    new ParamItem("Email", SqlDbType.VarChar, account.Email),
                                                    new ParamItem("Address", SqlDbType.NVarChar, account.Address),
                                                    new ParamItem("CountryID", SqlDbType.Int, account.CountryID),
                                                    new ParamItem("ProvinceID", SqlDbType.Int, account.ProvinceID),
                                                    new ParamItem("DistrictID", SqlDbType.Int, account.DistrictID),
                                                    new ParamItem("Avatar", SqlDbType.VarChar, account.Avatar),
                                                    new ParamItem("AvatarHostIndex", SqlDbType.TinyInt, (int)account.AvatarHostIndex),
                                                    new ParamItem("RegisterDate", SqlDbType.DateTime, account.RegisterDate),
                                                    new ParamItem("IDCode", SqlDbType.VarChar, account.IDCode),
                                                    new ParamItem("IssuedDate", SqlDbType.DateTime, account.IssuedDate),
                                                    new ParamItem("IssuedPlace", SqlDbType.NVarChar, account.IssuedPlace),
                                                    new ParamItem("AccountRole", SqlDbType.TinyInt, (int)account.AccountRole),
                                                    new ParamItem("AccountType", SqlDbType.TinyInt, (int)account.AccountType)};

                return await Task.FromResult(base.ExecuteSql("pro_Account_Insert", Params.Create(arr)));
            }
            catch (Exception ex)
            {
                base.WriteError("Error in AccountService at Register() Method", ex.Message);
            }

            return -1;
        }


        public async Task<Account> Login(string userName, string password)
        {
            try
            {
                ParamItem[] arr = new ParamItem[] { new ParamItem("UserName", SqlDbType.VarChar, userName),
                                                    new ParamItem("Password", SqlDbType.VarChar, Encrypt.MD5Encrypt(password))};

                return await Task.FromResult(base.SqlQuery("pro_Account_Login", Params.Create(arr)).SingleOrDefault());
            }
            catch (Exception ex)
            {
                base.WriteError("Error in AccountService at Login() Method", ex.Message);
            }

            return null;
        }


        public async Task<long> UpdateStatus(long userID, long accountID, AccountStatus status)
        {
            try
            {
                ParamItem[] arr = new ParamItem[] { new ParamItem("UserID", SqlDbType.BigInt, userID),
                                                    new ParamItem("AccountID", SqlDbType.BigInt, accountID),
                                                    new ParamItem("Status", SqlDbType.TinyInt, (int)status)};

                return await Task.FromResult(base.ExecuteSql("pro_Account_UpdateStatus", Params.Create(arr)));
            }
            catch (Exception ex)
            {
                base.WriteError("Error in AccountService at UpdateStatus() Method", ex.Message);
            }

            return -1;
        }


        public async Task<Account> SocialLogin(Account account)
        {
            try
            {
                ParamItem[] arr = new ParamItem[] { new ParamItem("UserName", SqlDbType.VarChar, account.UserName),
                                                    new ParamItem("FullName", SqlDbType.NVarChar, account.FullName),
                                                    new ParamItem("Gender", SqlDbType.TinyInt, (int)account.Gender),
                                                    new ParamItem("BirthDate", SqlDbType.DateTime, account.BirthDate),
                                                    new ParamItem("Email", SqlDbType.VarChar, account.Email),
                                                    new ParamItem("CountryID", SqlDbType.Int, account.CountryID),
                                                    new ParamItem("ProvinceID", SqlDbType.Int, account.ProvinceID),
                                                    new ParamItem("DistrictID", SqlDbType.Int, account.DistrictID),
                                                    new ParamItem("RegisterDate", SqlDbType.DateTime, account.RegisterDate),
                                                    new ParamItem("AccountType", SqlDbType.TinyInt, (int)account.AccountType)};

                return await Task.FromResult(base.SqlQuery("pro_Account_SocialLogin", Params.Create(arr)).SingleOrDefault());
            }
            catch (Exception ex)
            {
                base.WriteError("Error in AccountService at SocialLogin() Method", ex.Message);
            }

            return null;
        }


        public async Task<long> UpdatePhoneNumber(long userID, long accountID, string phoneNumber)
        {
            try
            {
                ParamItem[] arr = new ParamItem[] { new ParamItem("UserID", SqlDbType.BigInt, userID),
                                                    new ParamItem("AccountID", SqlDbType.BigInt, accountID),
                                                    new ParamItem("Status", SqlDbType.VarChar, phoneNumber)};

                long result = await Task.FromResult(base.ExecuteSql("pro_Account_UpdatePhoneNumber", Params.Create(arr)));
                if (result > 0)
                {
                    await UpdateStatus(userID, accountID, AccountStatus.Activated);
                    return result;
                }
            }
            catch (Exception ex)
            {
                base.WriteError("Error in AccountService at UpdatePhoneNumber() Method", ex.Message);
            }

            return -1;
        }


        public async Task<Account> GetDetails(long accountID)
        {
            try
            {
                ParamItem[] arr = new ParamItem[] { new ParamItem("AccountID", SqlDbType.BigInt, accountID) };

                return await Task.FromResult(base.SqlQuery("pro_Account_GetDetails", Params.Create(arr)).SingleOrDefault());
            }
            catch (Exception ex)
            {
                base.WriteError("Error in AccountService at UpdateStatus() Method", ex.Message);
            }

            return null;
        }


        public async Task<long> ChangeAvatar(long accountID, string avatar)
        {
            try
            {
                ParamItem[] arr = new ParamItem[] { new ParamItem("AccountID", SqlDbType.BigInt, accountID),
                                                    new ParamItem("Avatar", SqlDbType.VarChar, avatar)};

                return await Task.FromResult(base.ExecuteSql("pro_Account_ChangeAvatar", Params.Create(arr)));
            }
            catch (Exception ex)
            {
                base.WriteError("Error in AccountService at ChangeAvatar() Method", ex.Message);
            }

            return -1;
        }

        public async Task<long> ChangePassword(long accountID, string password)
        {
            try
            {
                ParamItem[] arr = new ParamItem[] { new ParamItem("AccountID", SqlDbType.BigInt, accountID),
                                                    new ParamItem("Password", SqlDbType.VarChar, Encrypt.MD5Encrypt(password))};

                return await Task.FromResult(base.ExecuteSql("pro_Account_ChangePassword", Params.Create(arr)));
            }
            catch (Exception ex)
            {
                base.WriteError("Error in AccountService at ChangePassword() Method", ex.Message);
            }

            return -1;
        }


        public async Task<long> UpdateInfo(Account account, long userID)
        {
            try
            {
                ParamItem[] arr = new ParamItem[] { new ParamItem("UserID", SqlDbType.BigInt, userID),
                                                    new ParamItem("AccountID", SqlDbType.BigInt, account.Id),
                                                    new ParamItem("FullName", SqlDbType.NVarChar, account.FullName),
                                                    new ParamItem("Gender", SqlDbType.TinyInt, (int)account.Gender),
                                                    new ParamItem("BirthDate", SqlDbType.DateTime, account.BirthDate),
                                                    new ParamItem("PhoneNumber", SqlDbType.VarChar, account.PhoneNumber),
                                                    new ParamItem("Email", SqlDbType.VarChar, account.Email),
                                                    new ParamItem("Address", SqlDbType.NVarChar, account.Address),
                                                    new ParamItem("CountryID", SqlDbType.Int, account.CountryID),
                                                    new ParamItem("ProvinceID", SqlDbType.Int, account.ProvinceID),
                                                    new ParamItem("DistrictID", SqlDbType.Int, account.DistrictID),
                                                    new ParamItem("IDCode", SqlDbType.VarChar, account.IDCode),
                                                    new ParamItem("IssuedDate", SqlDbType.DateTime, account.IssuedDate),
                                                    new ParamItem("IssuedPlace", SqlDbType.NVarChar, account.IssuedPlace)};

                return await Task.FromResult(base.ExecuteSql("pro_Account_UpdateInfo", Params.Create(arr)));
            }
            catch (Exception ex)
            {
                base.WriteError("Error in AccountService at UpdateInfo() Method", ex.Message);
            }

            return -1;
        }


        public async Task<long> UpdateRole(long accountID, long employeeID, AccountRole role)
        {
            try
            {
                ParamItem[] arr = new ParamItem[] { new ParamItem("AccountID", SqlDbType.BigInt, accountID),
                                                    new ParamItem("EmployeeID", SqlDbType.BigInt, employeeID),
                                                    new ParamItem("AccountRole", SqlDbType.TinyInt, (int)role)};

                return await Task.FromResult(base.ExecuteSql("pro_Account_UpdateRole", Params.Create(arr)));
            }
            catch (Exception ex)
            {
                base.WriteError("Error in AccountService at UpdateRole() Method", ex.Message);
            }

            return -1;
        }

        public async Task<long> Delete(string userName, string password, AccountStatus status)
        {
            try
            {
                ParamItem[] arr = new ParamItem[] { new ParamItem("UserName", SqlDbType.VarChar, userName),
                                                    new ParamItem("Password", SqlDbType.VarChar, Encrypt.MD5Encrypt(password)),
                                                    new ParamItem("AccountType", SqlDbType.TinyInt, status)};

                return await Task.FromResult(base.ExecuteSql("pro_Account_Delete", Params.Create(arr)));

            }
            catch (Exception ex)
            {
                base.WriteError("Error in AccountService at Delete() Method", ex.Message);
            }

            return -1;
        }
    }
}
