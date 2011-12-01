<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Shipping.Mvc.Models.Condition.ConditionModel>>" %>
<%: @Html.DevExpress().GridView(
            settings =>
            {
                settings.Name = "gvIndex";
                settings.CallbackRouteValues = new{Controller="Condition", Action="FilterRecordConditionPartial"};
                settings.Width = Unit.Percentage(100);
                
                settings.Columns.Add("ConditionCode");
                settings.Columns.Add("ConditionName");
                settings.Columns.Add(
                    column =>
                    {
                        column.Caption = "Action";
                        column.SetDataItemTemplateContent(c =>
                        {
                            ViewContext.Writer.Write(
                                Html.ActionLink("Edit", "EditCondition", new { ID = DataBinder.Eval(c.DataItem, "ConditionCode") }) + "&nbsp" + 
                                Html.ActionLink("Delete", "DeleteCondition", new {ID = DataBinder.Eval(c.DataItem, "ConditionCode")},
                                new{onclick = "return confirm('Do you want to delete this record ?')"})
                                );
                        });

                        column.SetHeaderTemplateContent(c =>
                        {
                            ViewContext.Writer.Write(
                                Html.ActionLink("New", "AddCondition", new { ID = string.Empty }).ToHtmlString()
                                );
                        });
                    });

                settings.Settings.ShowFilterRow = true;
                settings.CommandColumn.Visible = true;
                settings.CommandColumn.ClearFilterButton.Visible = true;
            }
               ).Bind(Model).GetHtml()
%>


