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
    public partial class ProductsService : BaseService<ProductsViewModel>, IProductsService
    {
        public ProductsService(IRepository<ProductsViewModel> repository) : base(repository) { }
    }

    public partial class ProductsService : IProductsService
    {
        public async Task<long> Insert(ProductsModel product, long AccountID)
        {
            try
            {
                ParamItem[] arr = new ParamItem[] { new ParamItem("ProductID", SqlDbType.BigInt, product.ProductID),
                                                    new ParamItem("Title", SqlDbType.NVarChar, product.Title),
                                                    new ParamItem("Avatar", SqlDbType.NVarChar, product.Avatar),
                                                    new ParamItem("Address", SqlDbType.NVarChar, product.Address),
                                                    new ParamItem("Status ", SqlDbType.TinyInt, product.Status),
                                                    new ParamItem("Price ", SqlDbType.Decimal, product.Price),
                                                    new ParamItem("GrossFloorArea ", SqlDbType.Decimal, product.GrossFloorArea),
                                                    new ParamItem("AccountID ", SqlDbType.BigInt, AccountID),
                };

                return await Task.FromResult(ExecuteSql("pro_Products_Insert", Params.Create(arr)));
            }
            catch (Exception ex)
            {
                base.WriteError("Error in ProductsService at Insert() Method", ex.Message);
            }

            return -1;
        }

        public async Task<long> Delete(long productID)
        {
            try
            {
                ParamItem[] arr = new ParamItem[] { new ParamItem("ProductID", SqlDbType.BigInt, productID),
                };

                return await Task.FromResult(ExecuteSql("pro_Products_Detele", Params.Create(arr)));
            }
            catch (Exception ex)
            {
                base.WriteError("Error in ProductsService at Delete() Method", ex.Message);
            }

            return -1;
        }

        public async Task<IEnumerable<ProductsViewModel>> GetRecentlyViewed(long accountId)
        {
            try
            {
                ParamItem[] arr = new ParamItem[] { new ParamItem("AccountID", SqlDbType.BigInt, accountId)};

                return await Task.FromResult(base.SqlQuery("pro_RecentlyViewed_Products_Filter", Params.Create(arr)).ToList());
            }
            catch (Exception ex)
            {
                base.WriteError("Error in ProductsService at GetRecentlyViewed() Method", ex.Message);

            }

            return Enumerable.Empty<ProductsViewModel>();
        }

        public async Task<IEnumerable<ProductsViewModel>> GetFavourite(long accountId)
        {
            try
            {
                ParamItem[] arr = new ParamItem[] { new ParamItem("AccountID", SqlDbType.BigInt, accountId) };

                return await Task.FromResult(base.SqlQuery("pro_Favourite_Products_Filter", Params.Create(arr)).ToList());
            }
            catch (Exception ex)
            {
                base.WriteError("Error in ProductsService at GetFavourite() Method", ex.Message);

            }

            return Enumerable.Empty<ProductsViewModel>();
        }

    }

}
