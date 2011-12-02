<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TwoColumn.Master" Inherits="System.Web.Mvc.ViewPage<Shipping.Mvc.Models.LeadTime.LeadTimeModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AddLeadTime
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">

<h2>AddLeadTime</h2>

<script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"></script>

<% using (Html.BeginForm()) { %>
    <%: Html.ValidationSummary(true) %>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.CityCode) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.CityCode) %>
            <%: Html.ValidationMessageFor(model => model.CityCode) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.Days) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.Days) %>
            <%: Html.ValidationMessageFor(model => model.Days) %>
        </div>

        <p>
            <input type="submit" value="Create" />
        </p>
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
