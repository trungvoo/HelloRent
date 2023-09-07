using Hello.Core.Interface.Data;
using Hello.Core.Interface.Service;
using Hello.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.Service
{
    public partial class PropertyService : BaseService<Property>, IPropertyService
    {
        public PropertyService(IRepository<Property> repository) : base(repository) { }
    }

    public partial class PropertyService : IPropertyService
    {

        public async Task<IEnumerable<Property>> ListProperty()
        {
            try
            {
                return await Task.FromResult(base.SqlQuery("pro_Property_GetAll").ToList());
            }
            catch (Exception ex)
            {
                base.WriteError("Error in PropertyService at ListProperty() Method", ex.Message);
            }
            return Enumerable.Empty<Property>();
        }
    }
}
