using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcContrib.Pagination;
using MvcContrib.UI.Grid;
using MvcContrib.Sorting;
using VisitaJayaPerkasa.Web.Mvc;

namespace VisitaJayaPerkasa.Mvc.Models
{
    public class PagedViewModel<T>
    {
        //this must be saved to session
        private string OldSortOption;

        public ViewDataDictionary ViewData { get; set; }
        public IEnumerable<T> Query { get; set; }
        public GridSortOptions GridSortOptions { get; set; }
        public string DefaultSortColumn { get; set; }
        public IPagination<T> PagedList { get; private set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }

        public PagedViewModel<T> AddFilter(Func<T, bool> predicate)
        {
            if(predicate != null)
                Query = Query.Where(predicate);
            return this;
        }

        public PagedViewModel<T> AddFilter<TValue>(string key, TValue value, Func<T, bool> predicate)
        {
            if (predicate != null)
                ProcessQuery(value, predicate);
            ViewData[key] = value;
            return this;
        }

        public PagedViewModel<T> AddFilter<TValue>(string keyField, object value, Func<T, bool> predicate,
            IEnumerable<TValue> query, string textField)
        {
            if (predicate != null)
                ProcessQuery(value, predicate);
            var selectList = query.ToSelectList(keyField, textField, value);
            ViewData[keyField] = selectList;
            
            return this;
        }

        public PagedViewModel<T> Setup()
        {
            if (string.IsNullOrWhiteSpace(GridSortOptions.Column) || GridSortOptions.Column == "Action")
            {
                GridSortOptions.Column = DefaultSortColumn;
            }

            PagedList = Query.OrderBy(GridSortOptions.Column, GridSortOptions.Direction)
                .AsPagination(Page ?? 1, PageSize ?? 10);
            return this;
        }

        private void ProcessQuery<TValue>(TValue value, Func<T, bool> predicate)
        {
            if (value == null) return;
            if (typeof(TValue) == typeof(string))
            {
                if (string.IsNullOrWhiteSpace(value as string)) return;
            }

            Query = Query.Where(predicate);
        }
    }
}