<%@ Page Language="c#" CodeBehind="pages.aspx.cs" AutoEventWireup="false" Inherits="IrProject.Website.pages" %>

<%@ Register TagPrefix="cc1" Namespace="IrProject.WebControls" Assembly="WebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Giggle Research : Web Pages</title>
    <link href="_system/styles/main.css" type="text/css" rel="stylesheet">
    <link href="_gridhelper/grid.css" type="text/css" rel="stylesheet">
    <script language="javascript" type="text/javascript" src="_gridhelper/gridscripts.js"></script>
</head>
<body>
    <form id="Form1" method="post" runat="server">

        <table width="100%" cellpadding="0" cellsapcing="0" style="border-bottom: 1px #cacaca solid; margin-bottom: 0px;">
            <tr>
                <td width="150px"><a href="default.aspx">
                    <img src="_system/images/giggles.gif" border="0"></a></td>
                <td align="center">
                    <h3>Web Pages</h3>
                </td>
                <td width="150px"></td>
        </table>
        <p align="right" style="margin: 0px;"><a href="default.aspx">Home</a></p>



        <table class="filterResults" cellspacing="0" cellpadding="3">
            <tr>
                <td align="right">URL contains:</td>
                <td>
                    <asp:TextBox ID="tbxUrl" runat="server" MaxLength="50" Style="width: 200px;" /></td>
            </tr>
            <tr>
                <td align="right">Title contains:</td>
                <td>
                    <asp:TextBox ID="tbxTitle" runat="server" MaxLength="50" Style="width: 200px;" /></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="btnSearch" Text="Search" runat="server" />
                    <asp:Button ID="btnReset" Text="Reset" runat="server" /></td>
            </tr>

        </table>


        <p>
            <table class="gridOuterTable" cellspacing="0" cellpadding="3">
                <tr>
                    <td>
                        <cc1:MyPager ID="pager" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <cc1:MyGrid ID="dgrPages" CssClass="gridTableData" DataKeyField="Id" runat="server">
                            <Columns>
                                <asp:HyperLinkColumn
                                    DataTextField="url"
                                    DataNavigateUrlField="url"
                                    HeaderText="URL"
                                    Target="_blank"
                                    SortExpression="url" />
                                <asp:BoundColumn DataField="title" SortExpression="title" HeaderText="Title" />
                                <asp:HyperLinkColumn
                                    DataTextField="inboundlinks"
                                    DataNavigateUrlField="id"
                                    DataNavigateUrlFormatString="inboundLinks.aspx?Id={0}"
                                    HeaderText="Inbound"
                                    SortExpression="inboundlinks">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:HyperLinkColumn>
                                <asp:HyperLinkColumn
                                    DataTextField="outboundlinks"
                                    DataNavigateUrlField="id"
                                    DataNavigateUrlFormatString="outboundlinks.aspx?Id={0}"
                                    HeaderText="Outbound"
                                    SortExpression="outboundlinks">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:HyperLinkColumn>
                                <asp:BoundColumn DataField="pagerank" HeaderText="PageRank" SortExpression="pagerank" />
                                <asp:HyperLinkColumn
                                    DataTextField="termcount"
                                    DataNavigateUrlField="id"
                                    DataNavigateUrlFormatString="terms.aspx?Id={0}"
                                    HeaderText="Term count"
                                    SortExpression="termcount">
                                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                                </asp:HyperLinkColumn>
                            </Columns>
                        </cc1:MyGrid>
                    </td>
                </tr>
            </table>






    </form>
</body>
</html>
