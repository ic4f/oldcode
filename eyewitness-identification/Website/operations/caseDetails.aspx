<%@ Page Language="c#" CodeBehind="caseDetails.aspx.cs" AutoEventWireup="false" Inherits="Ei.Website.operations.caseDetails" %>

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

        <p>
            <div style="border: 1px red solid; background-color: #f1f1f1; padding: 10px; text-align: center;">
                <asp:Button ID="btnDelete" CssClass="button" Text="Delete This Case" runat="server" />
                <asp:Literal ID="ltrNoDelete" runat="server" />
            </div>


            <!-----------------end of content----------------->
            <asp:Literal ID="ltrLayout2" runat="server" />
    </form>
</body>
</html>
