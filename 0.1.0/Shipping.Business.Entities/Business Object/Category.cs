using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Shipping.Business.Entities
{
    [Table(Name="Category")]
    public class Category
    {

        [Column(Name="category_code", IsPrimaryKey=true)]
        public string CategoryCode { set; get; }

        [Column(Name="category_name")]
        public string CategoryName { set; get; }
    }
}
