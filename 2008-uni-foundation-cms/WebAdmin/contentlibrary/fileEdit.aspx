<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="fileEdit.aspx.vb" Inherits="Foundation.WebAdmin.contentlibrary.fileEdit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title id="ctlTitleTag" runat="server" />
    <link href="../_system/styles/main.css" type="text/css" rel="stylesheet">
    <script language="javascript" type="text/javascript" src="../_system/javascript/menuhelper.js"></script>
    <link href="../_system/styles/addeditpage.css" type="text/css" rel="stylesheet">
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

        <asp:Panel ID="pnlForm" runat="server">

            <div class="panel-form">

                <p style="font-size: 11pt; font-weight: bold; color: #414141;" align="center">Edit or Replace File</p>

                <p class="field-title">Current file</p>
                <p class="field-input">
                    <table style="border: 1px solid #cacaca; padding: 2px; font-size: 11px; background-color: #ffffff;">
                        <tr>
                            <td>
                                <asp:Literal ID="ltrFile" runat="server" /></td>
                        </tr>
                    </table>

                    <p class="field-title">New file</p>
                <p class="field-input">
                    <input type="file" id="tbxFile" runat="server" />
                    <asp:CustomValidator ID="valFileType" Text="Wrong file format" runat="server" />
                    <asp:Literal ID="ltrTypes" runat="server" />
                </p>

                <p>
                    To edit the file, download the current file to your computer, make the edits, 
						and then use the textbox above to select the new version. 
						<br>
                    Once you click the SAVE button, the selected file will <b>REPLACE</b>
                the current file.
								
            </div>

            <p>

                <div class="panel-form">

                    <p style="font-size: 11pt; font-weight: bold; color: #414141;" align="center">Edit File Information</p>

                    <table width="100%" cellpadding="0" cellspacing="0" style="font-size: 11px;">
                        <tr valign="top">
                            <td width="80%">

                                <p class="field-title">Unique Description*</p>
                                <p class="field-input">
                                    <asp:TextBox ID="tbxDescription" Style="width: 400px;" MaxLength="100" CssClass="textbox" runat="server" />
                                    <asp:RequiredFieldValidator ID="valDescription" runat="server" ErrorMessage="Please provide a description" ControlToValidate="tbxDescription" />
                                </p>

                                <p class="field-title">Comments</p>
                                <p class="field-input">
                                    <asp:TextBox ID="tbxComment" TextMode="multiline" Style="width: 100%; height: 198px;" CssClass="textbox" runat="server" /></p>

                            </td>
                            <td>&nbsp;</td>
                            <td class="labels-area">
                                <p class="field-title">Labels</p>
                                <input type="hidden" id="hidCats" name="hidCats" runat="server" />
                                <asp:Literal ID="ltrFrame" runat="server" />

                            </td>
                        </tr>
                    </table>

                    <p>*Required fields</p>

                    <div class="buttons">
                        <asp:Button ID="btnSave" Text="Save" CssClass="button" runat="server" />&nbsp;
								<asp:Button ID="btnCancel" Text="Cancel" CssClass="button" runat="server" />
                    </div>

                </div>

        </asp:Panel>


        <script>
            function getlabels() {
                var sb = "";
                var cf = window.frames['iframecat'].document.forms[0];
                for (var i = 0; i < cf.length; i++) {
                    if (cf.elements[i].id.indexOf("cblLabels") > -1)
                        sb += cf.elements[i].checked + " ";
                }
                document.getElementById("hidCats").value = sb;
            }
        </script>




        <!-----------------end of content----------------->
        <asp:Literal ID="ltrLayout2" runat="server" />
    </form>
</body>
</html>
