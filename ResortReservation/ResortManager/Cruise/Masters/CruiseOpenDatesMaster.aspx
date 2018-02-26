<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CruiseOpenDatesMaster.aspx.cs" Inherits="Cruise_Masters_CruiseOpenDatesMaster" %>

<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
      <script  type="text/javascript" src="../js/calendar/calendar.js"></script>
    <script  type="text/javascript" src="../js/calendar/calendar-en.js"></script>
    <script type="text/javascript" src="../js/calendar/calendar-setup.js"></script>
        <script  type="text/javascript" src="../js/global.js"></script>
    <script  type="text/javascript" src="../js/client/viewbookings.js"></script>
    <link rel="stylesheet" type="text/css" media="all" href="../../css/pageheader.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../../style.css" />
    <link href="../../css/roleRights.css" rel="stylesheet" type="text/css" />
       <link rel="stylesheet" type="text/css" media="all" href="../css/calendar-blue2.css"
    <title>Open Dates Master</title>

    <script type="text/javascript">
        function dateSelectionChanged(sender, args) {
            selectedDate = sender.get_selectedDate();
            var hdnfNoOfRooms = document.getElementById("hdnfNoOfRooms").value;
            convertdate(selectedDate, hdnfNoOfRooms)
        }

        function convertdate(selecteddate, nofnights) {

            var today = new Date();
            var seld = new Date(selecteddate);
            var a = seld.getDate();
            var b = nofnights;
            var dd = parseInt(a) + parseInt(b);
            var result = new Date();
            result.setDate(result.getDate(selecteddate) + nofnights);
            var mm = selecteddate.getMonth() + 1;
            var yyyy = selecteddate.getFullYear();
            var today = mm + '/' + dd + '/' + yyyy;
            document.getElementById("txtDeBordingDate").value = result.getDates;
        }



    </script>
    <style>
        .controlsCss {
            Width: 161px;
        }

        .auto-style1 {
            height: 24px;
        }

        .auto-style2 {
            height: 19px;
        }
    </style>


