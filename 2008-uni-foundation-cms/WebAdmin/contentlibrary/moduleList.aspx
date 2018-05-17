<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="moduleList.aspx.vb" Inherits="Foundation.WebAdmin.contentlibrary.moduleList" %>

<%@ Register TagPrefix="cc" Namespace="Core" Assembly="Core" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title id="ctlTitleTag" runat="server" />
    <link href="../_system/styles/main.css" type="text/css" rel="stylesheet">
    <script language="javascript" type="text/javascript" src="../_system/javascript/menuhelper.js"></script>
    <link href="../_gridhelper/grid.css" type="text/css" rel="stylesheet">
    <script language="javascript" type="text/javascript" src="../_gridhelper/gridscripts.js"></script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <asp:Literal ID="ltrLayout1" runat="server" />
        <!---------------------content-------------------->

        <table style="border: 1px #cacaca solid; background-color: #e4e4e4; font-size: 11px;" cellspacing="0" cellpadding="3">
            <tr>
                <td align="right">Label:</td>
                <td>
                    <asp:DropDownList ID="ddlLabel" CssClass="textbox" Style="width: 200px;" runat="server" /></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="btnSearch" CssClass="button" Text="Find Modules" runat="server" /></td>
            </tr>
        </table>

        <p style="font-size: 11px;">Displaying
            <asp:Literal ID="ltrCount" runat="server" />
            modules</p>

        <asp:Panel ID="pnlGrid" runat="server">
            <table class="gridOuterTable" style="width: auto;" cellspacing="0" cellpadding="3">
                <tr>
                    <td>
                        <cc:MyBaseGrid ID="dgrModules" CssClass="gridTableData" DataKeyField="Id" runat="server">
                            <Columns>
                                <asp:BoundColumn DataField="id" HeaderText="Id" />
                                <asp:BoundColumn DataField="admintitle" HeaderText="Description" />
                                <asp:TemplateColumn SortExpression="Rank" HeaderText="Position">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="lnkDown" runat="server" CommandName="Down" src="../_system/images/layout/down1.gif" />
                                        &nbsp;
											<asp:ImageButton ID="lnkUp" runat="server" CommandName="Up" src="../_system/images/layout/up1.gif" />
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" />
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="isarchived" HeaderText="Status">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundColumn>
                                <asp:HyperLinkColumn
                                    HeaderText="Web pages"
                                    DataTextField="pagecount"
                                    DataNavigateUrlField="Id"
                                    DataNavigateUrlFormatString="../webpages/allPages.aspx?ModuleId={0}">
                                    <ItemStyle HorizontalAlign="right" Width="60" />
                                    <HeaderStyle Wrap="false" />
                                </asp:HyperLinkColumn>
                                <asp:BoundColumn DataField="Created" DataFormatString="{0:d}" HeaderText="Created">
                                    <ItemStyle Wrap="False" HorizontalAlign="center" />
                                    <HeaderStyle Wrap="false" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Modified" DataFormatString="{0:d}" HeaderText="Modified">
                                    <ItemStyle Wrap="False" HorizontalAlign="center" />
                                    <HeaderStyle Wrap="false" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="displayedname" SortExpression="u.displayedname" HeaderText="Modified by">
                                    <ItemStyle Wrap="false" />
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:HyperLinkColumn Text="edit"
                                    DataNavigateUrlField="Id" DataNavigateUrlFormatString="moduleEdit.aspx?Id={0}">
                                    <ItemStyle HorizontalAlign="Center" Width="60" />
                                </asp:HyperLinkColumn>
                                <asp:HyperLinkColumn Text="preview"
                                    DataNavigateUrlField="Id" DataNavigateUrlFormatString="moduleView.aspx?Id={0}">
                                    <ItemStyle HorizontalAlign="Center" Width="60" />
                                </asp:HyperLinkColumn>
                                <asp:ButtonColumn Text="delete" CommandName="Delete">
                                    <ItemStyle HorizontalAlign="Center" Width="60" />
                                </asp:ButtonColumn>
                            </Columns>
                        </cc:MyBaseGrid>
                    </td>
                </tr>
            </table>
        </asp:Panel>




        <!-----------------end of content----------------->
        <asp:Literal ID="ltrLayout2" runat="server" />
    </form>
</body>
</html>
