using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserBasket
    {
        public string? UserName { get; set; }
        public List<BasketItem> BasketItems { get; set; } = new List<BasketItem>();

        public UserBasket()
        {

        }

        public UserBasket(string userName)
        {
            UserName = userName;
        }

    }
}
