using Hello.Common.Utils;
using Hello.Core.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hello.Core.Interface.Service
{
    public partial interface IFeaturesService : IBaseService<Features>
    {
        Task<IEnumerable<Features>> ListFeature();
        Task<IEnumerable<Features>> ListFurniture();
        Task<IEnumerable<Features>> ListById(List<string> listId, ThumbType type);
    }
}
