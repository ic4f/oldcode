<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="lefthimageadd.aspx.vb" Inherits="Foundation.WebAdmin.contentlibrary.lefthimageadd" %>

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
                            <input type="file" id="tbxFile" runat="server" name="tbxFile" />
                            <asp:RequiredFieldValidator ID="val2" runat="server" ErrorMessage="Please select a file" ControlToValidate="tbxFile" />
                        </p>
                        <p>
                            The file must be a black & white photo (jpg format) with the following dimensions: 168 x 106 pixels (no borders)
						
							<p class="field-title">Unique Description*</p>
                        <p class="field-input">
                            <asp:TextBox ID="tbxDescription" Style="width: 400px;" MaxLength="100" CssClass="textbox" runat="server" />
                            <asp:RequiredFieldValidator ID="valDescription" runat="server" ErrorMessage="Please provide a description" ControlToValidate="tbxDescription" />
                            <asp:CustomValidator ID="valUnique" Text="A file with this description already exists" runat="server" />
                        </p>


                        <p class="field-title">Displayed Text*</p>
                        <p class="field-input">
                            <asp:TextBox CssClass="textbox" ID="tbxText" runat="server" Style="width: 200px;" MaxLength="20" />
                            <asp:RequiredFieldValidator ID="val1" ControlToValidate="tbxText" Text=" - Please provide the text" runat="server" />
                        </p>

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
