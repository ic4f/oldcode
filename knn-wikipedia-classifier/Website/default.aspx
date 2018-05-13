<%@ Page Language="c#" CodeBehind="default.aspx.cs" AutoEventWireup="false" Inherits="DataMining.Website._default" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>default</title>
</head>
<body style="margin: 50px;">
    <form id="Form1" method="post" runat="server">


        <h2 align="center">K-NN/Wikipedia Project</h2>

        <h4>Size of index:</h4>
        <ul>
            <li>
            10.000 documents
			<li>
            77.721 terms
			<li>
            19.043 categories
        </ul>



        <h3>Search Demo</h3>

        <p>
            <asp:TextBox ID="tbxSearch" runat="server" />
            <asp:Button ID="btnSearch" runat="server" Text="Search" />
            <asp:Button ID="btnReset" runat="server" Text="Reset" />
        <p>
            <asp:Literal ID="ltrSearchResults" runat="server" />

            <h3>Similar Documents / Categories Demo</h3>

            <table cellpadding="2">
                <tr>
                    <td align="right">document:</td>
                    <td>
                        <asp:DropDownList ID="ddlDocs" runat="server" /></td>
                </tr>
                <tr>
                    <td align="right">k:</td>
                    <td>
                        <asp:TextBox ID="tbxK" runat="server" Columns="2" />
                        (compare to <b>k</b> nearest neighbors)</td>
                </tr>
                <tr>
                    <td align="right">c:</td>
                    <td>
                        <asp:TextBox ID="tbxC" runat="server" Columns="2" />
                        (select top <b>c</b> categories)</td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnGetSim" runat="server" Text="Find Similar Docs" />
                        <asp:Button ID="btnCats" runat="server" Text="Assign Categories" />
                        <asp:Button ID="btnResetSim" runat="server" Text="Reset" />
                    </td>
                </tr>
            </table>
            <p>
                <asp:Literal ID="ltrSimResults" runat="server" />
    </form>
</body>
</html>
