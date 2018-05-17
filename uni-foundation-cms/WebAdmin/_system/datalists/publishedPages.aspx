<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="publishedPages.aspx.vb" Inherits="Foundation.WebAdmin.publishedPages" %>

<html>
<head>
    <link href="../../_system/styles/datalist.css" type="text/css" rel="stylesheet">
    <script language="javascript" type="text/javascript" src="../../_system/javascript/menuhelper.js"></script>
</head>
<body bgcolor="#ffffff">
    <form id="Form2" method="post" runat="server">
        <div class="iframe">

            <table style="width: 100%; border: 1px #cacaca solid; background-color: #f1f1f1; font-size: 11px;" cellspacing="0" cellpadding="3">
                <tr>
                    <td align="right">Page Type:</td>
                    <td>
                        <asp:DropDownList ID="ddlType" CssClass="textbox" Style="width: 200px;" runat="server" /></td>
                </tr>
                <tr>
                    <td align="right">Label:</td>
                    <td>
                        <asp:DropDownList ID="ddlLabel" CssClass="textbox" Style="width: 200px;" runat="server" /></td>
                </tr>
                <tr valign="top">
                    <td align="right">Menu:</td>
                    <td>
                        <div style="font-size: 11px; background-color: #f8f8f8; border: solid 1px #cacaca; padding: 15px; padding-top: 0px; width: 167px;">
                            <asp:Literal ID="ltrTreeDisplay" runat="server" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnSearch" CssClass="button" Text="Find Web Pages" runat="server" />
                        <asp:Button ID="btnReset" CssClass="button" Text="Reset" runat="server" /></td>
                </tr>
            </table>
            <p>
                <table style="width: 100%; border: 1px #cacaca solid; background-color: #f8f8f8; font-size: 11px;" cellspacing="0" cellpadding="3">
                    <tr>
                        <td>
                            <asp:Button ID="btnClear" CssClass="button" Text="Clear Selection" runat="server" Visible="false" />
                            <asp:RadioButtonList ID="radPages" runat="server" Style="font-size: 11px;" /></td>
                    </tr>
                </table>

                <input type="hidden" id="hidMenu" name="hidMenu" runat="server" value="-1" />
                <script>
                    function getMenuId() {
                        document.getElementById("hidMenu").value = "-1";
                        var sb = "";
                        var cf = document.forms[0];
                        for (var i = 0; i < cf.length; i++) {
                            if (cf.elements[i].name.indexOf("radmenu") > -1)
                                if (cf.elements[i].checked)
                                    document.getElementById("hidMenu").value = cf.elements[i].value;
                        }
                    }
                </script>

        </div>
    </form>
</body>
</html>
