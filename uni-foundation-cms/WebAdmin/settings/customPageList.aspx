<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="customPageList.aspx.vb" Inherits="Foundation.WebAdmin.settings.customPageList" %>

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


        <p style="font-size: 11px;">Displaying
            <asp:Literal ID="ltrCount" runat="server" />
            pages</p>

        <asp:Panel ID="pnlGrid" runat="server">
            <table class="gridOuterTable" style="width: auto;" cellspacing="0" cellpadding="3">
                <tr>
                    <td>
                        <cc:MySortGrid ID="dgrPages" CssClass="gridTableData" DataKeyField="Id" runat="server">
                            <Columns>
                                <asp:BoundColumn DataField="id" SortExpression="p.id" HeaderText="Id" />
                                <asp:BoundColumn DataField="pagetitle" SortExpression="p.pagetitle" HeaderText="Page Title">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="menutext" SortExpression="m.text" HeaderText="Menu">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="url" SortExpression="p.url" HeaderText="URL">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ispublished" SortExpression="p.ispublished" HeaderText="Published">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Created" SortExpression="p.Created" HeaderText="Created" DataFormatString="{0: MM/dd/yyyy}">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Modified" SortExpression="p.Modified" HeaderText="Modified" DataFormatString="{0: MM/dd/yyyy}">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="displayedname" SortExpression="p.ModifiedBy" HeaderText="Modified by">
                                    <ItemStyle Wrap="false" />
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:HyperLinkColumn Text="info"
                                    DataNavigateUrlField="Id" DataNavigateUrlFormatString="../webpages/pageInfo.aspx?Id={0}">
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                </asp:HyperLinkColumn>
                                <asp:HyperLinkColumn Text="edit/publish"
                                    DataNavigateUrlField="Id" DataNavigateUrlFormatString="custompageEdit.aspx?Id={0}">
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                </asp:HyperLinkColumn>
                                <asp:BoundColumn DataField="id" />
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
