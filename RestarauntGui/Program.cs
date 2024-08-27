using Restaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestarauntGui
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmLogin());
        }

        public static void CBFill(ComboBox cb)
        {
            using (RestaurantContext context = new RestaurantContext())
            {
                // Retrieve the data from the Categories table
                var categories = context.Categories
                    .Select(c => new { id = c.ID, name = c.Name })
                    .ToList();

                // Set the DisplayMember and ValueMember properties
                cb.DisplayMember = "Name";  // Property from the anonymous type
                cb.ValueMember = "ID";      // Property from the anonymous type

                // Bind the data to the ComboBox
                cb.DataSource = categories;

                // Optional: Set the selected index to -1 to have no selection by default
                cb.SelectedIndex = -1;
            }
        }

    }
}
