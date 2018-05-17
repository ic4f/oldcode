<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="quoteEdit.aspx.vb" Inherits="Foundation.WebAdmin.contentlibrary.quoteEdit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title id="ctlTitleTag" runat="server" />
    <link href="../_system/styles/main.css" type="text/css" rel="stylesheet">
    <link href="../_system/styles/addeditpage.css" type="text/css" rel="stylesheet">
    <script language="javascript" type="text/javascript" src="../_system/javascript/menuhelper.js"></script>
    <script language="javascript" type="text/javascript" src="../_system/javascript/utils.js"></script>
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

            <p class="field-title">Author*</p>
            <p class="field-input">
                <asp:TextBox CssClass="textbox" ID="tbxAuthor" runat="server" Style="width: 250px;" MaxLength="25" />
                <asp:RequiredFieldValidator ID="val2" ErrorMessage="<br>Please provide the quote author" ControlToValidate="tbxAuthor" runat="server" />
            </p>

            <p class="field-title">Quote*</p>
            <p class="field-input">
                <asp:TextBox CssClass="textbox" ID="tbxQuote" runat="server" Style="width: 250px;" TextMode="MultiLine" Rows="4"
                    onKeyDown="countChars(tbxQuote, remLen1, 100)" onKeyUp="countChars(tbxQuote, remLen1, 100)" />
                <br>
                <input readonly type="text" name="remLen1" size="3" maxlength="4" value="100" style="border: 1px solid #414141; font-size: 11px;">
                <font color="#000000">characters remaining</font>
                <asp:RequiredFieldValidator ID="val3" ErrorMessage="<br>Please provide the quote text" ControlToValidate="tbxQuote" runat="server" />
            </p>

            <p class="field-title">Comments</p>
            <p class="field-input">
                <asp:TextBox ID="tbxComment" TextMode="multiline" Style="width: 100%; height: 150px;" CssClass="textbox" runat="server" /></p>

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
