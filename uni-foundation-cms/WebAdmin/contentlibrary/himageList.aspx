<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="himageList.aspx.vb" Inherits="Foundation.WebAdmin.contentlibrary.himageList" %>

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


        <asp:Panel ID="pnlGrid" runat="server">
            <table class="gridOuterTable" style="width: auto;" cellspacing="0" cellpadding="3">
                <tr>
                    <td>
                        <cc:MyBaseGrid ID="dgrImages" CssClass="gridTableData" DataKeyField="Id" runat="server">
                            <Columns>
                                <asp:TemplateColumn HeaderText="Preview">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lnkPreview" runat="server" /></ItemTemplate>
                                    <HeaderStyle Wrap="false" />
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="Created" DataFormatString="{0:d}" HeaderText="Created">
                                    <ItemStyle Wrap="False" HorizontalAlign="center" />
                                    <HeaderStyle Wrap="false" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="displayedname" SortExpression="u.displayedname" HeaderText="Created by">
                                    <ItemStyle Wrap="false" />
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:ButtonColumn Text="delete" CommandName="Delete">
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" Width="60" />
                                </asp:ButtonColumn>
                            </Columns>
                        </cc:MyBaseGrid>
                    </td>
                </tr>
            </table>
        </asp:Panel>



        <!-----------------end of content----------------->
        <asp:Literal ID="ltrLayout2" runat="server" />
    </form>
</body>
</html>
