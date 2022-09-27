using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBasketRepository
    {
        Task<UserBasket> GetBasketAsync(string userName);
        IEnumerable<string> GetUsers();
        Task<UserBasket> UpdateBasketAsync(UserBasket userBasket);
        Task<bool> DeleteBasketAsync(string UserName);
    }
}
