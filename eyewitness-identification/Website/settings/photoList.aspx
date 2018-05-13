<%@ Page Language="c#" CodeBehind="photoList.aspx.cs" AutoEventWireup="false" Inherits="Ei.Website.settings.photoList" %>

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

        <table border="1" style="width: 100%; background-color: #f6f6f6; border: 1px #cacaca solid; font-size: 11px; border-collapse: collapse;" cellspacing="0" cellpadding="3">
            <tr style="background-color: #e4e4e4;">
                <td style="font-weight: bold; color: #414141; width: 20%;">Gender</td>
                <td style="font-weight: bold; color: #414141; width: 20%;">Race</td>
                <td style="font-weight: bold; color: #414141; width: 20%;">Hair Color</td>
                <td style="font-weight: bold; color: #414141; width: 20%;">Age Range</td>
                <td style="font-weight: bold; color: #414141; width: 20%;">Weight Range</td>
            </tr>
            <tr valign="top">
                <td>
                    <asp:CheckBoxList ID="cblGender" runat="server" Style="font-size: 11px;" /></td>
                <td>
                    <asp:CheckBoxList ID="cblRace" runat="server" Style="font-size: 11px;" /></td>
                <td>
                    <asp:CheckBoxList ID="cblHair" runat="server" Style="font-size: 11px;" /></td>
                <td>
                    <asp:CheckBoxList ID="cblAge" runat="server" Style="font-size: 11px;" /></td>
                <td>
                    <asp:CheckBoxList ID="cblWeight" runat="server" Style="font-size: 11px;" /></td>
            </tr>
            <tr>
                <td colspan="5" align="center">
                    <asp:Button ID="btnSearch" CssClass="button" Text="Find Photos" runat="server" /></td>
            </tr>
        </table>
        <p>
            <asp:Panel ID="pnlGrid" runat="server">
                <table class="gridOuterTable" style="width: 100%;" cellspacing="0" cellpadding="3">
                    <tr>
                        <td>
                            <cc:MyPager id="pager" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <cc:MyGrid id="dgrPhotos" cssclass="gridTableData" DataKeyField="Id" runat="server">
                                <columns>								
									<asp:BoundColumn DataField="id">																
										<headerstyle Wrap="False"/>
										<itemstyle width="96px"/>										
									</asp:boundcolumn>										
									<asp:BoundColumn DataField="id" SortExpression="p.id" HeaderText="Id#">																
										<headerstyle Wrap="False" horizontalalign="center"/>			
										<itemstyle horizontalalign="center"/>							
									</asp:boundcolumn>											
									<asp:BoundColumn DataField="externalref" SortExpression="p.externalref" HeaderText="Ref#">																
										<headerstyle Wrap="False" horizontalalign="center"/>
										<itemstyle horizontalalign="center"/>	
									</asp:boundcolumn>									
									<asp:BoundColumn DataField="gender" SortExpression="p.gender" HeaderText="Gender">																
										<headerstyle Wrap="False" horizontalalign="center"/>
										<itemstyle horizontalalign="center"/>	
									</asp:boundcolumn>									
									<asp:BoundColumn DataField="race" SortExpression="r.id" HeaderText="Race">																
										<headerstyle Wrap="False" horizontalalign="center"/>
										<itemstyle horizontalalign="center"/>	
									</asp:boundcolumn>									
									<asp:BoundColumn DataField="haircolor" SortExpression="h.id" HeaderText="Hair">																
										<headerstyle Wrap="False" horizontalalign="center"/>
										<itemstyle horizontalalign="center"/>	
									</asp:boundcolumn>									
									<asp:BoundColumn DataField="agerange" SortExpression="a.id" HeaderText="Age">																
										<headerstyle Wrap="False" horizontalalign="center"/>
										<itemstyle horizontalalign="center"/>	
									</asp:boundcolumn>
									<asp:BoundColumn DataField="weightrange" SortExpression="w.id" HeaderText="Weight">	
										<headerstyle Wrap="False" horizontalalign="center"/>
										<itemstyle horizontalalign="center"/>	
									</asp:boundcolumn>																																		
									<asp:BoundColumn DataField="Created" SortExpression="r.Created" HeaderText="Created" DataFormatString="{0: MM/dd/yyyy}">
										<headerstyle Wrap="False" horizontalalign="center"/>
										<itemstyle horizontalalign="center"/>	
									</asp:boundcolumn>									
									<asp:BoundColumn DataField="Modified" SortExpression="r.Modified" HeaderText="Modified" DataFormatString="{0: MM/dd/yyyy}">
										<headerstyle Wrap="False" horizontalalign="center"/>
										<itemstyle horizontalalign="center"/>	
									</asp:boundcolumn>									
									<asp:BoundColumn DataField="modifiedby" SortExpression="modifiedby" HeaderText="Modified by">
										<headerstyle Wrap="False" horizontalalign="center"/>
										<ItemStyle horizontalalign="center" wrap="false" />										
										</asp:BoundColumn>																																						
									<asp:HyperlinkColumn text="edit"
										DataNavigateUrlField="Id" DataNavigateUrlFormatString="photoEdit.aspx?Id={0}">
										<ItemStyle HorizontalAlign="Center" width="80"/>
									</asp:HyperlinkColumn>																						
								</columns>
                            </cc:MyGrid>
                        </td>
                    </tr>
                </table>
            </asp:Panel>



            <!-----------------end of content----------------->
            <asp:Literal ID="ltrLayout2" runat="server" />
    </form>
</body>
</html>
