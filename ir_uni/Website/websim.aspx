<%@ Page Language="c#" CodeBehind="websim.aspx.cs" AutoEventWireup="false" Inherits="IrProject.Website.websim" %>

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
                    <h3>Web Page Similarity</h3>
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
                        <asp:Button ID="btnSearch" Text="Find Similar Pages" runat="server" /></td>
                </tr>
            </table>

            <asp:RequiredFieldValidator ID="valSearch" ControlToValidate="tbxUrl" ErrorMessage="Please provide a URL" runat="server" />

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
                <asp:Label ID="lblError" runat="server" Visible="false" Text="No similar web pages found" />

        <p>
            <asp:Panel ID="pnlDocs" Visible="false" runat="server">

                <h4 align="center">Relevant Web Pages</h4>
                <p>
                    There are
                    <asp:Literal ID="ltrDocCount" runat="server" />
                    uni.edu web pages relevant to
                    <asp:Literal ID="ltrUrl" runat="server" />.
				<br>
                    The following is a ranked list of the top 100 results.
				<p>
                    If you submitted a page which is part of this index, it might be not the top result because:
				<ol>
                    <li>
                    the page may have changed; 
					<li>
                    the term weights in the index have been calculated based on their IDF values - 
					which depend on the entire index, whereas the query term weights are calculated based on their counts and html markup type.
					Therefore, the page on the web and the page in the index are not identical based on similarity criteria.
                </ol>

                    <p>
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
                                            <asp:BoundColumn DataField="termcount" SortExpression="termcount" HeaderText="Term count">
                                                <ItemStyle Width="80px" HorizontalAlign="Right" />
                                            </asp:BoundColumn>
                                        </Columns>
                                    </cc1:MyBaseGrid>
                                </td>
                            </tr>
                        </table>
            </asp:Panel>

    </form>
</body>
</html>
