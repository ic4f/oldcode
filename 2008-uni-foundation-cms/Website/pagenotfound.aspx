<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="pagenotfound.aspx.vb" Inherits="Foundation.WebMain.pagenotfound" %>

<%@ Register TagPrefix="uc" TagName="layout1" Src="_system/controls/LayoutHelper1.ascx" %>
<%@ Register TagPrefix="uc" TagName="layout2" Src="_system/controls/LayoutHelper2.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>page not found</title>
    <link href="_system/styles/main.css" rel="stylesheet" type="text/css">
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <uc:layout1 ID="ucLayoutHelper1" runat="server" />

        <p>
            We are sorry, but the page you are looking for was not found. 

												
			<uc:layout2 ID="ucLayoutHelper2" runat="server" />
    </form>
    <script src="http://www.google-analytics.com/urchin.js" type="text/javascript">
    </script>
    <script type="text/javascript">
        _uacct = "UA-1066817-3";
        urchinTracker();
    </script>
</body>
</html>
