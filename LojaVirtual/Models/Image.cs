﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Models
{
    public class Image
    {
        [Display(Name = "Código")]
        public int Id { get; set; }


        [Display(Name = "Caminho")]
        public string Path { get; set; }


        public int ProductId { get; set; }


        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
