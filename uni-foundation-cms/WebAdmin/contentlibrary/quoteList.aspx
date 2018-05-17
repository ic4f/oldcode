<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="quoteList.aspx.vb" Inherits="Foundation.WebAdmin.contentlibrary.quoteList" %>

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



        <asp:Panel ID="pnlGrid" runat="server">
            <table class="gridOuterTable" style="width: auto;" cellspacing="0" cellpadding="3">
                <tr>
                    <td>
                        <cc:MySortGrid ID="dgrQuotes" CssClass="gridTableData" DataKeyField="Id" runat="server">
                            <Columns>
                                <asp:BoundColumn DataField="id" SortExpression="q.id" HeaderText="Id" />
                                <asp:BoundColumn DataField="text" SortExpression="q.text" HeaderText="Quote">
                                    <HeaderStyle Wrap="false" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="author" SortExpression="q.author" HeaderText="Author">
                                    <HeaderStyle Wrap="false" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Created" DataFormatString="{0:d}" SortExpression="q.Created" HeaderText="Created">
                                    <ItemStyle Wrap="False" HorizontalAlign="center" />
                                    <HeaderStyle Wrap="false" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Modified" DataFormatString="{0:d}" SortExpression="q.Modified" HeaderText="Modified">
                                    <ItemStyle Wrap="False" HorizontalAlign="center" />
                                    <HeaderStyle Wrap="false" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="displayedname" SortExpression="u.displayedname" HeaderText="Modified by">
                                    <ItemStyle Wrap="false" />
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:HyperLinkColumn Text="edit"
                                    DataNavigateUrlField="Id" DataNavigateUrlFormatString="quoteEdit.aspx?Id={0}">
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
