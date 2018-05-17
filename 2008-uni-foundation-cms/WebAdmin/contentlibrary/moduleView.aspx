<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="moduleView.aspx.vb" Inherits="Foundation.WebAdmin.contentlibrary.moduleView" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title id="ctlTitleTag" runat="server" />
    <link href="../_system/styles/main.css" type="text/css" rel="stylesheet">
    <script language="javascript" type="text/javascript" src="../_system/javascript/menuhelper.js"></script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <asp:Literal ID="ltrLayout1" runat="server" />
        <!---------------------content-------------------->

        <div style="position: relative; height: 620px;">
            <div style="position: absolute; left: 0px; top: 0px;">
                <img src="../_system/images/layout/samplescreenshot.jpg">
            </div>
            <div style="position: absolute; left: 740px; top: 245px; width: 168px; text-align: center;">
                <asp:Literal ID="ltrModule" runat="server" />
            </div>

        </div>

        <!-----------------end of content----------------->
        <asp:Literal ID="ltrLayout2" runat="server" />
    </form>
</body>
</html>
