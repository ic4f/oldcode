<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="lefthimages.aspx.vb" Inherits="Foundation.WebAdmin.contentlibrary.lefthimages" %>

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


        <asp:Panel ID="pnlModify" runat="server" class="textcontentpanel">
            <p><a href="lefthimageAdd.aspx">New left header image</a></p>
            <p class="commentunderlink">Updoad new left header image</p>
        </asp:Panel>
        <p>
            <asp:Panel ID="pnlView" runat="server" class="textcontentpanel">
                <p><a href="lefthimageList.aspx">List of left header images</a></p>
                <p class="commentunderlink">View, delete left header images</p>
            </asp:Panel>



            <!-----------------end of content----------------->
            <asp:Literal ID="ltrLayout2" runat="server" />
    </form>
</body>
</html>
