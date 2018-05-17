<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="selectedPageModules.aspx.vb" Inherits="Foundation.WebAdmin.selectedPageModules" %>

<%@ Register TagPrefix="cc" Namespace="Core" Assembly="Core" %>
<html>
<head>
    <link href="../../_system/styles/datalist.css" type="text/css" rel="stylesheet">
</head>
<body bgcolor="#ffffff">
    <form id="Form2" method="post" runat="server">
        <div class="iframe">
            <cc:DataCheckboxList ID="cblModules" runat="server" Style="font-size: 11px;" />
        </div>
    </form>
</body>
</html>
