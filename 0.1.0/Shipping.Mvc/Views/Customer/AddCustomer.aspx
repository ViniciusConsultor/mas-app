<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TwoColumn.Master" Inherits="System.Web.Mvc.ViewPage<Shipping.Mvc.Models.Customer.CustomerModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AddCustomer
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">

<h2>AddCustomer</h2>

<script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"></script>

<% using (Html.BeginForm()) { %>
    <%: Html.ValidationSummary(true) %>
    <fieldset>
        <legend>CustomerModel</legend>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.CustomerName) %>
        </div>
        <div class="editorContainer">
        <%: @Html.DevExpress().TextBox(
            settings =>
            {
                settings.Name = "tbCustomerName";
                settings.Width = 170;
                settings.Properties.NullText = "Enter your customer name";
            }
            ).GetHtml()
        %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.Office) %>
        </div>
        <div class="editor-field">
            <%: 
                @Html.DevExpress().TextBox(
                    settings => {
                        settings.Name="tbOffice";
                        settings.Width=170;
                        settings.Properties.NullText="Enter your office";
                    }
                ).GetHtml() 
                
            %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.Address) %>
        </div>
        <div class="editor-field">
            <%:
                @Html.DevExpress().TextBox(
                    settings => {
                        settings.Name = "tbAddress";
                        settings.Width=170;
                        settings.Properties.MaskSettings.Mask = "Jl. ";
                        settings.Properties.MaskSettings.IncludeLiterals = MaskIncludeLiteralsMode.None;
                    }
                ).GetHtml()
            %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.Phone) %>
        </div>
        <div class="editor-field">
            <%: 
                @Html.DevExpress().TextBox(
                    settings => {
                        settings.Width = 170;
                        settings.Name = "tbPhone";
                        settings.Properties.MaskSettings.Mask = "+62-000-0000-0000";
                        settings.Properties.MaskSettings.IncludeLiterals = MaskIncludeLiteralsMode.None;
                    }
                ).GetHtml()
            %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.Fax) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.Fax) %>
            <%: Html.ValidationMessageFor(model => model.Fax) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.Email) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.Email) %>
            <%: Html.ValidationMessageFor(model => model.Email) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.ContactPerson) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.ContactPerson) %>
            <%: Html.ValidationMessageFor(model => model.ContactPerson) %>
        </div>

        <p>
            <input type="submit" value="Create" />
        </p>
    </fieldset>
<% } %>

<div>
    <%: Html.ActionLink("Back to List", "Index") %>
</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Header" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Sidebar" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
