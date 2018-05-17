<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="imageAdd.aspx.vb" Inherits="Foundation.WebAdmin.contentlibrary.imageAdd" %>

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
            <p>
                <asp:Literal ID="ltrPreview" runat="server" />
                <div class="buttons">
                    <asp:Button ID="btnAddAnother" Text="Add Another" CssClass="button" runat="server" />
                    <asp:Button ID="btnReturn" Text="Return" CssClass="button" runat="server" />
                </div>
        </asp:Panel>

        <asp:Panel ID="pnlError" CssClass="panel-error" runat="server">
            <asp:Literal ID="ltrErrorMessage" runat="server" />
        </asp:Panel>

        <asp:Panel ID="pnlForm" runat="server">

            <div class="panel-form">

                <p style="font-size: 11pt; font-weight: bold; color: #414141;" align="center">Select Image File</p>

                <p class="field-title">File*</p>
                <p class="field-input">
                    <input type="file" id="tbxFile" runat="server" name="tbxFile" />
                    <asp:RequiredFieldValidator ID="valFile" runat="server" ErrorMessage="Please select a file" ControlToValidate="tbxFile" />
                    <asp:CustomValidator ID="valFileType" Text="This is not an image file" runat="server" />
                    <asp:Literal ID="ltrTypes" runat="server" />
                </p>

                <p class="field-title">Maximum Width</p>
                <p class="field-input">
                    <asp:TextBox ID="tbxWidth" Text="480" Style="width: 35px;" MaxLength="4" CssClass="textbox" runat="server" />
                    pixels</p>

                <p class="field-title">Maximum Height</p>
                <p class="field-input">
                    <asp:TextBox ID="tbxHeight" Text="360" Style="width: 35px;" MaxLength="4" CssClass="textbox" runat="server" />
                    pixels</p>

                <p>
                    If you do not specify the image width and height (i.e., leave these fields BLANK), the system will not resize your image.
							If you plan to display this image within a web page, please make sure it is not wider than 480 pixels. 
						<center>
                            <div style="width: 480px; padding: 3px; border: 1px #cacaca solid; background: #e4e4e4;">
                                This sample rectangle is 480 pixels wide
                            </div>
                        </center>
                    <br>
            </div>

            <p>

                <div class="panel-form">

                    <p style="font-size: 11pt; font-weight: bold; color: #414141;" align="center">Image Information</p>

                    <table width="100%" cellpadding="0" cellspacing="0" style="font-size: 11px;">
                        <tr valign="top">
                            <td width="80%">

                                <p class="field-title">Unique Description*</p>
                                <p class="field-input">
                                    <asp:TextBox ID="tbxDescription" Style="width: 400px;" MaxLength="100" CssClass="textbox" runat="server" />
                                    <asp:RequiredFieldValidator ID="valDescription" runat="server" ErrorMessage="Please provide a description" ControlToValidate="tbxDescription" />
                                    <asp:CustomValidator ID="valUnique" Text="A file of this type with this description already exists" runat="server" />
                                </p>

                                <p class="field-title">Comments</p>
                                <p class="field-input">
                                    <asp:TextBox ID="tbxComment" TextMode="multiline" Style="width: 100%; height: 198px;" CssClass="textbox" runat="server" /></p>

                            </td>
                            <td>&nbsp;</td>
                            <td class="labels-area">
                                <p class="field-title">Labels</p>
                                <input type="hidden" id="hidCats" name="hidCats" runat="server" />
                                <iframe id="iframecat" name="iframecat" src="../_system/datalists/labels.aspx" class="sidebaroptionframe" style="height: 243px;"></iframe>
                            </td>
                        </tr>
                    </table>

                    <p>*Required fields</p>

                    <div class="buttons">
                        <asp:Button ID="btnSave" Text="Upload" CssClass="button" runat="server" />&nbsp;
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
