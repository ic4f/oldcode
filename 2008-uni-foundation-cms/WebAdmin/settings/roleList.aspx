<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="roleList.aspx.vb" Inherits="Foundation.WebAdmin.settings.roleList" %>

<%@ Register TagPrefix="cc" Namespace="Core" Assembly="Core" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title id="ctlTitleTag" runat="server" />
    <link href="../_system/styles/main.css" type="text/css" rel="stylesheet">
    <script language="javascript" type="text/javascript" src="../_system/javascript/menuhelper.js"></script>
    <script language="javascript" type="text/javascript" src="../_gridhelper/gridscripts.js"></script>
    <link href="../_gridhelper/grid.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <asp:Literal ID="ltrLayout1" runat="server" />
        <!---------------------content-------------------->


        <asp:Panel ID="pnlGrid" runat="server">
            <table class="gridOuterTable" style="width: auto;" cellspacing="0" cellpadding="3">
                <tr>
                    <td>
                        <cc:MySortGrid ID="dgrRoles" CssClass="gridTableData" DataKeyField="Id" runat="server">
                            <Columns>
                                <asp:BoundColumn DataField="Name" SortExpression="r.Name" HeaderText="Role">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Created" SortExpression="r.Created" HeaderText="Created" DataFormatString="{0: MM/dd/yyyy}">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Modified" SortExpression="r.Modified" HeaderText="Modified" DataFormatString="{0: MM/dd/yyyy}">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ModifiedByName" SortExpression="r.ModifiedBy" HeaderText="Modified by">
                                    <ItemStyle Wrap="false" />
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:HyperLinkColumn
                                    Text="permissions"
                                    DataNavigateUrlField="Id"
                                    DataNavigateUrlFormatString="rolePermissions.aspx?RoleId={0}">
                                    <ItemStyle HorizontalAlign="Center" Width="80" />
                                </asp:HyperLinkColumn>
                                <asp:HyperLinkColumn
                                    Text="users"
                                    DataNavigateUrlField="Id"
                                    DataNavigateUrlFormatString="roleUsers.aspx?RoleId={0}">
                                    <ItemStyle HorizontalAlign="Center" Width="80" />
                                </asp:HyperLinkColumn>
                                <asp:HyperLinkColumn Text="edit"
                                    DataNavigateUrlField="Id" DataNavigateUrlFormatString="roleEdit.aspx?Id={0}">
                                    <ItemStyle HorizontalAlign="Center" Width="80" />
                                </asp:HyperLinkColumn>
                                <asp:ButtonColumn Text="delete" CommandName="Delete">
                                    <ItemStyle HorizontalAlign="Center" Width="80" />
                                </asp:ButtonColumn>
                            </Columns>
                        </cc:MySortGrid>
                    </td>
                </tr>
            </table>
        </asp:Panel>



        <!-----------------end of content----------------->
        <asp:Literal ID="ltrLayout2" runat="server" />
    </form>
</body>
</html>
