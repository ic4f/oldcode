<%@ Page Language="vb" AutoEventWireup="false" Codebehind="pageInfo.aspx.vb" Inherits="Foundation.WebAdmin.webpages.pageInfo"%>
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
		<td align="right" style="font-weight:bold;color:#444444;">Title:</td>
		<td><asp:literal id="ltrTitle" runat="server"/></td>
	</tr>
	<tr>
		<td align="right" style="font-weight:bold;color:#444444;">URL:</td>
		<td><asp:literal id="ltrUrl" runat="server"/></td>
	</tr>
	<tr>
		<td align="right" style="font-weight:bold;color:#444444;">ID:</td>
		<td><asp:literal id="ltrId" runat="server"/></td>
	</tr>		
	<tr>
		<td align="right" style="font-weight:bold;color:#444444;">Menu:</td>
		<td><asp:literal id="ltrMenu" runat="server"/></td>
	</tr>			
	<tr>
		<td align="right" style="font-weight:bold;color:#444444;">Page Group:</td>
		<td><asp:literal id="ltrGroup" runat="server"/></td>
	</tr>			
	<tr>
		<td colspan="2" style="padding-left:98px;">		
			<asp:button id="btnEdit" text="Edit" cssclass="button" runat="server" style="width:80px;"/>
		</td>
	</tr>		
</table>

<p>

<asp:panel id="pnlCanDelete" runat="server" style="border:1px solid red;padding:10px;background-color:#f6f6f6;width:560px;" visible="false">
	<p style="font-weight:bold;color:#444444;font-size:14px;margin-bottom:5px;"">Delete Page</p>
	There are no internal links to this page. To delete this page, press the button below. This operation cannot be undone.
	<p><asp:literal id="ltrAdmin" runat="server"/></p>
	<p align="center"><asp:button id="btnDelete" cssclass="button" text="Delete Page" runat="server"/></p>
</asp:panel>

<p>

<asp:panel id="pnlAdminDelete" runat="server" style="border:1px solid red;padding:10px;background-color:pink;width:560px;" visible="false">
	<p style="font-weight:bold;color:#444444;font-size:14px;margin-bottom:5px;"">Special Delete</p>
	Developers only: your account has the permission to delete this page. Please make sure that there are no dependencies except the system dependency.
	Deleting the page will only delete the record from the database: you must manually delete the relevant physical files.
	<p align="center"><asp:button id="btnAdminDelete" cssclass="button" text="Delete Custom Page" runat="server"/></p>
</asp:panel>

<p>

<asp:panel id="pnlDependencies" runat="server" style="border:1px solid #cacaca;padding:10px;" visible="false">
	
	<p style="font-weight:bold;color:#444444;font-size:14px;margin-bottom:5px;">Page Dependencies</p>
	
	You <span style="color:maroon;">cannot delete</span> this page because of the following dependencies:
	
	<div style="margin-left:20px;">

		<p>
		<asp:panel id="pnlDependSystem" runat="server" visible="false">
			<p><b>System:</b> This page is hard-coded into the system and cannot be deleted.</p>
		</asp:panel>
		
		<p>
		<asp:panel id="pnlDependDepartments" runat="server" visible="false">
			<p><b>Departments:</b> <asp:literal id="ltrDependDepartments" runat="server"/></p>
			<asp:repeater id="rptDependDepartments" runat="server">
				<itemTemplate>
					<p style="margin-left:20px;margin-bottom:0px;margin-top:5px;"><asp:literal id="ltrDependDepartment" runat="server"/></p>
				</itemTemplate>
			</asp:repeater>					
		</asp:panel>		
				
		<p>
		<asp:panel id="pnlDependPages" runat="server" visible="false">
			<p><b>Inbound links:</b> <asp:literal id="ltrInboundPageLinks" runat="server"/></p>
			<asp:repeater id="rptDependPages" runat="server">
				<itemTemplate>
					<p style="margin-left:20px;margin-bottom:0px;margin-top:5px;"><asp:literal id="ltrDependPage" runat="server"/></p>
				</itemTemplate>
			</asp:repeater>					
		</asp:panel>
			
		<p>
		<asp:panel id="pnlDependModules" runat="server" visible="false">
			<p><b>Module links:</b> <asp:literal id="ltrInboundModuleLinks" runat="server"/></p>
			<asp:repeater id="rptDependModules" runat="server">
				<itemTemplate>
					<p style="margin-left:20px;margin-bottom:0px;margin-top:5px;"><asp:literal id="ltrDependModule" runat="server"/></p>
				</itemTemplate>
			</asp:repeater>			
		</asp:panel>

		<p>
		<asp:panel id="pnlDependMenus" runat="server" visible="false">
			<p><b>Menus:</b> This page is set as the default page for <asp:literal id="ltrDependMenus" runat="server"/></p>
			<asp:repeater id="rptDependMenus" runat="server">
				<itemTemplate>
					<p style="margin-left:20px;margin-bottom:0px;margin-top:5px;"><asp:literal id="ltrDependMenu" runat="server"/></p>
				</itemTemplate>
			</asp:repeater>			
		</asp:panel>

	</div>		
	
</asp:panel>

<p>

<asp:panel id="pnlLinks" runat="server" style="border:1px solid #cacaca;padding:10px;" visible="false">
	<p style="font-weight:bold;color:#444444;font-size:14px;margin-bottom:5px;">Page Links</p>
	
	<div style="margin-left:20px;">
	
		<p>
		<asp:panel id="pnlLinksToPages" runat="server" visible="false">
			<p><b>Outbound links:</b> this page links to <asp:literal id="ltrLinksToPages" runat="server"/></p>
			<asp:repeater id="rptLinksToPages" runat="server">
				<itemTemplate>
					<p style="margin-left:20px;margin-bottom:0px;margin-top:5px;"><asp:literal id="ltrLinksToPage" runat="server"/></p>
				</itemTemplate>
			</asp:repeater>					
		</asp:panel>
		
		<p>
		<asp:panel id="pnlLinksToFiles" runat="server" visible="false">
			<p><b>File references:</b> <asp:literal id="ltrLinksToFiles" runat="server"/></p>
			<asp:repeater id="rptLinksToFiles" runat="server">
				<itemTemplate>
					<p style="margin-left:20px;margin-bottom:0px;margin-top:5px;"><asp:literal id="ltrLinksToFile" runat="server"/></p>
				</itemTemplate>
			</asp:repeater>					
		</asp:panel>
		
		<p>
		<asp:panel id="pnlLinksToImages" runat="server" visible="false">
			<p><b>Image references:</b> <asp:literal id="ltrLinksToImages" runat="server"/></p>
			<asp:repeater id="rptLinksToImages" runat="server">
				<itemTemplate>
					<p style="margin-left:20px;margin-bottom:0px;margin-top:5px;"><asp:literal id="ltrLinksToImage" runat="server"/></p>
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
