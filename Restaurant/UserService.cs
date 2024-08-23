using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    public class UserService
    {
        private static RestaurantContext _context = new RestaurantContext();
        public static string User { get; set; }
        
        // Method to check if the user is valid
        public static bool isValidUser(string username, string password)
        {
            // Check if the user exists
             var user = _context.Users
                        .FirstOrDefault(u => u.Username == username && u.Password == password);
            User = _context.Users
                        .FirstOrDefault(u => u.Username == username && u.Password == password).Username;

            return user != null;
        }
    }

}
