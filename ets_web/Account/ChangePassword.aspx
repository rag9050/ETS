<%@ Page Title="ETS:ChangePassword" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="ets_web.ChangePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript">
    function cnt(text) {
        var Password = $('.newpassword');
        if (Password.val().length < 6)
        {
            $('#alertmsg').text("Password minimum length should be 6");
            Password.focus();
            return false;
        }
        else {
            $('#alertmsg').text("");
        }
    }
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divtitle" runat="server" class="title">

        <asp:Label ID="lblTitle" runat="server" Text="Change Account Password" Visible="true" CssClass="subtitle"></asp:Label>
    </div>
    <div class="grid_div">
        <table width="100%">
            <tr>
                <td align="left" style="width: 20%">New Password</td>
                <td align="left" style="width: 80%">
                    <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" MaxLength="20" onblur="cnt(this)" CssClass="manadatory-textbox newpassword" Width="50%"></asp:TextBox>
                    <span id="alertmsg"  style="color:red"></span>
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 20%">
                    Confirm Password</td>
                <td align="left" style="width: 80%">
                   <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" MaxLength="20" CssClass="manadatory-textbox confirmpassword" Width="50%"></asp:TextBox></td>
            </tr>           
            <tr>
                <td align="left" colspan="2">
                    &nbsp;</td>
            </tr>            
            <tr>
                <td align="center" colspan="2">
                    <asp:Button ID="btnChangePassword" runat="server" CssClass="button changepassword" Text=" Change Password " OnClick="btnChangePassword_Click" />&nbsp;&nbsp;
                            <asp:Button ID="btnCancel" runat="server" CssClass="buttons" Text="Cancel"/>

                </td>
            </tr>
        </table>
    </div>
</asp:Content>
