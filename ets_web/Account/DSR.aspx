<%@ Page Title="ETS:DSR" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DSR.aspx.cs" Inherits="ets_web.Account.DSR" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <link href="http://code.jquery.com/ui/1.11.4/themes/ui-lightness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link rel="Stylesheet" href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.10/themes/redmond/jquery-ui.css" />  
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>  
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"></script> 
    <script type="text/javascript">
        $(function () {            
            $('.autosuggest').autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json;  charset=utf-8",
                        url: "DSR.aspx/GetAutoCompleteData",
                        data: "{ 'username':'" + request.term + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                   return {
                                   value: item
                               }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            +alert("Error");
                        }
                    });
                }
            });
        });
        function validate() {
            var i = $(".progress").attr('value');
            if (i >= 1 && i <= 100) {
                return true;
            }
            else {
            alert("Progress must be in between 1 to 100.");
        }
       return false;
     }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
    <div id="divtitle" runat="server" class="title">
        <br />
        <asp:Label ID="lblTitle" runat="server" Text="Daily Status Report" Visible="true" CssClass="subtitle"></asp:Label>
    </div>
    <!-- Template Gridview-->
    <div class="demo">
        <asp:HiddenField ID="hdn_Id" runat="server" />
        <div class="ui-widget">
            <asp:GridView ID="gvDSRTemplate" runat="server" AutoGenerateColumns="False"
                BorderColor="Black" BorderStyle="Solid" CellPadding="4" ForeColor="#333333"
                HorizontalAlign="Center" ShowHeaderWhenEmpty="True" Width="100%"
                Font-Size="9pt">
                <PagerSettings Mode="NumericFirstLast" Position="Bottom" />

                <Columns>
                    <asp:TemplateField HeaderText="Title">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlTitle" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTitle_SelectedIndexChanged">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                                <asp:ListItem Value="1">Work Item</asp:ListItem>
                                <asp:ListItem Value="2">Implementation</asp:ListItem>
                                <asp:ListItem Value="3">Preparation</asp:ListItem>
                                <asp:ListItem Value="4">Impediment</asp:ListItem>
                                <asp:ListItem Value="5">Adhoc</asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="100" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="TaskID">
                        <ItemTemplate>
                            <asp:TextBox ID="txtTaskID" runat="server" Enabled="false" Width="100"  MaxLength="2" class="autosuggest"/>
                            <%--<ajax:FilteredTextBoxExtender ID="txtTaskID_FilteredTextBoxExtender" runat="server" Enabled="true" TargetControlID="txtTaskID" FilterType="Numbers"></ajax:FilteredTextBoxExtender>--%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="100" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Details">
                        <ItemTemplate>
                            <asp:TextBox ID="txtDetails" runat="server" CssClass="manadatory-textbox" Enabled="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Eff.Perf.">
                        <ItemTemplate>
                            <asp:TextBox ID="txtEffertsPerformed" runat="server" CssClass="manadatory-textbox" Width="100" Enabled="false" MaxLength="2" />
                            <ajax:FilteredTextBoxExtender ID="txtEffertsPerformed_FilteredTextBoxExtender" runat="server" Enabled="true" TargetControlID="txtEffertsPerformed" FilterType="Numbers"></ajax:FilteredTextBoxExtender>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemStyle Width="100" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Eff.Rem.">
                        <ItemTemplate>
                            <asp:TextBox ID="txtEffertsRemaining" runat="server" CssClass="manadatory-textbox" Width="100" Enabled="false" MaxLength="2" />
                            <ajax:FilteredTextBoxExtender ID="txtEffertsRemaining_FilteredTextBoxExtender" runat="server" Enabled="true" TargetControlID="txtEffertsRemaining" FilterType="Numbers"></ajax:FilteredTextBoxExtender>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemStyle Width="100" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Progress">
                        <ItemTemplate>
                            <asp:TextBox ID="txtProgress" runat="server" CssClass="manadatory-textbox progress" Width="100" Enabled="false" MaxLength="3" />
                          <%--  <ajax:FilteredTextBoxExtender ID="txtProgress_FilteredTextBoxExtender" runat="server" Enabled="true" TargetControlID="txtProgress" FilterType="Numbers"></ajax:FilteredTextBoxExtender>--%>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemStyle Width="100" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlStatus" runat="server" Enabled="false">
                                <asp:ListItem Value="0">In Progress</asp:ListItem>
                                <asp:ListItem Value="1">Blocked</asp:ListItem>
                                <asp:ListItem Value="2">Closed</asp:ListItem>
                                <asp:ListItem Value="3">Rejected</asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemStyle Width="100" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnAdd" runat="server" Enabled="false" ToolTip="Add" CssClass="button" OnClick="btnAdd_Click" OnClientClick="return validate();" Text="+" Height="20px" Width="20px" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="btnClear" runat="server" OnClick="btnClear_Click" ToolTip="Clear" Enabled="false" ImageUrl="~/Images/edit_clear.png" Height="20px" Width="20px" />
                        </ItemTemplate>
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
        </div>

    <!--Data Gridview -->
    <div>
        <asp:GridView ID="dvDSRData" runat="server" AutoGenerateColumns="False"
            BorderColor="Black" BorderStyle="Solid" CellPadding="4" ForeColor="#333333"
            HorizontalAlign="Center" ShowHeaderWhenEmpty="True" Width="100%"
            Font-Size="9pt" OnRowCommand="dvDSRData_RowCommand">
            <PagerSettings Mode="NumericFirstLast" Position="Bottom" />
            <Columns>
                <asp:BoundField HeaderText="SNo" DataField="SNO" ItemStyle-Width="30">
                    <ItemStyle Width="30px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField HeaderText="Title" DataField="TITLE" />
                <asp:BoundField HeaderText="TaskID" DataField="TASKID" />
                <asp:BoundField HeaderText="Details" DataField="DETAILS" />
                <asp:BoundField HeaderText="Eff.Perf." DataField="EFFORTSPERFORMED" />
                <asp:BoundField HeaderText="Eff.Rem." DataField="EFFORTSREMAINING" />
                <asp:BoundField HeaderText="Progress" DataField="PROGRESS" />
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("STATUS") %>' />
                        <asp:HiddenField ID="hdnStatus" runat="server" Value='<%#Eval("STATUSID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="btnDelete" runat="server" ToolTip="Remove" ImageUrl="~/Images/DeleteRed.png" CommandArgument='<%#Eval("SNO") %>' Height="20px" Width="20px" />
                    </ItemTemplate>
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
        <br />
        <br />
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" Visible="false" Style="margin-left: 825px" OnClick="btnSubmit_Click" />
        <asp:Button ID="btnRemove" runat="server" Text="Reset" Visible="false" OnClick="btnReset_Click" />
    </div>
</asp:Content>
