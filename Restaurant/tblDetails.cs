using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    public class tblDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DetailID { get; set; }

        public int MainID { get; set; }
        public int proID { get; set; }
        public int qty { get; set; }
        public double price { get; set; }
        public double amount { get; set; }

    }
}
