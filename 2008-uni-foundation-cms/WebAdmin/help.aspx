<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="help.aspx.vb" Inherits="Foundation.WebAdmin.help" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title id="ctlTitleTag" runat="server" />
    <link href="_system/styles/main.css" type="text/css" rel="stylesheet">
    <script language="javascript" type="text/javascript" src="_system/javascript/menuhelper.js"></script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <asp:Literal ID="ltrLayout1" runat="server" />
        <!---------------------content-------------------->




        <p>System Manual:</p>

        <p>
            <dd><a href="manual.pdf">PDF</a></p>
        <p>
            <dd><a href="manual.doc">Word</a></p>




        <!-----------------end of content----------------->
        <asp:Literal ID="ltrLayout2" runat="server" />
    </form>
</body>
</html>
