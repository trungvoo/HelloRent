using Hello.Common.Utils;
using Hello.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.Core.Interface.Service
{
    public interface IProductService : IBaseService<Product>
    {
        Task<long> Insert(Product product);
        Task<long> InsertPicture(long productID, long accountID, List<string> imageList);
        Task<IEnumerable<Product>> SearchNearArea(double lat, double lng, float distance, PropertyType type, int propertyId, float minPrice, float maxPrice, float minArea, float maxArea, int bed, int bath, ProductStatus status, int beginRow, int numRows);
        Task<IEnumerable<Product>> SearchBounds(double startLat, double startLng, double endLat, double endLng, PropertyType type, int propertyId, float minPrice, float maxPrice, float minArea, float maxArea, int bed, int bath, ProductStatus status, int beginRow, int numRows);
        Task<IEnumerable<Product>> SearchByAccount(long accountID, ProductStatus status, long productID, string keyword, int beginRow, int numRows);
        Task<IEnumerable<Product>> GetById(long productId, long accountId);
        Task<long> Favorite(long productId, long accountId);
        Task<long> UserView(long productID);
        Task<IEnumerable<Product>> GetByUserLikes(long accountId);
        Task<long> UpdateNote(long accountID, long productID, string note, string nonUnicode);
        Task<long> UpdateStatus(long accountID, long productID, ProductStatus status);
        Task<long> UpdateInfo(Product product);
    }
}
