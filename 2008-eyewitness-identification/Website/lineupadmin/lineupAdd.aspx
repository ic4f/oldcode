<%@ Page language="c#" Codebehind="lineupAdd.aspx.cs" AutoEventWireup="false" Inherits="Ei.Website.lineupadmin.lineupAdd" %>
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

			
			
			<asp:panel id="pnlForm" cssclass="panel-form" runat="server">
				<p class="field-title">Description*</p>
				<p class="field-input"><asp:textbox cssclass="textbox" id="tbxDescription" runat="server" style="width:500px;" MaxLength="100"/>
					<asp:requiredfieldvalidator id="valDescription" controltovalidate="tbxDescription" text="<br>Please provide a description" runat="server"/>
					<asp:customvalidator id="valUnique" ErrorMessage="<br>This description already exists" runat="server"/></p>									

				<p class="field-title">Case*</p>
				<p class="field-input"><asp:dropdownlist cssclass="textbox" id="ddlCase" runat="server"/>
									<asp:requiredfieldvalidator id="val2" controltovalidate="ddlCase" text="<br>Please select a case" runat="server"/>
				</p>

				<p class="field-title">Suspect*</p>
				<p class="field-input"><asp:dropdownlist cssclass="textbox" id="ddlSuspect" runat="server"/>
									<asp:requiredfieldvalidator id="val3" controltovalidate="ddlSuspect" text="<br>Please select a suspect" runat="server"/>
				</p>				

				<p>
				<table cellpadding="0" cellspacing="0">
					<tr valign="top">
						<td><asp:label id="lblSuspect" runat="server"/></td>
						<td width=20><td>
						<td>
							<p style="margin:0 0 5 0;"><span style="font-weight:bold;color:#414141;">Gender:</span> <asp:label id="lblGender" runat="server"/></p>
							<p style="margin:0 0 5 0;"><span style="font-weight:bold;color:#414141;">Race:</span> <asp:label id="lblRace" runat="server"/></p>
							<p style="margin:0 0 5 0;"><span style="font-weight:bold;color:#414141;">Hair color:</span> <asp:label id="lblHair" runat="server"/></p>
							<p style="margin:0 0 5 0;"><span style="font-weight:bold;color:#414141;">Age range:</span> <asp:label id="lblAge" runat="server"/></p>
							<p style="margin:0 0 5 0;"><span style="font-weight:bold;color:#414141;">Weight range:</span> <asp:label id="lblWeight" runat="server"/></p>
							<p style="margin:0 0 5 0;"><span style="font-weight:bold;color:#414141;">Notes:</span> <asp:label id="lblNotes" runat="server"/></p>						
						</td>
					</tr>
				</table>	
				</p>
				
				<p class="field-title">Notes</p>
				<p class="field-input"><asp:textbox id="tbxNotes" textmode="multiline" style="width:100%;height:150px;" cssclass="textbox" runat="server"/></p>
													
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