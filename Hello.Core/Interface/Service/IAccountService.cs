using Hello.Common.Utils;
using Hello.Core.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hello.Core.Interface.Service
{
    public partial interface IAccountService : IBaseService<Account>
    {
        Task<Account> Verify(string userName, AccountType type);
        Task<long> Register(Account account);
        Task<Account> Login(string userName, string password);
        Task<long> UpdateStatus(long userID, long accountID, AccountStatus status);
        Task<long> Delete(string userName, string password, AccountStatus status);
        Task<Account> SocialLogin(Account account);
        Task<long> UpdatePhoneNumber(long userID, long accountID, string phoneNumber);
        Task<Account> GetDetails(long accountID);
        Task<long> ChangeAvatar(long accountID, string avatar);
        Task<long> ChangePassword(long accountID, string password);
        Task<long> UpdateInfo(Account account, long userID);
        Task<long> UpdateRole(long accountID, long employeeID, AccountRole role);
    }
}
