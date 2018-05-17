<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="default.aspx.vb" Inherits="Foundation.WebAdmin.contentorg._default" %>

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

        <asp:Panel ID="pnlTags" runat="server" class="textcontentpanel">
            <p><a href="tags.aspx">Public Tags</a></p>
            <p class="commentunderlink">Create, view, edit, delete public tags</p>
        </asp:Panel>
        <p>
            <asp:Panel ID="pnlFileTypes" runat="server" class="textcontentpanel">
                <p><a href="filetypes.aspx">File Types</a></p>
                <p class="commentunderlink">View and edit file types</p>
            </asp:Panel>
            <p>
                <asp:Panel ID="pnlLabels" runat="server" class="textcontentpanel">
                    <p><a href="labels.aspx">Labels</a></p>
                    <p class="commentunderlink">Create, view, edit, delete content labels</p>
                </asp:Panel>


                <!-----------------end of content----------------->
                <asp:Literal ID="ltrLayout2" runat="server" />
    </form>
</body>
</html>
