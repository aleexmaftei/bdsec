using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.Entities.DTOs
{
    public class CartDto
    {
        public int ProductId { get; set; }
        public int QuantityBuy { get; set; }
    }
}
