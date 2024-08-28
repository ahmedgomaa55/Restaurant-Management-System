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

            User user1 = new User() { ID = 1, Username = "ahmed", Password = "123456" };
            User user2 = new User() { ID = 2, Username = "rodayna", Password = "123456" };
            User user3 = new User() { ID = 3, Username = "mo", Password = "00" };


            context.Users.Add(user1);
            context.Users.Add(user2);
            context.Users.Add(user3);


            context.SaveChanges();
        }
    }
}
