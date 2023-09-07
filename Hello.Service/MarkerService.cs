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
    public partial class MarkerService : BaseService<Marker>, IMarkerService
    {
        public MarkerService(IRepository<Marker> repository) : base(repository) { }
    }

    public partial class MarkerService : IMarkerService
    {

        public async Task<IEnumerable<Marker>> ListAttractionByLocation(double lat, double lng, int numRows)
        {
            try
            {
                ParamItem[] arr = new ParamItem[] { new ParamItem("Latitude", SqlDbType.Float, lat),
                                                    new ParamItem("Longitude", SqlDbType.Float, lng),
                                                    new ParamItem("NumRows", SqlDbType.Int, numRows)};

                return await Task.FromResult(base.SqlQuery("pro_Attraction_GetByLocation", Params.Create(arr)).ToList());
            }
            catch (Exception ex)
            {
                base.WriteError("Error in MarkerService at ListAttractionByLocation() Method", ex.Message);
            }

            return Enumerable.Empty<Marker>();
        }


        public async Task<IEnumerable<Marker>> ListAttractionByKeyword(string keyword, int provinceID)
        {
            try
            {
                if (!string.IsNullOrEmpty(keyword.Trim()))
                {
                    string[] arrWord = keyword.Trim().Split(new char[0]);
                    if (arrWord.Length > 1)
                        keyword = string.Join(" AND ", arrWord);
                }

                ParamItem[] arr = new ParamItem[] { new ParamItem("Keyword", SqlDbType.NVarChar, keyword),
                                                    new ParamItem("ProvinceID", SqlDbType.Int, provinceID)};

                return await Task.FromResult(base.SqlQuery("pro_Attraction_GetByKeyword", Params.Create(arr)).ToList());
            }
            catch (Exception ex)
            {
                base.WriteError("Error in MarkerService at ListAttracByKeyword() Method", ex.Message);
            }

            return Enumerable.Empty<Marker>();
        }


        public async Task<IEnumerable<Marker>> ListMarkerByDistrict(int districtID, MarkerType markerType)
        {
            try
            {
                ParamItem[] arr = new ParamItem[] { new ParamItem("DistrictID", SqlDbType.Int, districtID),
                                                    new ParamItem("MarkerType", SqlDbType.TinyInt, (int) markerType)};

                return await Task.FromResult(base.SqlQuery("pro_Marker_GetByDistrict", Params.Create(arr)).ToList());
            }
            catch (Exception ex)
            {
                base.WriteError("Error in MarkerService at ListMarkerByDistrict() Method", ex.Message);
            }

            return Enumerable.Empty<Marker>();
        }


        public async Task<IEnumerable<Marker>> ListMarkerByKeyword(string keyword, int provinceID)
        {
            try
            {
                if (!string.IsNullOrEmpty(keyword.Trim()))
                {
                    string[] arrWord = keyword.Trim().Split(new char[0]);
                    if (arrWord.Length > 1)
                        keyword = string.Join(" AND ", arrWord);
                }

                ParamItem[] arr = new ParamItem[] { new ParamItem("Keyword", SqlDbType.NVarChar, keyword),
                                                    new ParamItem("ProvinceID", SqlDbType.Int, provinceID)};

                return await Task.FromResult(base.SqlQuery("pro_Marker_GetByKeyword", Params.Create(arr)).ToList());
            }
            catch (Exception ex)
            {
                base.WriteError("Error in MarkerService at ListMarkerByKeyword() Method", ex.Message);
            }

            return Enumerable.Empty<Marker>();
        }


        public async Task<IEnumerable<Marker>> ListMarkerByCountry(int countryID, MarkerType type, int numRows)
        {
            try
            {
                ParamItem[] arr = new ParamItem[] { new ParamItem("CountryID", SqlDbType.Int, countryID),
                                                    new ParamItem("MarkerType", SqlDbType.TinyInt, (int)type),
                                                    new ParamItem("NumRows", SqlDbType.Int, numRows)};

                return await Task.FromResult(base.SqlQuery("pro_Marker_GetByCountry", Params.Create(arr)).ToList());
            }
            catch (Exception ex)
            {
                base.WriteError("Error in MarkerService at ListMarkerByCountry() Method", ex.Message);
            }

            return Enumerable.Empty<Marker>();
        }
    }
}
