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
                settings.Name = "textBox1";
                settings.Text = "34343";
            }
            ).GetHtml()
        %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.Office) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.Office) %>
            <%: Html.ValidationMessageFor(model => model.Office) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.Address) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.Address) %>
            <%: Html.ValidationMessageFor(model => model.Address) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.Phone) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.Phone) %>
            <%: Html.ValidationMessageFor(model => model.Phone) %>
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
