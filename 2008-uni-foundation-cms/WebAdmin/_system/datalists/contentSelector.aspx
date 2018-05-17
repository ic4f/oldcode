<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="contentSelector.aspx.vb" Inherits="Foundation.WebAdmin.contentSelector" %>

<%@ Register TagPrefix="cc" Namespace="Core" Assembly="Core" %>
<html>
<head>
    <link href="../../_system/styles/datalist.css" type="text/css" rel="stylesheet">
    <link href="../../_gridhelper/grid.css" type="text/css" rel="stylesheet">
    <script language="javascript" type="text/javascript" src="../../_gridhelper/gridscripts.js"></script>
    <script language="javascript" type="text/javascript" src="../../_system/javascript/menuhelper.js"></script>
</head>
<body bgcolor="#ffffff">
    <form id="Form2" method="post" runat="server">

        <asp:Button ID="btnImages" Text="Show Images" CssClass="button" runat="server" />
        <asp:Button ID="btnFiles" Text="Show Files" CssClass="button" runat="server" />
        <asp:Button ID="btnPages" Text="Show Pages" CssClass="button" runat="server" />



        <p>
            <asp:Panel ID="pnlImages" runat="server" Visible="false">
                <asp:Panel ID="pnlImageSearch" runat="server">
                    <table style="border: 1px #cacaca solid; background-color: #e4e4e4; font-size: 11px;" cellspacing="0" cellpadding="3">
                        <tr>
                            <td align="right">Label:</td>
                            <td>
                                <asp:DropDownList ID="ddlImageLabel" CssClass="textbox" Style="width: 200px;" runat="server" /></td>
                        </tr>
                        <tr>
                            <td align="right">Keyword:</td>
                            <td>
                                <asp:TextBox ID="tbxImageKeyword" CssClass="textbox" Style="width: 200px;" MaxLength="250" runat="server" /></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btnImageSearch" CssClass="button" Text="Find Images" runat="server" />
                                <asp:Button ID="btnImageReset" CssClass="button" Text="Reset" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:LinkButton ID="lnkShowImageOptions" Text="Show image search options" Visible="False" runat="server" Style="font-size: 11px;" />
                <p>
                    <asp:Panel ID="pnlImageGrid" runat="server" Visible="false">
                        <cc:MySortGrid ID="dgrImages" CssClass="gridTableData" DataKeyField="Id" runat="server">
                            <Columns>
                                <asp:BoundColumn DataField="id" SortExpression="f.id" HeaderText="Id" />
                                <asp:TemplateColumn HeaderText="Preview">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPreview" runat="server" /></ItemTemplate>
                                    <HeaderStyle Wrap="false" />
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="Id" SortExpression="f.Id" HeaderText="Link">
                                    <ItemStyle Wrap="False" />
                                    <HeaderStyle Wrap="false" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Description" SortExpression="f.Description" HeaderText="Description">
                                    <HeaderStyle Wrap="false" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Dimensions" SortExpression="f.imagewidth" HeaderText="Dimensions">
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" />
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
                            </Columns>
                        </cc:MySortGrid>
                    </asp:Panel>
            </asp:Panel>



            <asp:Panel ID="pnlFiles" runat="server" Visible="false">
                <asp:Panel ID="pnlFileSearch" runat="server">
                    <table style="border: 1px #cacaca solid; background-color: #e4e4e4; font-size: 11px;" cellspacing="0" cellpadding="3">
                        <tr>
                            <td align="right">Label:</td>
                            <td>
                                <asp:DropDownList ID="ddlFileLabel" CssClass="textbox" Style="width: 200px;" runat="server" /></td>
                        </tr>
                        <tr>
                            <td align="right">Type:</td>
                            <td>
                                <asp:DropDownList ID="ddlFileType" CssClass="textbox" Style="width: 200px;" runat="server" /></td>
                        </tr>
                        <tr>
                            <td align="right">Keyword:</td>
                            <td>
                                <asp:TextBox ID="tbxFileKeyword" CssClass="textbox" Style="width: 200px;" MaxLength="250" runat="server" /></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btnFileSearch" CssClass="button" Text="Find Files" runat="server" />
                                <asp:Button ID="btnFileReset" CssClass="button" Text="Reset" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:LinkButton ID="lnkShowFileOptions" Text="Show file search options" Visible="False" runat="server" Style="font-size: 11px;" />
                <p>
                    <asp:Panel ID="pnlFileGrid" runat="server" Visible="false">
                        <table class="gridOuterTable" style="width: auto;" cellspacing="0" cellpadding="3">
                            <tr>
                                <td>
                                    <cc:MySortGrid ID="dgrFiles" CssClass="gridTableData" DataKeyField="Id" runat="server">
                                        <Columns>
                                            <asp:BoundColumn DataField="id" SortExpression="f.id" HeaderText="Id" />
                                            <asp:BoundColumn DataField="Id" SortExpression="f.Id" HeaderText="Link">
                                                <ItemStyle Wrap="False" />
                                                <HeaderStyle Wrap="false" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="Description" SortExpression="f.Description" HeaderText="Description">
                                                <HeaderStyle Wrap="false" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="Type" SortExpression="f.extension" HeaderText="Type">
                                                <ItemStyle Wrap="False" />
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
                                        </Columns>
                                    </cc:MySortGrid>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
            </asp:Panel>

            <p>
                <asp:Panel ID="pnlPages" runat="server" Visible="false">
                    <asp:Panel ID="pnlPageSearch" runat="server">
                        <table style="border: 1px #cacaca solid; background-color: #f1f1f1; font-size: 11px;" cellspacing="0" cellpadding="3">
                            <tr>
                                <td align="right">Label:</td>
                                <td>
                                    <asp:DropDownList ID="ddlPageLabel" CssClass="textbox" Style="width: 200px;" runat="server" /></td>
                            </tr>
                            <tr>
                                <td align="right">Keyword:</td>
                                <td>
                                    <asp:TextBox ID="tbxPageKeyword" CssClass="textbox" Style="width: 200px;" MaxLength="25" runat="server" /></td>
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
                                    <asp:Button ID="btnPageSearch" CssClass="button" Text="Find Pages" runat="server" />
                                    <asp:Button ID="btnPageReset" CssClass="button" Text="Reset" runat="server" /></td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:LinkButton ID="lnkShowPageOptions" Text="Show page search options" Visible="False" runat="server" Style="font-size: 11px;" />
                    <p>
                        <asp:Panel ID="pnlPageGrid" runat="server" Visible="false">
                            <cc:MySortGrid ID="dgrPages" CssClass="gridTableData" DataKeyField="Id" runat="server">
                                <Columns>
                                    <asp:BoundColumn DataField="id" SortExpression="p.id" HeaderText="Id" />
                                    <asp:BoundColumn DataField="id" SortExpression="p.id" HeaderText="Link">
                                        <ItemStyle Wrap="False" />
                                        <HeaderStyle Wrap="false" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="pagetitle" SortExpression="p.pagetitle" HeaderText="Title">
                                        <ItemStyle Wrap="False" />
                                        <HeaderStyle Wrap="false" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="pagecategory" SortExpression="p.pagecategory" HeaderText="Type">
                                        <ItemStyle Wrap="False" />
                                        <HeaderStyle Wrap="false" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="menutext" SortExpression="m.text" HeaderText="Menu">
                                        <ItemStyle Wrap="False" />
                                        <HeaderStyle Wrap="false" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="Created" SortExpression="p.Created" HeaderText="Created" DataFormatString="{0: MM/dd/yyyy}">
                                        <ItemStyle Wrap="False" HorizontalAlign="center" />
                                        <HeaderStyle Wrap="false" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="Modified" SortExpression="p.Modified" HeaderText="Modified" DataFormatString="{0: MM/dd/yyyy}">
                                        <ItemStyle Wrap="False" HorizontalAlign="center" />
                                        <HeaderStyle Wrap="false" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="id" HeaderText="Preview">
                                        <ItemStyle Wrap="False" HorizontalAlign="center" />
                                        <HeaderStyle Wrap="false" />
                                    </asp:BoundColumn>
                                </Columns>
                            </cc:MySortGrid>
                        </asp:Panel>
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


    </form>
</body>
</html>
