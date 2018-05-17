<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="dstoryEdit.aspx.vb" Inherits="Foundation.WebAdmin.webpages.dstoryEdit" %>

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
                <asp:CustomValidator ID="valUniqueTitleError" ControlToValidate="tbxPageTitle" Text=" - The page title must be unique" runat="server" />
                <a href="#" onclick="javascript:open('../_system/datalists/pagetitles.aspx', '', 'width=300, height=500, resizable, scrollbars');">Display existing page titles</a>
            </p>

            <p class="field-title">
                Title*
							<asp:RequiredFieldValidator ID="val12" ControlToValidate="tbxContentTitle" Text=" - Please provide a title" runat="server" />
            </p>
            <p class="field-input">
                <asp:TextBox CssClass="textbox" ID="tbxContentTitle" runat="server" Style="width: 526px;" MaxLength="250" /></p>

            <p class="field-title">
                Displayed Donor Name(s)*
						<asp:RequiredFieldValidator ID="val2" ControlToValidate="tbxNames" Text=" - Please provide a name" runat="server" />
            </p>
            <p class="field-input">
                <asp:TextBox CssClass="textbox" ID="tbxNames" runat="server" Style="width: 526px;" MaxLength="250" /></p>

            <p class="field-title">Photo</p>
            <p class="field-input">
                <div style="padding: 8px; border: 1px solid #cacaca; width: 508px;">
                    <input type="file" id="tbxFile" runat="server" name="tbxFile" />
                    <asp:CustomValidator ID="valFileType" Text="This is not an image file" runat="server" />
                    <br>
                    <asp:Literal ID="ltrTypes" runat="server" />
                    <br>
                    <br>
                    For best results, provide an image which is exactly
                    <asp:Literal ID="ltrWidth" runat="server" />
                    pixels wide and  
							<asp:Literal ID="ltrHeight" runat="server" />
                    pixels high. 
							<asp:Literal ID="ltrImage" runat="server" />
                    <br>
                    <br>
                    <asp:CheckBox ID="cbxRemove" runat="server" />Remove current image
                </div>
            </p>

            <p class="field-title">Display Options</p>
            <p class="field-input">
                <div style="padding: 3px; border: 1px solid #cacaca; width: 518px;">
                    <p>
                        <asp:CheckBox ID="cbxFeatured" runat="server" />Featured</p>
                </div>
            </p>

            <p class="field-title">
                Summary*
									<asp:RequiredFieldValidator ID="val4" ControlToValidate="tbxSummary" Text=" - Please provide a summary" runat="server" />
            </p>
            <p class="field-input">
                <asp:TextBox CssClass="textbox" ID="tbxSummary" runat="server" Style="width: 526px;" TextMode="MultiLine" Rows="8"
                    onKeyDown="countChars(tbxSummary, remLen2, 500)" onKeyUp="countChars(tbxSummary, remLen2, 500)" />
                <br>
                <input readonly type="text" name="remLen2" size="3" maxlength="4" value="500" style="border: 1px solid #414141; font-size: 11px;">
                <font color="#000000">characters remaining</font>
            </p>

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

            <p class="field-title">Comments</p>
            <p class="field-input">
                <asp:TextBox ID="tbxComment" TextMode="multiline" Style="width: 100%; height: 150px;" CssClass="textbox" runat="server" /></p>
            </td>
								<td>&nbsp;</td>
            <td class="labels-area">

                <p class="field-title">Sidebar Modules</p>
                <input type="hidden" id="hidModules" name="hidModules" runat="server" />
                <asp:Literal ID="ltrModulesFrame" runat="server" />

                <p class="field-title">Public Tags</p>
                <input type="hidden" id="hidTags" name="hidTags" runat="server" />
                <asp:Literal ID="ltrTagsFrame" runat="server" />

                <p class="field-title">Content Labels</p>
                <input type="hidden" id="hidLabels" name="hidLabels" runat="server" />
                <asp:Literal ID="ltrLabelsFrame" runat="server" />

            </td>
            </tr>
						</table>
						
						<p>*Required fields</p>

            <div class="buttons">
                <div style="border: 1px solid #cacaca; background-color: #ffffff; padding: 10px; margin-bottom: 5px;">
                    SAVE DRAFT means that the page BODY is saved as a draft and is not displayed until published.
                    <br>
                    <br>
                    <b>All other fields and relationships 
							are published</b>

                </div>
                <asp:Button ID="btnSave" Text="Save Draft" CssClass="button" runat="server" />&nbsp;
							<asp:Button ID="btnCancel" Text="Cancel" CssClass="button" runat="server" />&nbsp;
							<asp:Button ID="btnPublish" Text="Publish" CssClass="button" runat="server" />
            </div>

        </asp:Panel>


        <script>
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
