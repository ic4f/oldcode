<%@ Page Language="c#" CodeBehind="caseList.aspx.cs" AutoEventWireup="false" Inherits="Ei.Website.settings.caseList" %>

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


        <asp:Panel ID="pnlGrid" runat="server">
            <table class="gridOuterTable" style="width: auto;" cellspacing="0" cellpadding="3">
                <tr>
                    <td>
                        <cc:MySortGrid id="dgrCases" cssclass="gridTableData" DataKeyField="Id" runat="server">
                            <columns>								
									<asp:BoundColumn DataField="externalref" SortExpression="c.externalref" HeaderText="Number">																
										<headerstyle Wrap="False"/>
									</asp:boundcolumn>
									<asp:BoundColumn DataField="Created" SortExpression="c.Created" HeaderText="Created" DataFormatString="{0: MM/dd/yyyy}">
										<headerstyle Wrap="False"/>
									</asp:boundcolumn>									
									<asp:BoundColumn DataField="Modified" SortExpression="c.Modified" HeaderText="Modified" DataFormatString="{0: MM/dd/yyyy}">
										<headerstyle Wrap="False"/>
									</asp:boundcolumn>									
									<asp:BoundColumn DataField="displayedname" SortExpression="c.ModifiedBy" HeaderText="Modified by">
										<ItemStyle wrap="false"/>
										<headerstyle Wrap="False"/>
									</asp:BoundColumn>
									<asp:HyperlinkColumn
										text="suspects"																		 
										DataNavigateUrlField="Id" 
										DataNavigateUrlFormatString="caseSuspects.aspx?CaseId={0}">
										<ItemStyle HorizontalAlign="Center" width="80"/>
									</asp:HyperlinkColumn>		
									<asp:HyperlinkColumn
										text="users"																		 
										DataNavigateUrlField="Id" 
										DataNavigateUrlFormatString="caseUsers.aspx?CaseId={0}">
										<ItemStyle HorizontalAlign="Center" width="80"/>
									</asp:HyperlinkColumn>			
									<asp:HyperlinkColumn text="delete"
										DataNavigateUrlField="Id" DataNavigateUrlFormatString="caseDetails.aspx?Id={0}">
										<ItemStyle HorizontalAlign="Center" width="80"/>
									</asp:HyperlinkColumn>																																														
									<asp:HyperlinkColumn text="edit"
										DataNavigateUrlField="Id" DataNavigateUrlFormatString="caseEdit.aspx?Id={0}">
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
