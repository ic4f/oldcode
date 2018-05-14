<%@ Page Language="c#" CodeBehind="default.aspx.cs" AutoEventWireup="false" Inherits="IrProject.Website._default" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
<head>
    <title>Giggle Research : Home</title>
    <link href="_system/styles/main.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="Form1" method="post" runat="server">

        <center>
            <div style="width: 70%; text-align: left;">

                <p align="center">
                    <img src="_system/images/giggle.gif"></p>

                <p>
                    <b><a href="search.aspx">Search result ranking</a></b>
                    <br>
                    Experiment with PageRank and different term-weighing criteria, display top authorities with authority and hub scores

					<p>
                        <b><a href="websim.aspx">Web page similarity</a></b>
                        <br>
                        Find similar pages for any web page; the rest is same as above
					
					<p>
                        <b><a href="web.aspx">Web page content extraction</a></b>
                        <br>
                        Extract content and display term counts for any web page
					
					<p>
                        <b><a href="pages.aspx">Web pages</a></b>
                        <br>
                        Browse the entire network
					
					<p>
                        <b><a href="about.aspx">About this project</a></b>
                        <br>
                        Project description and source code
					
					<p>
                        <b>Index size:</b>
                        <br>
                        - Documents (web pages): 27,331
					<br>
                        - Terms: 92,963
					<br>
                        - Term-document records: 3,589,563
					<br>
                        - Links: 354,434						
					

            </div>
        </center>

    </form>
</body>
</html>
