<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="default.aspx.vb" Inherits="Foundation.WebMain._default" %>

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

        <asp:Panel runat="server" ID="pnlFeature" CssClass="homepage-panel-feature">
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Literal ID="ltrFeature" runat="server" />
                    </td>
                </tr>
            </table>
        </asp:Panel>

        <p>
            <asp:Panel ID="pnl100" runat="server">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr valign="top">
                        <td>

                            <asp:Panel runat="server" ID="pnlNews100" CssClass="homepage-panel-news">
                                <p class="homepage-panel-title">News & Updates</p>
                                <asp:Repeater ID="rptNews100" runat="server">
                                    <ItemTemplate>
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
                                    </ItemTemplate>
                                </asp:Repeater>
                                <p class="homepage-readall-link"><a href="newsList.aspx?id=lotw36p2">view all news</a></p>
                            </asp:Panel>

                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <p>
                <asp:Panel ID="pnl5050" runat="server">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr valign="top">
                            <td style="width: 49%;">

                                <asp:Panel runat="server" ID="pnlNews5050" CssClass="homepage-panel-news">
                                    <p class="homepage-panel-title">News & Updates</p>
                                    <asp:Repeater ID="rptNews5050" runat="server">
                                        <ItemTemplate>
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
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <p class="homepage-readall-link"><a href="newsList.aspx?id=lotw36p2">view all news</a></p>
                                </asp:Panel>

                            </td>
                            <td style="width: 2%;"></td>
                            <td style="width: 49%;">

                                <asp:Panel runat="server" ID="pnlDStories" CssClass="homepage-panel-dstories">
                                    <p class="homepage-panel-title">Donor Stories</p>
                                    <asp:Repeater ID="rptDStories" runat="server">
                                        <ItemTemplate>
                                            <div class="dstory-list-div">
                                                <p style="margin-top: 10px;">
                                                    <asp:Literal ID="ltrImage" runat="server" /></p>
                                                <p class="dstory-list-title" style="margin-top: 0px;">
                                                    <asp:Literal ID="ltrTitle" runat="server" /></p>
                                                <p class="dstory-list-summary">
                                                    <asp:Literal ID="ltrSummary" runat="server" /></p>
                                            </div>
                                            <p>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <p class="homepage-readall-link"><a href="dstoryList.aspx?id=lotw36p3">view all stories</a></p>
                                </asp:Panel>

                                <p>

                                    <asp:Panel runat="server" ID="pnlPrograms" CssClass="homepage-panel-programs">
                                        <p class="homepage-panel-title">Featured Programs</p>
                                        <asp:Repeater ID="rptPrograms" runat="server">
                                            <ItemTemplate>
                                                <p>
                                                    <asp:HyperLink ID="lnkProgram" runat="server" /></p>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <p class="homepage-readall-link"><a href="programList.aspx?id=lotw36p6">view all programs</a></p>
                                    </asp:Panel>

                            </td>
                        </tr>
                    </table>
                </asp:Panel>

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
