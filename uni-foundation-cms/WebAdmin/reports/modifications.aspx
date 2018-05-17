<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="modifications.aspx.vb" Inherits="Foundation.WebAdmin.reports.modifications" %>

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


        <p>
            Select user:
            <asp:DropDownList CssClass="textbox" ID="ddlUser" runat="server" />

        <p>
            <asp:Panel ID="pnlGrig" runat="server">
                <table class="gridOuterTable" style="width: auto;" cellspacing="0" cellpadding="3">
                    <tr>
                        <td>
                            <cc:MyBaseGrid ID="dgrModifications" CssClass="gridTableData" runat="server">
                                <Columns>
                                    <asp:BoundColumn DataField="contenttype" HeaderText="Content Type">
                                        <ItemStyle Wrap="False" />
                                        <HeaderStyle Wrap="false" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="description" HeaderText="Description">
                                        <ItemStyle Wrap="False" />
                                        <HeaderStyle Wrap="false" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="modified" HeaderText="Modified">
                                        <ItemStyle Wrap="False" HorizontalAlign="center" />
                                        <HeaderStyle Wrap="false" />
                                    </asp:BoundColumn>
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
