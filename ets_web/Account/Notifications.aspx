<%@ Page Title="ETS:Notification" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Notifications.aspx.cs" Inherits="ets_web.Account.Notifications" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%--<link href="css/Properties.css" rel="stylesheet" type="text/css" />--%>

    <div id="divtitle" runat="server" class="title">
        <asp:Label ID="lblTitle" runat="server" Text="Notifications" Visible="true" CssClass="subtitle"></asp:Label>

        <script>
            function Confim() {
                var result = window.confirm('Are you sure?');
                if (result == true)
                    return true;
                else
                    return false;
            }
        </script>

    </div>
    <div align="right" style="color: #0066FF">
        &nbsp;&nbsp;&nbsp
    </div>
    <div align="right" style="color: #0066FF">
        <asp:Button ID="btnNewNotification" runat="server" Text="New Notification" OnClick="btnNewNotification_Click" />
    </div>
    <div>
        <asp:GridView ID="gvNotificationData" runat="server" AutoGenerateColumns="False"
            BorderColor="Black" BorderStyle="Solid" CellPadding="4" ForeColor="#333333"
            HorizontalAlign="Center" ShowHeaderWhenEmpty="True" Width="100%"
            Font-Size="10pt" OnRowDataBound="gvNotificationData_RowDataBound" OnPageIndexChanging="gvNotificationData_PageIndexChanging" AllowPaging="true" PageSize="<%$ appSettings:gridviewrowcount %>">
            <Columns>


                <asp:TemplateField HeaderText="Title">
                    <ItemTemplate>
                        <asp:Label ID="lblTitle" runat="server" Text='<%#Eval("Title") %>' />
                    </ItemTemplate>
                    <ItemStyle Width="600px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Type">
                    <ItemTemplate>
                        <asp:Label ID="lblType" runat="server" Text='<%#Eval("Type") %>' />
                    </ItemTemplate>
                    <ItemStyle Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Control">
                    <ItemTemplate>
                        <asp:Label ID="lblControl" runat="server" Text='<%#Eval("Control") %>' />
                    </ItemTemplate>
                    <ItemStyle Width="100px" />
                </asp:TemplateField>

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton Text="Inactive" ID="lnkSelect" runat="server" OnClientClick="return Confim();" CommandArgument='<%#Eval("SNo") %>' OnClick="lnkSelect_Click" OnRowDataBound="gvNotificationData_RowDataBound" />
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
