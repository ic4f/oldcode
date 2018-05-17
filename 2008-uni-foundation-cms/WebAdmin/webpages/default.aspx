<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="default.aspx.vb" Inherits="Foundation.WebAdmin.webpages._default" %>

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

        <asp:Panel ID="pnlPages" runat="server" class="textcontentpanel">
            <p><a href="pages.aspx">Standard Pages</a></p>
            <p class="commentunderlink">Create, edit, preview and publish standard pages</p>
        </asp:Panel>
        <p>
            <asp:Panel ID="pnlNews" runat="server" class="textcontentpanel">
                <p><a href="news.aspx">News</a></p>
                <p class="commentunderlink">Create, edit, preview and publish news</p>
            </asp:Panel>
            <p>
                <asp:Panel ID="pnlDstories" runat="server" class="textcontentpanel">
                    <p><a href="dstories.aspx">Donor Stories</a></p>
                    <p class="commentunderlink">Create, edit, preview and publish donor stories</p>
                </asp:Panel>
                <p>
                    <asp:Panel ID="pnlGiving" runat="server" class="textcontentpanel">
                        <p><a href="../giving/">Giving Opportunities</a></p>
                        <p class="commentunderlink">Create, edit, preview and publish college, department and program descriptions</p>
                    </asp:Panel>
                    <p>
                        <asp:Panel ID="pnlAll" runat="server" class="textcontentpanel">
                            <p><a href="allPages.aspx">All Web Pages</a></p>
                            <p class="commentunderlink">Create, edit, preview and publish all types of pages</p>
                        </asp:Panel>
                        <p>




                            <!-----------------end of content----------------->
                            <asp:Literal ID="ltrLayout2" runat="server" />
    </form>
</body>
</html>
