using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Proffer.Models.ApiModels
{
    public class PostsModel
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date_listed { get; set; }

        [ForeignKey("Product_id")]
        public ProductModel Product { get; set; }

        [ForeignKey("User_id")]
        public ApiLoginModel User { get; set; }
    }
}
