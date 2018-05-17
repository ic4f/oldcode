<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="menuEdit.aspx.vb" Inherits="Foundation.WebAdmin.websiteorg.menuEdit" %>

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
            <p class="field-title">Text*</p>
            <p class="field-input">
                <asp:TextBox CssClass="textbox" ID="tbxText" runat="server" Style="width: 300px;" MaxLength="50" />
                <asp:RequiredFieldValidator ID="valText" ErrorMessage="<br>Please provide the item text" ControlToValidate="tbxText" runat="server" />
                <asp:CustomValidator ID="valUnique" ErrorMessage="<br>This item exists: please choose a different text" runat="server" />
            </p>

            <p class="field-title">Parent Menu*</p>
            <div style="padding: 0 10 10 10; background-color: #ffffff; border: solid #cacaca 1px;">
                <p style="margin-left: -7px;">
                    <asp:Literal ID="ltrTopLevel" runat="server" /></p>
                <p>
                    <asp:Literal ID="ltrTreeDisplay" runat="server" /></p>
            </div>

            <p class="field-title">Default Page</p>
            <p class="field-input">
                <asp:DropDownList ID="ddlPages" runat="server" /></p>

            <p class="field-title">Left Header Image</p>
            <p class="field-input">
                <asp:DropDownList ID="ddlImage" runat="server" /></p>

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
