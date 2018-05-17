<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="default.aspx.vb" Inherits="Foundation.WebAdmin.settings._default" %>

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


        <asp:Panel ID="pnlUsers" runat="server" class="textcontentpanel">
            <p><a href="users.aspx">Users</a></p>
            <p class="commentunderlink">Create, view, edit, delete users</p>
        </asp:Panel>
        <p>
            <asp:Panel ID="pnlRoles" runat="server" class="textcontentpanel">
                <p><a href="roles.aspx">Roles & Permissions</a></p>
                <p class="commentunderlink">Create, view, edit, delete roles</p>
            </asp:Panel>
            <p>
                <asp:Panel ID="pnlPages" runat="server" class="textcontentpanel">
                    <p><a href="customPages.aspx">Custom Pages</a></p>
                    <p class="commentunderlink">Create, view, edit, delete custom pages</p>
                </asp:Panel>



                <!-----------------end of content----------------->
                <asp:Literal ID="ltrLayout2" runat="server" />
    </form>
</body>
</html>
