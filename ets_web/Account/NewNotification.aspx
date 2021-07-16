<%@ Page Title="ETS:New Notification" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewNotification.aspx.cs" Inherits="ets_web.Account.NewNotification" %>
<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divtitle" runat="server" class="title">
        <asp:Label ID="lblTitle" runat="server" Text="New Notification" Visible="true" CssClass="subtitle"></asp:Label>
    </div>
    <div class="grid_div">
        <table width="100%">
            
            <tr>
                <td align="left" style="width: 20%"><font color="darkgreen" >Title</font></td>
                <td align="left" style="width: 80%">
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="manadatory-textbox" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="2"><font color="darkgreen">Description</font></td>
            </tr>
            <tr>
                <td align="left" colspan="2">
                    <asp:TextBox ID="txtDescription" CssClass="manadatory-textbox"
                         runat="server" Rows="10" Height="200" TextMode="MultiLine" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 20%"><font color="darkgreen">Type</font></td>
                <td align="left" style="width: 80%">
                    <asp:DropDownList ID="ddlType" runat="server" Width="100%" CssClass="select-item">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 20%"><font color="darkgreen">Control Type</font></td>
                <td align="left" style="width: 80%">
                    <asp:DropDownList ID="ddlcontroltype" runat="server" Width="100%" CssClass="select-item">
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td align="center"  colspan="2">
                    <asp:Button ID="btnSubmit" width="100px" runat="server" Text="Submit" CssClass="button" OnClick="btnSubmit_Click" />
                    
                    <asp:Button ID="btnBack" width="100px" runat="server" Text="Back" CssClass="buttons" OnClick="btnBack_Click"/>
                    
                </td>
          
                <td align="left"  colspan="2" font="darkblack">
                    &nbsp;</td>
            </tr>

        </table>
    </div>
</asp:Content>
