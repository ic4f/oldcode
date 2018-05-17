<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="himageAdd.aspx.vb" Inherits="Foundation.WebAdmin.contentlibrary.himageAdd" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title id="ctlTitleTag" runat="server" />
    <link href="../_system/styles/main.css" type="text/css" rel="stylesheet">
    <link href="../_system/styles/addeditpage.css" type="text/css" rel="stylesheet">
    <script language="javascript" type="text/javascript" src="../_system/javascript/menuhelper.js"></script>
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

            <table width="100%" cellpadding="0" cellspacing="0" style="font-size: 11px;">
                <tr valign="top">
                    <td width="80%">


                        <p class="field-title">File*</p>
                        <p class="field-input">
                            <input type="file" id="tbxFile" runat="server" />
                            <asp:RequiredFieldValidator ID="val1" runat="server" ErrorMessage="Please select a file" ControlToValidate="tbxFile" />
                        </p>

                        <p>
                            The file must be a jpg image with the following dimensions: 520 x 130 pixels (no borders)
										
					<p>*Required fields</p>

                        <div class="buttons">
                            <asp:Button ID="btnSave" Text="Upload" CssClass="button" runat="server" />&nbsp;
							<asp:Button ID="btnCancel" Text="Cancel" CssClass="button" runat="server" />
                        </div>

        </asp:Panel>



        <!-----------------end of content----------------->
        <asp:Literal ID="ltrLayout2" runat="server" />
    </form>
</body>
</html>
