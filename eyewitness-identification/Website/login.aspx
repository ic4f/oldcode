<%@ Page Language="c#" CodeBehind="login.aspx.cs" AutoEventWireup="false" Inherits="Ei.Website.login" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>UNI Eyewitness identification System Login</title>
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
