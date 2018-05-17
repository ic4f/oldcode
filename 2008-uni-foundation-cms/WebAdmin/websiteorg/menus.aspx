<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="menus.aspx.vb" Inherits="Foundation.WebAdmin.websiteorg.menus" %>

<%@ Register TagPrefix="cc" Namespace="Core" Assembly="Core" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title id="ctlTitleTag" runat="server" />
    <link href="../_system/styles/main.css" type="text/css" rel="stylesheet">
    <script language="javascript" type="text/javascript" src="../_system/javascript/menuhelper.js"></script>
    <script language="javascript" type="text/javascript" src="../_gridhelper/gridscripts.js"></script>
    <link href="../_gridhelper/grid.css" type="text/css" rel="stylesheet">
    <link href="../_system/styles/addeditpage.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <asp:Literal ID="ltrLayout1" runat="server" />
        <!---------------------content-------------------->

        <table style="border: 1px #cacaca solid; background-color: #f8f8f8; width: 100%;" cellspacing="0" cellpadding="10">
            <tr>
                <td>
                    <table style="width: 100%; background: #ffffff; border: 1px #cacaca solid; border-collapse: collapse;" border="1">
                        <tr valign="top">
                            <td>
                                <div class="edithelpertitle">Staging Menu Editor</div>
                                <div style="padding: 0 10 10 10;">

                                    <p>
                                        <asp:Label ID="lblMenuTitle" runat="server" /></p>

                                    <p>
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="pnlAdd" runat="server">
                                                        <table style="border: 1px #cacaca solid;" cellspacing="0" cellpadding="3">
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="tbxAdd" CssClass="textbox" Style="margin-top: 1px;" MaxLength="50" runat="server" /></td>
                                                                <td>
                                                                    <asp:Button ID="btnAdd" CssClass="button" Text="Add Item" runat="server" /></td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                                <td style="font-size: 11px;">&nbsp;&nbsp;&nbsp;<asp:HyperLink ID="lnkLevelUp2" runat="server" Text="Show parent menu" /></td>
                                            </tr>
                                        </table>

                                        <p>

                                            <asp:Panel ID="pnlMenuGrid" runat="server">
                                                <table class="gridOuterTable" style="width: auto;" cellspacing="0" cellpadding="3">
                                                    <tr>
                                                        <td>
                                                            <cc:MyBaseGrid ID="dgrMenu" CssClass="gridTableData" DataKeyField="Id" runat="server">
                                                                <Columns>
                                                                    <asp:HyperLinkColumn
                                                                        HeaderText="Item Text"
                                                                        DataTextField="Text"
                                                                        DataNavigateUrlField="id"
                                                                        DataNavigateUrlFormatString="menus.aspx?Id={0}">
                                                                        <HeaderStyle Wrap="False" />
                                                                    </asp:HyperLinkColumn>
                                                                    <asp:TemplateColumn HeaderText="Position">
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="lnkDown" runat="server" CommandName="Down" src="../_system/images/layout/down1.gif" />
                                                                            &nbsp;
																	<asp:ImageButton ID="lnkUp" runat="server" CommandName="Up" src="../_system/images/layout/up1.gif" />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Wrap="False" />
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn HeaderText="Sub-menus">
                                                                        <ItemStyle HorizontalAlign="right" />
                                                                        <ItemTemplate>
                                                                            <asp:HyperLink ID="lnkSubmenus" runat="server" />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Wrap="False" />
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn HeaderText="Web Pages">
                                                                        <ItemStyle HorizontalAlign="right" />
                                                                        <ItemTemplate>
                                                                            <asp:HyperLink ID="lnkPages" runat="server" />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Wrap="False" />
                                                                    </asp:TemplateColumn>
                                                                    <asp:BoundColumn HeaderText="Default Page" DataField="HasDefaultPage">
                                                                        <ItemStyle HorizontalAlign="center" />
                                                                        <HeaderStyle Wrap="False" />
                                                                    </asp:BoundColumn>
                                                                    <asp:HyperLinkColumn Text="edit"
                                                                        DataNavigateUrlField="Id" DataNavigateUrlFormatString="menuEdit.aspx?Id={0}">
                                                                        <ItemStyle HorizontalAlign="Center" Width="70" />
                                                                    </asp:HyperLinkColumn>
                                                                    <asp:ButtonColumn Text="delete" CommandName="Delete">
                                                                        <ItemStyle HorizontalAlign="Center" Width="70" />
                                                                    </asp:ButtonColumn>
                                                                </Columns>
                                                            </cc:MyBaseGrid>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                            <p style="font-size: 11px;">
                                                <asp:Label ID="lblNoMenu" runat="server" Text="The selected item has no sub-menu" /></p>
                                    <p style="font-size: 11px;">
                                        *A menu can be deleted if all of the following conditions are met:
										<ul style="font-size: 11px;">
                                            <li>
                                            It has no sub-menus
											<li>
                                            No web pages are assigned to it
											<li>
                                            It is not set as the menu for a page group
                                        </ul>
                                    </p>
                                    <p style="font-size: 11px;">In addition, if a menu is hard-coded into the system, it and cannot be edited or deleted.</p>

                                </div>
                            </td>
                            <td>
                                <div class="edithelpertitle">Staging Menu Browser</div>
                                <div style="padding: 0 10 10 10; font-size: 11px;">
                                    <p>
                                        <asp:HyperLink ID="lnkLevelUp1" runat="server" Text="Show parent menu" /></p>
                                    <p>
                                        <asp:Literal ID="ltrTreeDisplay" runat="server" /></p>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

        <p>

            <table style="border: 1px #cacaca solid; background-color: #f8f8f8; width: 100%;" cellspacing="0" cellpadding="10">
                <tr>
                    <td>
                        <div style="width: 100%; background: #ffffff; border: 1px #cacaca solid;">
                            <div class="edithelpertitle">Menu Publisher</div>
                            <div style="padding: 0 10 1 10;">
                                <p style="font-size: 11px;">Last published:
                                    <asp:Literal ID="ltrPublished" runat="server" /></p>
                                <p style="font-size: 11px;">Last modified:
                                    <asp:Literal ID="ltrModified" runat="server" /></p>
                                <p style="font-size: 11px; margin-top: 20px;">
                                    <asp:Button ID="btnPublish" Text="Publish Menu" CssClass="button" runat="server" Style="width: 100px; padding: 5px;" />
                                    <asp:Literal ID="ltrPublishText" runat="server" />
                                </p>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>

            <p>

                <table style="border: 1px #cacaca solid; background-color: #f8f8f8; width: 100%;" cellspacing="0" cellpadding="10">
                    <tr>
                        <td>
                            <div style="width: 100%; background: #ffffff; border: 1px #cacaca solid;">
                                <div class="edithelpertitle">Staging Menu Change Log</div>
                                <div style="padding: 0 10 1 10;">
                                    <p>
                                        <asp:Panel ID="pnlLogGrid" runat="server">
                                            <table class="gridOuterTable" style="width: 100%;" cellspacing="0" cellpadding="3">
                                                <tr>
                                                    <td>
                                                        <cc:MyBaseGrid ID="dgrLog" CssClass="gridTableData" DataKeyField="Id" runat="server">
                                                            <Columns>
                                                                <asp:BoundColumn
                                                                    HeaderText="Description"
                                                                    DataField="description"></asp:BoundColumn>
                                                                <asp:BoundColumn
                                                                    HeaderText="Date/time"
                                                                    DataField="created">
                                                                    <ItemStyle HorizontalAlign="center" />
                                                                </asp:BoundColumn>
                                                                <asp:BoundColumn
                                                                    HeaderText="User"
                                                                    DataField="displayedname"></asp:BoundColumn>
                                                            </Columns>
                                                        </cc:MyBaseGrid>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <p style="font-size: 11px;">
                                            <asp:Label ID="lblNoLog" runat="server" Text="There have been no edits to the staging menu so far" /></p>

                                </div>
                            </div>
                        </td>
                    </tr>
                </table>

                <!-----------------end of content----------------->
                <asp:Literal ID="ltrLayout2" runat="server" />
    </form>
</body>
</html>
