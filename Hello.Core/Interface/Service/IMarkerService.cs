using Hello.Common.Utils;
using Hello.Core.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hello.Core.Interface.Service
{
    public partial interface IMarkerService : IBaseService<Marker>
    {
        Task<IEnumerable<Marker>> ListAttractionByLocation(double lat, double lng, int numRows);
        Task<IEnumerable<Marker>> ListAttractionByKeyword(string keyword, int provinceID);
        Task<IEnumerable<Marker>> ListMarkerByDistrict(int districtID, MarkerType markerType);
        Task<IEnumerable<Marker>> ListMarkerByKeyword(string keyword, int provinceID);
        Task<IEnumerable<Marker>> ListMarkerByCountry(int countryID, MarkerType type, int numRows);
    }
}
