<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TwoColumn.Master" Inherits="System.Web.Mvc.ViewPage<Shipping.Mvc.Models.Supplier.SupplierModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Supplier
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">

<h2>Edit Supplier</h2>

<% using (Html.BeginForm("Edit", "Supplier", FormMethod.Post, new { id = "edit-action-form" }))
   {%>
		<%: Html.ValidationSummary(true) %>

		<fieldset class="one-column">
        <legend>Edit Supplier</legend>
        
        <div class="editor-label">
            <%: Html.LabelFor(m => m.SelectedCategoryId) %>
        </div>
        <div class="editor-field">
            <%: Html.DropDownListFor(m => m.SelectedCategoryId, Model.Categories)%>
            <%:  Html.ValidationMessageFor(m => m.SelectedCategoryId) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(m => m.SupplierName) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(m => m.SupplierName) %>
            <%: Html.ValidationMessageFor(m => m.SupplierName)%>
        </div>
            
        <div class="editor-label">
              <%: Html.LabelFor(m => m.Address) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(m => m.Address) %>
            <%: Html.ValidationMessageFor(m => m.Address) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(m => m.Phone) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(m => m.Phone) %>
            <%: Html.ValidationMessageFor(m => m.Phone) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(m => m.Fax) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(m => m.Fax) %>
            <%: Html.ValidationMessageFor(m => m.Fax) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(m => m.Email) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(m => m.Email) %>
            <%: Html.ValidationMessageFor(m => m.Email) %>
        </div>

        <%: Html.HiddenFor(model => model.Id)%>

        <p>
            <button name="submit">Edit Supplier</button>
        </p>
       
       </fieldset>  
       <% } %>   

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Sidebar" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
