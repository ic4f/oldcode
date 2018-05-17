<%@ Page Language="c#" CodeBehind="photoCategorize.aspx.cs" AutoEventWireup="false" Inherits="Ei.Website.settings.photoCategorize" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title id="ctlTitleTag" runat="server" />
    <link href="../_system/styles/main.css" type="text/css" rel="stylesheet">
    <script language="javascript" type="text/javascript" src="../_system/javascript/menuhelper.js"></script>
    <link href="../_system/styles/addeditpage.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <asp:Literal ID="ltrLayout1" runat="server" />
        <!---------------------content-------------------->

        <asp:Panel ID="pnlConfirm" CssClass="panel-confirm" runat="server">
            <asp:Literal ID="ltrConfirmMessage" runat="server" />
            <div class="buttons">
                <asp:Button ID="btnAddAnother" Text="Categorize Next 10" CssClass="button" runat="server" Style="width: auto;" />
                <asp:Button ID="btnReturn" Text="Return" CssClass="button" runat="server" />
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlError" CssClass="panel-error" runat="server">
            <asp:Literal ID="ltrErrorMessage" runat="server" />

        </asp:Panel>

        <asp:Panel ID="pnlForm" runat="server" CssClass="panel-form">

            <table cellpadding="5" cellspacing="0" style="border: 1px solid #cacaca; border-collapse: collapse; background-color: #e4e4e4; width: 100%;" border="1">
                <tr>
                    <td style="font-weight: bold; color: #414141;">Photo<img src="../_system/images/layout/blank.gif" height="1px" width="80px"></td>
                    <td style="width: 18%; font-weight: bold; color: #414141;">Gender</td>
                    <td style="width: 18%; font-weight: bold; color: #414141;">Race</td>
                    <td style="width: 18%; font-weight: bold; color: #414141;">Hair Color</td>
                    <td style="width: 18%; font-weight: bold; color: #414141;">Age Range</td>
                    <td style="width: 18%; font-weight: bold; color: #414141;">Weight Range</td>
                </tr>
            </table>

            <asp:Repeater ID="rptPhotos" runat="server">
                <ItemTemplate>
                    <p>
                        <table cellpadding="5" cellspacing="0" class="addphotos_table" border="1">
                            <tr valign="top">
                                <td>
                                    <asp:Literal ID="ltrPhoto" runat="server" /></td>
                                <td style="width: 18%;">
                                    <asp:Literal ID="ltrGender" runat="server" /></td>
                                <td style="width: 18%;">
                                    <asp:Literal ID="ltrRace" runat="server" /></td>
                                <td style="width: 18%;">
                                    <asp:Literal ID="ltrHair" runat="server" /></td>
                                <td style="width: 18%;">
                                    <asp:Literal ID="ltrAge" runat="server" /></td>
                                <td style="width: 18%;">
                                    <asp:Literal ID="ltrWeight" runat="server" /></td>
                            </tr>
                        </table>
                </ItemTemplate>
            </asp:Repeater>

            <div class="buttons">
                <asp:Button ID="btnSave" Text="Save" CssClass="button" runat="server" />&nbsp;
					<asp:Button ID="btnCancel" Text="Cancel" CssClass="button" runat="server" />
            </div>

        </asp:Panel>

        <asp:Literal ID="ltrJavaScript" runat="server" />



        <!-----------------end of content----------------->
        <asp:Literal ID="ltrLayout2" runat="server" />
    </form>
</body>
</html>
