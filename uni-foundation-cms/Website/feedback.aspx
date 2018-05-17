<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="feedback.aspx.vb" Inherits="Foundation.WebMain.feedback" %>

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


        <asp:Panel ID="pnlFeedback" runat="server">
            <p style="margin: 0;">Name:</p>
            <p style="margin: 0; margin-bottom: 10px;">
                <asp:TextBox ID="tbxName" CssClass="textbox" runat="server" Style="width: 250px;" />
                <asp:RequiredFieldValidator ID="val1" ErrorMessage="<br>Please provide your name" ControlToValidate="tbxName" runat="server" />
            </p>

            <p style="margin: 0;">E-mail Address:</p>
            <p style="margin: 0; margin-bottom: 10px;">
                <asp:TextBox ID="tbxEmail" CssClass="textbox" runat="server" Style="width: 250px;" />
                <asp:RequiredFieldValidator ID="val2" ErrorMessage="<br>Please provide your e-mail address" ControlToValidate="tbxEmail" runat="server" />
            </p>

            <p style="margin: 0;">Comments:</p>
            <p style="margin: 0; margin-bottom: 10px;">
                <asp:TextBox ID="tbxComments" TextMode="multiline" Style="width: 100%; height: 150px;" CssClass="textbox" runat="server" />
                <asp:RequiredFieldValidator ID="val3" ErrorMessage="<br>Please type in your comments" ControlToValidate="tbxComments" runat="server" />
            </p>

            <asp:Button ID="btnSave" Text="Send" CssClass="button" runat="server" />
        </asp:Panel>

        <p>
            <asp:Literal ID="ltrConfirm" runat="server" Visible="false" /></p>



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
