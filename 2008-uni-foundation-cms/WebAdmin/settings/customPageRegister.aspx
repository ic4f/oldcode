<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="customPageRegister.aspx.vb" Inherits="Foundation.WebAdmin.settings.customPageRegister" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title id="ctlTitleTag" runat="server" />
    <link href="../_system/styles/main.css" type="text/css" rel="stylesheet">
    <script language="javascript" type="text/javascript" src="../_system/javascript/menuhelper.js"></script>
    <link href="../_system/styles/addeditpage.css" type="text/css" rel="stylesheet">
    <script language="javascript" type="text/javascript" src="../tiny_mce/tiny_mce.js"></script>
    <script language="javascript" type="text/javascript" src="../_system/javascript/init.js"></script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <asp:Literal ID="ltrLayout1" runat="server" />
        <!---------------------content-------------------->



        <asp:Panel ID="pnlConfirm" CssClass="panel-confirm" runat="server">
            <asp:Literal ID="ltrConfirmMessage" runat="server" />
            <div class="buttons">
                <asp:Button ID="btnAddAnother" Text="Add Another" CssClass="button" runat="server" />
                <asp:Button ID="btnReturn" Text="Return" CssClass="button" runat="server" />
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlError" CssClass="panel-error" runat="server">
            <asp:Literal ID="ltrErrorMessage" runat="server" />
        </asp:Panel>

        <asp:Panel ID="pnlForm" CssClass="panel-form" runat="server">

            <p class="field-title">Unique Page Title*</p>
            <p class="field-input">
                <asp:TextBox CssClass="textbox" ID="tbxPageTitle" runat="server" Style="width: 526px;" MaxLength="250" />
                <asp:RequiredFieldValidator ID="val1" ControlToValidate="tbxPageTitle" Text=" - Please provide a title" runat="server" />
                <asp:CustomValidator ID="valUniqueTitle" ControlToValidate="tbxPageTitle" Text=" - The page title must be unique" runat="server" />
                <a href="#" onclick="javascript:open('../_system/datalists/pagetitles.aspx', '', 'width=300, height=500, resizable, scrollbars');">Display existing page titles</a>
            </p>

            <p class="field-title">
                Title*
						<asp:RequiredFieldValidator ID="val2" ControlToValidate="tbxContentTitle" Text=" - Please provide a title" runat="server" />
            </p>
            <p class="field-input">
                <asp:TextBox CssClass="textbox" ID="tbxContentTitle" runat="server" Style="width: 526px;" MaxLength="250" /></p>

            <p class="field-title">
                File Name* (example: myNewPage.aspx)
						<asp:RequiredFieldValidator ID="Requiredfieldvalidator1" ControlToValidate="tbxFileName" Text=" - Please provide a file name" runat="server" />
            </p>
            <p class="field-input">
                <asp:TextBox CssClass="textbox" ID="tbxFileName" runat="server" Style="width: 200px;" MaxLength="50" /></p>

            <p>
                <table width="100%" cellpadding="0" cellspacing="0" style="font-size: 11px;">
                    <tr valign="top">
                        <td width="80%">
                            <p class="field-title">Body</p>
                            <p class="field-input">
                                <div style="padding: 5px; border: 1px solid #cacaca; background-color: #ececec;">
                                    <textarea class="mceEditor" id="tbxBody" name="tbxBody" runat="server"></textarea>
                                    <p>
                                        <iframe id="iframeContent" name="iframeContent" src="../_system/datalists/contentSelector.aspx" style="border: 1px #cacaca solid; width: 100%; height: 400px;"></iframe>
                                </div>
                            </p>
                            </div>
            </p>

            <p class="field-title">Redirect Link</p>
            <p class="field-input">
                <asp:TextBox CssClass="textbox" ID="tbxRedirect" runat="server" Style="width: 100%;" MaxLength="100" /></p>

            <p class="field-title">Options</p>
            <p class="field-input">
                <div style="padding: 3px; border: 1px solid #cacaca;">
                    <p>
                        <asp:CheckBox ID="cbxDisplayPublishedDate" runat="server" />Display revision date</p>
                    <p>
                        <asp:CheckBox ID="cbxDisplayPrintable" runat="server" />Display printable version link</p>
                    <p>
                        <asp:CheckBox ID="cbxDisplayBookmarking" runat="server" />Display social bookmarking tools</p>
                </div>
            </p>

            <p class="field-title">Editing Options</p>
            <p class="field-input">
                <div style="padding: 3px; border: 1px solid #cacaca;">
                    <p>
                        <asp:CheckBox ID="cbxDisplayContent" runat="server" />Display page title and body</p>
                    <p>
                        <asp:CheckBox ID="cbxCanEditBody" runat="server" />Allow body edits</p>
                    <p>
                        <asp:CheckBox ID="cbxCanChangeMenu" runat="server" />Allow menu change</p>
                </div>
            </p>

            <p class="field-title">
                Comments*
									<asp:RequiredFieldValidator ID="valComments" ControlToValidate="tbxComment" Text=" - Please provide a comment to explain the user what this page does" runat="server" />
            </p>
            </p>
								<p class="field-input">
                                    <asp:TextBox ID="tbxComment" TextMode="multiline" Style="width: 100%; height: 150px;" CssClass="textbox" runat="server" /></p>
            </td>
							<td>&nbsp;</td>
            <td class="labels-area">

                <p class="field-title">Parent Menu* <span style="color: red;">
                    <asp:Literal ID="ltrParentError" runat="server" Text="- Please select a parent menu" Visible="false" /></span></p>
                <input type="hidden" id="hidMenu" name="hidMenu" runat="server" />
                <iframe id="iframeMenu" name="iframeMenu" src="../_system/datalists/menu.aspx" class="sidebaroptionframe"></iframe>

                <p class="field-title">Sidebar Modules</p>
                <input type="hidden" id="hidModules" name="hidModules" runat="server" />
                <iframe id="iframeModules" name="iframeModules" src="../_system/datalists/modules.aspx" class="sidebaroptionframe"></iframe>

                <p class="field-title">Public Tags</p>
                <input type="hidden" id="hidTags" name="hidTags" runat="server" />
                <iframe id="iframeTags" name="iframeTags" src="../_system/datalists/tags.aspx" class="sidebaroptionframe"></iframe>

                <p class="field-title">Content Labels</p>
                <input type="hidden" id="hidLabels" name="hidLabels" runat="server" />
                <iframe id="iframeLabels" name="iframeLabels" src="../_system/datalists/labels.aspx" class="sidebaroptionframe"></iframe>

            </td>
            </tr>
					</table>
					
					<p>*Required fields</p>

            <div class="buttons">
                <asp:Button ID="btnSave" Text="Save Draft" CssClass="button" runat="server" />&nbsp;
							<asp:Button ID="btnCancel" Text="Cancel" CssClass="button" runat="server" />
            </div>

        </asp:Panel>


        <script>
            function getmenu() {
                document.getElementById("hidMenu").value = "-1";
                var sb = "";
                var cf = window.frames['iframeMenu'].document.forms[0];
                for (var i = 0; i < cf.length; i++) {
                    if (cf.elements[i].name.indexOf("radmenu") > -1)
                        if (cf.elements[i].checked)
                            document.getElementById("hidMenu").value = cf.elements[i].value;
                }
            }
            function getmodules() {
                var sb = "";
                var cf = window.frames['iframeModules'].document.forms[0];
                for (var i = 0; i < cf.length; i++) {
                    if (cf.elements[i].id.indexOf("cblModules") > -1)
                        sb += cf.elements[i].checked + " ";
                }
                document.getElementById("hidModules").value = sb;
            }
            function gettags() {
                var sb = "";
                var cf = window.frames['iframeTags'].document.forms[0];
                for (var i = 0; i < cf.length; i++) {
                    if (cf.elements[i].id.indexOf("cblTags") > -1)
                        sb += cf.elements[i].checked + " ";
                }
                document.getElementById("hidTags").value = sb;
            }
            function getlabels() {
                var sb = "";
                var cf = window.frames['iframeLabels'].document.forms[0];
                for (var i = 0; i < cf.length; i++) {
                    if (cf.elements[i].id.indexOf("cblLabels") > -1)
                        sb += cf.elements[i].checked + " ";
                }
                document.getElementById("hidLabels").value = sb;
            }
        </script>


        <!-----------------end of content----------------->
        <asp:Literal ID="ltrLayout2" runat="server" />
    </form>
</body>
</html>
