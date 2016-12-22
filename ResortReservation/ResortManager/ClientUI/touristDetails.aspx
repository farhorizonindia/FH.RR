<%@ Page Language="C#" AutoEventWireup="true" CodeFile="touristDetails.aspx.cs" Inherits="ClientUI_touristDetails" %>
<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Tourist Details</title>    
    <script language="javascript" type="text/javascript" src="../js/calendar/calendar.js"></script>
    <script language="javascript" type="text/javascript" src="../js/calendar/calendar-en.js"></script>
    <script language="javascript" type="text/javascript" src="../js/calendar/calendar-setup.js"></script>
    <script language="javascript" type="text/javascript" src="../js/popups.js"></script>
    <script language="javascript" type="text/javascript" src="../js/global.js"></script>
    <script language="javascript" type="text/javascript" src="../js/client/touristdetails.js"></script>
    
    <link rel="stylesheet" type="text/css" media="all" href="../css/pageheader.css" />    
    <link rel="stylesheet" type="text/css" media="all" href="../style.css" />    
    <link rel="stylesheet" type="text/css" media="all" href="../css/calendar-blue2.css" title="win2k-cold-1" /> 

</head>
<body>
    <form id="form1" runat="server">
    <phc:PageHeaderControl id="pageheader1" runat="server" PageTitle="Tourist Details" />
    <div>
    <asp:ScriptManager ID="scmgrtouristdetails" runat ="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="pnltouristdetails" runat="server">
    <ContentTemplate>  
        <div id="divMain">
        <div id="leftCol" style="float:left; background-color:lightblue; height:375px; margin-right:5px">      
            <table id="inputsectionLeft" class="inputsection" style="font-family: Verdana; font-size: x-small; padding-left:10px;" cellspacing="2">
                <tr>
                    <td class="labelcell">
                        Title:</td>
                    <td class="inputcell">
                        <asp:DropDownList cssclass="select" ID="ddlSuffix" runat="server" Width="70px">
                            <asp:ListItem Value="Choose"></asp:ListItem>
                            <asp:ListItem Value="Mr">Mr.</asp:ListItem>
                            <asp:ListItem Value="Ms">Ms.</asp:ListItem>
                            <asp:ListItem Value="Mrs">Mrs.</asp:ListItem>
                        </asp:DropDownList></td>
                    </tr>
                <tr>
                    <td class="labelcell">First Name:</td>
                    <td  class="inputcell">
                    <asp:TextBox cssclass="input"  ID="txtFName" runat="server" Width="140px" MaxLength="50"></asp:TextBox><span style="color: #ff0000">*</span>
                    </td>
                </tr>
                <tr>
                    <td class="labelcell">Middle Name:</td>
                    <td class="inputcell">
                        <asp:TextBox cssclass="input"  ID="txtMName" runat="server" Width="140px" MaxLength="50"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="labelcell">
                Last Name:</td>
                    <td class="inputcell">
                <asp:TextBox cssclass="input"  ID="txtLName" runat="server" Width="140px" MaxLength="50"></asp:TextBox></td>
                </tr>
                <tr>
            <td class="labelcell">
                Gender:</td>
            <td class="inputcell">
                <asp:DropDownList cssclass="select" ID="ddlGender" runat="server" >
                    <asp:ListItem Value="0">Choose</asp:ListItem>
                    <asp:ListItem Value="M">Male</asp:ListItem>
                    <asp:ListItem Value="F">Female</asp:ListItem>
                </asp:DropDownList><span style="color: #ff0000">*</span></td>
        </tr>
        <tr>
            <td class="labelcell">
                Date Of Birth:</td>
            <td class="inputcell">
            <asp:TextBox cssclass="input"  ID="txtDOB" runat="server" Width="109px" ></asp:TextBox><span style="color: #ff0000"></span><input type="button" class="datebutton" id="btnDOB" onclick="return setupCalendar('txtDOB','btnDOB')" onfocus="return setupCalendar('txtDOB','btnDOB')" value="..." /><span
                style="color: #ff0000">*</span>&nbsp;
            </td>
        </tr>
                <tr>
                    <td class="labelcell">
                        Nationality:</td>
                    <td class="inputcell">
                <asp:DropDownList cssclass="select" ID="ddlNationality" runat="server" Width="147px" >
                </asp:DropDownList>
            <asp:Button cssclass="appbutton" ID="btnSearchTourists" Text="Look Up" runat="server" OnClick="btnSearchTourists_Click" Visible="False" Width="46px" /></td>
                </tr>
                <tr>
                    <td class="labelcell">
                Place Of Birth:</td>
                    <td class="inputcell">
                <asp:TextBox cssclass="input"  ID="txtBirthPlace" runat="server" Width="146px" ></asp:TextBox></td>
                </tr>        
                <tr>
                    <td class="labelcell" style="width: 164px">
                Permanent Address In India:</td>
                    <td class="inputcell">
                <asp:TextBox cssclass="input"  ID="txtPermAdd" runat="server" Width="269px" ></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="labelcell" style="width: 164px">
                Special Message:</td>
                    <td class="inputcell">
                <asp:TextBox cssclass="input"  ID="txtMessage" TextMode="MultiLine" runat="server" Height="30px" Width="267px" ></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="labelcell" style="width: 164px">Email Id</td>
                     <td class="inputcell">

                          <asp:TextBox cssclass="input"  ID="txtEmailId" runat="server" Width="269px" ></asp:TextBox>
                     </td>


                </tr>
    </table>                       
        </div>
        <div id="rightCol" style="float:left; background-color:lightblue; height:375px;">
            <table id="inputsectionRight" class="inputsection" style="font-family: Verdana; font-size: x-small; padding-left:10px;" cellspacing="2">
                <tr>
                    <td class="labelcell">Passport Number:</td>
                    <td class="inputcell">
                        <asp:TextBox cssclass="input"  ID="txtPassportNo" runat="server" Width="140px" ></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="labelcell">
                Passport Issue Date:</td>
                    <td class="inputcell">
                        <asp:TextBox cssclass="input"  ID="txtPassportIssueDate" runat="server" Width="109px" ></asp:TextBox>
                        <input type="button" class="datebutton" id="btnPPIssueDt" onclick="return setupCalendar('txtPassportIssueDate','btnPPIssueDt')" onfocus="return setupCalendar('txtPassportIssueDate','btnPPIssueDt')" value="..." /></td>
                </tr>
                <tr>
                    <td class="labelcell">
                        Passport Expiry Date:</td>
                    <td class="inputcell">
                        <asp:TextBox cssclass="input"  ID="txtPassportExpiryDate" runat="server" Width="109px" ></asp:TextBox>
                        <input type="button" class="datebutton" id="btnPPExpiryDate" onclick="return setupCalendar('txtPassportExpiryDate','btnPPExpiryDate')" onfocus="return setupCalendar('txtPassportExpiryDate','btnPPExpiryDate')" value="..." /></td>
                </tr>
                <tr>
                    <td class="labelcell">
                        Visa No:</td>
                    <td class="inputcell">
                    <asp:TextBox cssclass="input"  ID="txtVisaNo" runat="server" Width="140px" ></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="labelcell">
                        Visa Expiry Date:</td>
                    <td class="inputcell">
                <asp:TextBox cssclass="input"  ID="txtVisaExpiryDate" runat="server" Width="109px" ></asp:TextBox>
                <input type="button" class="datebutton" id="btnVisaExpiryDate" onclick="return setupCalendar('txtVisaExpiryDate','btnVisaExpiryDate')" onfocus="return setupCalendar('txtVisaExpiryDate','btnVisaExpiryDate')" value="..." /></td>
                </tr>
                <tr>
                    <td class="labelcell">
                Date Of Entry In India:</td>
                    <td class="inputcell">
            <asp:TextBox cssclass="input"  ID="txtIndiaEntryDate" runat="server" Width="109px" ></asp:TextBox><input type="button" class="datebutton" id="btnDOE" onclick="return setupCalendar('txtIndiaEntryDate','btnDOE')" onfocus="return setupCalendar('txtIndiaEntryDate','btnDOE')" value="..." size="22" /></td>
                </tr>
                <tr>
                    <td class="labelcell">
                Proposed Stay In India:</td>
                    <td class="inputcell">
                <asp:TextBox cssclass="input"  ID="txtProStayInIndia" runat="server" Width="45px" ></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="labelcell">
                Arr. Date and Time:</td>
                    <td class="inputcell">
                <asp:TextBox cssclass="input"  ID="txtArrivaldate" runat="server" Width="109px" ></asp:TextBox><input type="button" class="datebutton" id="btnArrivalDT" onclick="return setupCalendar('txtArrivaldate','btnArrivalDT')" onfocus="return setupCalendar('txtArrivaldate','btnArrivalDT')" value="..." size="25" /></td>
                </tr>
                <tr>
                    <td class="labelcell">
                Purpose Of Visit:</td>
                    <td class="inputcell">
                <asp:TextBox cssclass="input"  ID="txtVisitPurpose" runat="server" Width="140px" ></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="labelcell">
                Room No./Type:</td>
                    <td class="inputcell">
                <asp:TextBox cssclass="input"  ID="txtRoomDetails" runat="server" Width="140px" ></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="labelcell">
                Employed In India:</td>
                    <td class="inputcell">
                <asp:RadioButton ID="radEmpYes" Text="   Yes    " GroupName="grpEmp" runat="server" Width="66px" /><asp:RadioButton ID="radEmpNo" Text="    No" GroupName="grpEmp" runat="server" Width="58px" /></td>
                </tr>
                <tr>
                    <td class="labelcell">
                        Allergies/Precautions:</td>
                    <td class="inputcell">
                <asp:TextBox cssclass="input"  ID="txtAllerges" runat="server" Width="140px" ></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="labelcell">
                Meal Preference:</td>
                    <td class="inputcell">
                <asp:TextBox cssclass="input"  ID="txtMealPref" runat="server" Width="140px" ></asp:TextBox></td>
                </tr>
            </table>
        </div>
        <div style="clear:both;" />
        </div>
        <table id="buttonsection" class="buttonsection">
        <tr>
            <td style="height: 18px">
                <asp:Button cssclass="appbutton" ID="btnSaveTouristDetails" runat="server" Text="Save"
                    Width="72px" OnClick="btnSaveTouristDetails_Click" Font-Size="X-Small" />
                </td>
            <td style="height: 18px; width: 166px;">
                <asp:Button cssclass="appbutton" ID="btnDelete" runat="server" Text="Delete"
                    Width="72px" OnClick="btnDelete_Click" Font-Size="X-Small" /></td>            
        </tr>
        </table>
        <div id="Panel3">
        </div>
        <table id="statussection" class="statussection">
        <tr>
            <td colspan="6" style="height: 18px">
                <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Width="920px"></asp:Label></td>
        </tr>
        </table>
     </ContentTemplate>
     </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
