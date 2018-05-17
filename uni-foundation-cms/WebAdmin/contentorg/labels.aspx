<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="labels.aspx.vb" Inherits="Foundation.WebAdmin.contentorg.labels" %>

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
            <p><a href="labelAdd.aspx">New label</a></p>
            <p class="commentunderlink">Add label</p>
        </asp:Panel>
        <p>
            <asp:Panel ID="pnlView" runat="server" class="textcontentpanel">
                <p><a href="labelList.aspx">List of labels</a></p>
                <p class="commentunderlink">View, edit, delete labels, view lists of files/images/web pages for each label</p>
            </asp:Panel>



            <!-----------------end of content----------------->
            <asp:Literal ID="ltrLayout2" runat="server" />
    </form>
</body>
</html>
