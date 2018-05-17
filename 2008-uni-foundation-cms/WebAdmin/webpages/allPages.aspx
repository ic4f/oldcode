<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="allPages.aspx.vb" Inherits="Foundation.WebAdmin.webpages.allPages" %>

<%@ Register TagPrefix="cc" Namespace="Core" Assembly="Core" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title id="ctlTitleTag" runat="server" />
    <link href="../_system/styles/main.css" type="text/css" rel="stylesheet">
    <script language="javascript" type="text/javascript" src="../_system/javascript/menuhelper.js"></script>
    <script language="javascript" type="text/javascript" src="../_gridhelper/gridscripts.js"></script>
    <link href="../_gridhelper/grid.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <asp:Literal ID="ltrLayout1" runat="server" />
        <!---------------------content-------------------->


        <table style="border: 1px #cacaca solid; background-color: #f1f1f1; font-size: 11px;" cellspacing="0" cellpadding="3">
            <tr>
                <td align="right">Type:</td>
                <td>
                    <asp:DropDownList ID="ddlType" CssClass="textbox" Style="width: 200px;" runat="server" /></td>
            </tr>
            <tr>
                <td align="right">Status:</td>
                <td>
                    <asp:DropDownList ID="ddlStatus" CssClass="textbox" Style="width: 200px;" runat="server" /></td>
            </tr>
            <tr>
                <td align="right">Label:</td>
                <td>
                    <asp:DropDownList ID="ddlLabel" CssClass="textbox" Style="width: 200px;" runat="server" /></td>
            </tr>
            <tr>
                <td align="right">Tag:</td>
                <td>
                    <asp:DropDownList ID="ddlTag" CssClass="textbox" Style="width: 200px;" runat="server" /></td>
            </tr>
            <tr>
                <td align="right">Sidebar Module:</td>
                <td>
                    <asp:DropDownList ID="ddlModule" CssClass="textbox" Style="width: 200px;" runat="server" /></td>
            </tr>
            <tr>
                <td align="right">Keyword:</td>
                <td>
                    <asp:TextBox ID="tbxKeyword" CssClass="textbox" Style="width: 200px;" MaxLength="25" runat="server" /></td>
            </tr>
            <tr valign="top">
                <td align="right">Menu:</td>
                <td>
                    <div style="font-size: 11px; background-color: #f8f8f8; border: solid 1px #cacaca; padding: 15px; padding-top: 0px; width: auto;">
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




        <p style="font-size: 11px;">Displaying
            <asp:Literal ID="ltrCount" runat="server" />
            pages</p>

        <asp:Panel ID="pnlGrid" runat="server">
            <table class="gridOuterTable" style="width: auto;" cellspacing="0" cellpadding="3">
                <tr>
                    <td>
                        <cc:MySortGrid ID="dgrPages" CssClass="gridTableData" DataKeyField="Id" runat="server">
                            <Columns>
                                <asp:BoundColumn DataField="id" SortExpression="p.id" HeaderText="Id" />
                                <asp:BoundColumn DataField="pagetitle" SortExpression="p.pagetitle" HeaderText="Page Title">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="menutext" SortExpression="m.text" HeaderText="Menu">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="text" SortExpression="pc.text" HeaderText="Type">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ispublished" SortExpression="p.ispublished" HeaderText="Published">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Created" SortExpression="p.Created" HeaderText="Created" DataFormatString="{0: MM/dd/yyyy}">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Modified" SortExpression="p.Modified" HeaderText="Modified" DataFormatString="{0: MM/dd/yyyy}">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="displayedname" SortExpression="p.ModifiedBy" HeaderText="Modified by">
                                    <ItemStyle Wrap="false" />
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundColumn>
                                <asp:HyperLinkColumn Text="info"
                                    DataNavigateUrlField="Id" DataNavigateUrlFormatString="pageInfo.aspx?Id={0}">
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                </asp:HyperLinkColumn>
                                <asp:HyperLinkColumn Text="edit/publish">
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                </asp:HyperLinkColumn>
                                <asp:BoundColumn DataField="id" />
                            </Columns>
                        </cc:MySortGrid>
                    </td>
                </tr>
            </table>
        </asp:Panel>

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


        <!-----------------end of content----------------->
        <asp:Literal ID="ltrLayout2" runat="server" />
    </form>
</body>
</html>
