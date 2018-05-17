<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="departments.aspx.vb" Inherits="Foundation.WebAdmin.giving.departments" %>

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
            <p><a href="departmentAdd.aspx">New department</a></p>
            <p class="commentunderlink">Add department</p>
        </asp:Panel>
        <p>
            <asp:Panel ID="pnlView" runat="server" class="textcontentpanel">
                <p><a href="departmentList.aspx">List of departments</a></p>
                <p class="commentunderlink">View, edit, delete departments</p>
            </asp:Panel>

            <asp:Panel ID="pnlPageGroupMenu" runat="server" class="textcontentpanel" Visible="false">
                <p style="color: red;">WARNING: You must assign a menu to the college, department and program page groups before you can add pages</p>
                <p>
                    <dd><a href="../websiteorg/pageGroups.aspx">Edit page groups</a>
            </asp:Panel>

            <!-----------------end of content----------------->
            <asp:Literal ID="ltrLayout2" runat="server" />
    </form>
</body>
</html>
