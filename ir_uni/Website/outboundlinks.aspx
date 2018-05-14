<%@ Page Language="c#" CodeBehind="outboundlinks.aspx.cs" AutoEventWireup="false" Inherits="IrProject.Website.outboundlinks" %>

<%@ Register TagPrefix="cc1" Namespace="IrProject.WebControls" Assembly="WebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Giggle Research : Outbound Links</title>
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
                    <h3>Outbound Links</h3>
                </td>
                <td width="150px"></td>
        </table>
        <p align="right" style="margin: 0px;"><a href="default.aspx">Home</a></p>

        <p>Outbound links from:
            <asp:Label ID="lblUrl" runat="server" /></p>

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
                                <asp:BoundColumn DataField="text" SortExpression="text" HeaderText="Anchor text" />
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
                                <asp:BoundColumn DataField="termcount" SortExpression="termcount" HeaderText="Term count">
                                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                                </asp:BoundColumn>
                            </Columns>
                        </cc1:MyGrid>
                    </td>
                </tr>
            </table>






    </form>
</body>
</html>
