<%@ Page language="c#" Codebehind="roleEdit.aspx.cs" AutoEventWireup="false" Inherits="Ei.Website.settings.roleEdit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 
<html>
  <head>
		<title id="ctlTitleTag" runat="server"/>   
		<link href="../_system/styles/main.css" type="text/css" rel="stylesheet">  		
		<script language="javascript" type="text/javascript" src="../_system/javascript/menuhelper.js"></script>	
		<link href="../_system/styles/addeditpage.css" type="text/css" rel="stylesheet">  
  </head>
  <body>	
    <form id="Form1" method="post" runat="server">       			
			<asp:literal id="ltrLayout1" runat="server"/>				
			<!---------------------content-------------------->	

			
			<asp:panel id="pnlConfirm" cssclass="panel-confirm" runat="server">
				<asp:literal id="ltrConfirmMessage" runat="server"/>
				<div class="buttons">
					<asp:button id="btnReturn" text="Return" cssclass="button" runat="server" />
				</div>	
			</asp:panel>
			
			<asp:panel id="pnlError" cssclass="panel-error" runat="server">
				<asp:literal id="ltrErrorMessage" runat="server"/>	
			</asp:panel>		

			<asp:panel id="pnlForm" cssclass="panel-form" runat="server">
				<p class="field-title">Role*</p>
				<p class="field-input"><asp:textbox cssclass="textbox" id="tbxRole" runat="server" style="width:200px;" MaxLength="50"/>
					<asp:requiredfieldvalidator id="valRole" controltovalidate="tbxRole" text="<br>Please provide a role name" runat="server"/>
					<asp:customvalidator id="valUnique" ErrorMessage="<br>This role already exists" runat="server"/></p>									
																
				<p>*Required fields</p>
							
				<div class="buttons">
					<asp:button id="btnSave" text="Save" cssclass="button" runat="server"/>&nbsp;
					<asp:button id="btnCancel" text="Cancel" cssclass="button" runat="server"/>
				</div>
				
			</asp:panel>				



			<!-----------------end of content----------------->			
			<asp:literal id="ltrLayout2" runat="server"/>				
		</form>	
  </body>
</html>			
