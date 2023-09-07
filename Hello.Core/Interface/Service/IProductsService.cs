using Hello.Common.Utils;
using Hello.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.Core.Interface.Service
{
    public interface IProductsService : IBaseService<ProductsViewModel>
    {
        Task<long> Insert(ProductsModel model, long AccountID);
        Task<long> Delete(long productID);
        Task<IEnumerable<ProductsViewModel>> GetRecentlyViewed(long accountId);
        Task<IEnumerable<ProductsViewModel>> GetFavourite(long accountId);

    }
}
