using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interface
{
    public interface IGenericRepository<TEntityModel> where TEntityModel : class
    {
        Task<List<TEntityModel>> GetAll();
        Task<bool> Add(TEntityModel entityModel);
        Task<bool> Delete(string id);
        Task<bool> Update(TEntityModel entityModel);
        Task<TEntityModel> GetById(string id);
    }
}
