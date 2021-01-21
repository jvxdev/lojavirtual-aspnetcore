using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Models
{
    public class Category
    {

        public int Id { get; set; }

        
        public String Name { get; set; }


        public String Slug { get; set; }


        public int? FatherCategoryId { get; set; }


        [ForeignKey("FatherCategoryId")]
        public virtual Category FatherCategory { get; set; }
    }
}
