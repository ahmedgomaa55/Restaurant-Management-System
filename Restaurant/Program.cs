using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RestaurantContext context = new RestaurantContext();
            User user1 = new User() { ID = 1, Username = "ahmed gomaa", Password = "123456" };
            context.Users.Add(user1);
            context.SaveChanges();
        }
    }
}
