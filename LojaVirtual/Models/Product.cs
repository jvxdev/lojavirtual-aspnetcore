using LojaVirtual.Libraries.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaVirtual.Models
{
    public class Product
    {
        [Display(Name = "Código")]
        public int Id { get; set; }


        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E005")]
        [Display(Name = "Nome do produto")]
        public string? Name { get; set; }


        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E005")]
        [Display(Name = "Descrição")]
        public string? Description { get; set; }


        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E005")]
        [Display(Name = "Preço")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }


        [Range(0, 1000, ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E007")]
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E005")]
        [Display(Name = "Quantidade")]
        public int? Amount { get; set; }


        [Range(0.001, 30, ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E007")]
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E005")]
        [Display(Name = "Peso")]
        public double? Weight { get; set; }


        [Range(11, 105, ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E007")]
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E005")]
        [Display(Name = "Largura")]
        public int? Width { get; set; }


        [Range(2, 105, ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E007")]
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E005")]
        [Display(Name = "Altura")]
        public int? Height { get; set; }


        [Range(16, 105, ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E007")]
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E005")]
        [Display(Name = "Comprimento")]
        public int? Lenght { get; set; }


        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E005")]
        [Display(Name = "Categoria do produto")]
        public int? CategoryId { get; set; }


        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }


        public virtual ICollection<Image> Images { get; set; }
    }
}
