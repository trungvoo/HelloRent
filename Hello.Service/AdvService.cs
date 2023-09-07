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
   public partial class AdvService:BaseService<Advertising>, IAdvService
    {
       public AdvService(IRepository<Advertising> repository) : base(repository) { }
    }

   public partial class AdvService : IAdvService
   {

       public async Task<IEnumerable<Advertising>> ListByLevel(AdvLevel level, Status status, int beginRow, int numRows)
       {
           try
           {
               ParamItem[] arr = new ParamItem[] { new ParamItem("Level", SqlDbType.TinyInt, (int)level),
                                                    new ParamItem("Status", SqlDbType.TinyInt, (int)status),
                                                    new ParamItem("BeginRow", SqlDbType.Int, beginRow),
                                                    new ParamItem("NumRow", SqlDbType.Int, numRows)};

               return await Task.FromResult(base.SqlQuery("pro_Adv_List", Params.Create(arr)).ToList());
           }
           catch (Exception ex)
           {
               base.WriteError("Error in AdvService at ListByLevel() Method", ex.Message);
           }

           return Enumerable.Empty<Advertising>();
       }
   }
}
