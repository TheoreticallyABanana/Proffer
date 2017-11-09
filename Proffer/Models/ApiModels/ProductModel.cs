using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proffer.Models.ApiModels
{
    public class ProductModel
    {
        [Required]
        [Key]
        public int Product_id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Product_tag { get; set; }

        [ForeignKey("User_id")]
        public ApiLoginModel User {get; set;}
    }

}