</head>
<body>
    <form id="form1" runat="server">
        <phc:PageHeaderControl ID="pageheader1" runat="server" PageTitle="Cruise Open Departure Dates" />
        <div>
            <asp:ScriptManager ID="scmgrLocation" runat="server">
            </asp:ScriptManager>
             <br />
             <table>
                    <tr>
                       
                        <td>Start Date:
                        </td>
                        <td>
                            <asp:TextBox ID="txtfrom" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtfrom" runat="server" />
                        </td>
                        <td>End Date:
                        </td>
                        <td>
                            <asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtTo" runat="server" />
                        </td>
                         <td>Packages:
                        </td>
                        <td>
                            <asp:DropDownList ID="drppackaging" runat="server"></asp:DropDownList>
                        </td>
                        <td>Close/Open:
                        </td>
                        <td>
                              <asp:DropDownList ID="drpcloseopen" runat="server">
                                        <asp:ListItem Value="">All</asp:ListItem>
                                        <asp:ListItem Value="false">Open</asp:ListItem>
                                        <asp:ListItem Value="true">Close</asp:ListItem>
                                      
                                    </asp:DropDownList>
                        </td>
                       <td>   </td> <td>  </td> <td></td> <td></td>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" />
                        </td>
                    </tr>
                    
                </table>
            <br />
            <asp:UpdatePanel ID="updatepanel1" runat="server">
                <ContentTemplate>


                    



                    <div id="gridsection" class="gridsection">
                        <asp:GridView ID="GridOpenDates" ForeColor="#333333" AutoGenerateColumns="False" BorderStyle="Ridge" runat="server" Width="964px" OnRowDeleting="GridOpenDates_RowDeleting1" DataKeyNames="Id" OnRowCommand="GridOpenDates_RowCommand">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <AlternatingRowStyle BackColor="White" />
                            <RowStyle BackColor="#EFF3FB" HorizontalAlign="Left" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sn.">
                                    <ItemTemplate><%#Container.DataItemIndex+1 %> </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Package Name">
                                    <ItemTemplate>

                                        <asp:Label runat="server" ID="lblPckName" Text='<%#Eval("PackageName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="AccomName">
                                    <ItemTemplate>

                                        <asp:Label runat="server" ID="lbAccomName" Text='<%#Eval("AccomName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="River">
                                    <ItemTemplate>

                                        <asp:Label runat="server" ID="lbRiverName" Text='<%#Eval("RiverName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Country">
                                    <ItemTemplate>

                                        <asp:Label runat="server" ID="lbCountryName" Text='<%#Eval("CountryName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Boarding Loc.">
                                    <ItemTemplate>

                                        <asp:Label runat="server" ID="lbBoardingFrom" Text='<%#Eval("BoardingFrom") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Deboarding Loc.">
                                    <ItemTemplate>

                                        <asp:Label runat="server" ID="lbBoardingTo" Text='<%#Eval("BoardingTo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CheckInDate" HeaderText="Boarding Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                <asp:BoundField DataField="CheckOutDate" HeaderText="Deboarding Date" DataFormatString="{0:dd-MMM-yyyy}" />

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Select">Edit</asp:LinkButton>
                                        <asp:LinkButton ID="lnkDelete" runat="server" OnClientClick="return confirm('Are you certain you want to delete this departure?')" CommandName="Delete">Delete</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnlOpenclose" Text='<%#Eval("Openclose") %>' runat="server" CommandName="Open" CommandArgument='<%#Eval("Id") %>'> </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <table>
                        <tr>
                            <td>Select Accom.</td>
                            <td>
                                <asp:DropDownList ID="ddlAccom" CssClass="controlsCss" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="reqfddlAccom" runat="server" ErrorMessage="*" ControlToValidate="ddlAccom" InitialValue="-Select-" ForeColor="Red" ValidationGroup="Open"></asp:RequiredFieldValidator>
                            </td>
                            <td>&nbsp;</td>

                        </tr>
                        <tr>
                            <td class="auto-style1">Country</td>
                            <td class="auto-style1">
                                <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" CssClass="controlsCss">
                                </asp:DropDownList>
                            </td>
                            <td class="auto-style1">
                                <asp:RequiredFieldValidator ID="reqfddlCountry" runat="server" ControlToValidate="ddlCountry" ErrorMessage="*" ForeColor="Red" InitialValue="-Select-" ValidationGroup="Open"></asp:RequiredFieldValidator>
                            </td>
                            <td class="auto-style1"></td>
                        </tr>
                        <tr>
                            <td>River</td>
                            <td>
                                <asp:DropDownList ID="ddlRiver" CssClass="controlsCss" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="reqfddlRiver" runat="server" ControlToValidate="ddlRiver" ErrorMessage="*" ForeColor="Red" InitialValue="-Select-" ValidationGroup="Open"></asp:RequiredFieldValidator>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>Select package</td>
                            <td>
                                <asp:DropDownList ID="ddlpackage" CssClass="controlsCss" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpackage_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="reqfddlpackage" runat="server" ErrorMessage="*" ControlToValidate="ddlpackage" InitialValue="-Select-" ForeColor="Red" ValidationGroup="Open"></asp:RequiredFieldValidator>

                                <asp:HiddenField runat="server" ID="hdnfNoOfRooms" />

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>CheckIn Date</td>
                            <td>
                                <asp:TextBox ID="txtBoardingDate" CssClass="controlsCss" runat="server" Width="83px" AutoPostBack="true" OnTextChanged="txtBoardingDate_TextChanged"></asp:TextBox>
                                <asp:CalendarExtender ID="txtBoardingDate_CalendarExtender" runat="server" TargetControlID="txtBoardingDate"></asp:CalendarExtender>
                                <br />

                                <asp:RegularExpressionValidator ID="regtxtBoardingDate"
                                    ValidationExpression="^([0-9]|0[1-9]|1[012])\/([0-9]|0[1-9]|[12][0-9]|3[01])\/(19|20)\d\d$"
                                    ControlToValidate="txtBoardingDate" ErrorMessage="Invalid Format. Use MM/DD/YYYY" runat="server"
                                    CssClass="colorred" ValidationGroup="Open"></asp:RegularExpressionValidator>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="reqftxtBoardingDate" runat="server" ErrorMessage="*" ControlToValidate="txtBoardingDate" ForeColor="Red" ValidationGroup="Open"></asp:RequiredFieldValidator>
                                <asp:HiddenField ID="hfid" runat="server" />
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>CheckOut Date</td>
                            <td>
                                <asp:TextBox ID="txtDeBordingDate" CssClass="controlsCss" runat="server" Width="83px" Height="19px" Enabled="False"></asp:TextBox>
                                <asp:CalendarExtender ID="txtDeBordingDate_CalendarExtender" runat="server" TargetControlID="txtDeBordingDate"></asp:CalendarExtender>
                                <br />
                                <asp:RegularExpressionValidator ID="regtxtDeBordingDate"
                                    ValidationExpression="^([0-9]|0[1-9]|1[012])\/([0-9]|0[1-9]|[12][0-9]|3[01])\/(19|20)\d\d$"
                                    ControlToValidate="txtDeBordingDate" ErrorMessage="Invalid Format. Use MM/DD/YYYY" runat="server"
                                    CssClass="colorred" ValidationGroup="Open"></asp:RegularExpressionValidator>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="reqftxtDeBordingDate" runat="server" ErrorMessage="*" ControlToValidate="txtDeBordingDate" ForeColor="Red" ValidationGroup="Open"></asp:RequiredFieldValidator>
                                <br />
                                <%-- <asp:CompareValidator ID="cmprtxtDeBordingDate" runat="server" ControlToCompare="txtBoardingDate" ControlToValidate="txtDeBordingDate" ForeColor="Red" ErrorMessage="*Invalid Date Range(End Date should be greater)" Operator="GreaterThan" Type="Date" />--%>

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style2"></td>
                            <td class="auto-style2"></td>
                            <td class="auto-style2"></td>
                            <td class="auto-style2"></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btnAdd" runat="server" CssClass="appbutton" Height="24px" OnClick="btnAdd_Click" Text="Add" Width="65px" ValidationGroup="Open" />
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>
                                <asp:Button ID="btnAdd0" runat="server" CssClass="appbutton" CausesValidation="false" Height="24px" Text="Reload" Width="65px" OnClick="btnAdd0_Click" />
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>

                </ContentTemplate>

            </asp:UpdatePanel>

        </div>



        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="200">
        <ProgressTemplate>
            <iframe id="pgrIFrame" frameborder="0" marginheight="0" marginwidth="0" scrolling="no"
                src="javascript:'<html></html>';" style="position: absolute; top: 729px; left: 36px;
                height: 68px; width: 208px; z-index: 19999"></iframe>
            <asp:Panel ID="Panel1" runat="server" BackColor="white" BorderColor="#C2D3FC" BorderStyle="solid"
                BorderWidth="1" Height="100" Style="z-index: 20000" Width="300">
                <div style="position: relative; top: 20px; left: 70px;">
                    <asp:Image ID="image2" runat="server" ImageUrl="~/images/indicator.gif" />                    
                Please Wait....
            </asp:Panel>
        </ProgressTemplate>
    </asp:UpdateProgress>
           <asp:AlwaysVisibleControlExtender ID="AlwaysVisibleControlExtender1" runat="server"
        TargetControlID="Panel1" HorizontalOffset="300" VerticalOffset="150">
    </asp:AlwaysVisibleControlExtender>
    </form>
</body>
</html>
