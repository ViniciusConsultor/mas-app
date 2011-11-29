﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shipping.Business.Entities;
namespace Shipping.Data
{
    public interface ICategoryRepository
    {
        List<Category> GetCategories();
    }
}