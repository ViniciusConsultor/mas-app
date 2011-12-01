<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Shipping.Mvc.Models.City.CityModel>>" %>
<%: @Html.DevExpress().GridView(
            settings =>
            {
                settings.Name = "gvIndex";
                settings.CallbackRouteValues = new { Controller = "City", Action = "FilterRecordCityPartial" };
                settings.Width = Unit.Percentage(100);
                
                settings.Columns.Add("CityCode");
                settings.Columns.Add("CityName");
                settings.Columns.Add(
                    column =>
                    {
                        column.Caption = "Action";
                        column.SetDataItemTemplateContent(c =>
                        {
                            ViewContext.Writer.Write(
                                Html.ActionLink("Edit", "EditCity", new { ID = DataBinder.Eval(c.DataItem, "CityCode") }) + "&nbsp" + 
                                Html.ActionLink("Delete", "DeleteCity", new {ID = DataBinder.Eval(c.DataItem, "CityCode")},
                                new{onclick = "return confirm('Do you want to delete this record ?')"})
                                );
                        });

                        column.SetHeaderTemplateContent(c =>
                        {
                            ViewContext.Writer.Write(
                                Html.ActionLink("New", "AddCity", new { ID = string.Empty }).ToHtmlString()
                                );
                        });
                    });

                settings.Settings.ShowFilterRow = true;
                settings.CommandColumn.Visible = true;
                settings.CommandColumn.ClearFilterButton.Visible = true;
            }
               ).Bind(Model).GetHtml()
%>
