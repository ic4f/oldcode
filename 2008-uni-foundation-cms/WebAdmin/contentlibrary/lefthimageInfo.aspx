<%@ Page Language="vb" AutoEventWireup="false" Codebehind="lefthimageInfo.aspx.vb" Inherits="Foundation.WebAdmin.contentlibrary.lefthimageInfo"%>
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

		
<table cellpadding="3" cellspacing="0" border="1" style="border:1px solid #cacaca;border-collapse:collapse;">
	<tr>
		<td align="right" style="font-weight:bold;color:#444444;">Description:</td>
		<td><asp:literal id="ltrDescription" runat="server"/></td>
	</tr>
	<tr>
		<td align="right" style="font-weight:bold;color:#444444;">ID:</td>
		<td><asp:literal id="ltrId" runat="server"/></td>
	</tr>			
</table>	

<p><asp:literal id="ltrImage" runat="server"/></p>

<p>

<asp:panel id="pnlCanDelete" runat="server" style="border:1px solid red;padding:10px;background-color:#f6f6f6;width:560px;" visible="false">
	<p style="font-weight:bold;color:#444444;font-size:14px;margin-bottom:5px;"">Delete Header Image</p>
	This header image is not assigned to any menus. To delete this image, press the button below. This operation cannot be undone.
	<p align="center"><asp:button id="btnDelete" cssclass="button" text="Delete Image" runat="server"/></p>
</asp:panel>

<p>

<asp:panel id="pnlDependencies" runat="server" style="border:1px solid #cacaca;padding:10px;" visible="false">
	
	<p style="font-weight:bold;color:#444444;font-size:14px;margin-bottom:5px;">Header Image Dependencies</p>
	
	You <span style="color:maroon;">cannot delete</span> this header image because of the following dependencies:
	
	<div style="margin-left:20px;">
				
		<p>
		<asp:panel id="pnlDependMenus" runat="server" visible="false">
			<p><b>Menu Assignment:</b> <asp:literal id="ltrDependMenus" runat="server"/></p>
			<asp:repeater id="rptDependMenus" runat="server">
				<itemTemplate>
					<p style="margin-left:20px;margin-bottom:0px;margin-top:5px;"><asp:literal id="ltrDependMenu" runat="server"/></p>
				</itemTemplate>
			</asp:repeater>					
		</asp:panel>			

	</div>		
	
</asp:panel>	


			<!-----------------end of content----------------->			
			<asp:literal id="ltrLayout2" runat="server"/>				
		</form>	
  </body>
</html>			