<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="fileList.aspx.vb" Inherits="Foundation.WebAdmin.contentlibrary.fileList" %>

<%@ Register TagPrefix="cc" Namespace="Core" Assembly="Core" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title id="ctlTitleTag" runat="server" />
    <link href="../_system/styles/main.css" type="text/css" rel="stylesheet">
    <script language="javascript" type="text/javascript" src="../_system/javascript/menuhelper.js"></script>
    <link href="../_gridhelper/grid.css" type="text/css" rel="stylesheet">
    <script language="javascript" type="text/javascript" src="../_gridhelper/gridscripts.js"></script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <asp:Literal ID="ltrLayout1" runat="server" />
        <!---------------------content-------------------->


        <table style="border: 1px #cacaca solid; background-color: #e4e4e4; font-size: 11px;" cellspacing="0" cellpadding="3">
            <tr>
                <td align="right">Label:</td>
                <td>
                    <asp:DropDownList ID="ddlLabel" CssClass="textbox" Style="width: 200px;" runat="server" /></td>
            </tr>
            <tr>
                <td align="right">Type:</td>
                <td>
                    <asp:DropDownList ID="ddlType" CssClass="textbox" Style="width: 200px;" runat="server" /></td>
            </tr>
            <tr>
                <td align="right">Keyword:</td>
                <td>
                    <asp:TextBox ID="tbxKeyword" CssClass="textbox" Style="width: 200px;" MaxLength="250" runat="server" /></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="btnSearch" CssClass="button" Text="Find Files" runat="server" /></td>
            </tr>
        </table>

        <p style="font-size: 11px;">Displaying
            <asp:Literal ID="ltrCount" runat="server" />
            files</p>

        <asp:Panel ID="pnlGrid" runat="server">
            <table class="gridOuterTable" style="width: auto;" cellspacing="0" cellpadding="3">
                <tr>
                    <td>
                        <cc:MySortGrid ID="dgrFiles" CssClass="gridTableData" DataKeyField="Id" runat="server">
                            <Columns>
                                <asp:BoundColumn DataField="id" SortExpression="f.id" HeaderText="Id" />
                                <asp:TemplateColumn SortExpression="f.Id" HeaderText="File">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFile" runat="server" /></ItemTemplate>
                                    <ItemStyle Wrap="False" />
                                    <HeaderStyle Wrap="false" />
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="Description" SortExpression="f.Description" HeaderText="Description">
                                    <HeaderStyle Wrap="false" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Type" SortExpression="Type" HeaderText="Type">
                                    <ItemStyle Wrap="False" />
                                    <HeaderStyle Wrap="false" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="FileSize" DataFormatString="{0} KB" SortExpression="f.FileSize" HeaderText="Size">
                                    <ItemStyle Wrap="False" HorizontalAlign="right" />
                                    <HeaderStyle Wrap="false" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Created" DataFormatString="{0:d}" SortExpression="f.Created" HeaderText="Created">
                                    <ItemStyle Wrap="False" HorizontalAlign="center" />
                                    <HeaderStyle Wrap="false" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Modified" DataFormatString="{0:d}" SortExpression="f.Modified" HeaderText="Modified">
                                    <ItemStyle Wrap="False" HorizontalAlign="center" />
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="displayedname" SortExpression="u.displayedname" HeaderText="Modified by">
                                    <ItemStyle Wrap="false" />
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:HyperLinkColumn Text="info/delete"
                                    DataNavigateUrlField="Id" DataNavigateUrlFormatString="myFileInfo.aspx?Id={0}">
                                    <ItemStyle HorizontalAlign="Center" Width="60" />
                                </asp:HyperLinkColumn>
                                <asp:HyperLinkColumn Text="edit/replace"
                                    DataNavigateUrlField="Id" DataNavigateUrlFormatString="fileEdit.aspx?Id={0}">
                                    <ItemStyle HorizontalAlign="Center" Width="60" />
                                </asp:HyperLinkColumn>
                            </Columns>
                        </cc:MySortGrid>
                    </td>
                </tr>
            </table>
        </asp:Panel>



        <!-----------------end of content----------------->
        <asp:Literal ID="ltrLayout2" runat="server" />
    </form>
</body>
</html>
