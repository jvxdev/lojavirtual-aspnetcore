﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Models
{
    public class OrderSituation
    {
        public int Id { get; set; }


        [ForeignKey("Order")]
        public int? OrderId { get; set; }


        public DateTime Date { get; set; }


        public string Situation { get; set; }


        public string Data { get; set; }


        public virtual Order Order { get; set; }
    }
}
