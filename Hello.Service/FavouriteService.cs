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
    public partial class FavouriteService : BaseService<Favourite>, IFavouriteService
    {
        public FavouriteService(IRepository<Favourite> repository) : base(repository) { }
    }

    public partial class FavouriteService : IFavouriteService
    {

        public async Task<IEnumerable<Favourite>> GetFavouriteByAccountProduct(long accountId, long productId)
        {
            try
            {
                ParamItem[] arr = new ParamItem[] { new ParamItem("AccountID", SqlDbType.BigInt, accountId),
                                                    new ParamItem("ProductID", SqlDbType.BigInt, productId)
                };

                return await Task.FromResult(base.SqlQuery("pro_Favourite_GetByAccountProduct", Params.Create(arr)).ToList());
            }
            catch (Exception ex)
            {
                base.WriteError("Error in ProductsService at GetRecentlyViewed() Method", ex.Message);

            }

            return Enumerable.Empty<Favourite>();
        }

        public async Task<long> Update(Favourite favourite)
        {
            try
            {
                ParamItem[] arr = new ParamItem[] { new ParamItem("ProductID", SqlDbType.BigInt, favourite.ProductID),
                                                    new ParamItem("AccountID", SqlDbType.BigInt, favourite.AccountID)};

                return await Task.FromResult(base.ExecuteSql("pro_Favourite_Update", Params.Create(arr)));
            }
            catch (Exception ex)
            {
                base.WriteError("Error in FavouriteService at Update() Method", ex.Message);
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
                base.WriteError("Error in FavouriteService at Update() Method", ex.Message);
            }
            return -1;
        }

    }
}
