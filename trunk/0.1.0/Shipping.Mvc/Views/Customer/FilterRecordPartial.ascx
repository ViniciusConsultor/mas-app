<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Shipping.Mvc.Models.Customer.CustomerModel>>" %>
<%: @Html.DevExpress().GridView(
            settings =>
            {
                settings.Name = "gvIndex";
                settings.CallbackRouteValues = new{Controller="Customer", Action="FilterRecordPartial"};
                settings.Width = Unit.Percentage(100);
                
                settings.Columns.Add("CustomerName");
                settings.Columns.Add("Office");
                settings.Columns.Add("Address");
                settings.Columns.Add("Phone");
                settings.Columns.Add("Fax");
                settings.Columns.Add("Email");
                settings.Columns.Add("ContactPerson");
                settings.Columns.Add(
                    column => {
                        column.FieldName = "ID";
                        column.Caption = "Action";
                            
                        column.ColumnType = MVCxGridViewColumnType.HyperLink;
                        var hyperlinkProperties = column.PropertiesEdit as HyperLinkProperties;
                        hyperlinkProperties.Text = "Edit";
                        hyperlinkProperties.NavigateUrlFormatString = "https://localhost/Shipping.Mvc/Portal/Customer/Edit?" + column.EditItemTemplate;
                    }
                );

                settings.Settings.ShowFilterRow = true;
                settings.CommandColumn.Visible = true;
                settings.CommandColumn.ClearFilterButton.Visible = true;
            }
               ).Bind(Model).GetHtml()
%>


