using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicAdd.Models
{
    public class ProductViewModel
    {
        public tblProduct Product { get; set; } = new tblProduct();

        public List<tblSize> Sizes { get; set; } = new List<tblSize>();
    }
}