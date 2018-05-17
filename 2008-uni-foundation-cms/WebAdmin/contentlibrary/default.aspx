<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="default.aspx.vb" Inherits="Foundation.WebAdmin.contentlibrary._default" %>

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

        <asp:Panel ID="pnlHeaderImages" runat="server" class="textcontentpanel">
            <p><a href="lefthimages.aspx">Left Header Images</a></p>
            <p class="commentunderlink">Upload, view, delete left header images</p>

            <p><a href="himages.aspx">Right Header Images</a></p>
            <p class="commentunderlink">Upload, view, delete right header images</p>
        </asp:Panel>
        <p>
            <asp:Panel ID="pnlQuotes" runat="server" class="textcontentpanel">
                <p><a href="quotes.aspx">Quotes</a></p>
                <p class="commentunderlink">Create, view, edit and delete quotes</p>
            </asp:Panel>
            <p>
                <asp:Panel ID="pnlModules" runat="server" class="textcontentpanel">
                    <p><a href="modules.aspx">Sidebar Modules</a></p>
                    <p class="commentunderlink">Create, view, edit and delete sidebar modules</p>
                </asp:Panel>
                <p>
                    <asp:Panel ID="pnlFiles" runat="server" class="textcontentpanel">
                        <p><a href="files.aspx">Files</a></p>
                        <p class="commentunderlink">Upload, view, edit and delete files</p>
                    </asp:Panel>
                    <p>
                        <asp:Panel ID="pnlImages" runat="server" class="textcontentpanel">
                            <p><a href="images.aspx">Images</a></p>
                            <p class="commentunderlink">Upload, view, edit and delete images</p>
                        </asp:Panel>


                        <!-----------------end of content----------------->
                        <asp:Literal ID="ltrLayout2" runat="server" />
    </form>
</body>
</html>
