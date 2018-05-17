<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="programDepartments.aspx.vb" Inherits="Foundation.WebAdmin.giving.programDepartments" %>

<%@ Register TagPrefix="cc" Namespace="Core" Assembly="Core" %>

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


        <asp:Panel ID="pnlForm" CssClass="panel-form" runat="server">

            <p>
                <cc:DataCheckboxList ID="cblDepartments" runat="server" /></p>

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
