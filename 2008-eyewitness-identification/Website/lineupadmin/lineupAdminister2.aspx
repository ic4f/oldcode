<%@ Page Language="c#" CodeBehind="lineupAdminister2.aspx.cs" AutoEventWireup="false" Inherits="Ei.Website.lineupadmin.lineupAdminister2" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title id="ctlTitleTag" runat="server" />
    <link href="../_system/styles/main.css" type="text/css" rel="stylesheet">
    <script language="javascript" type="text/javascript" src="../_system/javascript/menuhelper.js"></script>
    <script language="javascript" type="text/javascript" src="../_system/javascript/utils.js"></script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <asp:Literal ID="ltrLayout1" runat="server" />
        <!---------------------content-------------------->

        <p>
            <div style="padding: 5px; border: red solid 1px; background-color: pink;">
                <b>Warning:</b> The witness should not see this page. If the witness views this page, it could contaminate his or her memory for the actual lineup.
            </div>

            <p>
                You have selected the following lineup: 
			
			<p>
                <table cellpadding="3" cellspacing="0" border="1" style="background-color: #f6f6f6; border: 1px #cacaca solid; border-collapse: collapse;">
                    <tr>
                        <asp:Literal ID="ltrLineup" runat="server" /></tr>
                </table>

                <p>
                    To change this selection, <a href="lineupAdminister.aspx">return to previous screen</a>
        .
			
			<p>
                <div style="padding: 5px; border: green solid 1px; background-color: lightgreen;">
                    <p>
                        To proceed with administering this lineup, please click the link below, which will open a new window in your browser. 
				The lineup results will not be recorded until the lineup administration is completed.
				<p align="center"><b>
                    <asp:Literal ID="ltrStart" runat="server" /></b></p>
                </div>

                <script language="javascript" type="text/javascript">
                    function open_win(lineupId) {
                        window.open("popupLineupAdmin.aspx?Id=" + lineupId, "_blank", "fullscreen")
                    }
                </script>




                <!-----------------end of content----------------->
                <asp:Literal ID="ltrLayout2" runat="server" />
    </form>
</body>
</html>
