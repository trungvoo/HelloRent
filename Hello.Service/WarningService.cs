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
    public partial class WarningService : BaseService<Warning>, IWarningService
    {
        public WarningService(IRepository<Warning> repository) : base(repository) { }
    }

    public partial class WarningService : IWarningService
    {

        public async Task<long> Insert(Warning model)
        {
            try
            {
                ParamItem[] arr = new ParamItem[] { new ParamItem("AccountID", SqlDbType.BigInt, model.AccountID),
                                                    new ParamItem("ObjectID", SqlDbType.BigInt, model.ObjectID),
                                                    new ParamItem("ObjectType", SqlDbType.TinyInt, (int)model.ObjectType),
                                                    new ParamItem("Type", SqlDbType.TinyInt, (int)model.Type),
                                                    new ParamItem("Content", SqlDbType.NVarChar, model.Content),
                                                    new ParamItem("CreateDate", SqlDbType.DateTime, model.CreateDate)};

                return await Task.FromResult(base.ExecuteSql("pro_Warning_Insert", Params.Create(arr)));
            }
            catch (Exception ex)
            {
                base.WriteError("Error in WarningService at Insert() method", ex.Message);
            }

            return -1;
        }
    }
}
