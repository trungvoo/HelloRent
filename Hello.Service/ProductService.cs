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
    public partial class ProductService : BaseService<Product>, IProductService
    {
        public ProductService(IRepository<Product> repository) : base(repository) { }
    }

    public partial class ProductService : IProductService
    {

        #region Private Function
        private DataTable ProductStatusTable(ProductStatus status)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[1] { new DataColumn("id", typeof(int)) });

            switch (status)
            {
                case ProductStatus.New:
                case ProductStatus.Recertified:
                    dt.Rows.Add((int)ProductStatus.New);
                    dt.Rows.Add((int)ProductStatus.Recertified);
                    break;
                case ProductStatus.Undefined:
                    dt.Rows.Add((int)ProductStatus.New);
                    dt.Rows.Add((int)ProductStatus.Activated);
                    dt.Rows.Add((int)ProductStatus.Certified);
                    dt.Rows.Add((int)ProductStatus.EndCertified);
                    dt.Rows.Add((int)ProductStatus.Completed);
                    dt.Rows.Add((int)ProductStatus.Recertified);
                    dt.Rows.Add((int)ProductStatus.Failed);
                    break;
                default:
                    dt.Rows.Add((int)status);
                    break;
            }

            return dt;
        }

        #endregion


        public async Task<long> Insert(Product product)
        {
            try
            {
                ParamItem[] arr = new ParamItem[] { new ParamItem("Address", SqlDbType.NVarChar, product.Address),
                                                    new ParamItem("Latitude", SqlDbType.Decimal, product.Latitude),
                                                    new ParamItem("Longitude", SqlDbType.Decimal, product.Longitude),
                                                    new ParamItem("CountryID", SqlDbType.Int, product.CountryID),
                                                    new ParamItem("ProvinceID", SqlDbType.Int, product.ProvinceID),
                                                    new ParamItem("DistrictID", SqlDbType.Int, product.DistrictID),
                                                    new ParamItem("PropertyType", SqlDbType.Int, product.PropertyID),
                                                    new ParamItem("BuildingID", SqlDbType.Int, product.BuildingID),
                                                    new ParamItem("Thumbnail", SqlDbType.VarChar, product.Thumbnail),
                                                    new ParamItem("HostIndex", SqlDbType.TinyInt, (int)product.HostIndex),
                                                    new ParamItem("Deposit", SqlDbType.Decimal, product.Deposit),
                                                    new ParamItem("Price", SqlDbType.Decimal, product.Price),
                                                    new ParamItem("Floor", SqlDbType.Int, product.Floor),
                                                    new ParamItem("FloorCount", SqlDbType.Int, product.FloorCount),
                                                    new ParamItem("SiteArea", SqlDbType.Decimal, product.SiteArea),
                                                    new ParamItem("GrossFloorArea", SqlDbType.Decimal, product.GrossFloorArea),
                                                    new ParamItem("Bedroom", SqlDbType.Int, product.Bedroom),
                                                    new ParamItem("Bathroom", SqlDbType.Int, product.Bathroom),
                                                    new ParamItem("DirectionID", SqlDbType.Int, product.DirectionID),
                                                    new ParamItem("ServiceFee", SqlDbType.Decimal, product.ServiceFee),
                                                    new ParamItem("FeatureList", SqlDbType.VarChar, product.FeatureList),
                                                    new ParamItem("FurnitureList", SqlDbType.VarChar, product.FurnitureList),
                                                    new ParamItem("Elevator", SqlDbType.Bit, product.Elevator),
                                                    new ParamItem("Pets", SqlDbType.Bit, product.Pets),
                                                    new ParamItem("NumPerson", SqlDbType.Int, product.NumPerson),
                                                    new ParamItem("Title", SqlDbType.NVarChar, product.Title),
                                                    new ParamItem("Content", SqlDbType.NVarChar, product.Content),
                                                    new ParamItem("Note", SqlDbType.NVarChar, product.Note),
                                                    new ParamItem("AccountID", SqlDbType.BigInt, product.AccountID),
                                                    new ParamItem("CreateDate", SqlDbType.DateTime, product.CreateDate),
                                                    new ParamItem("ContactName", SqlDbType.NVarChar, product.ContactName),
                                                    new ParamItem("ContactPhone", SqlDbType.VarChar, product.ContactPhone),
                                                    new ParamItem("NonUnicode", SqlDbType.NVarChar, product.NonUnicode)};

                var result = await Task.FromResult(base.SqlQuery("pro_Product_Insert", Params.Create(arr)).SingleOrDefault());
                if (result != null)
                    return result.Id;
            }
            catch (Exception ex)
            {
                base.WriteError("Error in ProductService at Insert() Method", ex.Message);
            }

            return -1;
        }


        public async Task<long> InsertPicture(long productID, long accountID, List<string> imageList)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[7] { new DataColumn("id", typeof(int)),
                                                        new DataColumn("name", typeof(string)),
                                                        new DataColumn("hostindex", typeof(int)),
                                                        new DataColumn("product", typeof(long)),
                                                        new DataColumn("account", typeof(long)),
                                                        new DataColumn("note", typeof(string)),
                                                        new DataColumn("creatDate", typeof(DateTime))});

                int i = 0;
                foreach (string image in imageList)
                {
                    i++;
                    dt.Rows.Add(i, image, HostIndex.CurrentHost, productID, accountID, "", DateTime.Now);
                }


                ParamItem[] arr = new ParamItem[] { new ParamItem("ListPicture", SqlDbType.Structured, dt, "list_picture_table") };

                return await Task.FromResult(base.ExecuteSql("pro_Picture_Insert", Params.Create(arr)));
            }
            catch (Exception ex)
            {
                base.WriteError("Error in ProductService at InsertPicture() Method", ex.Message);
            }

            return -1;
        }


        public async Task<IEnumerable<Product>> SearchNearArea(double lat, double lng, float distance, PropertyType type, int propertyId, float minPrice, float maxPrice, float minArea, float maxArea, int bed, int bath, ProductStatus status, int beginRow, int numRows)
        {
            try
            {
                ParamItem[] arr = new ParamItem[] { new ParamItem("Latitude", SqlDbType.Float, lat),
                                                    new ParamItem("Longitude", SqlDbType.Float, lng),
                                                    new ParamItem("Distance", SqlDbType.Float, distance),
                                                    new ParamItem("PropertyType", SqlDbType.TinyInt, (int)type),
                                                    new ParamItem("PropertyID", SqlDbType.Int, propertyId),
                                                    new ParamItem("MinPrice", SqlDbType.Float, minPrice),
                                                    new ParamItem("MaxPric", SqlDbType.Float, maxPrice),
                                                    new ParamItem("MinArea", SqlDbType.Float, minArea),
                                                    new ParamItem("MaxArea", SqlDbType.Float, maxArea),
                                                    new ParamItem("Bed", SqlDbType.VarChar, bed),
                                                    new ParamItem("Bath", SqlDbType.Int, bath),
                                                    new ParamItem("Status", SqlDbType.TinyInt, (int)status),
                                                    new ParamItem("BeginRow", SqlDbType.Int, beginRow),
                                                    new ParamItem("NumRows", SqlDbType.Int, numRows)};

                return await Task.FromResult(base.SqlQuery("pro_Product_SearchNearArea", Params.Create(arr)).ToList());
            }
            catch (Exception ex)
            {
                base.WriteError("Error in ProductService at SearchNearArea() Method", ex.Message);
            }

            return Enumerable.Empty<Product>();
        }


        public async Task<IEnumerable<Product>> SearchBounds(double startLat, double startLng, double endLat, double endLng, PropertyType type, int propertyId, float minPrice, float maxPrice, float minArea, float maxArea, int bed, int bath, ProductStatus status, int beginRow, int numRows)
        {
            try
            {
                ParamItem[] arr = new ParamItem[] { new ParamItem("StartLat", SqlDbType.Float, startLat),
                                                    new ParamItem("StartLng", SqlDbType.Float, startLng),
                                                    new ParamItem("EndLat", SqlDbType.Float, endLat),
                                                    new ParamItem("EndLng", SqlDbType.Float, endLng),
                                                    new ParamItem("PropertyType", SqlDbType.TinyInt, (int)type),
                                                    new ParamItem("PropertyID", SqlDbType.Int, propertyId),
                                                    new ParamItem("MinPrice", SqlDbType.Float, minPrice),
                                                    new ParamItem("MaxPric", SqlDbType.Float, maxPrice),
                                                    new ParamItem("MinArea", SqlDbType.Float, minArea),
                                                    new ParamItem("MaxArea", SqlDbType.Float, maxArea),
                                                    new ParamItem("Bed", SqlDbType.VarChar, bed),
                                                    new ParamItem("Bath", SqlDbType.Int, bath),
                                                    new ParamItem("Status", SqlDbType.TinyInt, (int)status),
                                                    new ParamItem("BeginRow", SqlDbType.Int, beginRow),
                                                    new ParamItem("NumRows", SqlDbType.Int, numRows)};

                return await Task.FromResult(base.SqlQuery("pro_Product_SearchBounds", Params.Create(arr)).ToList());
            }
            catch (Exception ex)
            {
                base.WriteError("Error in ProductService at SearchBounds() Method", ex.Message);
            }

            return Enumerable.Empty<Product>();
        }


        public async Task<IEnumerable<Product>> SearchByAccount(long accountID, ProductStatus status, long productID, string keyword, int beginRow, int numRows)
        {
            try
            {

                if (!string.IsNullOrEmpty(keyword.Trim()))
                {
                    string[] arrWord = keyword.Trim().Split(new char[0]);
                    if (arrWord.Length > 1)
                        keyword = string.Join(" AND ", arrWord);
                }

                ParamItem[] arr = new ParamItem[] { new ParamItem("AccountID", SqlDbType.BigInt, accountID),
                                                    new ParamItem("ListStatus", SqlDbType.Structured, ProductStatusTable(status), "list_id_table"),
                                                    new ParamItem("ProductID", SqlDbType.BigInt, productID),
                                                    new ParamItem("Keyword", SqlDbType.NVarChar, keyword),
                                                    new ParamItem("BeginRow", SqlDbType.Int, beginRow),
                                                    new ParamItem("NumRows", SqlDbType.Int, numRows)};

                return await Task.FromResult(base.SqlQuery("pro_Product_SearchByAccount", Params.Create(arr)).ToList());
            }
            catch (Exception ex)
            {
                base.WriteError("Error in ProductService at SearchByAccount() Method", ex.Message);
            }

            return Enumerable.Empty<Product>();
        }


        public async Task<IEnumerable<Product>> GetById(long productId, long accountId)
        {
            try
            {
                ParamItem[] arr = new ParamItem[] { new ParamItem("AccountID", SqlDbType.BigInt, accountId),
                                                    new ParamItem("ProductID", SqlDbType.BigInt, productId),
                                                    new ParamItem("DataType", SqlDbType.TinyInt, (int)ObjectType.Product)};

                return await Task.FromResult(base.SqlQuery("pro_Product_GetByID", Params.Create(arr)).ToList());
            }
            catch (Exception ex)
            {
                base.WriteError("Error in ProductService at GetById() Method", ex.Message);

            }

            return Enumerable.Empty<Product>();
        }


        public async Task<long> Favorite(long productId, long accountId)
        {
            try
            {
                ParamItem[] arr = new ParamItem[] { new ParamItem("AccountID", SqlDbType.BigInt, accountId),
                                                    new ParamItem("ObjectID", SqlDbType.BigInt, productId),
                                                    new ParamItem("DataType", SqlDbType.TinyInt, (int)ObjectType.Product)};

                return await Task.FromResult(base.ExecuteSql("pro_UserLike", Params.Create(arr)));
            }
            catch (Exception ex)
            {
                base.WriteError("Error in ProductService at Favorite() Method", ex.Message);

            }

            return -1;
        }


        public async Task<long> UserView(long productID)
        {
            try
            {
                ParamItem[] arr = new ParamItem[] { new ParamItem("ProductID", SqlDbType.BigInt, productID),
                                                    new ParamItem("FieldName", SqlDbType.VarChar, "NumView")};

                return await Task.FromResult(base.ExecuteSql("pro_Product_IncreaseValues", Params.Create(arr)));
            }
            catch (Exception ex)
            {
                base.WriteError("Error in ProductService at UserView() Method", ex.Message);

            }

            return -1;
        }


        public async Task<IEnumerable<Product>> GetByUserLikes(long accountId)
        {
            try
            {
                ParamItem[] arr = new ParamItem[] { new ParamItem("AccountID", SqlDbType.BigInt, accountId),
                                                    new ParamItem("DataType", SqlDbType.TinyInt, (int)ObjectType.Product)};

                return await Task.FromResult(base.SqlQuery("pro_Product_GetByUserLikes", Params.Create(arr)).ToList());
            }
            catch (Exception ex)
            {
                base.WriteError("Error in ProductService at GetByUserLikes() Method", ex.Message);

            }

            return Enumerable.Empty<Product>();
        }


        public async Task<long> UpdateNote(long accountID, long productID, string note, string nonUnicode)
        {
            try
            {
                ParamItem[] arr = new ParamItem[] { new ParamItem("AccountID", SqlDbType.BigInt, accountID),
                                                    new ParamItem("ProductID", SqlDbType.BigInt, productID),
                                                    new ParamItem("Note", SqlDbType.NVarChar, note),
                                                    new ParamItem("NonUnicode", SqlDbType.VarChar, nonUnicode)};

                return await Task.FromResult(base.ExecuteSql("pro_Product_UpdateNote", Params.Create(arr)));
            }
            catch (Exception ex)
            {
                base.WriteError("Error in ProductService at UpdateNote() Method", ex.Message);

            }

            return -1;
        }


        public async Task<long> UpdateStatus(long accountID, long productID, ProductStatus status)
        {
            try
            {
                ParamItem[] arr = new ParamItem[] { new ParamItem("AccountID", SqlDbType.BigInt, accountID),
                                                    new ParamItem("ProductID", SqlDbType.BigInt, productID),
                                                    new ParamItem("Status", SqlDbType.TinyInt, (int)status)};

                return await Task.FromResult(base.ExecuteSql("pro_Product_UpdateStatus", Params.Create(arr)));
            }
            catch (Exception ex)
            {
                base.WriteError("Error in ProductService at UpdateStatus() Method", ex.Message);

            }

            return -1;
        }


        public async Task<long> UpdateInfo(Product product)
        {
            try
            {
                ParamItem[] arr = new ParamItem[] { new ParamItem("AccountID", SqlDbType.BigInt, product.AccountID),
                                                    new ParamItem("ProductID", SqlDbType.BigInt, product.Id),
                                                    new ParamItem("Address", SqlDbType.NVarChar, product.Address),
                                                    new ParamItem("Latitude", SqlDbType.Decimal, product.Latitude),
                                                    new ParamItem("Longitude", SqlDbType.Decimal, product.Longitude),
                                                    new ParamItem("ProvinceID", SqlDbType.Int, product.ProvinceID),
                                                    new ParamItem("DistrictID", SqlDbType.Int, product.DistrictID),
                                                    new ParamItem("PropertyID", SqlDbType.Int, product.PropertyID),
                                                    new ParamItem("BuildingID", SqlDbType.Int, product.BuildingID),
                                                    new ParamItem("Deposit", SqlDbType.Decimal, product.Deposit),
                                                    new ParamItem("Price", SqlDbType.Decimal, product.Price),
                                                    new ParamItem("Floor", SqlDbType.Int, product.Floor),
                                                    new ParamItem("FloorCount", SqlDbType.Int, product.FloorCount),
                                                    new ParamItem("SiteArea", SqlDbType.Decimal, product.SiteArea),
                                                    new ParamItem("GrossFloorArea", SqlDbType.Decimal, product.GrossFloorArea),
                                                    new ParamItem("Bedroom", SqlDbType.Int, product.Bedroom),
                                                    new ParamItem("Bathroom", SqlDbType.Int, product.Bathroom),
                                                    new ParamItem("DirectionID", SqlDbType.Int, product.DirectionID),
                                                    new ParamItem("ServiceFee", SqlDbType.Decimal, product.ServiceFee),
                                                    new ParamItem("FeatureList", SqlDbType.VarChar, product.FeatureList),
                                                    new ParamItem("FurnitureList", SqlDbType.VarChar, product.FurnitureList),
                                                    new ParamItem("Elevator", SqlDbType.Bit, product.Elevator),
                                                    new ParamItem("Pets", SqlDbType.Bit, product.Pets),
                                                    new ParamItem("NumPerson", SqlDbType.Int, product.NumPerson),
                                                    new ParamItem("Title", SqlDbType.NVarChar, product.Title),
                                                    new ParamItem("Content", SqlDbType.NVarChar, product.Content),
                                                    new ParamItem("ContactName", SqlDbType.NVarChar, product.ContactName),
                                                    new ParamItem("ContactPhone", SqlDbType.VarChar, product.ContactPhone)};

                return await Task.FromResult(base.ExecuteSql("pro_Product_UpdateInfo", Params.Create(arr)));
            }
            catch (Exception ex)
            {
                base.WriteError("Error in ProductService at UpdateInfo() Method", ex.Message);
            }

            return -1;
        }
    }

}
