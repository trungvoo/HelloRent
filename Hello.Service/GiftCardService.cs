using Hello.Common.Parameter;
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
    public partial class GiftCardService : BaseService<GiftCard>, IGiftCardService
    {
        public GiftCardService(IRepository<GiftCard> repository) : base(repository) { }
    }

    public partial class GiftCardService : IGiftCardService
    {
        public async Task<GiftCard> GetByAccount(long accountID)
        {
            try
            {
                ParamItem[] arr = new ParamItem[] { new ParamItem("AccountID", SqlDbType.BigInt, accountID) };

                return await Task.FromResult(base.SqlQuery("pro_GiftCard_GetByAccount", Params.Create(arr)).SingleOrDefault());
            }
            catch (Exception ex)
            {
                base.WriteError("Error in GiftCardService at GetByAccount() Method", ex.Message);
            }

            return null;
        }

        public async Task<long> UpdateAccountGift(long accountID, int giftID)
        {
            try
            {
                ParamItem[] arr = new ParamItem[] { new ParamItem("AccountID", SqlDbType.BigInt, accountID),
                                                    new ParamItem("GiftID", SqlDbType.Int, giftID)};

                return await Task.FromResult(base.ExecuteSql("pro_AccountGift_Update", Params.Create(arr)));
            }
            catch (Exception ex)
            {
                base.WriteError("Error in GiftCardService at UpdateAccountGift() Method", ex.Message);
            }

            return -1;
        }
    }

}
