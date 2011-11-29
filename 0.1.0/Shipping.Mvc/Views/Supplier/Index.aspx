<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TwoColumn.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Shipping.Mvc.Models.Supplier.SupplierModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Supplier
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">

<h2>Supplier</h2>

<%: Html.Partial("SupplierPartial", Model)  %>

<%-- <table>
        <tr>
            <th>Supplier Name</th>
            <th>Address</th>
            <th>Phone</th>
        </tr>

<% if (Model.Suppliers != null)
{

    foreach (var supplier in Model.Suppliers.OrderBy(o => o.SupplierName))
    {
        
     %>    
            <tr>
                <td><%: Html.ActionLink(supplier.SupplierName, "Edit", new { id = supplier.Id }) %></td>
                <td><%: supplier.Address %></td>
                <td><%: supplier.Phone %></td>
                <td><%: Html.ActionLink("Delete", "Delete", new { id = supplier.Id })%></td>
            </tr>
<%  
    }
}
%>
    </table>

    <p>
        <%: Html.ActionLink("Create", "Create", "Supplier") %>
    </p>--%>




</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Sidebar" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
