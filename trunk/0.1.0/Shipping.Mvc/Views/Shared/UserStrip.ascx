<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Shipping.Mvc.Models.Header.UserStripModel>" %>
<ul>
 	<li><a href="<%: Url.Action("RedirectToLogIn", "LogOut", new { ReturnUrl = Request.Url.ToString() }) %>" id="header-logout"><%: Model.FullName %> (Logout)</a>&nbsp;|&nbsp;</li>
    <%if (Model.IsAdmin)
    {%>
	<li><a href="<%: String.Format(Model.Portal50Url,"account/account.aspx") %>">Account Management</a></li>
    <%}
      else
      { %>
      <li><a href="<%: String.Format(Model.Portal50Url,"account/account.aspx") %>">My Account</a></li>
    <%} %>
</ul>