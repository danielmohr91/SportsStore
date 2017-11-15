<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<MvcDemo.Core.Models.DocumentFileViewModel>>" %>

    <table>
        <tr>
            <th></th>
            <th>
                ID
            </th>
            <th>
                Document Name
            </th>
            <th>
                Added Date
            </th>
            <th>
                Latest Version
            </th>
            <th>
                Added By 
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { /* id=item.PrimaryKey */ }) %> |
                <%= Html.ActionLink("Details", "Details", new { /* id=item.PrimaryKey */ })%>
            </td>
            <td>
                <%= Html.Encode(item.ID) %>
            </td>
            <td>
                <%= Html.Encode(item.DocName) %>
            </td> 
            <td>
                <%= Html.Encode(item.AddedDate) %>
            </td>

            <td>
                <%= Html.Encode(item.VersionNumber) %>
            </td>
            <td>
                <%= Html.Encode(item.AddedBy) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink("Create New", "Create") %>
    </p>


