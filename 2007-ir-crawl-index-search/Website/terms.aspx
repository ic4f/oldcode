<%@ Page Language="c#" CodeBehind="terms.aspx.cs" AutoEventWireup="false" Inherits="IrProject.Website.terms" %>

<%@ Register TagPrefix="cc1" Namespace="IrProject.WebControls" Assembly="WebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Giggle Research : Page Terms</title>
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
                    <h3>Page Terms</h3>
                </td>
                <td width="150px"></td>
        </table>
        <p align="right" style="margin: 0px;"><a href="default.aspx">Home</a></p>

        <p>Terms for:
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
                        <cc1:MyGrid ID="dgrTerms" CssClass="gridTableData" DataKeyField="term" runat="server">
                            <Columns>
                                <asp:BoundColumn DataField="term" SortExpression="term" HeaderText="Term" />
                                <asp:BoundColumn DataField="textcount" SortExpression="textcount" HeaderText="text count">
                                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="boldcount" SortExpression="boldcount" HeaderText="bold count">
                                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="headercount" SortExpression="headercount" HeaderText="header count">
                                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="anchorcount" SortExpression="anchorcount" HeaderText="anchor count">
                                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="titlecount" SortExpression="titlecount" HeaderText="title count">
                                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="urlcount" SortExpression="urlcount" HeaderText="url count">
                                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="externalanchorcount" SortExpression="externalanchorcount" HeaderText="External anchor count">
                                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="totalcount" SortExpression="totalcount" HeaderText="total">
                                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="totalcount_a" SortExpression="totalcount_a" HeaderText="total with external">
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
