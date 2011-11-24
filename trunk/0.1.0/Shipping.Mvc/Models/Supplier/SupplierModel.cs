using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Web.Mvc;

namespace Shipping.Mvc.Models.Supplier
{
    public class SupplierModel
    {
        public virtual Guid Id { get; set; }
        public List<SelectListItem> Categories { get; set; }
        [DisplayName("Category")]
        public Guid? SelectedCategoryId { get; set; }
        [DisplayName("Supplier Name")]
        public string SupplierName { get; set; }
        [DisplayName("Address")]
        public string Address { get; set; }
        [DisplayName("Phone")] 
        public string Phone { get; set; }
        [DisplayName("Fax")]    
        public string Fax { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
    }
}