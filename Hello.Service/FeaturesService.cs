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
    public partial class FeaturesService : BaseService<Features>, IFeaturesService
    {
        public FeaturesService(IRepository<Features> repository) : base(repository) { }
    }

    public partial class FeaturesService : IFeaturesService
    {

        public async Task<IEnumerable<Features>> ListFeature()
        {
            try
            {
                return await Task.FromResult(base.SqlQuery("pro_Features_GetAll").ToList());
            }
            catch (Exception ex)
            {
                base.WriteError("Error in FeaturesService at ListFeature() Method", ex.Message);
            }

            return Enumerable.Empty<Features>();
        }

        public async Task<IEnumerable<Features>> ListFurniture()
        {
            try
            {
                return await Task.FromResult(base.SqlQuery("pro_Furniture_GetAll").ToList());
            }
            catch (Exception ex)
            {
                base.WriteError("Error in FeaturesService at ListFurniture() Method", ex.Message);
            }

            return Enumerable.Empty<Features>();
        }


        public async Task<IEnumerable<Features>> ListById(List<string> listId, ThumbType type)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[1] { new DataColumn("id", typeof(int)) });

                foreach (string id in listId)
                    dt.Rows.Add(int.Parse(id));

                ParamItem[] arr = new ParamItem[] { new ParamItem("ListID", SqlDbType.Structured, dt, "list_id_table") };

                if (type == ThumbType.Feature)
                    return await Task.FromResult(base.SqlQuery("pro_Features_GetByID", Params.Create(arr)).ToList());
                else if( type == ThumbType.Furniture)
                    return await Task.FromResult(base.SqlQuery("pro_Furniture_GetByID", Params.Create(arr)).ToList());

            }
            catch (Exception ex)
            {
                base.WriteError("Error in FeaturesService at ListById() Method", ex.Message);
            }

            return Enumerable.Empty<Features>();
        }
    }
}
