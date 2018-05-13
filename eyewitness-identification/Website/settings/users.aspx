<%@ Page Language="c#" CodeBehind="users.aspx.cs" AutoEventWireup="false" Inherits="Ei.Website.settings.users" %>

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
            <p><a href="userAdd.aspx">New user</a></p>
            <p class="commentunderlink">Add user</p>
        </asp:Panel>
        <p>
            <asp:Panel ID="pnlView" runat="server" class="textcontentpanel">
                <p><a href="userList.aspx">List of users</a></p>
                <p class="commentunderlink">View, edit, delete users, set roles</p>
                <p><a href="userArchive.aspx">User archive</a></p>
                <p class="commentunderlink">Archived user accounts</p>
            </asp:Panel>



            <!-----------------end of content----------------->
            <asp:Literal ID="ltrLayout2" runat="server" />
    </form>
</body>
</html>
