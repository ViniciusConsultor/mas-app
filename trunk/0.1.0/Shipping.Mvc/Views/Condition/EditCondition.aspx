<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TwoColumn.Master" Inherits="System.Web.Mvc.ViewPage<Shipping.Mvc.Models.Condition.ConditionModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    EditCondition
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">

<h2>EditCondition</h2>

<script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"></script>

<% using (Html.BeginForm()) { %>
    <%: Html.ValidationSummary(true) %>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.ConditionCode) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.ConditionCode) %>
            <%: Html.ValidationMessageFor(model => model.ConditionCode) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.ConditionName) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.ConditionName) %>
            <%: Html.ValidationMessageFor(model => model.ConditionName) %>
        </div>

        <p>
            <input type="submit" value="Save" />
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
