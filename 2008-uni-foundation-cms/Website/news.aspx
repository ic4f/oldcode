<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="news.aspx.vb" Inherits="Foundation.WebMain.news" %>

<%@ Register TagPrefix="uc" TagName="layout1" Src="_system/controls/LayoutHelper1.ascx" %>
<%@ Register TagPrefix="uc" TagName="layout2" Src="_system/controls/LayoutHelper2.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title id="htmlTitle" runat="server" />
    <link href="_system/styles/main.css" rel="stylesheet" type="text/css">
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <uc:layout1 ID="ucLayoutHelper1" runat="server" />

        <h3>News Archive</h3>
        <p style="font-size: 11px; margin-bottom: 0px;">
            <asp:Literal ID="ltrNewsDate" runat="server" /></p>
        <p style="margin-top: 5px; font-weight: bold; color: #56386c; font-size: 13px;">
            <asp:Literal ID="ltrNewsTitle" runat="server" /></p>
        <p>
            <asp:Literal ID="ltrNewsBody" runat="server" /></p>

        <asp:Literal ID="ltrTags" runat="server" />

        <asp:Literal ID="ltrBookmarks" runat="server" />
        <p>
            <table cellpadding='0' cellspacing='0' style='font-size: 10px; width: 100%;'>
                <tr>
                    <td>
                        <asp:Literal ID="ltrPrintable" runat="server" /></td>
                    <td align="right">
                        <asp:Literal ID="ltrRevised" runat="server" />
                    </td>
                </tr>
            </table>

            <uc:layout2 ID="ucLayoutHelper2" runat="server" />
    </form>
    <script src="http://www.google-analytics.com/urchin.js" type="text/javascript">
    </script>
    <script type="text/javascript">
        _uacct = "UA-1066817-3";
        urchinTracker();
    </script>
</body>
</html>
