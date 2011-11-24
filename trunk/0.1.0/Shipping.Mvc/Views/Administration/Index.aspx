<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TwoColumn.Master" Inherits="System.Web.Mvc.ViewPage<Shipping.Mvc.Models.Supplier.SupplierModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Add Supplier
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">

<h2>Supplier</h2>

<% using (Html.BeginForm("Create", "Administration", FormMethod.Post, new { enctype = "multipart/form-data" }))
   {%>
		<%: Html.ValidationSummary(true) %>

		<fieldset class="one-column">
        <legend>Add Supplier</legend>
                
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

        <p>
            <button name="submit">Create Supplier</button>
        </p>
       
       </fieldset>  
       <% } %>   

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Sidebar" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
