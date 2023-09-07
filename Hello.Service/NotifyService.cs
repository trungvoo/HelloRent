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
    public partial class NotifyService : BaseService<Notify>, INotifyService
    {
        public NotifyService(IRepository<Notify> repositore) : base(repositore) { }
    }
    public partial class NotifyService : INotifyService
    {
        public async Task<Notify> GetMainLauncher()
        {
            try
            {
                ParamItem[] arr = new ParamItem[] { new ParamItem("Type", SqlDbType.TinyInt, (int)NotifyType.MainLauncher) };

                return await Task.FromResult(base.SqlQuery("pro_Notify_MainLauncher", Params.Create(arr)).SingleOrDefault());
            }
            catch (Exception ex)
            {
                base.WriteError("Error in NotifyService at GetMainLauncher() Method", ex.Message);
            }

            return null;
        }
    }


}
