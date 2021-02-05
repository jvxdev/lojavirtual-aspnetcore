using LojaVirtual.Libraries.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Models
{
    public class Category
    {

        [Display(Name = "Código")]
        public int Id { get; set; }


        [Display(Name = "Nome")]
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E005")]
        [MinLength(2, ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E002")]
        public String Name { get; set; }


        [Display(Name = "Slug")]
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E005")]
        [MinLength(2, ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E002")]
        public String Slug { get; set; }


        [Display(Name = "Categoria Pai")]
        public int? FatherCategoryId { get; set; }


        [ForeignKey("FatherCategoryId")]
        public virtual Category FatherCategory { get; set; }
    }
}
