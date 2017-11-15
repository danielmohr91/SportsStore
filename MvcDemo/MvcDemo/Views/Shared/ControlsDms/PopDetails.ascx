<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MvcDemo.Core.Models.DocumentFileViewModel>" %>
<fieldset>
    <legend>Fields</legend>
    <div class="display-label">
        ID</div>
    <div class="display-field">
        <%= Html.Encode(Model.ID) %></div>
    <div class="display-label">
        FileId</div>
    <div class="display-field">
        <%= Html.Encode(Model.FileId) %></div>
    <div class="display-label">
        DocName</div>
    <div class="display-field">
        <%= Html.Encode(Model.DocName) %></div>
</fieldset>
<p>
    <%=Html.ActionLink("Edit", "Edit", new { /* id=Model.PrimaryKey */ }) %>
    |
    <%=Html.ActionLink("Back to List", "Index") %>
</p>
