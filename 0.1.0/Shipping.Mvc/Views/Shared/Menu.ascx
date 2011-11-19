<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Shipping.Mvc.Models.Header.MenuModel>" %>
<ul>
   <%for (int x = 0; x < Model.Menu.Items.Count; x++)
     {%>
     <li>
        <a href="<%: Model.Menu.Items[x].Url %>"><img src="<%: Model.Menu.Items[x].ImageUrl %>" alt="<%: Model.Menu.Items[x].Alt%>" /></a>     
        <%if (Model.Menu.Items[x].Items.Count > 0)
          {%>
          <ul>
            <%for (int y = 0; y < Model.Menu.Items[x].Items.Count; y++)
              { %>
              <%if (!String.IsNullOrWhiteSpace(Model.Menu.Items[x].Items[y].Section)) {%>
               <li class="section"><%: Model.Menu.Items[x].Items[y].Section %></li>
              <%} else { %>
                <li><a href="<%:Model.Menu.Items[x].Items[y].Url %>"><%:Model.Menu.Items[x].Items[y].Text %></a></li>
              <%} %>
            <%} %>
          </ul>
        <%} %>
     </li>
   <%} %>
</ul>