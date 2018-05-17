<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="college.aspx.vb" Inherits="Foundation.WebMain.college" %>

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

        <p>
            <table width="100%">
                <tr valign="top">
                    <td>
                        <div style="border: 1px #e4e4e4 solid; padding: 5 5 10 5; font-size: 11px; background: #fdfdd4;">
                            <p style="font-weight: bold; margin-top: 0px;">Programs:</p>
                            <asp:Repeater ID="rptPrograms" runat="server">
                                <ItemTemplate>
                                    <p style="margin: 5 0 0 5;">
                                        <asp:HyperLink ID="lnkProgram" runat="server" /></p>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </td>
                    <td></td>
                </tr>
            </table>

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
