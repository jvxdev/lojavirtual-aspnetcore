using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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


        [Display(Name = "Quantidade")]
        public int Amount { get; set; }


        [Display(Name = "Peso")]
        public double Weight { get; set; }


        [Display(Name = "Largura")]
        public int Width { get; set; }


        [Display(Name = "Altura")]
        public int Height { get; set; }


        [Display(Name = "Comprimento")]
        public int Lenght { get; set; }


        public int CategoryId { get; set; }


        [Display(Name = "Categoria")]
        public virtual Category Category { get; set; }


        public virtual ICollection<Image> Images { get; set; }
    }
}
