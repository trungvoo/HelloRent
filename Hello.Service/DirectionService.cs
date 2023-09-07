using Hello.Core.Interface.Data;
using Hello.Core.Interface.Service;
using Hello.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.Service
{
    public partial class DirectionService : BaseService<Direction>, IDirectionService
    {
        public DirectionService(IRepository<Direction> repository) : base(repository) { }
    }

    public partial class DirectionService : IDirectionService
    {
        public async Task<IEnumerable<Direction>> ListDirection()
        {
            try
            {
                return await Task.FromResult(base.SqlQuery("pro_Direction_GetAll").ToList());
            }
            catch (Exception ex)
            {
                base.WriteError("Error in HouseDirectionService at ListDirection() Method", ex.Message);
            }

            return Enumerable.Empty<Direction>();
        }
    }
}
