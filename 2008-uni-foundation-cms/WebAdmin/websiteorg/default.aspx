<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="default.aspx.vb" Inherits="Foundation.WebAdmin.websiteorg._default" %>

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


        <asp:Panel ID="pnlMenus" runat="server" class="textcontentpanel">
            <p><a href="menus.aspx">Website Menus</a></p>
            <p class="commentunderlink">Build and publish web site menus</p>
        </asp:Panel>
        <p>
            <asp:Panel ID="pnlPageGroups" runat="server" class="textcontentpanel">
                <p><a href="pageGroups.aspx">Page Groups</a></p>
                <p class="commentunderlink">View a list of special page groups</p>
            </asp:Panel>




            <!-----------------end of content----------------->
            <asp:Literal ID="ltrLayout2" runat="server" />
    </form>
</body>
</html>
