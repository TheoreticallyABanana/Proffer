using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProfferAPI.Models
{
    public class SalesModel
    {
        [Required]
        [Key]
        public int Sales_id { get; set; }

        [Required]
        public decimal Sales_price { get; set; }

        [Required]
        public DateTime Date_sold { get; set; }

        [Required]
        public string User_id { get; set; }
        [ForeignKey("User_id")]
        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        public int Product_id { get; set; }
        [ForeignKey("Product_id")]
        public ProductsModel ProductsModel { get; set; }
    }
}
