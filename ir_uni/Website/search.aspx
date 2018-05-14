<%@ Page Language="c#" CodeBehind="search.aspx.cs" AutoEventWireup="false" Inherits="IrProject.Website.search" %>

<%@ Register TagPrefix="cc1" Namespace="IrProject.WebControls" Assembly="WebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Giggle Research : Search Results Ranking</title>
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
                    <h3>Search Results Ranking</h3>
                </td>
                <td width="150px"></td>
        </table>
        <p align="right" style="margin: 0px;"><a href="default.aspx">Home</a></p>

        <p>
            <table cellpadding="0" cellspacing="2">
                <tr>
                    <td>
                        <asp:TextBox ID="tbxSearch" Style="margin-top: 2px; width: 200px;" MaxLength="100" runat="server" /></td>
                    <td>
                        <asp:Button ID="btnSearch" Text="Search" runat="server" />
                        <asp:Button ID="btnAH" Text="Authorities & Hubs" runat="server" />
                    </td>
                </tr>
            </table>

            <asp:RequiredFieldValidator ID="valSearch" ControlToValidate="tbxSearch" ErrorMessage="Please provide search terms" runat="server" />

            <div style="border: 1px #cacaca solid; padding: 5px; background-color: #f8f8f8;">

                <asp:TextBox ID="tbxPageRank" Text="0.6" Style="margin-top: 2px; width: 50px;" MaxLength="5" runat="server" />
                <font style="font-size: 11px;">PageRank weight w (default = 0.6): increasing w will decrease the weight of PageRank in the similarity calculation.</font>
                <p>
                    Select index:
				<asp:RadioButtonList ID="radIndex" runat="server" Style="font-size: 11px;" />
                <p>
                    <font style="font-size: 11px;">(Weighted index uses the following coefficients: plain text = 1; bold/heading/title/url/external anchor = 3)</font>
            </div>

            <p>
                <asp:Panel ID="pnlSearch" Visible="false" runat="server">

                    <p>
                        <asp:Literal ID="ltrSearchResult" runat="server" />
                        <br>
                        The following is a ranked list of the top 100 results.
				
				<p>
                    <asp:Panel ID="pnlDocs" Visible="false" runat="server">
                        <table class="gridOuterTable" cellspacing="0" cellpadding="3">
                            <tr>
                                <td>
                                    <cc1:MyBaseGrid ID="dgrDocs" CssClass="gridTableData" DataKeyField="docid" runat="server">
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="#">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNumber" runat="server" /></ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:HyperLinkColumn
                                                DataTextField="url"
                                                DataNavigateUrlField="url"
                                                HeaderText="URL"
                                                Target="_blank" />
                                            <asp:BoundColumn DataField="title" HeaderText="Title" />
                                            <asp:BoundColumn DataField="similarity" HeaderText="Similarity" />
                                            <asp:BoundColumn DataField="pagerank" HeaderText="PageRank" />
                                            <asp:HyperLinkColumn
                                                DataTextField="inboundlinks"
                                                DataNavigateUrlField="docid"
                                                DataNavigateUrlFormatString="inboundLinks.aspx?Id={0}"
                                                HeaderText="Inbound">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:HyperLinkColumn>
                                            <asp:HyperLinkColumn
                                                DataTextField="outboundlinks"
                                                DataNavigateUrlField="docid"
                                                DataNavigateUrlFormatString="outboundlinks.aspx?Id={0}"
                                                HeaderText="Outbound">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:HyperLinkColumn>
                                            <asp:HyperLinkColumn
                                                DataTextField="termcount"
                                                DataNavigateUrlField="docid"
                                                DataNavigateUrlFormatString="terms.aspx?Id={0}"
                                                HeaderText="Term count"
                                                SortExpression="termcount">
                                                <ItemStyle Width="80px" HorizontalAlign="Right" />
                                            </asp:HyperLinkColumn>
                                        </Columns>
                                    </cc1:MyBaseGrid>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>

                </asp:Panel>

                <asp:Panel ID="pnlAH" Visible="false" runat="server">
                    <p>
                        <asp:Literal ID="ltrAHResult" runat="server" />
                </asp:Panel>

    </form>
</body>
</html>
