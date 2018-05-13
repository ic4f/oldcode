<%@ Page Language="c#" CodeBehind="default.aspx.cs" AutoEventWireup="false" Inherits="Ei.Website.operations._default" %>

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



        <asp:Panel ID="pnlCases" runat="server" class="textcontentpanel">
            <p><a href="cases.aspx">Cases</a></p>
            <p class="commentunderlink">Create, view, edit, delete cases; assign users, add suspects</p>
        </asp:Panel>
        <p>
            <asp:Panel ID="pnlSuspects" runat="server" class="textcontentpanel">
                <p><a href="suspects.aspx">Suspects</a></p>
                <p class="commentunderlink">Create, view, edit, delete suspects</p>
            </asp:Panel>
            <p>
                <asp:Panel ID="pnlLineups" runat="server" class="textcontentpanel">
                    <p><a href="lineups.aspx">Lineups & Results</a></p>
                    <p class="commentunderlink">View lineups & lineup administration results</p>
                </asp:Panel>



                <!-----------------end of content----------------->
                <asp:Literal ID="ltrLayout2" runat="server" />
    </form>
</body>
</html>
