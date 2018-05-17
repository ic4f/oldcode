<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="tagList.aspx.vb" Inherits="Foundation.WebAdmin.contentorg.tagList" %>

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


        <asp:Panel ID="pnlGrig" runat="server">
            <table class="gridOuterTable" style="width: auto;" cellspacing="0" cellpadding="3">
                <tr>
                    <td>
                        <cc:MySortGrid ID="dgrTags" CssClass="gridTableData" DataKeyField="Id" runat="server">
                            <Columns>
                                <asp:BoundColumn DataField="Text" SortExpression="t.Text" HeaderText="Text" />
                                <asp:HyperLinkColumn
                                    HeaderText="Web pages"
                                    SortExpression="pagecount"
                                    DataTextField="pagecount"
                                    DataNavigateUrlField="Id"
                                    DataNavigateUrlFormatString="../webpages/allPages.aspx?TagId={0}">
                                    <ItemStyle HorizontalAlign="right" Width="60" />
                                    <HeaderStyle Wrap="false" />
                                </asp:HyperLinkColumn>
                                <asp:BoundColumn DataField="Created" SortExpression="t.created" DataFormatString="{0:d}" HeaderText="Created">
                                    <ItemStyle Wrap="False" HorizontalAlign="center" />
                                    <HeaderStyle Wrap="false" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Modified" SortExpression="t.Modified" DataFormatString="{0:d}" HeaderText="Modified">
                                    <ItemStyle Wrap="False" HorizontalAlign="center" />
                                    <HeaderStyle Wrap="false" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="displayedname" SortExpression="u.displayedname" HeaderText="Modified by">
                                    <ItemStyle Wrap="false" />
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:HyperLinkColumn Text="edit"
                                    DataNavigateUrlField="Id" DataNavigateUrlFormatString="tagEdit.aspx?Id={0}">
                                    <ItemStyle HorizontalAlign="Center" Width="60" />
                                </asp:HyperLinkColumn>
                                <asp:ButtonColumn Text="delete" CommandName="Delete">
                                    <ItemStyle HorizontalAlign="Center" Width="60" />
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
