<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="menu.aspx.vb" Inherits="Foundation.WebAdmin.menu" %>

<html>
<head>
    <link href="../../_system/styles/datalist.css" type="text/css" rel="stylesheet">
    <script language="javascript" type="text/javascript" src="../../_system/javascript/menuhelper.js"></script>
</head>
<body bgcolor="#ffffff">
    <form id="Form2" method="post" runat="server">
        <div class="iframe" style="width: 200px;">
            <p>
                <asp:Literal ID="ltrTreeDisplay" runat="server" /></p>
        </div>
    </form>
</body>
</html>
