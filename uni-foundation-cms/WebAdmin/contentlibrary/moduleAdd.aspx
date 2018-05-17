<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="moduleAdd.aspx.vb" Inherits="Foundation.WebAdmin.contentlibrary.moduleAdd" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title id="ctlTitleTag" runat="server" />
    <link href="../_system/styles/main.css" type="text/css" rel="stylesheet">
    <script language="javascript" type="text/javascript" src="../_system/javascript/menuhelper.js"></script>
    <script language="javascript" type="text/javascript" src="../_system/javascript/utils.js"></script>
    <link href="../_system/styles/addeditpage.css" type="text/css" rel="stylesheet">
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

            <span style="color: red;">
                <asp:Literal ID="ltrLinkError" runat="server" Text="Link Error: fill out the external link textbox OR select a web page" Visible="false" /></span>
            <span style="color: red;">
                <asp:Literal ID="ltrContentError" runat="server" Text="Error: you must provide at least one of the following: an image, a title, a body" Visible="false" /></span>

            <table width="100%" cellpadding="0" cellspacing="0" style="font-size: 11px;">
                <tr valign="top">
                    <td width="60%">

                        <p class="field-title">Unique Title (displayed in CMS only)*</p>
                        <p class="field-input">
                            <asp:TextBox CssClass="textbox" ID="tbxAdminTitle" runat="server" Style="width: 400px;" MaxLength="50" />
                            <asp:RequiredFieldValidator ID="val1" ControlToValidate="tbxAdminTitle" Text=" - Please provide a title" runat="server" />
                            <asp:CustomValidator ID="valUniqueTitle" ControlToValidate="tbxAdminTitle" Text=" - This title must be unique" runat="server" />
                            <br>
                            <a href="#" onclick="javascript:open('../_system/datalists/moduletitles.aspx', '', 'width=300, height=500, resizable, scrollbars');">Display existing module titles</a>
                        </p>

                        <p class="field-title">Image</p>
                        <p class="field-input">
                            <div style="padding: 8px; border: 1px solid #cacaca; width: 381px;">
                                <input type="file" id="tbxFile" runat="server" name="tbxFile" />
                                <asp:CustomValidator ID="valFileType" Text="This is not an image file" runat="server" />
                                <br>
                                <asp:Literal ID="ltrTypes" runat="server" />
                                <br>
                                <br>
                                For best results, provide an image which is exactly
                                <asp:Literal ID="ltrWidth" runat="server" />
                                pixels wide and at most
								<asp:Literal ID="ltrHeight" runat="server" />
                                pixels high
                            </div>
                        </p>

                        <p class="field-title">Title</p>
                        <p class="field-input">
                            <asp:TextBox CssClass="textbox" ID="tbxTitle" runat="server" Style="width: 240px;" TextMode="MultiLine" Rows="2"
                                onKeyDown="countChars(tbxTitle, remLen1, 100)" onKeyUp="countChars(tbxTitle, remLen1, 100)" />
                            <br>
                            <input readonly type="text" name="remLen1" size="3" maxlength="4" value="100" style="border: 1px solid #414141; font-size: 11px;">
                            <font color="#000000">characters remaining</font>
                        </p>

                        <p class="field-title">Body</p>
                        <p class="field-input">
                            <asp:TextBox CssClass="textbox" ID="tbxBody" runat="server" Style="width: 240px;" TextMode="MultiLine" Rows="8"
                                onKeyDown="countChars(tbxBody, remLen2, 250)" onKeyUp="countChars(tbxBody, remLen2, 250)" />
                            <br>
                            <input readonly type="text" name="remLen2" size="3" maxlength="4" value="250" style="border: 1px solid #414141; font-size: 11px;">
                            <font color="#000000">characters remaining</font>
                        </p>

                        <p class="field-title">External Link</p>
                        <p class="field-input">
                            <asp:TextBox CssClass="textbox" ID="tbxLink" runat="server" Style="width: 400px;" MaxLength="50" /></p>

                        <p class="field-title">Status Options</p>
                        <p class="field-input">
                            <div style="padding: 3px; border: 1px solid #cacaca; width: 391px;">
                                <asp:RadioButtonList ID="radStatus" runat="server" Style="font-size: 11px;" />
                            </div>
                        </p>

                        <p class="field-title">Content Labels</p>
                        <input type="hidden" id="hidLabels" name="hidLabels" runat="server" />
                        <iframe id="iframeLabels" name="iframeLabels" src="../_system/datalists/labels.aspx" class="sidebaroptionframe" style="height: 115px;"></iframe>

                    </td>
                    <td>&nbsp;</td>
                    <td class="labels-area">
                        <p class="field-title">Link to Web Page</p>
                        <input type="hidden" id="hidPage" name="hidPage" runat="server" />
                        <iframe id="iframePage" name="iframePage" src="../_system/datalists/publishedPages.aspx" class="sidebaroptionframe" style="width: 100%; height: 730px;"></iframe>

                    </td>
                </tr>
            </table>

            <p>*Required fields</p>

            <div class="buttons">
                <asp:Button ID="btnSave" Text="Save" CssClass="button" runat="server" />&nbsp;
							<asp:Button ID="btnCancel" Text="Cancel" CssClass="button" runat="server" />
            </div>

        </asp:Panel>

        <script>
            function getPage() {
                document.getElementById("hidPage").value = "-1";
                var sb = "";
                var cf = window.frames['iframePage'].document.forms[0];
                for (var i = 0; i < cf.length; i++) {
                    if (cf.elements[i].name.indexOf("radPage") > -1)
                        if (cf.elements[i].checked)
                            document.getElementById("hidPage").value = cf.elements[i].value;
                }
            }
            function getLabels() {
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
