using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Shipping.Mvc.Models.Category
{
    public class CategoryModel
    {

        [Required(ErrorMessage="Please fill category code")]
        public string CategoryCode { set; get; }

        public string CategoryName { set; get; }
    }
}