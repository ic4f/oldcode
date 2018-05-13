<%@ Page Language="c#" CodeBehind="userList.aspx.cs" AutoEventWireup="false" Inherits="Ei.Website.settings.userList" %>

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
                        <cc:MySortGrid id="dgrUsers" cssclass="gridTableData" DataKeyField="Id" runat="server">
                            <columns>								
								<asp:BoundColumn DataField="DisplayedName" SortExpression="u.DisplayedName" HeaderText="Name">																																																						
									<headerstyle Wrap="False"/>
								</asp:boundcolumn>							
								<asp:BoundColumn DataField="Created" SortExpression="u.Created" HeaderText="Created" DataFormatString="{0: MM/dd/yyyy}">
									<headerstyle Wrap="False"/>
								</asp:boundcolumn>								
								<asp:BoundColumn DataField="Modified" SortExpression="u.Modified" HeaderText="Modified" DataFormatString="{0: MM/dd/yyyy}">
									<headerstyle Wrap="False"/>
								</asp:boundcolumn>								
								<asp:BoundColumn DataField="ModifiedByName" SortExpression="u.ModifiedBy" HeaderText="Modified by">
									<ItemStyle wrap="false"/>
									<headerstyle Wrap="False"/>
								</asp:BoundColumn>	
								<asp:HyperlinkColumn
									text="set roles"																		 
									DataNavigateUrlField="Id" 
									DataNavigateUrlFormatString="userRoles.aspx?CmsUserId={0}">
									<ItemStyle HorizontalAlign="Center" width="60"/>
								</asp:HyperlinkColumn>																																						
								<asp:HyperlinkColumn
									text="assign to cases"																		 
									DataNavigateUrlField="UserId" 
									DataNavigateUrlFormatString="userCases.aspx?UserId={0}">
									<ItemStyle HorizontalAlign="Center" width="100"/>
								</asp:HyperlinkColumn>									
								<asp:HyperlinkColumn text="edit"
									DataNavigateUrlField="Id" DataNavigateUrlFormatString="userEdit.aspx?Id={0}">
									<ItemStyle HorizontalAlign="Center" width="60"/>
								</asp:HyperlinkColumn>		
								<asp:ButtonColumn text="remove" CommandName="Delete">
									<ItemStyle HorizontalAlign="Center" width="60"/>
								</asp:ButtonColumn>																											
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
