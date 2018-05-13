<%@ Page Language="c#" CodeBehind="photoAdd.aspx.cs" AutoEventWireup="false" Inherits="Ei.Website.settings.photoAdd" %>

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
            To add photos to the system, you need to complete two steps: 
            <ol>
                <li><b>Add</b>
                photos to the database;
	<li><b>Categorize</b>
                the added photos by gender, race, hair color, age and weight ranges.
            </ol>


            <p>
                <div style="border: solid 1px #cacaca; padding: 5px; background-color: #f8f8f8;">
                    <b>To add photos to the database:</b>
                    <ol>
                        <li>Move the photo files to
                            <asp:Literal ID="ltrPhotoSourceDir" runat="server" />, located on the server;
	<li>
                        Run photoloader.exe from the command prompt (located on the server at [Your .Net projects directory]\ei\PhotoLoader\bin\Release\photoloader.exe);
	<li>
                        After running photoloader.exe, you may reload this page to see how many photos have been added.
                    </ol>
                </div>

                <p>
                    <div style="border: solid 1px #cacaca; padding: 5px; background-color: #f8f8f8;">
                        <b>To categorize photos:</b>
                        <blockquote>
                            <p>
                                There are currently <b>
                                    <asp:Literal ID="ltrUncatPhotos" runat="server" /></b>
                            uncategorized photos in the database. You may categorize them in sets of 10.
	<p>
        <asp:HyperLink ID="lnkNext10" runat="server" NavigateUrl="photoCategorize.aspx" Text="Categorize next 10 photos" />
                        </blockquote>
                    </div>











                    <!-----------------end of content----------------->
                    <asp:Literal ID="ltrLayout2" runat="server" />
    </form>
</body>
</html>
