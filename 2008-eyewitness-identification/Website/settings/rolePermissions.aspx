<%@ Page Language="c#" CodeBehind="rolePermissions.aspx.cs" AutoEventWireup="false" Inherits="Ei.Website.settings.rolePermissions" %>

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


        <table cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr valign="top">
                <td width="200px">
                    <p><b>Select a category:</b></p>
                    <asp:Repeater ID="rptTopCats" runat="server">
                        <ItemTemplate>
                            <p>
                                <dd>
                                    <asp:HyperLink ID="lnkTopCat" runat="server" />
                        </ItemTemplate>
                    </asp:Repeater>
                    <p>
                        <img src="../_system/images/layout/blank.gif" width="200px" height="1px">
                </td>
                <td width="20px"></td>
                <td bgcolor="#cacaca" width="1px"></td>
                <td width="20px"></td>
                <td>
                    <asp:Panel ID="pnlPermissions" runat="server">
                        <p>
                            <asp:Literal ID="ltrPermissionTitle" runat="server" /></p>

                        <asp:Panel ID="pnlConfirm" CssClass="panel-confirm" runat="server" Visible="false" Style="margin-bottom: 0px;">
                            Permissions have been saved.
                        </asp:Panel>

                        <blockquote>
                            <asp:Repeater ID="rptCats" runat="server">
                                <ItemTemplate>
                                    <p style="margin-bottom: 3px;">
                                        <span style="font-size: 11px; font-weight: bold; color: #414141;">
                                            <asp:Label ID="lblCat" runat="server" /></span>
                                    </p>
                                    <asp:Panel ID="pnlPerms" runat="server" Style="font-size: 11px;" />
                                </ItemTemplate>
                            </asp:Repeater>
                        </blockquote>

                        <div class="buttons">
                            <asp:Button ID="btnSave" Text="Save" CssClass="button" runat="server" />&nbsp;
								<asp:Button ID="btnCancel" Text="Reset" CssClass="button" runat="server" />
                        </div>

                    </asp:Panel>
                    &nbsp;										
                </td>
            </tr>
        </table>



        <!-----------------end of content----------------->
        <asp:Literal ID="ltrLayout2" runat="server" />
    </form>
</body>
</html>

