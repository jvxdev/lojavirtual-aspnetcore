using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Models
{
    public class Product
    {
        [Display(Name = "Código")]
        public int Id { get; set; }


        [Display(Name = "Nome")]
        public string Name { get; set; }


        [Display(Name = "Descrição")]
        public string Description { get; set; }


        [Display(Name = "Preço")]
        public decimal Price { get; set; }
    }
}
