<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="default.aspx.vb" Inherits="Foundation.WebAdmin._default" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title id="ctlTitleTag" runat="server" />
    <link href="_system/styles/main.css" type="text/css" rel="stylesheet">
    <script language="javascript" type="text/javascript" src="_system/javascript/menuhelper.js"></script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <asp:Literal ID="ltrLayout1" runat="server" />
        <!---------------------content-------------------->

        <p>
            Welcome back,
            <asp:Literal ID="ltrUserName" runat="server" />
        ! 
			
			<p>
                Please use the menu on the left to navigate the system.
			
			
				
				<asp:Panel Visible="false" ID="pnlLoad" runat="server" Style="text-align: center; border: 2px solid red; padding: 20px; background: pink;">
                    <p>
                        You must press this button before you can operate the site
					<p>
                        <asp:Button CssClass="button" Text="Load System" ID="btnLoad" runat="server" Style="width: 180px; height: 30px; font-weight: bold; font-size: 16px;" /></p>
                </asp:Panel>





                <!-----------------end of content----------------->
                <asp:Literal ID="ltrLayout2" runat="server" />
    </form>
</body>
</html>
