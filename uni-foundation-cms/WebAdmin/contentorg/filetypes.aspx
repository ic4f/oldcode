<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="filetypes.aspx.vb" Inherits="Foundation.WebAdmin.contentorg.filetypes" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title id="ctlTitleTag" runat="server" />
    <link href="../_system/styles/main.css" type="text/css" rel="stylesheet">
    <script language="javascript" type="text/javascript" src="../_system/javascript/menuhelper.js"></script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <asp:Literal ID="ltrLayout1" runat="server" />
        <!---------------------content-------------------->



        <asp:Panel ID="pnlView" runat="server" class="textcontentpanel">
            <p><a href="filetypeList.aspx">List of file types</a></p>
            <p class="commentunderlink">
                View, edit file type descriptions
					<br>
                <br>
                New file types are automatically added by uploading <a href="files.aspx">files</a>
            </p>
        </asp:Panel>




        <!-----------------end of content----------------->
        <asp:Literal ID="ltrLayout2" runat="server" />
    </form>
</body>
</html>
