using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shipping.Logging;
using Shipping.Business.Entities;
using Shipping.Business.Entities.Collections;

namespace Shipping.Data.Sql
{
    public class SqlCategoryRepository : ICategoryRepository
    {
        private string _mainConnectionString;
        private Dictionary<string, string> _satelliteConnectionStrings;
        private ILogger _logger;

        public SqlCategoryRepository(string mainConnectionString, Dictionary<string, string> satelliteConnectionStrings, ILogger logger)
        {
            _mainConnectionString = mainConnectionString;
            _satelliteConnectionStrings = satelliteConnectionStrings;
            _logger = logger;
        }

        public List<Category> GetCategories()
        {
            _logger.Debug(this, "Getting list of all Category");


            return SqlUtility.ExecuteXmlStoredProcedure<CategoryCollection>(_mainConnectionString, _logger, "Category_GetCategories");
        }
    }
}
