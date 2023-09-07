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
    public partial class RecentlyViewedService : BaseService<RecentlyViewed>, IRecentlyViewedService
    {
        public RecentlyViewedService(IRepository<RecentlyViewed> repository) : base(repository) { }
    }

    public partial class RecentlyViewedService : IRecentlyViewedService
    {
        public async Task<long> Update(RecentlyViewed recentlyViewed)
        {
            try
            {
                ParamItem[] arr = new ParamItem[] { new ParamItem("ProductID", SqlDbType.Int, recentlyViewed.ProductID),
                                                    new ParamItem("AccountID", SqlDbType.Int, recentlyViewed.AccountID)};

                return await Task.FromResult(base.ExecuteSql("pro_RecentlyViewed_Update", Params.Create(arr)));
            }
            catch (Exception ex)
            {
                base.WriteError("Error in RecentlyViewedService at Update() Method", ex.Message);
            }
            return -1;
        }

        public async Task<long> Delete(long ProductID, long AccountID)
        {
            try
            {
                ParamItem[] arr = new ParamItem[] { new ParamItem("ProductID", SqlDbType.BigInt, ProductID),
                                                    new ParamItem("AccountID", SqlDbType.BigInt, AccountID)};

                return await Task.FromResult(base.ExecuteSql("pro_RecentlyViewed_Delete", Params.Create(arr)));
            }
            catch (Exception ex)
            {
                base.WriteError("Error in RecentlyViewedService at Update() Method", ex.Message);
            }
            return -1;
        }

    }
}
