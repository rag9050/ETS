<%@ Page Title="ETS:NewRequirement" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewRequirement.aspx.cs" Inherits="ets_web.Account.NewRequirements" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divtitle" runat="server" class="title">

        <asp:Label ID="lblTitle" runat="server" Text="Post Requirement" Visible="true" CssClass="subtitle"></asp:Label>
    </div>
    <div class="grid_div">
        <table width="100%">
            <tr runat="server" id="tr1" visible="false">
                <td align="left" style="width:20%">Task Id</td>
                <td align="left" style="width: 80%">
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr runat="server" id="trTaskId" visible="false">
              <td align="left" style="width:20%">Task Id</td>
                <td align="left" style="width: 80%">
                   <asp:TextBox ID="TaskTextBox" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" style="width:20%">Title</td>
                <td align="left" style="width: 80%">
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="manadatory-textbox" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 20%">Type</td>
                <td align="left" style="width: 80%">
                    <asp:DropDownList ID="ddlType" runat="server" Width="100%" CssClass="select-item">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 20%">Estimated Efforts</td>
                <td align="left" style="width: 80%">
                    <asp:TextBox ID="txtEffHours" runat="server" CssClass="manadatory-textbox" Width="100%" MaxLength="2"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="txtEffHours_FilteredTextBoxExtender" runat="server" Enabled="true" TargetControlID="txtEffHours" FilterType="Numbers"></ajax:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 20%">
                    <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>
                </td>
                <td align="left" style="width: 80%">
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="select-item" Width="100%">
                    </asp:DropDownList>
                </td>
            </tr>
            <%--<tr>
                <td align="left" style="width:20%">Assign To
                </td>
                <td>
                    <asp:TextBox ID="txtAssignTo" runat="server" OnTextChanged="txtAssignTo_TextChanged" Width="100%"></asp:TextBox>
                </td>
            </tr>--%>
            <tr>
                <td align="left" colspan="2">Description</td>
            </tr>
            <tr>
                <td align="left" colspan="2">
                    <asp:TextBox ID="txtDescription" CssClass="manadatory-textbox" runat="server" Rows="10" Height="300" TextMode="MultiLine" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr id="trUpload" runat="server" visible="false">
                <td align="left" colspan="2">
                    <asp:FileUpload ID="fupUpload" runat="server" />
                    <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
                </td>
            </tr>
            <%--<tr>
                <td align="left" colspan="2">Comments</td>
            </tr>
            <tr>
                <td align="left" colspan="2">
                    <asp:TextBox ID="txtComments" CssClass="manadatory-textbox" runat="server" Rows="10" Height="100" TextMode="MultiLine" Width="100%"></asp:TextBox>
                </td>
            </tr>--%>
            <tr>
                <td align="center" colspan="2">
                    <asp:Button ID="btnRegister" runat="server" CssClass="button" Text=" Post " OnClick="btnRegister_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" CssClass="buttons" Text="Cancel" OnClick="btnCancel_Click" />

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr id="trgrid" runat="server" visible="true">
                <td>
                    <asp:GridView ID="GridView1" runat="server" Visible="true" AutoGenerateColumns="false" Width="351px">
                        <Columns>

                            <asp:TemplateField HeaderText="FileName">
                                <ItemTemplate>
                                    <asp:Label Text='<%#Eval("FileName") %>' ID="lblname" runat="server"  Width="200px"/>
                                </ItemTemplate>
                                <ItemStyle Width="100px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="FilePath">
                                <ItemTemplate>
                                    <asp:LinkButton Text="Download" ID="lnkdownload" runat="server" CommandArgument='<%#Eval("Download") %>' OnClick="lnkdownload_Click"   Width="200px"/>
                                </ItemTemplate>
                                <ItemStyle Width="100px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label Text='<%#Eval("Date") %>' ID="lnkSelect" runat="server"  Width="200px" />
                                </ItemTemplate>
                                <ItemStyle Width="100px" />
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
