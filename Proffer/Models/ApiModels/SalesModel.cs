using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Proffer.Models.ApiModels
{
    public class SalesModel
    {
        [Key]
        public int Id { get; set; }

        public float Offer_price { get; set; }

        public float Sales_price { get; set; }

        [ForeignKey("User_id")]
        public ApiLoginModel User { get; set; }
        
        public string status { get; set; }
    }
}
