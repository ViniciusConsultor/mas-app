<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Shipping.Mvc.Models.LeadTime.LeadTimeModel>>" %>
<%: @Html.DevExpress().GridView(
            settings =>
            {
                settings.Name = "gvIndex";
                settings.CallbackRouteValues = new { Controller = "LeadTime", Action = "FilterRecordLeadTimePartial" };
                settings.Width = Unit.Percentage(100);

                settings.Columns.Add("CityCode");
                settings.Columns.Add("Days");
                settings.Columns.Add(
                    column =>
                    {
                        column.Caption = "Action";
                        column.SetDataItemTemplateContent(c =>
                        {
                            ViewContext.Writer.Write(
                                Html.ActionLink("Edit", "EditLeadTime", new { ID = DataBinder.Eval(c.DataItem, "CityCode") }) + "&nbsp" + 
                                Html.ActionLink("Delete", "DeleteLeadTime", new {ID = DataBinder.Eval(c.DataItem, "CityCode")},
                                new{onclick = "return confirm('Do you want to delete this record ?')"})
                                );
                        });

                        column.SetHeaderTemplateContent(c =>
                        {
                            ViewContext.Writer.Write(
                                Html.ActionLink("New", "AddLeadTime", new { ID = string.Empty }).ToHtmlString()
                                );
                        });
                    });

                settings.Settings.ShowFilterRow = true;
                settings.CommandColumn.Visible = true;
                settings.CommandColumn.ClearFilterButton.Visible = true;
            }
               ).Bind(Model).GetHtml()
%>


