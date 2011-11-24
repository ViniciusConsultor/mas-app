﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TwoColumn.Master" Inherits="System.Web.Mvc.ViewPage<Shipping.Mvc.Models.Supplier.SupplierIndexModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit Supplier
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">

<h2>Edit Supplier</h2>

 <table>
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
                <td><%: Html.ActionLink("Delete", "DeleteTree", new { id = supplier.Id }) %></td>
            </tr>
<%  
    }
}
%>
    </table>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Sidebar" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
