<%@ Page Language="c#" CodeBehind="web.aspx.cs" AutoEventWireup="false" Inherits="IrProject.Website.web" %>

<%@ Register TagPrefix="cc1" Namespace="IrProject.WebControls" Assembly="WebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Giggle Research : Web Page Similarity</title>
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
                    <h3>Web Page Content Extraction</h3>
                </td>
                <td width="150px"></td>
        </table>
        <p align="right" style="margin: 0px;"><a href="default.aspx">Home</a></p>

        <p>
            <table cellpadding="0" cellspacing="2">
                <tr>
                    <td>
                        <asp:TextBox ID="tbxUrl" Style="margin-top: 2px; width: 350px;" runat="server" /></td>
                    <td>
                        <asp:Button ID="btnExtract" Text="Extract Content" runat="server" />
                        <asp:Button ID="btnIndex" Text="Display Terms" runat="server" />
                    </td>
                </tr>
            </table>

            <asp:RequiredFieldValidator ID="valSearch" ControlToValidate="tbxUrl" ErrorMessage="Please provide a URL" runat="server" />

        <p>
            <asp:Panel ID="pnlExtract" Visible="false" runat="server">
                <h4 align="center">Extracted Content</h4>
                <p>
                    The following is the content extracted from
                    <asp:Literal ID="ltrExtractedUrl" runat="server" />. 
				<br>
                    All tags except &#60;b&#62;, &#60;h1-6&#62;, &#60;a&#62; have been removed.
				<div style="border: 1px solid #cacaca; padding: 20px;">
                    <asp:Label ID="lblExtract" runat="server" />
                </div>
            </asp:Panel>

            <p>
                <asp:Panel ID="pnlIndex" Visible="false" runat="server">
                    <h4 align="center">Page Terms</h4>
                    <p>
                        The following is a list of
                        <asp:Literal ID="ltrTermCount" runat="server" />
                        terms from
                        <asp:Literal ID="ltrIndexUrl" runat="server" />
                    . 				

				<p>
                    <table class="gridOuterTable" cellspacing="0" cellpadding="3">
                        <tr>
                            <td>
                                <cc1:MySortGrid ID="dgrTerms" CssClass="gridTableData" DataKeyField="term" runat="server">
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
                                        <asp:BoundColumn DataField="totalcount" SortExpression="totalcount" HeaderText="total">
                                            <ItemStyle Width="80px" HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                    </Columns>
                                </cc1:MySortGrid>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>

    </form>
</body>
</html>
