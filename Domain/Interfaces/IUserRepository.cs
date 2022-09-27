using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        bool UserControl(string username, string password);
        User UsersControl(string username, string password);
        bool AddUser(string username, string password);
    }
}
