<%@  Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MvcDemo.Core.Models.DocumentListViewModel>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    DocumentList
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {

            $(function () {
                var hideDelay = 500;
                var currentID;
                var hideTimer = null;

                // One instance that's reused to show info for the current person  
                var container = $('<div id="personPopupContainer">'
              + '<table width="" border="0" cellspacing="0" cellpadding="0" align="center" class="personPopupPopup">'
              + '<tr>'
              + '   <td class="corner topLeft"></td>'
              + '   <td class="top"></td>'
              + '   <td class="corner topRight"></td>'
              + '</tr>'
              + '<tr>'
              + '   <td class="left">&nbsp;</td>'
              + '   <td><div id="personPopupContent"></div></td>'
              + '   <td class="right">&nbsp;</td>'
              + '</tr>'
              + '<tr>'
              + '   <td class="corner bottomLeft">&nbsp;</td>'
              + '   <td class="bottom">&nbsp;</td>'
              + '   <td class="corner bottomRight"></td>'
              + '</tr>'
              + '</table>'
              + '</div>');

                $('body').append(container);

                $('.DocPopupTrigger').live('mouseover', function () { 
                    var Id = $(this).attr('rel');

                    // If no guid in url rel tag, don't popup blank
                    if (Id == '')
                        return;

                    if (hideTimer)
                        clearTimeout(hideTimer);

                    var pos = $(this).offset();
                    var width = $(this).width();
                    container.css({
                        left: (pos.left + width) + 'px',
                        top: pos.top - 5 + 'px'
                    });

                    $('#personPopupContent').html('&nbsp;');

                    $.ajax({
                        type: 'GET',
                        url: 'Dms/DocFileList/' + Id,
                        cache: true,
                        success: function (data) {
                            // Verify requested person is this person since we could have multiple ajax  
                            // requests out if the server is taking a while.  
                            var text = $(data).html();
                            $('#personPopupContent').html(text);
                        }
                    });

                    container.css('display', 'block');
                });

                $('.DocPopupTrigger').live('mouseout', function () {
                    if (hideTimer)
                        clearTimeout(hideTimer);
                    hideTimer = setTimeout(function () {
                        container.css('display', 'none');
                    }, hideDelay);
                });

                // Allow mouse over of details without hiding details  
                $('#personPopupContainer').mouseover(function () {
                    if (hideTimer)
                        clearTimeout(hideTimer);
                });

                // Hide after mouseout  
                $('#personPopupContainer').mouseout(function () {
                    if (hideTimer)
                        clearTimeout(hideTimer);
                    hideTimer = setTimeout(function () {
                        container.css('display', 'none');
                    }, hideDelay);
                });
            });
        }); 
    </script>
    <h2> DocumentList</h2>
    <fieldset>
        <legend>Fields</legend>
        <div class="display-label">
            Total Item Count</div>
        <div class="display-field">
            <%= Html.Encode(Model.TotalItemCount) %></div>
        <div class="display-label">
            ThisPageItemCount</div>
        <div class="display-field">
            <%= Html.Encode(Model.ThisPageItemCount) %></div>
        <div class="display-label">
            TotalPageCount</div>
        <div class="display-field">
            <%= Html.Encode(Model.TotalPageCount) %></div>
    </fieldset>
    <table>
        <tr>
            <th>
            </th>
            <th>
                ID
            </th>
            <th>
                DocumentName
            </th>
            <th>
                UpdatedDate
            </th>
            <th>
                Description
            </th>
            <th>
                OwnerName
            </th>
            <th>
                FileCount
            </th>
        </tr>
        <% foreach (var item in Model.DocumentList)
           { %>
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id=item.ID }) %>
                |
                <%= Html.ActionLink("Details", "Details", new { id=item.ID })%>
            </td>
            <td>
                <%= Html.Encode(item.ID.ToString()) %>
            </td>
            <td>
                <%= "<a class='DocPopupTrigger' href ='' rel='" + item.ID + "'>" + Html.Encode(item.DocumentName) + "</a>" %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.UpdateDate)) %>
            </td>
            <td>
                <%= Html.Encode(item.Description) %>
            </td>
            <td>
                <%= Html.Encode(item.OwnerName) %>
            </td>
            <td>
                <%= Html.Encode(item.DocFilesCount) %>
            </td>
        </tr>
        <% } %>
    </table>
    <p>
        <%= Html.ActionLink("Create New", "Create") %>
    </p>
    <div id="Test">
    </div>
</asp:Content>
