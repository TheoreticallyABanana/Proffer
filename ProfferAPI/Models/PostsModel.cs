using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProfferAPI.Models
{
    public class PostsModel
    {
        [Key]
        [Required]
        public int Posts_id { get; set; }

        [Required]
        public DateTime Date_listed { get; set; }

        [Required]
        public int Product_id { get; set; }
        [ForeignKey("Product_id")]
        public virtual ProductsModel ProductsModel { get; set; }

        [Required]
        public string User_id { get; set; }
        [ForeignKey("User_id")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
