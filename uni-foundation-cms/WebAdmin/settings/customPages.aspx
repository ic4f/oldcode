<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="customPages.aspx.vb" Inherits="Foundation.WebAdmin.settings.customPages" %>

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
            <p>
                This area is for developers only. A custom page is a web page which has been added to the system as a NEW FILE. 					 
					<p><a href="customPageRegister.aspx">New custom page</a></p>
            <p class="commentunderlink">Register custom page</p>
            <p><a href="customPageList.aspx">List of custom pages</a></p>
            <p class="commentunderlink">View, edit, delete custom pages</p>
        </asp:Panel>


        <!-----------------end of content----------------->
        <asp:Literal ID="ltrLayout2" runat="server" />
    </form>
</body>
</html>
