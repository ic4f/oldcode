<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="login.aspx.vb" Inherits="Foundation.WebAdmin.login" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>UNI Foundation Content Management System Login</title>
    <meta name="title" content="Content Management System Sign-in" id="ctlTitle" runat="server" />
    <link href="_system/styles/main.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <asp:Literal ID="ltrLayout1" runat="server" />
        <!---------------------content-------------------->

        <p>
            <asp:Label ID="lblPasswordSent" runat="server" Visible="false" /></p>

        <asp:Panel ID="pnlLogin" runat="server">
            <table cellspacing="1" cellpadding="3" border="0">
                <tr>
                    <td>Login:</td>
                    <td>
                        <asp:TextBox class="textbox" Style="width: 200px;" ID="tbxLogin" runat="server" MaxLength="50" /></td>
                </tr>
                <tr>
                    <td>Password:</td>
                    <td>
                        <asp:TextBox class="textbox" Style="width: 200px;" ID="tbxPassword" runat="server" MaxLength="50" EnableViewState="False" TextMode="Password" /></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button class="button" ID="btnLogin" runat="server" Text="Login" /></td>
                </tr>
            </table>
            <p>
                <asp:Label ID="lblAuthFailure" runat="server" CssClass="loginValidation" /></p>
            <p>
                <asp:LinkButton ID="lnkHelp" runat="server" CausesValidation="False" Text="Forgot your password? Click here for help!" />
        </asp:Panel>

        <asp:Panel ID="pnlHelp" runat="server">
            <p>
                <asp:Label ID="lblHelp" runat="server" />
            <p><b>
                <asp:Label ID="lblEmailNotFound" runat="server" Visible="false" /></b></p>
            <p>You must be a registered user to use this system. Please type in your email address, which you use for this system, and your password will be emailed to you.</p>
            <p>
                <asp:TextBox class="textbox" Style="width: 200px;" ID="tbxHelp" runat="server" MaxLength="50" AutoPostBack="True" />&nbsp;
					<asp:Button class="button" ID="btnHelp" Text="Get Password" runat="server" />
                <asp:RequiredFieldValidator ID="valHelp" runat="server" ErrorMessage="Please submit your email" ControlToValidate="tbxHelp" />
            <p>
                <asp:LinkButton ID="lnkBack" runat="server" CausesValidation="False" Text="Back to sign-in" /></p>
        </asp:Panel>

        <p style="margin-top: 30px;">
            <div style="background-color: #f8f8f8; border: 1px solid #cacaca; color: #666666; font-size: 11px; padding: 10px; width: 600px;">
                <b>System recommendations</b><br>
                This system was designed to work best on any standards-compliant internet broser, like <a href="http://www.mozilla.com/firefox/">Firefox</a>,
			at a screen resolution of at least 1152 x 864.			
            </div>



            <!-----------------end of content----------------->
            <asp:Literal ID="ltrLayout2" runat="server" />
    </form>
</body>
</html>
