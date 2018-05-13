<%@ Page Language="c#" CodeBehind="suspectAdd.aspx.cs" AutoEventWireup="false" Inherits="Ei.Website.operations.suspectAdd" %>

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
                <asp:Button ID="btnAddAnother" Text="Add Another" CssClass="button" runat="server" />
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

            <p class="field-title">Photo*</p>
            <p class="field-input">
                <input type="file" id="tbxFile" runat="server" name="tbxFile" />
                <asp:RequiredFieldValidator ID="valFile" runat="server" ErrorMessage="Please select a file" ControlToValidate="tbxFile" />
                <asp:CustomValidator ID="valFileType" Text="This is not an image file" runat="server" />
                <asp:Literal ID="ltrTypes" runat="server" />
            </p>

            <p class="field-title">Gender*</p>
            <p class="field-input">
                <asp:DropDownList ID="ddlGender" CssClass="textbox" runat="server" />
                <asp:RequiredFieldValidator ID="Requiredfieldvalidator1" ControlToValidate="ddlGender" Text="<br>Please select a gender" runat="server" />
            </p>

            <p class="field-title">Race*</p>
            <p class="field-input">
                <asp:DropDownList ID="ddlRace" CssClass="textbox" runat="server" />
                <asp:RequiredFieldValidator ID="Requiredfieldvalidator2" ControlToValidate="ddlRace" Text="<br>Please select a race" runat="server" />
            </p>

            <p class="field-title">Hair Color*</p>
            <p class="field-input">
                <asp:DropDownList ID="ddlHairColor" CssClass="textbox" runat="server" />
                <asp:RequiredFieldValidator ID="Requiredfieldvalidator3" ControlToValidate="ddlHairColor" Text="<br>Please select a hair color" runat="server" />
            </p>

            <p class="field-title">Age Range*</p>
            <p class="field-input">
                <asp:DropDownList ID="ddlAgeRange" CssClass="textbox" runat="server" />
                <asp:RequiredFieldValidator ID="Requiredfieldvalidator4" ControlToValidate="ddlAgeRange" Text="<br>Please select an age range" runat="server" />
            </p>

            <p class="field-title">Weight Range*</p>
            <p class="field-input">
                <asp:DropDownList ID="ddlWeightRange" CssClass="textbox" runat="server" />
                <asp:RequiredFieldValidator ID="Requiredfieldvalidator5" ControlToValidate="ddlWeightRange" Text="<br>Please select a weight range" runat="server" />
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
