<%@ Page Language="c#" CodeBehind="lineupAdd2.aspx.cs" AutoEventWireup="false" Inherits="Ei.Website.lineupadmin.lineupAdd2" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title id="ctlTitleTag" runat="server" />
    <link href="../_system/styles/main.css" type="text/css" rel="stylesheet">
    <script language="javascript" type="text/javascript" src="../_system/javascript/menuhelper.js"></script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <asp:Literal ID="ltrLayout1" runat="server" />
        <!---------------------content-------------------->

        <div style="background-color: #f6f6f6; border: 1px #cacaca solid; text-align: center; padding: 10px;">
            <asp:Button ID="btnSave" CssClass="button" Text="Save Lineup" Style="width: 150px; height: 30px; font-weight: bold;" runat="server" />
            <asp:Label ID="lblError" runat="server" Visible="false" Text="Your lineup must contain 6 photos" Style="color: red; font-weight: bold;" />
        </div>

        <p>
            Suspect photo position:
            <asp:DropDownList CssClass="textbox" ID="ddlPosition" runat="server" />
        <p>

            <table cellpadding="3" cellspacing="0" border="1" style="background-color: #f6f6f6; border: 1px #cacaca solid; border-collapse: collapse;">
                <tr>
                    <asp:Literal ID="ltrPositionCells" runat="server" /></tr>
            </table>

            <p>

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

                    <div style="background-color: #f6f6f6; border: 1px #cacaca solid; padding: 3px; font-size: 11px">
                        Photos to display:
                        <asp:DropDownList ID="ddlToDisplay" CssClass="textbox" runat="server" />
                        <span style="margin-left: 20px;">
                            <asp:Literal ID="ltrResults" runat="server" /></span>
                    </div>

                    <p>

                        <div style="background-color: #f6f6f6; border: 1px #cacaca solid;">
                            <asp:Literal ID="ltrPhotos" runat="server" />
                        </div>

                        <script>
                            function remove(pos)
                            {
                                document["pos" + pos].src = "../_system/images/layout/blank.gif";	
                                var hid = document.getElementById("hdPos" + pos);
                                document.getElementById(hid.value).style.border = "1px #666666 solid";		
                                hid.value = -1;
                            }

                            function add(id)
                            {		
                                var hid0 = document.getElementById("hdPos0");
                                var hid1 = document.getElementById("hdPos1");
                                var hid2 = document.getElementById("hdPos2");
                                var hid3 = document.getElementById("hdPos3");
                                var hid4 = document.getElementById("hdPos4");
                                var hid5 = document.getElementById("hdPos5");
                                var newSrc = "<asp:literal id="ltrJsHelper1" runat="server"/>" + id + "s.jpg";	
                                var emptySrc = "<asp:literal id="ltrJsHelper2" runat="server"/>";	
			
                                if (document["pos0"] && document["pos0"].src == emptySrc)
                                {
                                    document["pos0"].src = newSrc;	
                                    hid0.value = id;
                                    document.getElementById(id).style.border = "5px green solid";
                                }		
                                else if (document["pos1"] && document["pos1"].src == emptySrc)
                                {
                                    document["pos1"].src = newSrc;
                                    hid1.value = id;
                                    document.getElementById(id).style.border = "5px green solid";
                                }
                                else if (document["pos2"] && document["pos2"].src == emptySrc)
                                {
                                    document["pos2"].src = newSrc;	
                                    hid2.value = id;
                                    document.getElementById(id).style.border = "5px green solid";
                                }
                                else if (document["pos3"] && document["pos3"].src == emptySrc)
                                {
                                    document["pos3"].src = newSrc;	
                                    hid3.value = id;
                                    document.getElementById(id).style.border = "5px green solid";
                                }
                                else if (document["pos4"] && document["pos4"].src == emptySrc)
                                {
                                    document["pos4"].src = newSrc;	
                                    hid4.value = id;
                                    document.getElementById(id).style.border = "5px green solid";
                                }		
                                else if (document["pos5"] && document["pos5"].src == emptySrc)
                                {
                                    document["pos5"].src = newSrc;	
                                    hid5.value = id;
                                    document.getElementById(id).style.border = "5px green solid";
                                }
                            }
                        </script>

                        <input type="hidden" id="hdPos0" name="hdPos0" runat="server" value="-1" />
                        <input type="hidden" id="hdPos1" name="hdPos1" runat="server" value="-1" />
                        <input type="hidden" id="hdPos2" name="hdPos2" runat="server" value="-1" />
                        <input type="hidden" id="hdPos3" name="hdPos3" runat="server" value="-1" />
                        <input type="hidden" id="hdPos4" name="hdPos4" runat="server" value="-1" />
                        <input type="hidden" id="hdPos5" name="hdPos5" runat="server" value="-1" />



                        <!-----------------end of content----------------->
                        <asp:Literal ID="ltrLayout2" runat="server" />
    </form>
</body>
</html>
