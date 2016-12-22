<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PackageMaster.aspx.cs" Inherits="Rate_PackageMaster" %>

<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>

<%@ Register assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Room Category Master</title>
    <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />
    <script language="javascript" type="text/javascript" src="../js/master/roomcategorymaster.js"></script>
    <style type="text/css">
        .auto-style1
        {
            width: 128px;
            height: 23px;
        }

        .auto-style3
        {
            width: 467px;
        }
        .auto-style4
        {
            height: 23px;
            width: 467px;
        }
    </style>

    <script type="text/javascript">

        function ShowImagePreview() {

            var preview = document.querySelector('#<%=Image1.ClientID %>');
            var file = document.querySelector('#<%=uploadLogo.ClientID %>').files[0];
            var reader = new FileReader();

            reader.onloadend = function () {
                preview.src = reader.result;
            }

            if (file) {
                reader.readAsDataURL(file);
            } else {
                preview.src = "";
            }


        }
      </script>
</head>
<body>
    <form id="form1" runat="server">
        <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="Package Master" />
        <br />
        <div>
            <asp:ScriptManager ID="scmgrMarketMaster" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="pnlMarketMaster" runat="server">
                <ContentTemplate>
                    <div id="gridsection" class="gridsection">
                        <asp:GridView ID="GridPackages" runat="server" CellPadding="4" ForeColor="#333333" Width="883px" AutoGenerateColumns="False" DataKeyNames="PackageId" OnRowDeleting="GridPackages_RowDeleting" OnRowCommand="GridPackages_RowCommand">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="PackageId" HeaderText="PackageId" />
                                <asp:BoundField DataField="PackageName" HeaderText="PackageName" />
                                <asp:BoundField DataField="NoOfNights" HeaderText="NoOfNights" />
                                <asp:BoundField DataField="PackageType" HeaderText="PackageType" />
                                <asp:BoundField DataField="MasterPackageId" HeaderText="MasterPackageId" />
                                <asp:BoundField DataField="MasterPackage" HeaderText="MasterPackage" />
                                <asp:BoundField DataField="BordingFrom" HeaderText="BordingFrom" />
                                <asp:BoundField DataField="BoadingTo" HeaderText="BoadingTo" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                       
                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Select">Edit</asp:LinkButton>
                                       
                                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete"  OnClientClick="return confirm('Are you certain you want to delete this Package?') " >Delete</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>
                    </div>
                    <div>
                    <table id="inputsection" class="inputsection" style="width:77%">
                        <tr>
                            <td style="width: 128px">Package Type</td>
                            <td class="auto-style3">
                                <asp:DropDownList ID="ddlPackageType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpackageType_selectChanged">
                                    <asp:ListItem>-Select-</asp:ListItem>
                                    <asp:ListItem>Master Package</asp:ListItem>
                                    <asp:ListItem>Child Package</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqfddlPackageType" runat="server" ControlToValidate="ddlPackageType" ErrorMessage="*" InitialValue="-Select-" ValidationGroup="valpackage"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 128px">Select Master Package</td>
                            <td class="auto-style3">
                                <asp:DropDownList ID="ddlMasterPackage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMasterPackage_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqfddlMasterPackage" runat="server" ControlToValidate="ddlMasterPackage" ErrorMessage="*" InitialValue="-Master Package-" ValidationGroup="valpackage"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 128px">Package Name</td>
                            <td class="auto-style3">
                                <asp:TextBox ID="txtPackageName" runat="server" CssClass="input" Height="48px" MaxLength="25" TextMode="MultiLine" Width="240px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqftxtPackageName" runat="server" ControlToValidate="txtPackageName" ErrorMessage="*" ValidationGroup="valpackage"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 128px">Description</td>
                            <td class="auto-style3">
                                <asp:TextBox ID="txtPackageDesc" runat="server" TextMode="MultiLine" Width="311px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 128px">Itinerary Link</td>
                            <td class="auto-style3">
                                <asp:TextBox ID="txtItineraryLink" runat="server" Width="322px"></asp:TextBox>
                            </td>
                        </tr>
                    
                        <tr>
                            <td style="width: 128px">Bording From</td>
                            <td class="auto-style3">
                                <asp:DropDownList ID="ddlBoardingFrom" runat="server"  >
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqfddlBoardingFrom" runat="server" ControlToValidate="ddlBoardingFrom" ErrorMessage="*" InitialValue="-Select-" ValidationGroup="valpackage"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 128px">Boarding To</td>
                            <td class="auto-style3">
                                <asp:DropDownList ID="ddlBoardingTo" runat="server" OnSelectedIndexChanged="ddlNights_changeEvent">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqfddlBoardingTo" runat="server" ControlToValidate="ddlBoardingTo" ErrorMessage="*" InitialValue="-Select-" ValidationGroup="valpackage"></asp:RequiredFieldValidator>
