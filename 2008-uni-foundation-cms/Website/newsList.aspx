<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="newsList.aspx.vb" Inherits="Foundation.WebMain.newsList" %>

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

        <asp:Repeater ID="rptNews" runat="server">
            <ItemTemplate>
                <asp:Label ID="lblYear" runat="server" />
                <div style="margin-left: 15px;">
                    <asp:Label ID="lblMonth" runat="server" />
                    <asp:Literal ID="ltrHighlightDivOpen" runat="server" />
                    <table style="margin-left: 5px;">
                        <tr valign="top">
                            <td>
                                <asp:Literal ID="ltrImage" runat="server" /></td>
                            <td>
                                <p class="news-list-date"><span style="font-size: 10px;">
                                    <asp:Label ID="lblDate" runat="server" /></span></p>
                                <p class="news-list-title"><span style="font-weight: normal; font-size: 11px;">
                                    <asp:HyperLink ID="lnkTitle" runat="server" CssClass="news-list-title-link" /></span></p>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 5px;"></td>
                        </tr>
                    </table>
                    <asp:Literal ID="ltrHighlightDivClose" runat="server" />
                </div>
            </ItemTemplate>
        </asp:Repeater>

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
