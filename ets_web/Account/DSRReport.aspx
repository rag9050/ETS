<%@ Page Title="ETS:DSR Report" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DSRReport.aspx.cs" Inherits="ets_web.Account.DSRReport" %>
<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<link rel="stylesheet" href="http://code.jquery.com/ui/1.11.4/themes/ui-lightness/jquery-ui.css" />
       <script type="text/javascript" src="http://code.jquery.com/jquery-1.10.2.js"></script>
        <script type="text/javascript" src="http://code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
        <script type="text/javascript">
            $(function () {
                $('#<%= txtDSRDate.ClientID %>').datepicker(
                    {
                        dateFormat: 'dd/mm/yy',
                        changeMonth: true,
                        changeYear: true,
                        maxDate: new Date(),
                    });
            });
      </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divtitle" runat="server" class="title">
    <asp:Label ID="lblTitle" runat="server" Text="Member DSR Report" Visible="true" CssClass="subtitle"></asp:Label>
    </div> 
        <div style="width: 600px; float: left">
        Date :
        <asp:TextBox ID="txtDSRDate" runat="server" AutoPostBack="true" OnTextChanged="txtDSRDate_TextChanged1"></asp:TextBox>       
    </div>
        <div align="right" style="color: #0066FF">
            &nbsp;&nbsp;&nbsp
        </div>
        <div>
            <asp:GridView ID="dvDSRData" runat="server" AutoGenerateColumns="False"
            BorderColor="Black" BorderStyle="Solid" CellPadding="4" ForeColor="#333333"
            HorizontalAlign="Center" ShowHeaderWhenEmpty="True" Width="100%"
            Font-Size="10pt" OnPageIndexChanging="dvDSRData_PageIndexChanging" AllowPaging="true" PageSize="<%$ appSettings:gridviewrowcount %>">
            <PagerSettings Mode="NumericFirstLast" Position="Bottom" />
            <Columns>               
                <asp:BoundField HeaderText="Name" DataField="Name" > 
                    <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Title" DataField="Title" >
                    <ItemStyle Width="50px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Description" DataField="Description" />
                 <asp:TemplateField HeaderText="Task ID">
                    <ItemTemplate>
                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("TaskID").ToString() == "0"? "-" : Eval("TaskID").ToString()%>' />
                    </ItemTemplate>
                    <ItemStyle Width="50px" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="E.P." DataField="EffertsPerformed" >
                    <ItemStyle Width="30px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="E.R." DataField="EffertsRemaining" >
                    <ItemStyle Width="30px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Progress" DataField="Progress" >
                    <ItemStyle Width="50px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <asp:Label ID="lblStatus" runat="server" Text='<%# (dcDSRStatus.FirstOrDefault(m => m.Key == Convert.ToInt16(Eval("Status")))).Value%>' />
                    </ItemTemplate>
                    <ItemStyle Width="100px" />
                </asp:TemplateField>
            </Columns>
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <AlternatingRowStyle BackColor="#EFF3FB" />
        </asp:GridView>
        </div>
</asp:Content>
