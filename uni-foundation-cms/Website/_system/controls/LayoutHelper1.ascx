<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="LayoutHelper1.ascx.vb" Inherits="Foundation.WebMain.LayoutHelper1" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>

<div id="d00">

    <table align="center" cellpadding="0" cellspacing="0" style="width: 872px; margin-top: 0px;" border="0">
        <tr valign="top">
            <td colspan="2" style="padding-top: 15px;"><a href="http://www.uni-foundation.org">
                <img src="_system/images/layout/unilogo.jpg" border="0"></a></td>
            <td>
                <div id="d01">
                    <div id="d02">
                        <a href="http://www.uni.edu" class="toplink">UNI Home</a>&nbsp;&nbsp;
					<a href="http://www.uni-foundation.org" class="toplink">Foundation Home</a>
                    </div>
                    <div id="d03">
                        <asp:TextBox ID="tbxSearch" runat="server" CssClass="searchbox" /></div>
                    <div id="d04">
                        <asp:Button ID="btnSearch" runat="server" CssClass="searchbutton" Text="Search" /></div>
                </div>
            </td>
        </tr>
        <tr valign="top">
            <td style="width: 168px;">
                <asp:Literal ID="ltrHeaderImageLeft" runat="server" /></td>
            <td style="width: 520px; background-color: #cacaca;">
                <asp:Literal ID="ltrHeaderImageRight" runat="server" /></td>
            <td style="width: 168px; padding-left: 8px; padding-top: 15px; padding-right: 8px;">
                <p class="quote">
                    <asp:Literal ID="ltrQuote" runat="server" /></p>
                <p class="quote-signature">
                    <asp:Literal ID="ltrQuoteSig" runat="server" /></p>
            </td>
        </tr>
        <tr valign="top">
            <td style="background-color: #e8e8e8; border: solid 1px #cacaca; border-top: solid 1px #e8e8e8;">
                <asp:Literal ID="ltrMainMenu" runat="server" />
                <img src="_system/images/layout/mainmenugradient.jpg">
            </td>
            <td style="background-color: white; padding-left: 20px; padding-right: 20px; padding-top: 5px; padding-bottom: 20px; border-bottom: solid 1px #cacaca;">
                <div class="contextmenu-div">
                    <asp:Literal ID="ltrContextMenu" runat="server" /></div>
                <h3>
                    <asp:Literal ID="ltrBodyTitle" runat="server" /></h3>
                <asp:Literal ID="ltrBody" runat="server" />
            