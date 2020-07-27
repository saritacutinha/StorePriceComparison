using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StorePriceComparison.Models
{
    public partial class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }

        [Required, Display(Name = "Product Name"), MaxLength(50)]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("Category")]
        public int CategoryID { get; set; }

        public Category Category { get; set; }

        public ICollection<Price> Prices { get; set; }


    }

}
