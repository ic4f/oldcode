<%@ Page Language="c#" CodeBehind="caseEdit.aspx.cs" AutoEventWireup="false" Inherits="Ei.Website.operations.caseEdit" %>

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
            <p class="field-title">Reference Number*</p>
            <p class="field-input">
                <asp:TextBox CssClass="textbox" ID="tbxNumber" runat="server" Style="width: 200px;" MaxLength="15" />
                <asp:RequiredFieldValidator ID="valCase" ControlToValidate="tbxNumber" Text="<br>Please provide a number" runat="server" />
                <asp:CustomValidator ID="valUnique" ErrorMessage="<br>This number already exists" runat="server" />
            </p>

            <p class="field-title">Notes</p>
            <p class="field-input">
                <asp:TextBox ID="tbxDescription" TextMode="multiline" Style="width: 100%; height: 150px;" CssClass="textbox" runat="server" /></p>

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
