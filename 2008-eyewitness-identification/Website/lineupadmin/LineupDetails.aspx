<%@ Page Language="c#" CodeBehind="LineupDetails.aspx.cs" AutoEventWireup="false" Inherits="Ei.Website.lineupadmin.lineupDetails" %>

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

        <p>
            <table cellpadding="3" cellspacing="0" border="1" style="background-color: #f6f6f6; border: 1px #cacaca solid; border-collapse: collapse;">
                <tr>
                    <asp:Literal ID="ltrLineup" runat="server" /></tr>
            </table>
            <p>
                <table cellpadding="2" cellspacing="0">
                    <tr>
                        <td align="right" style="font-weight: bold; color: #414141;">Description:</td>
                        <td>
                            <asp:Literal ID="ltrDescription" runat="server" /></td>
                    </tr>
                    <tr>
                        <td align="right" style="font-weight: bold; color: #414141;">Case:</td>
                        <td>
                            <asp:Literal ID="ltrCase" runat="server" /></td>
                    </tr>
                    <tr>
                        <td align="right" style="font-weight: bold; color: #414141;">Suspect:</td>
                        <td>
                            <asp:Literal ID="ltrSuspect" runat="server" /></td>
                    </tr>
                    <tr valign="top">
                        <td align="right" style="font-weight: bold; color: #414141;">Notes:</td>
                        <td>
                            <asp:Literal ID="ltrNotes" runat="server" /></td>
                    </tr>
                </table>




                <!-----------------end of content----------------->
                <asp:Literal ID="ltrLayout2" runat="server" />
    </form>
</body>
</html>
