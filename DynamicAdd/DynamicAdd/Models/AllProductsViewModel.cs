using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicAdd.Models
{
    public class AllProductsViewModel
    {
        public List<ProductViewModel> AllProducts { get; set; } = new List<ProductViewModel>();
    }
}