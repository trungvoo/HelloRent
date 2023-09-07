using Hello.Core.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hello.Core.Interface.Service
{
    public partial interface IRecentlyViewedService : IBaseService<RecentlyViewed>
    {
        Task<long> Update(RecentlyViewed model);

        Task<long> Delete(long ProductID, long AccountID);
    }
}
