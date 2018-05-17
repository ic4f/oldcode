<%@ Page Language="c#" CodeBehind="lineupList.aspx.cs" AutoEventWireup="false" Inherits="Ei.Website.lineupadmin.lineupList" %>

<%@ Register TagPrefix="cc" Namespace="Core" Assembly="Core" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title id="ctlTitleTag" runat="server" />
    <link href="../_system/styles/main.css" type="text/css" rel="stylesheet">
    <script language="javascript" type="text/javascript" src="../_system/javascript/menuhelper.js"></script>
    <link href="../_system/styles/addeditpage.css" type="text/css" rel="stylesheet">
    <script language="javascript" type="text/javascript" src="../_gridhelper/gridscripts.js"></script>
    <link href="../_gridhelper/grid.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <asp:Literal ID="ltrLayout1" runat="server" />
        <!---------------------content-------------------->

        <p>Select Case:
            <asp:DropDownList CssClass="textbox" ID="ddlCase" runat="server" /></p>

        <p>
            <asp:Panel ID="pnlGrid" runat="server">
                <table class="gridOuterTable" style="width: auto;" cellspacing="0" cellpadding="3">
                    <tr>
                        <td>
                            <cc:MySortGrid id="dgrLineups" cssclass="gridTableData" DataKeyField="Id" runat="server">
                                <columns>								
									<asp:BoundColumn DataField="id" SortExpression="l.id" HeaderText="Id">																
										<headerstyle Wrap="False"/>
									</asp:boundcolumn>								
									<asp:BoundColumn DataField="description" SortExpression="l.description" HeaderText="Description">																
										<headerstyle Wrap="False"/>
									</asp:boundcolumn>
									<asp:BoundColumn DataField="Created" SortExpression="l.Created" HeaderText="Created" DataFormatString="{0: MM/dd/yyyy}">
										<headerstyle Wrap="False"/>
									</asp:boundcolumn>																	
									<asp:BoundColumn DataField="displayedname" SortExpression="l.ModifiedBy" HeaderText="Created by">
										<ItemStyle wrap="false"/>
										<headerstyle Wrap="False"/>
									</asp:BoundColumn>																																													
									<asp:HyperlinkColumn text="details"
										DataNavigateUrlField="Id" DataNavigateUrlFormatString="lineupDetails.aspx?Id={0}">
										<ItemStyle HorizontalAlign="Center" width="80"/>
									</asp:HyperlinkColumn>	
									<asp:HyperlinkColumn text="results"
										DataNavigateUrlField="Id" DataNavigateUrlFormatString="resultList.aspx?LineupId={0}">
										<ItemStyle HorizontalAlign="Center" width="80"/>
									</asp:HyperlinkColumn>																															
								</columns>
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
