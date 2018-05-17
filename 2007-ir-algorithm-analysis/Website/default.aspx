<%@ Page Language="c#" CodeBehind="default.aspx.cs" AutoEventWireup="false" Inherits="Giggle.Website._default" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
<head>
    <title>Giggle Search</title>
    <link rel="SHORTCUT ICON" href="favicon3.ico">
</head>
<body style="margin: 20px; font-family: Verdana; font-size: 10pt;">
    <form id="Form1" method="post" runat="server">

        <center>

            <img src="giggle.gif">
            <table>
                <tr>
                    <td>
                        <asp:TextBox Columns="60" ID="tbxQuery" runat="server" /></td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button Text="Giggle Search" ID="btnSearch" runat="server" />
                        <asp:Button Text="Giggle Authorities" ID="btnAuthorities" runat="server" />
                        <asp:Button Text="Giggle Cluster" ID="btnCluster" runat="server" /></td>
                </tr>
            </table>

        </center>

        <p>
            <table width="100%" cellpadding="10" style="font-family: Verdana; font-size: 10pt;">
                <tr valign="top">
                    <td width="200px">
                        <div style="border: 1px #999999 solid; width: 170px; padding: 0px 10px 0px 10px;">

                            <p style="margin-bottom: -15px;"><b>Select index:</b></p>
                            <br>
                            <asp:RadioButton AutoPostBack="true" ID="radLIndex" runat="server" GroupName="index" />
                            Lucene 
							<br>
                            <asp:RadioButton AutoPostBack="true" ID="radGIndex" runat="server" GroupName="index" />
                            Giggle

							<p style="margin-bottom: -15px;"><b>Apply term weights:</b></p>
                            <br>
                            <asp:CheckBox ID="none1" Enabled="false" Checked="true" runat="server" />text = 1 (hardcoded)
							<br>
                            <asp:CheckBox ID="cbxBold" runat="server" />bold tag = 2
							<br>
                            <asp:CheckBox ID="cbxHeading" runat="server" />heading tag = 3
							<br>
                            <asp:CheckBox ID="cbxTitle" runat="server" />title tag = 4
							<br>
                            <asp:CheckBox ID="cbxUrl" runat="server" />URL = 5		

							<p style="margin-bottom: -12px;"><b>PageRank weights:</b></p>
                            <br>
                            <asp:TextBox Columns="1" ID="none2" runat="server" Enabled="false" Text="0.6" />
                            c (hardcoded)
							<br>
                            <asp:TextBox Columns="1" ID="tbxPagerank" runat="server" />
                            w

							<p style="margin-bottom: -12px;"><b>Authorities/hubs:</b></p>
                            <br>
                            <asp:TextBox Columns="1" ID="tbxRoot" runat="server" />
                            root set size
							<br>
                            <asp:TextBox Columns="1" ID="tbxChildren" runat="server" />
                            maximum parents 
							<br>
                            <asp:TextBox Columns="1" ID="tbxParents" runat="server" />
                            maximum children

							<p style="margin-bottom: -12px;"><b>Clustering:</b></p>
                            <br>
                            <asp:RadioButton ID="radKmeans" runat="server" GroupName="cluster" />
                            K-means 
							<br>
                            <asp:RadioButton ID="radBuckshot" runat="server" GroupName="cluster" />
                            Buckshot 
							<br>
                            <asp:RadioButton ID="radBisect" runat="server" GroupName="cluster" />
                            Bisecting
							<p>
                                <asp:TextBox Columns="1" ID="tbxKmeans" runat="server" />
                                k in k-means
							<br>
                                <asp:TextBox Columns="1" ID="tbxDocs" runat="server" />
                            docs to cluster
							
							<p style="margin-bottom: -15px;"><b>Display:</b></p>
                            <br>
                            <asp:TextBox Columns="1" ID="tbxDisplay" runat="server" />
                            results
							
							<p align="center">
                                <asp:Button Text="Reset to Defaults" ID="btnReset" runat="server" />
                            <p>


                            <p>
                        </div>
                    </td>
                    <td>
                        <asp:Literal ID="ltrResults" runat="server" />

                    </td>
                </tr>
            </table>

    </form>
</body>
</html>
