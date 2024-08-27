using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PID { get; set; }

        [MaxLength(50)]
        public string PName { get; set; }

        public float PPrice { get; set; }

        public byte[] PImage { get; set; }

        // Foreign key for Category
        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        public Category Category { get; set; }


    }
}
