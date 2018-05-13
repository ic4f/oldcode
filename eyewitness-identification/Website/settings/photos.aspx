<%@ Page language="c#" Codebehind="photos.aspx.cs" AutoEventWireup="false" Inherits="Ei.Website.settings.photos" %>
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

				<asp:panel id="pnlModify" runat="server" class="textcontentpanel">
					<p><a href="photoAdd.aspx">Add photos</a></p>
					<p class="commentunderlink">Add and classify new photos</p>
				</asp:panel>
				<p>
				<asp:panel id="pnlView" runat="server" class="textcontentpanel">
					<p><a href="photoList.aspx">Browse database</a></p>
					<p class="commentunderlink">Browse, search photo database</p>			
				</asp:panel>

			<!-----------------end of content----------------->			
			<asp:literal id="ltrLayout2" runat="server"/>				
		</form>	
  </body>
</html>			
