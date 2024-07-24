using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicAdd.Models
{
    public partial class tblProduct
    {
        public List<tblSize> Sizes { get; set; } = new List<tblSize>();
    }
}   