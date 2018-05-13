<%@ Page language="c#" Codebehind="default.aspx.cs" AutoEventWireup="false" Inherits="Ei.Website.lineupadmin._default" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 
<html>
  <head>
		<title id="ctlTitleTag" runat="server"/>   
		<link href="../_system/styles/main.css" type="text/css" rel="stylesheet">  		
		<script language="javascript" type="text/javascript" src="../_system/javascript/menuhelper.js"></script>	
  </head>
  <body>	
    <form id="Form1" method="post" runat="server">      		
			<asp:literal id="ltrLayout1" runat="server"/>				
			<!---------------------content-------------------->	

			
			
				<asp:panel id="pnlCreate" runat="server" class="textcontentpanel">
					<p><a href="lineupAdd.aspx">Create Lineup</a></p>
					<p class="commentunderlink">Create new lineup</p>	
				</asp:panel>
				<p>
				<asp:panel id="pnlAdminister" runat="server" class="textcontentpanel">
					<p><a href="lineupAdminister.aspx">Administer Lineup</a></p>	
					<p class="commentunderlink">Administer existing lineups</p>			
				</asp:panel>		
				<p>
				<asp:panel id="pnlLineups" runat="server" class="textcontentpanel">
					<p><a href="lineupList.aspx">List of Lineups</a></p>			
					<p class="commentunderlink">View lineups & lineup administration results for assigned cases</p>	
				</asp:panel>				

									
			
						   
			<!-----------------end of content----------------->			
			<asp:literal id="ltrLayout2" runat="server"/>			
		</form>	
  </body>
</html>				