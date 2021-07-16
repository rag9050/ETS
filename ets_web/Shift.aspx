<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shift.aspx.cs" Inherits="ets_web.Shift" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ETS :: Shift</title>
    <style type="text/css">
        body{ padding:100px; width:1000px; margin:0 auto;}
        #title{font-weight:bolder; color: orchid; text-align:center; font-size:40px; width:100%; margin:0 auto;}
        .radio{font-weight:bold; color: blue; text-align:center; font-size:30px; }
        .time{font-weight:bold; color:seagreen; text-align:center; font-size:30px; }
        .wrapper{text-align:center;}
        .button{height:50px; width:150px; border:groove thin lime; background-color:burlywood; font-size:25px;}
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div id="title">Choose your Shift</div>
        <br /><br /><br />
        <div class="wrapper">
        <asp:RadioButton ID="rbMorning" runat="server" Text="Morning" CssClass="radio" GroupName="choice" /><span class="time"> &nbsp; [9:00am - 01:00pm]</span><span style="width:20x;"></span>
         <asp:RadioButton ID="rbNoon" runat="server" Text="Afternoon" CssClass="radio" GroupName="choice"/><span class="time">&nbsp; [2:00pm - 06:00pm]</span>
            <br /> <br /> <br />
            <asp:Button ID="btnSubmit" runat="server" Text="SUBMIT" CssClass="button" OnClick="btnSubmit_Click"/>
            </div>
    </div>
    </form>
</body>
</html>
