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
   public partial class InfoService:BaseService<Information>, IInfoService
    {
       public InfoService(IRepository<Information> repository) : base(repository) { }
    }

   public partial class InfoService : IInfoService
   {

       public async Task<Information> GetInfoByType(InfoType type)
       {
           try
           {
               ParamItem[] arr = new ParamItem[] { new ParamItem("InforType", SqlDbType.TinyInt, (int)type) };

               return await Task.FromResult(base.SqlQuery("pro_Information_GetByType", Params.Create(arr)).SingleOrDefault());
           }
           catch (Exception ex)
           {
               base.WriteError("Error in InforService at GetInfoByType() Method", ex.Message);
           }

           return null;
       }
   }
}
