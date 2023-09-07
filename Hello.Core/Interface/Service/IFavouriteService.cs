using Hello.Core.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hello.Core.Interface.Service
{
    public partial interface IFavouriteService : IBaseService<Favourite>
    {
        Task<IEnumerable<Favourite>> GetFavouriteByAccountProduct(long accountID, long productID);

        Task<long> Update(Favourite model);

        Task<long> Delete(long ProductID, long AccountID);
    }
}
