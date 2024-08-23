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
        private readonly RestaurantContext _context;

        // Constructor to inject the DbContext
        public UserService(RestaurantContext context)
        {
            _context = context;
        }

        // Method to check if the user is valid
        public bool isValidUser(string username, string password)
        {
            // Check if the user exists
            var user = _context.Users
                        .FirstOrDefault(u => u.Username == username && u.Password == password);

            return user != null;
        }
    }

}
