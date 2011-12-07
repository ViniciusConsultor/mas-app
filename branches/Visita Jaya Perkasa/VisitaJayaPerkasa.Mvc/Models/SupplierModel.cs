using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace VisitaJayaPerkasa.Mvc.Models
{
    public class SupplierModel
    {
        public virtual Guid Id { get; set; }

        [DisplayName("Category Type")]
        public string SelectedCategoryCode { get; set; }
        public List<SelectListItem> Categories { get; set; }

        [DisplayName("Supplier Name")]
        public string SupplierName { get; set; }

        [DisplayName("Address")]
        public string Address { get; set; }

        [DisplayName("Phone")]
        public string Phone { get; set; }

        [DisplayName("Fax")]
        public string Fax { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter valid email address")]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Contact Person")]
        public string ContactPerson { get; set; }

        [ScaffoldColumn(false)]
        public int deleted { get; set; }
    }
}