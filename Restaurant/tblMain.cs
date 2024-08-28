using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    public class tblMain
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MainID { get; set; }
        public DateTime aDate { get; set; }

        [MaxLength(15)]
        public string aTime { get; set; }

        [MaxLength(15)]
        public string TableName { get; set; }

        [MaxLength(15)]
        public string WaiterName { get; set; }

        [MaxLength(15)]
        public string status { get; set; }

        [MaxLength(15)]
        public string orderType { get; set; }

        public double Total { get; set; }

        public double received { get; set; }

        public double change { get; set; }

        public int driverID { get; set; }
        public string CustName { get; set; }
        public string CustPhone { get; set; }


    }
}
