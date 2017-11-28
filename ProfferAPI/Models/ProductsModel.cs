using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProfferAPI.Models
{
    public class ProductsModel
    {

        [Key]
        public int Product_id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Tag { get; set; }

        public DateTime Upload_date { get; set; }

        public string ImageName { get; set; }

        [Required]
        public string User_id { get; set; }
        [ForeignKey("User_id")]
        public ApplicationUser ApplicationUser { get; set; }

        public List<SalesModel> SalesModel { get; set; }
    }
}
