using DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        Task<bool> AddUser(User user);

        [OperationContract]
        Task<bool> DeleteUser(string userId);

        [OperationContract]
        Task<List<User>> GetAllUsers();

        [OperationContract]
        Task<User> GetUserById(string userId);

        [OperationContract]
        Task<bool> UpdateUser(User user);
    }
}
