<%@ Page Language="c#" CodeBehind="about.aspx.cs" AutoEventWireup="false" Inherits="IrProject.Website.about" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
<head>
    <title>Giggle Research : About this Project</title>
    <link href="_system/styles/main.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="Form1" method="post" runat="server">

        <table width="100%" cellpadding="0" cellsapcing="0" style="border-bottom: 1px #cacaca solid; margin-bottom: 0px;">
            <tr>
                <td width="150px"><a href="default.aspx">
                    <img src="_system/images/giggles.gif" border="0"></a></td>
                <td align="center">
                    <h3>About this Project</h3>
                </td>
                <td width="150px"></td>
        </table>
        <p align="right" style="margin: 0px;"><a href="default.aspx">Home</a></p>

        <p>
            This is a research project in information science by <a href="http://www.lordofthewebs.com">Sergei Golitsinski</a>
        . 
        <p>
            I will add a full description with all the source code as time permits. 


            <h4>PageRank</h4>

            <p>
                Here's the formula for including PageRank in the cos calculation:
                <pre>
	similarity = cos;
	if (cos > 0)
		similarity = w * cos + (1.0 - w) * pageRank;
</pre>


    </form>
</body>
</html>
