<%@ Page Language="c#" CodeBehind="popupLineupAdmin.aspx.cs" AutoEventWireup="false" Inherits="Ei.Website.lineupadmin.popupLineupAdmin" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Lineup Administration</title>
    <link href="../_system/styles/popup.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="Form1" method="post" runat="server">



        <asp:Panel ID="pnlBeginLineup" runat="server">
            <p>
                Please enter your name below:
				<p>
                    <table cellpadding="3">
                        <tr>
                            <td>First name:</td>
                            <td>
                                <asp:TextBox ID="tbxFirstName" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>Last name:</td>
                            <td>
                                <asp:TextBox ID="tbxLastName" runat="server" /></td>
                        </tr>
                    </table>
                    <p>
                        <asp:RequiredFieldValidator ID="valFirstName" runat="server" ErrorMessage="Please type in your first name" ControlToValidate="tbxFirstName" Display="Dynamic" maxlength="25" />
            <p>
                <asp:RequiredFieldValidator ID="valLastName" runat="server" ErrorMessage="Please type in your last name" ControlToValidate="tbxLastName" Display="Dynamic" maxlength="25" />
            <p>
                <asp:Button class="button" ID="btnBeginLineup" runat="server" Text="Begin Lineup" />
        </asp:Panel>



        <asp:Panel ID="pnlInstructions" runat="server" Visible="false">
            <p style="font-size: 16px; color: #333333; font-weight: bold;">
                Instructions</b>
				<p>
                    In a moment, you will be shown a series of photos. The person who committed the crime may or may not be included in the lineup. The person administering the lineup does not know whether the person being investigated is included. Even if you identify someone during this procedure the computer will continue to show you all photos in the series.
				<p>
                    Keep in mind that things like hair styles, beards and mustaches can be easily changed and the complexion colors may look slightly different in photographs.
				<p>
                    You should not feel that you have to make an identification. It is as important to exclude innocent persons as it is to identify the perpetrator.
				<p>
                    The photos will be shown to you one at a time and are not in any particular order. Take as much time as you need to look at each one. After each photo, you will be asked “Is this the person you saw?” If you answer yes, you will be asked to describe in your own words how certain you are.
				<p>
                    Because you are involved in an ongoing investigation, in order to prevent damaging the investigation, you should avoid discussing this identification procedure or its results.
				<p>
                    Do you understand the way the photo array procedure will be conducted and the other instructions you have been given?
				<br>
                    <br>
                    <p align="center">
                        <asp:Button ID="btnUnderstand" runat="server" Text="I understand the procedure and the instructions" CssClass="continuebutton" />
                        &nbsp;&nbsp;&nbsp;&nbsp;	
				<asp:Button ID="btnNotUnderstand" runat="server" Text="I DO NOT understand the procedure and the instructions" CssClass="terminatebutton" />
        </asp:Panel>



        <asp:Panel ID="pnlPhotoView" runat="server" Visible="false">
            <p>
                Please examine the photo below. When you are done looking at the photo, click on the button under the photo. 
					You will then have an opportunity to indicate if the photo is the perpetrator.
				<p align="center">
                    <asp:Label ID="lblPhoto" runat="server" />
            <p align="center">
                <asp:Button ID="btnFinishedLooking" runat="server" Text="I am finished looking at this photo" CssClass="button" />
        </asp:Panel>



        <asp:Panel ID="pnlPhotoViewResult" runat="server" Visible="false">
            <p align="center">
                Was that the person you witnessed?
				<p align="center">
                    <asp:Button ID="btnYes" runat="server" Text="YES" CssClass="button" />
                    <asp:Button ID="btnNotSure" runat="server" Text="NOT SURE" CssClass="button" />
                    <asp:Button ID="btnNo" runat="server" Text="NO" CssClass="button" />
        </asp:Panel>



        <asp:Panel ID="pnlIdentificationConfirmation" runat="server" Visible="false">
            <p>
                In your own words, can you describe how certain you are? Please type your responses below:
				<p>
                    <asp:TextBox ID="tbxIdentificationConfirmation" TextMode="multiline" runat="server" Columns="80" Rows="15" />
                    <asp:RequiredFieldValidator ID="valIdentificationConfirmation" runat="server" ErrorMessage="Please fill in the text box above" ControlToValidate="tbxIdentificationConfirmation" Display="Dynamic" />
            <p>
                <asp:Button ID="btnIdentificationConfirmation" runat="server" Text="Next" CssClass="button" />
        </asp:Panel>



        <asp:Panel ID="pnlContinueLineup" runat="server" Visible="false">
            <p>
                The lineup procedure requires that we continue to show you the remainder of the photos in case you change your mind
				<p align="center">
                    <asp:Button ID="btnContinueLineup" runat="server" Text="Continue Lineup Procedure" CssClass="button" />
        </asp:Panel>



        <asp:Panel ID="pnlFinalComments" runat="server" Visible="false">
            <p>
                You have completed the lineup. if you chose someone from the lineup procedure, what special factors supported your identification (ex., facial features, hair, marks, etc.)?
				<p>
                    <asp:TextBox ID="tbxFinalComments" TextMode="multiline" runat="server" Columns="80" Rows="15" />
                    <asp:RequiredFieldValidator ID="valFinalComments" runat="server" ErrorMessage="Please fill in the text box above" ControlToValidate="tbxFinalComments" Display="Dynamic" />
            <p>
                <asp:Button ID="btnFinalComments" runat="server" Text="Next" CssClass="button" />
        </asp:Panel>



        <asp:Panel ID="pnlFinishLineup" runat="server" Visible="false">
            <p>
                Please let the lineup administrator know that you have finished. Please remember that the lineup administrator does not know who the suspect is or what position he was in.
				<p align="center">
                    <asp:Button ID="btnFinishLineup" runat="server" Text="Exit" CssClass="button" />
        </asp:Panel>

        <asp:Label ID="lblLineupViewId" runat="server" Visible="false" />
        <asp:Label ID="lblCurrentPhotoIndex" runat="server" Visible="false" Text="0" />
        <asp:Label ID="lblCurrentPhotoId" runat="server" Visible="false" />
        <asp:Label ID="lblCurrentPhotoIsSuspect" runat="server" Visible="false" Text="false" />
        <asp:Label ID="lblSuspectIsProcessed" runat="server" Visible="false" Text="false" />

    </form>
</body>
</html>
