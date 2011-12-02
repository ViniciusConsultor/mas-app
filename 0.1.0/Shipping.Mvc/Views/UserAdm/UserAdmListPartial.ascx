<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserAdmListPartial.ascx.cs" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Shipping.Mvc.Models.UserAdm.UserAdmModel>>" %>
<%: Html.DevExpress().GridView(
            settings =>
            {
                settings.Name = "gvIndex";
                settings.CallbackRouteValues = new{Controller="UserAdm", Action="UserAdmListPartial"};
                settings.Width = Unit.Percentage(100);

                settings.Columns.Add("Nik");
                settings.Columns.Add("Username");
                settings.Columns.Add("FirstName");
                settings.Columns.Add("LastName");
                settings.Columns.Add("MobilePhoneNumber");
                settings.Columns.Add("BusinessPhoneNumber");
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