<asp:CompareValidator ID="cmprValtxtvalto" runat="server" ControlToCompare="ddlBoardingFrom" ControlToValidate="ddlBoardingTo" ForeColor="Red" ErrorMessage="*Both Locations cant be the same." Operator="NotEqual" Type="String" ValidationGroup="valpackage" />                            </td>
                        </tr>
                        <tr>
                            <td style="width: 128px">Select Hotel</td>
                            <td class="auto-style3">
                                <asp:DropDownList ID="ddlHotel" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqfddlHotel" runat="server" ControlToValidate="ddlHotel" ErrorMessage="*" InitialValue="-Select-" ValidationGroup="valpackage"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">No. of nights</td>
                            <td class="auto-style4">
                                <asp:DropDownList ID="ddlnights" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlNights_changeEvent" style="margin-left: 0px">
                                    <asp:ListItem>-Select-</asp:ListItem>
                                    <asp:ListItem>1</asp:ListItem>

                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem>4</asp:ListItem>
                                    <asp:ListItem>5</asp:ListItem>
                                    <asp:ListItem>6</asp:ListItem>
                                    <asp:ListItem>7</asp:ListItem>
                                    <asp:ListItem>8</asp:ListItem>
                                    <asp:ListItem>9</asp:ListItem>
                                    <asp:ListItem>10</asp:ListItem>


                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqfddlnights" runat="server" ControlToValidate="ddlnights" InitialValue="-Select-" ErrorMessage="*" ValidationGroup="valpackage"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                            <tr>
            <td>Package Image</td>
            <td> <input type="file" cssclass="appbutton" runat="server" id="uploadLogo" onchange="ShowImagePreview()"  />
   <asp:Image ID="Image1" runat="server" Width="128" Height="123" /></td>

        </tr>


                        <tr>
                            <td class="auto-style1">City on each night</td>
                            <td class="auto-style4">&nbsp;</td>
                        </tr>
                    </table>
                        </div>

                          <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="352px">
                            <Columns>
                                <asp:BoundField DataField="Night" HeaderText="Night"></asp:BoundField>
                                <asp:BoundField DataField="LocationName" HeaderText="City"></asp:BoundField>
                            </Columns>
                           <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" HorizontalAlign="Left" />
                        </asp:GridView>

                    <table id="tblcityEachNight">

                        <tr>
                            <td>
                                <asp:GridView ID="GridCityEachNight" runat="server" AutoGenerateColumns="false" CellPadding="4" ForeColor="#333333" GridLines="Both" Width="660px" OnRowDataBound="GridNights_RowDataBound">

                                    <AlternatingRowStyle BackColor="White" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                    <Columns>

                                        <asp:TemplateField HeaderText="Sn.">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lbSn" Text='<%#Eval("Sn") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nights">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lbNights" Text='<%#Eval("Nights") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="City on each night">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlcity" runat="server"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="reqfddlcity" runat="server" ControlToValidate="ddlcity" ErrorMessage="*" InitialValue="-Select City-" ValidationGroup="valpackage"></asp:RequiredFieldValidator>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="CheckIn">
                                            <ItemTemplate>
                                               <asp:RadioButton  runat="server" ID="rbCheckInYes" Text="Yes" GroupName="checkIn"/>&nbsp;&nbsp;<asp:RadioButton  runat="server" ID="rbcheckInNo" Text="No" Checked="true" GroupName="checkIn"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="CheckOut">
                                            <ItemTemplate>
                                               <asp:RadioButton  runat="server" ID="rbCheckOutYes" Text="Yes" GroupName="checkOut"/>&nbsp;&nbsp;<asp:RadioButton  runat="server" ID="rbcheckOutNo" Text="No" Checked="true" GroupName="checkOut"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>

                    

                    <table id="buttonsection" class="buttonsection">
                        <tr>
                            <td style="width: 74px; height: 26px">
                                <asp:Button CssClass="appbutton" ID="btnSbmit" runat="server" ValidationGroup="valpackage" Height="24px" Text="Submit"
                                    Width="65px" OnClick="btnSbmit_Click" /></td>
                            <td style="width: 74px; height: 26px">
                                <asp:Button CssClass="appbutton" ID="btnCancel" runat="server" Text="Cancel"
                                    Width="65px" OnClick="btnCancel_Click" /></td>
                        </tr>
                    </table>
                    <table id="statussection" class="statussection">
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                    <table id="hiddensection" class="hiddensection">
                        <tr>
                            <td>
                                <asp:HiddenField ID="hfId" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSbmit" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>

