using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    public class Staff
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StaffID { get; set; }

        [MaxLength(50)]
        public string SName { get; set; }

        [MaxLength(50)]
        public string SPhone { get; set; }

        [MaxLength(50)]
        public string SRole { get; set; }
    }
}
