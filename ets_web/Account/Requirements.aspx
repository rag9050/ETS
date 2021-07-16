<%@ Page Title="ETS:Requirement" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Requirements.aspx.cs" Inherits="ets_web.Account.Requirements" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="Stylesheet" href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.10/themes/redmond/jquery-ui.css" />  
<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>  
<script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"></script> 
<script type="text/javascript">
 $(function () {
  $('.autosuggest').autocomplete({
     source: function (request, response) {
     $.ajax({
         type: "POST",
         url: "Requirements.aspx/AutoSearch",
         data: "{ 'Taskid':'" + request.term + "'}",
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
    <div>
        <table>
            <tr>
                <td>
<asp:TextBox ID="txtContactsSearch" runat="server" CssClass="manadatory-textbox "  AutoPostBack="true" OnTextChanged="txtTaskSearch_TextChanged"></asp:TextBox></td>
       <td> <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" /></td>
                <td><asp:ImageButton id="imgRefresh" runat="server" ImageUrl="~/Images/Refresh.png" Width="20" Height="20" OnClick="imgRefresh_Click" style="margin-top: 0px"/></td>
                </tr>
   </table>
    </div>
      <div id="divtitle" runat="server" class="title">
    <asp:Label ID="lblTitle" runat="server" Text="Project Requirements" Visible="true" CssClass="subtitle"></asp:Label>
    </div> 
        <div style="width:175px; float:right">
           
            <asp:Button ID="btnnewrequirement" runat="server" Text="New Requirement"   OnClick="btnnewrequirements_Click" />         
        </div>
        <div align="right" style="color: #0066FF">
            &nbsp;&nbsp;&nbsp
        </div>
        <asp:GridView ID="gvEmployeeData" runat="server" AutoGenerateColumns="False" 
            BorderColor="Black" BorderStyle="Solid" CellPadding="4" ForeColor="#333333"
            HorizontalAlign="Center" ShowHeaderWhenEmpty="True" Width="100%" 
            Font-Size="9pt" OnRowDataBound="gvEmployeeData_RowDataBound" AllowPaging="True"
            OnPageIndexChanging="gvEmployeeData_PageIndexChanging"  PageSize="<%$appSettings:gridviewrowcount %>">
         <PagerSettings Mode="NumericFirstLast" Position="Bottom" />
            
            <Columns>
                <asp:TemplateField HeaderText="Task ID">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle Width="50px" />
                    <ItemTemplate>                       
                        <asp:LinkButton ID="lbtnTaskId" runat="server" Text='<%#"CR-"+Eval("TaskId") %>' OnClick="lnkView_Click"></asp:LinkButton>
                        <asp:HiddenField ID="hdnTaskID" runat="server" Value='<%#Eval("TaskId") %>' />
                    </ItemTemplate>
                </asp:TemplateField>         
                <asp:TemplateField HeaderText="Title">
                    <ItemStyle />
                    <ItemTemplate>
                        <asp:Label ID="lblTitle" runat="server" Text='<%#Eval("Title").ToString().Length>50?(Eval("Title") as string).Substring(0,50)+"...":Eval("Title")  %>'  ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Assigned To">
                    <ItemStyle />
                    <ItemTemplate>
                        <asp:Label ID="lblAssignedName" runat="server" Text='<%#String.IsNullOrEmpty("AssignedName")?"-":Eval("AssignedName")%>'  ></asp:Label>
                    </ItemTemplate>
                     <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>                
                <asp:BoundField HeaderText="Type" DataField="Type" >
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle Width="125px" />
                    </asp:boundField>
               <asp:BoundField HeaderText="Status" DataField="TaskStatus" >
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle Width="125px" />
                </asp:BoundField>               
                <asp:BoundField DataField="CreatedDate" HeaderText="Assigned Date" >
                 <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
                 <asp:TemplateField HeaderText="Assign" Visible="false">
                     <HeaderStyle HorizontalAlign="Center" />
                     <ItemStyle Width="30px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkbtn" runat="server" OnClick="lnkbtn_Click" >Assign</asp:LinkButton>        
                    </ItemTemplate>
                </asp:TemplateField>
                                                
            </Columns>
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"/>
            <RowStyle BackColor="#EFF3FB" />
            <AlternatingRowStyle BackColor="#EFF3FB" />
        </asp:GridView>
</asp:Content>
