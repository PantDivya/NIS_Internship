using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Project.Models
{
    public class UploadFile
    {
        public List<HttpPostedFileBase> File { get; set; }
    }
}