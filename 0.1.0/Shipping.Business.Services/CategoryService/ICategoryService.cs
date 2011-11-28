using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shipping.Business.Entities;
using Shipping.Business.Entities.Collections;

namespace Shipping.Business.Services
{
    public interface ICategoryService
    {
        List<Category> GetCategories();
    }
}
