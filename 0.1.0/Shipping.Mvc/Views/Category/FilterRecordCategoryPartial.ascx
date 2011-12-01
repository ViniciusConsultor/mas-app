<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Shipping.Mvc.Models.Category.CategoryModel>>" %>
<%: @Html.DevExpress().GridView(
            settings =>
            {
                settings.Name = "gvIndex";
                settings.CallbackRouteValues = new { Controller = "City", Action = "FilterRecordCategoryPartial" };
                settings.Width = Unit.Percentage(100);
                
                settings.Columns.Add("CategoryCode");
                settings.Columns.Add("CategoryName");
                settings.Columns.Add(
                    column =>
                    {
                        column.Caption = "Action";
                        column.SetDataItemTemplateContent(c =>
                        {
                            ViewContext.Writer.Write(
                                Html.ActionLink("Edit", "EditCategory", new { ID = DataBinder.Eval(c.DataItem, "CategoryCode") }) + "&nbsp" + 
                                Html.ActionLink("Delete", "DeleteCategory", new {ID = DataBinder.Eval(c.DataItem, "CategoryCode")},
                                new{onclick = "return confirm('Do you want to delete this record ?')"})
                                );
                        });

                        column.SetHeaderTemplateContent(c =>
                        {
                            ViewContext.Writer.Write(
                                Html.ActionLink("New", "AddCategory", new { ID = string.Empty }).ToHtmlString()
                                );
                        });
                    });

                settings.Settings.ShowFilterRow = true;
                settings.CommandColumn.Visible = true;
                settings.CommandColumn.ClearFilterButton.Visible = true;
            }
               ).Bind(Model).GetHtml()
%>
