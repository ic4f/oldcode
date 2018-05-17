<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="userEdit.aspx.vb" Inherits="Foundation.WebAdmin.settings.userEdit" %>

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

        <asp:Panel ID="pnlForm" CssClass="panel-form" runat="server">

            <p>This record was last modified on
                <asp:Literal ID="ltrModified" runat="server" />
                by
                <asp:Literal ID="ltrModifiedBy" runat="server" /></p>

            <p class="field-title">Email*</p>
            <p class="field-input">
                <asp:TextBox CssClass="textbox" ID="tbxEmail" runat="server" Style="width: 200px;" MaxLength="50" />
                <asp:RequiredFieldValidator ID="valEmail" ControlToValidate="tbxEmail" Text="<br>Please provide an email address" runat="server" />
                <asp:CustomValidator ID="valUnique" ErrorMessage="<br>This email already exists" runat="server" />
            </p>

            <p class="field-title">Password</p>
            <p class="field-input">
                <asp:TextBox CssClass="textbox" ID="tbxPassword" runat="server" Style="width: 200px;" MaxLength="20" />
                Leave blank to keep current password</p>

            <p class="field-title">First Name*</p>
            <p class="field-input">
                <asp:TextBox CssClass="textbox" ID="tbxFirstName" runat="server" Style="width: 200px;" MaxLength="20" />
                <asp:RequiredFieldValidator ID="valFirstName" ControlToValidate="tbxFirstName" Text="<br>Please provide a first name" runat="server" />
            </p>

            <p class="field-title">Middle</p>
            <p class="field-input">
                <asp:TextBox CssClass="textbox" ID="tbxMiddle" runat="server" Style="width: 200px;" MaxLength="20" /></p>

            <p class="field-title">Last Name*</p>
            <p class="field-input">
                <asp:TextBox CssClass="textbox" ID="tbxLastName" runat="server" Style="width: 200px;" MaxLength="30" />
                <asp:RequiredFieldValidator ID="valLastName" ControlToValidate="tbxFirstName" Text="<br>Please provide a last name" runat="server" />
            </p>

            <p class="field-title">Displayed Name</p>
            <p class="field-input">
                <asp:TextBox CssClass="textbox" ID="tbxDisplayedName" runat="server" Style="width: 200px;" MaxLength="70" /></p>

            <p>*Required fields</p>

            <div class="buttons">
                <asp:Button ID="btnSave" Text="Save" CssClass="button" runat="server" />&nbsp;
					<asp:Button ID="btnCancel" Text="Cancel" CssClass="button" runat="server" />
            </div>

        </asp:Panel>



        <!-----------------end of content----------------->
        <asp:Literal ID="ltrLayout2" runat="server" />
    </form>
</body>
</html>
