<%@ Page language="c#" Codebehind="roleList.aspx.cs" AutoEventWireup="false" Inherits="Ei.Website.settings.roleList" %>
<%@ Register TagPrefix="cc" Namespace="Core" Assembly="Core" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 
<html>
  <head>
		<title id="ctlTitleTag" runat="server"/>   
		<link href="../_system/styles/main.css" type="text/css" rel="stylesheet">  		
		<script language="javascript" type="text/javascript" src="../_system/javascript/menuhelper.js"></script>	
		<script language="javascript" type="text/javascript" src="../_gridhelper/gridscripts.js"></script>	
		<link href="../_gridhelper/grid.css" type="text/css" rel="stylesheet"> 		
  </head>
  <body>	
    <form id="Form1" method="post" runat="server">       			
			<asp:literal id="ltrLayout1" runat="server"/>				
			<!---------------------content-------------------->	

			
				<asp:panel id="pnlGrid" runat="server">
				<table class="gridOuterTable" style="width:auto;" cellspacing="0" cellpadding="3">
						<tr><td>						
							<cc:MySortGrid id="dgrRoles" cssclass="gridTableData" DataKeyField="Id" runat="server">
								<columns>								
									<asp:BoundColumn DataField="Name" SortExpression="r.Name" HeaderText="Role">																
										<headerstyle Wrap="False"/>
									</asp:boundcolumn>
									<asp:BoundColumn DataField="Created" SortExpression="r.Created" HeaderText="Created" DataFormatString="{0: MM/dd/yyyy}">
										<headerstyle Wrap="False"/>
									</asp:boundcolumn>									
									<asp:BoundColumn DataField="Modified" SortExpression="r.Modified" HeaderText="Modified" DataFormatString="{0: MM/dd/yyyy}">
										<headerstyle Wrap="False"/>
									</asp:boundcolumn>									
									<asp:BoundColumn DataField="ModifiedByName" SortExpression="r.ModifiedBy" HeaderText="Modified by">
										<ItemStyle wrap="false"/>
										<headerstyle Wrap="False"/>
									</asp:BoundColumn>
									<asp:HyperlinkColumn
										text="permissions"																		 
										DataNavigateUrlField="Id" 
										DataNavigateUrlFormatString="rolePermissions.aspx?RoleId={0}">
										<ItemStyle HorizontalAlign="Center" width="80"/>
									</asp:HyperlinkColumn>		
									<asp:HyperlinkColumn
										text="users"																		 
										DataNavigateUrlField="Id" 
										DataNavigateUrlFormatString="roleUsers.aspx?RoleId={0}">
										<ItemStyle HorizontalAlign="Center" width="80"/>
									</asp:HyperlinkColumn>																																						
									<asp:HyperlinkColumn text="edit"
										DataNavigateUrlField="Id" DataNavigateUrlFormatString="roleEdit.aspx?Id={0}">
										<ItemStyle HorizontalAlign="Center" width="80"/>
									</asp:HyperlinkColumn>		
									<asp:ButtonColumn text="delete" CommandName="Delete">
										<ItemStyle HorizontalAlign="Center" width="80"/>
									</asp:ButtonColumn>																						
								</columns>
							</cc:MySortGrid>
						</td></tr>
					</table>
				</asp:panel>	



			<!-----------------end of content----------------->			
			<asp:literal id="ltrLayout2" runat="server"/>				
		</form>	
  </body>
</html>			
