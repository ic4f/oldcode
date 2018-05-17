<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="tags.aspx.vb" Inherits="Foundation.WebAdmin.contentorg.tags" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title id="ctlTitleTag" runat="server" />
    <link href="../_system/styles/main.css" type="text/css" rel="stylesheet">
    <script language="javascript" type="text/javascript" src="../_system/javascript/menuhelper.js"></script>
    <link href="../_gridhelper/grid.css" type="text/css" rel="stylesheet">
    <script language="javascript" type="text/javascript" src="../_gridhelper/gridscripts.js"></script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <asp:Literal ID="ltrLayout1" runat="server" />
        <!---------------------content-------------------->

        <asp:Panel ID="pnlModify" runat="server" class="textcontentpanel">
            <p><a href="tagAdd.aspx">New tag</a></p>
            <p class="commentunderlink">Add public tag</p>
        </asp:Panel>
        <p>
            <asp:Panel ID="pnlView" runat="server" class="textcontentpanel">
                <p><a href="tagList.aspx">List of public tags</a></p>
                <p class="commentunderlink">View, edit, delete public tags, view lists of web pages for each tag</p>
            </asp:Panel>



            <!-----------------end of content----------------->
            <asp:Literal ID="ltrLayout2" runat="server" />
    </form>
</body>
</html>
