<%@ Page Title="ETS:Map User with Task" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MapUserWithTask.aspx.cs" Inherits="ets_web.Account.MapUserWithTask" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />  
<link rel="Stylesheet" href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.10/themes/redmond/jquery-ui.css" />  
<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>  
<script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"></script> 
    <script type="text/javascript">
        $(function () {
            $('#<%=txtMemberName.ClientID%>').autocomplete({
                 source: function (request, response) {
                     $.ajax({
                         type: "POST",
                         url: "MapUserWithTask.aspx/GetCountryName",
                         data: "{ 'pre':'" + request.term + "'}",
                         dataType: "json",
                         contentType: "application/json; charset=utf-8",
                         success: function (data) {
                             response($.map(data.d, function (item) {
                                 return {
                                     value: item
                                 }
                             }))
                         },
                         error: function (XMLHttpRequest, textStatus, errorThrown) {
                             alert("Error");
                         }
                     });
                 }
             });
         });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divtitle" runat="server" class="title">
        <asp:Label ID="lblTitle" runat="server" Text="Assign Task To Member" Visible="true" CssClass="subtitle"></asp:Label>
    </div>
    <div class="grid_div">
        <table width="100%">
            <tr>
                <td align="left" style="width: 20%">Task ID</td>
                <td align="left" style="width: 80%">
                    <asp:Label ID="lblTaskID" runat="server" />
                </td>
            </tr>
             <tr>
                <td align="left" style="width: 20%">Title</td>
                <td align="left" style="width: 80%">
                    <asp:Label ID="lblTaskTitle" runat="server"  />
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 20%">
                    Member Name</td>
                <td align="left" style="width: 80%">
                   <asp:TextBox ID="txtMemberName" runat="server" CssClass="manadatory-textbox" Width="50%"></asp:TextBox>
                    <asp:HiddenField ID="hdnUserCode" runat="server" />
                </td>
            </tr>  
             <tr>
                <td align="left" style="width: 20%">
                    Efforts</td>
                <td align="left" style="width: 80%">
                   <asp:TextBox ID="txtEfforts" runat="server" CssClass="manadatory-textbox" Width="50%"></asp:TextBox>
                </td>
            </tr>           
            <tr>
                <td align="left" colspan="2">
                    &nbsp;</td>
            </tr>            
            <tr>
                <td align="center" colspan="2">
                    <asp:Button ID="btnAssign" runat="server" CssClass="button" Text="Assign" OnClick="btnAssign_Click" />&nbsp;&nbsp;
                            <asp:Button ID="btnClose" runat="server" CssClass="buttons" Text="Close" OnClick="btnClose_Click"/>

                </td>
            </tr>
        </table>
    </div>
    <div><asp:HiddenField ID="hdnTaksID" runat="server" /></div>
</asp:Content>
