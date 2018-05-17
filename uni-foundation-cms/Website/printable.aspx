<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="printable.aspx.vb" Inherits="Foundation.WebMain.printable" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>printable</title>
</head>
<body style="margin: 20; font-family: Verdana, Arial, Sans-Serif; font-size: 12px;">
    <form id="Form1" method="post" runat="server">

        <p style="padding-bottom: 5px; border-bottom: 1px solid #cacaca;">
            <img src="_system/images/layout/printablelogo.gif"></p>

        <h3>
            <asp:Literal ID="ltrBodyTitle" runat="server" /></h3>
        <asp:Literal ID="ltrBody" runat="server" />

        <p style="padding-bottom: 5px; border-bottom: 1px solid #cacaca;">
        <p style="font-size: 11px; color: #666666;">
            Text from:
			<br>
            <asp:Literal ID="ltrUrl" runat="server" />
        </p>


    </form>
</body>
</html>
