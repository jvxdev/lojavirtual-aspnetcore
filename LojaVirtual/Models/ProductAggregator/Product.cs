using LojaVirtual.Libraries.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LojaVirtual.Models.ProductAggregator
{
    public class Product
    {
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E005")]
        [Display(Name = "Nome do produto")]
        [JsonIgnore]
        public string Name { get; set; }


        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E005")]
        [Display(Name = "Descrição")]
        [JsonIgnore]
        public string Description { get; set; }


        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E005")]
        [Display(Name = "Preço")]
        [Column(TypeName = "decimal(18,2)")]
        [JsonIgnore]
        public decimal Price { get; set; }

        // CORREIOS (FRETE)

        [Range(0, 1000, ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E007")]
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E005")]
        [Display(Name = "Quantidade")]
        [JsonIgnore]
        public int Amount { get; set; }


        [Range(0.001, 30, ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E007")]
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E005")]
        [Display(Name = "Peso")]
        [JsonIgnore]
        public double Weight { get; set; }


        [Range(11, 105, ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E007")]
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E005")]
        [Display(Name = "Largura")]
        [JsonIgnore]
        public int Width { get; set; }


        [Range(2, 105, ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E007")]
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E005")]
        [Display(Name = "Altura")]
        [JsonIgnore]
        public int Height { get; set; }


        [Range(16, 105, ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E007")]
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E005")]
        [Display(Name = "Comprimento")]
        [JsonIgnore]
        public int Lenght { get; set; }


        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E005")]
        [Display(Name = "Categoria do produto")]
        [JsonIgnore]
        public int CategoryId { get; set; }


        [ForeignKey("CategoryId")]
        [JsonIgnore]
        public virtual Category Category { get; set; }


        [JsonIgnore]
        public virtual ICollection<Image> Images { get; set; }
    }
}
