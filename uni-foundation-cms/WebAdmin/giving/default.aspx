<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="default.aspx.vb" Inherits="Foundation.WebAdmin.giving._default" %>

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


        <asp:Panel ID="pnlColleges" runat="server" class="textcontentpanel">
            <p><a href="colleges.aspx">Colleges</a></p>
            <p class="commentunderlink">Create, edit, preview and publish college descriptions</p>
        </asp:Panel>
        <p>
            <asp:Panel ID="pnlPrograms" runat="server" class="textcontentpanel">
                <p><a href="programs.aspx">Programs</a></p>
                <p class="commentunderlink">Create, edit, preview and publish program descriptions</p>
            </asp:Panel>
            <p>



                <!-----------------end of content----------------->
                <asp:Literal ID="ltrLayout2" runat="server" />
    </form>
</body>
</html>
