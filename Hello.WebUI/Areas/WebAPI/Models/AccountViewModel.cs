using Hello.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hello.WebUI.Areas.WebAPI.Models
{
    public class AccountInfo
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int CountryID { get; set; }
        public int ProvinceID { get; set; }
        public int DistrictID { get; set; }
        public string Avatar { get; set; }
        public string IDCode { get; set; }
        public DateTime IssuedDate { get; set; }
        public string IssuedPlace { get; set; }
        public AccountRole AccountRole { get; set; }
        public AccountType AccountType { get; set; }
        public AccountStatus Status { get; set; }
    }
}