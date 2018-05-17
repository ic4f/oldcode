<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="selectedPageTags.aspx.vb" Inherits="Foundation.WebAdmin.selectedPageTags" %>

<%@ Register TagPrefix="cc" Namespace="Core" Assembly="Core" %>
<html>
<head>
    <link href="../../_system/styles/datalist.css" type="text/css" rel="stylesheet">
</head>
<body bgcolor="#ffffff">
    <form id="Form2" method="post" runat="server">
        <div class="iframe">
            <cc:DataCheckboxList ID="cblTags" runat="server" Style="font-size: 11px;" />
        </div>
    </form>
</body>
</html>
