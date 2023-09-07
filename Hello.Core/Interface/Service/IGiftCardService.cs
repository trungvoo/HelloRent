using Hello.Core.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hello.Core.Interface.Service
{
    public partial interface IGiftCardService:IBaseService<GiftCard>
    {
        Task<GiftCard> GetByAccount(long accountID);
        Task<long> UpdateAccountGift(long accountID, int giftID);
    }
}
