<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Shipping.Mvc.Models.Supplier.SupplierModel>>" %>
<%: Html.DevExpress().GridView(
            settings =>
            {
                settings.Name = "gvIndex";
                settings.CallbackRouteValues = new{Controller="Supplier", Action="SuppplierPartial"};
                settings.Width = Unit.Percentage(100);

                settings.Columns.Add("SupplierName");
                settings.Columns.Add("Address");
                settings.Columns.Add("Phone");
                settings.Columns.Add("Fax");
                settings.Columns.Add("Email");
                settings.Columns.Add(
                    column =>
                    {
                        column.Caption = "Action";
                        column.SetDataItemTemplateContent(c =>
                        {
                            ViewContext.Writer.Write(
                                Html.ActionLink("Edit", "Edit", new { ID = DataBinder.Eval(c.DataItem, "Id") }) + "&nbsp" +
                                Html.ActionLink("Delete", "Delete", new { ID = DataBinder.Eval(c.DataItem, "Id") },
                                new { onclick = "return confirm('Do you want to delete this record ?')" })
                                );
                        });

                        column.SetHeaderTemplateContent(c =>
                        {
                            ViewContext.Writer.Write(
                                Html.ActionLink("New", "Create").ToHtmlString());
                        });
                    });

                settings.Settings.ShowFilterRow = true;
                settings.CommandColumn.Visible = true;
                settings.CommandColumn.ClearFilterButton.Visible = true;
            }
               ).Bind(Model).GetHtml()
%>
