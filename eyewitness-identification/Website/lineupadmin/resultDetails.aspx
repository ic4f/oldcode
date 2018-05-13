<%@ Page Language="c#" CodeBehind="resultDetails.aspx.cs" AutoEventWireup="false" Inherits="Ei.Website.lineupadmin.resultDetails" %>

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
            <table cellpadding="2" cellspacing="0">
                <tr>
                    <td align="right" style="font-weight: bold; color: #414141;">Witness:</td>
                    <td>
                        <asp:Literal ID="ltrWitness" runat="server" /></td>
                </tr>
                <tr>
                    <td align="right" style="font-weight: bold; color: #414141;">Administered:</td>
                    <td>
                        <asp:Literal ID="ltrAdministered" runat="server" /></td>
                </tr>
                <tr valign="top">
                    <td align="right" style="font-weight: bold; color: #414141;">Relevance Notes:</td>
                    <td>
                        <asp:Literal ID="ltrRelevanceNotes" runat="server" /></td>
                </tr>
            </table>

            <p>
                <asp:Repeater ID="rptResults" runat="server">
                    <ItemTemplate>
                        <p>
                            <table cellpadding="2" cellspacing="0" border="1" class="photoviews_table">
                                <tr valign="top">
                                    <td width="96">
                                        <asp:Literal ID="ltrPhoto" runat="server" /></td>
                                    <td>
                                        <table cellpadding="2" cellspacing="0">
                                            <tr>
                                                <td align="right" style="font-weight: bold; color: #414141;">Photo Id:</td>
                                                <td>
                                                    <asp:Literal ID="ltrPhotoRef" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="font-weight: bold; color: #414141;">Result:</td>
                                                <td>
                                                    <asp:Literal ID="ltrResult" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="font-weight: bold; color: #414141;">Certainty:</td>
                                                <td>
                                                    <asp:Literal ID="ltrCertainty" runat="server" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                    </ItemTemplate>
                </asp:Repeater>




                <!-----------------end of content----------------->
                <asp:Literal ID="ltrLayout2" runat="server" />
    </form>
</body>
</html>
