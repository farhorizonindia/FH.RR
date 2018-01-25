<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Touristentry.aspx.cs" Inherits="Cruise_Booking_Touristentry" %>

<%@ Register TagName="PageHeaderControl" TagPrefix="phc" Src="~/userControl/pageheader.ascx" %>

<!DOCTYPE html>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Resorts a Hotels and Restaurants </title>
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <link href='http://fonts.googleapis.com/css?family=Source+Sans+Pro:400,700' rel='stylesheet' type='text/css'>

    <link rel="stylesheet" href="css/css/reset.css">
    <!-- CSS reset -->
    <link rel="stylesheet" href="css/css/style.css">
    <!-- Resource style -->
    <script src="js/modernizr.js"></script>
    <!-- Meta-Tags -->
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <script type="application/x-javascript"> addEventListener("load", function() { setTimeout(hideURLbar, 0); }, false); function hideURLbar(){ window.scrollTo(0,1); } </script>
    <!-- //Meta-Tags -->
    <!-- Custom-Stylesheet-Links -->
    <!-- Bootstrap-CSS -->
    <link rel="stylesheet" href="css/Newcss/bootstrap.min.css" type="text/css" media="all">
    <!-- Index-Page-CSS -->
    <link rel="stylesheet" href="css/Newcss/style.css" type="text/css" media="all">
    <!-- Animate.CSS -->
    <link rel="stylesheet" href="css/animate.css" type="text/css" media="all">
    <link rel="stylesheet" href="css/Newcss/jquery-ui.css" type="text/css" media="all">
    <!-- //Custom-Stylesheet-Links -->
    <link rel="stylesheet" href="css/Newcss/cruise.css">
    <!-- Fonts -->
    <!-- Body-Font -->
    <link rel="stylesheet" href="//fonts.googleapis.com/css?family=Open+Sans:400,300,600,700,800" type="text/css">
    <!-- Logo-Font -->
    <link rel="stylesheet" href="//fonts.googleapis.com/css?family=Cinzel+Decorative:400,900,700" type="text/css">
    <!-- Navbar-Font -->
    <link rel="stylesheet" href="//fonts.googleapis.com/css?family=Montserrat:400,700" type="text/css">
    <!-- //Fonts -->

</head>
<body>
    <form id="form1" runat="server">
        <%-- ///header Start///--%>
        <nav class="navbar navbar-default  aits wow bounceInUp agileits ">
            <div class="container">

                <div class="navbar-header agileits ">

                    <button type="button" class="navbar-toggle agileits  collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false">
                        <span class="sr-only agileits ">Toggle navigation</span>
                        <span class="icon-bar "></span>
                        <span class="icon-bar "></span>
                        <span class="icon-bar "></span>
                    </button>
                    <a class="navbar-brand agileits " href="searchproperty1.aspx">Resorts</a>
                </div>

                <div id="navbar" class="navbar-collapse agileits  navbar-right collapse">
                    <asp:Label ID="lblUsername" runat="server" Font-Bold="true" ForeColor="Red" Text=" "></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="button-link" OnClick="LinkButton1_Click">Logout</asp:LinkButton>
                    <ul class="nav agileits  navbar-nav" runat="server" id="navlogin">
                        <li class="dropdown">
                            <a id="lblLoginas" runat="server" href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">LOGIN <span class="caret"></span></a>
                            <ul class="dropdown-menu">
                                <li><a runat="server" id="lnkLogin" href="agentLogin.aspx">Agent</a></li>
                                <li><a runat="server" id="lnkCustomerRegis" href="../Masters/NewRegister.aspx">Customer </a>


                                </li>
                            </ul>
                        </li>
                    </ul>
                </div>

            </div>
        </nav>
        <%-- ///header End///--%>
        <br />
        <br />
        <br />
        <br />
        <br />

        <div class="container-fluid">
            <div class="yours-details">
                <div class="row" runat="server">
                    <div class="col-sm-7" style="margin-left: 17%;">

                        <!-- 	 <div class="panel panel-default">
                                       <div class="panel-heading" role="tab" id="headingOne" data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                                         <h4 class="panel-title text-left">
                                           <a role="button" >
                                           SIGN IN TO BOOK FASTER
                                           </a>
                                         </h4>
                                       </div>
                       <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne">
                         <div class="panel-body">
                          <div class=" ">
                               <div class="ak-main-login payment-online-form-left agileits">
                               <h4>LOGIN</h4>
                                   <form class="ak-form" method="post" action="#">

                                       <ul class="agileits ">
                                           <li class="agileits  wow fadeInDown"><input required="" class="text-box-dark agileits " type="text" value="Enter Your Email" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Enter Your Email';}" name="email"></li>
                                           <li class="agileits  wow fadeInDown"><input required="" class="text-box-dark agileits " type="password" value="Your Password" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Your Password';}" name="password"></li>
                                       </ul>
                                       <div class="ak-btn-cont ak-btn-booking-cont">
                                           <button type="button" id="register-show" class="btn btn-primary agileits  wow fadeInDown button1"><span class="glyphicon agileits  glyphicon-arrow-left" aria-hidden="true"></span> Register</button>
                                           <button type="submit" id="login" class="btn btn-primary agileits  wow fadeInDown button2" >Login<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button>
                                       </div>
                                   </form>
                               </div>
                           </div><asp:Button runat="server" Text="Button"></asp:Button>
                       </div>
                    </div>  -->

                        <div runat="server" id="dvreg">
                            <div class="panel panel-default" id="dvpnlDefault" runat="server">


                                <div class="panel-heading" role="tab" id="headingTwo" runat="server"
                                    data-toggle="collapse" data-parent="#accordion" href="#collapse" aria-expanded="true" aria-controls="collapseTwo" style="background-color: #f5f5f5; border-color: #ddd;">
                                    <div class="panel-title">
                                        <span></span>
                                        <label style="margin-bottom: 0;">Add Details</label>
                                        <img src="../../images/next-arrow.png" style="width: 16px; float: right; margin-top: 4px; transform: rotate(90deg);" />
                                    </div>
                                </div>

                                <div id="collapse" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne">
                                    <div class="top-form panel-body" runat="server" id="custRegis">
                                        <%-- <h4>Register</h4>--%>
                                        <div class="ak-form">

                                            <ul class="agileits ">


                                                <li class="agileits  list-unstyled">

                                                    <%--<select class="text-box-dark agileits" name="title" id="title-name" style="padding: 1%;">
                                            <option selected="selected" value="Title">Title</option>
                                            <option value="Mr">Mr</option>
                                            <option value="Mrs">Mrs</option>
                                            <option value="Miss">Miss</option>
                                            <option value="Ms">Ms</option>
                                        </select>--%>
                                                </li>
                                            </ul>
                                            <div class="row agileits ">
                                                <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                                    <div style="width: 100%">

                                                        <div style="width: 86%; float: right;">
                                                            First Name:
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtFirstName" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                                            <asp:TextBox ID="txtFirstName" runat="server" Placeholder="First Name" class="text-box-dark agileits " TabIndex="2"></asp:TextBox>

                                                        </div>

                                                        <div style="width: 50%;">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlSuffix" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                                            <asp:DropDownList ID="ddlSuffix" runat="server" class="text-box-dark agileits" Style="margin-bottom: 0px; font-size: 12px; width: 26%; margin-top: 0%; border: 1px solid #9e9e9e; height: 52px;" TabIndex="1">

                                                                <%--<asp:ListItem Value="Choose"></asp:ListItem>--%>
                                                                <asp:ListItem Value="Mr">Mr.</asp:ListItem>
                                                                <asp:ListItem Value="Ms">Ms.</asp:ListItem>
                                                                <asp:ListItem Value="Mrs">Mrs.</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <%--<input required=""  id="" runat="server" type="text" value="First Name" onblur="if (this.value == '') {this.value = 'First Name';}" name="firstname" />--%>
                                                </div>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlSuffix" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                                <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                    Middle Name:<asp:TextBox ID="txtmdlname" runat="server" Placeholder="Middle Name" Font-Size="Larger" class="text-box-dark agileits " TabIndex="3"></asp:TextBox>
                                                    <%--<input required="" id="" runat="server" type="text" value="Last Name" onblur="if (this.value == '') {this.value = 'Last Name';}" name="lastname" />--%>
                                                </div>
                                            </div>
                                            <div class="row agileits ">

                                                <div class="col-sm-6 agileits">
                                                    Last Name:<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtLName" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtLName" runat="server" Placeholder="Last Name" class="text-box-dark agileits " TabIndex="4"></asp:TextBox>

                                                    <%-- <asp:TextBox ID="txtMailAddress" runat="server" PlaceHolder="Email Address" class="form-control"></asp:TextBox>--%>
                                                </div>

                                                <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                    Gender:
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ErrorMessage="*" InitialValue="0" ForeColor="Red" ControlToValidate="ddlGender" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                                    <asp:DropDownList ID="ddlGender" runat="server" TabIndex="5" class="text-box-dark agileits" CssClass="form-control" Style="font-size: 12px; border-radius: 0; padding: 17px 12px !important; margin-top: 0%; margin-bottom: 2%;">
                                                        <asp:ListItem Value="0">Choose</asp:ListItem>
                                                        <asp:ListItem Value="M">Male</asp:ListItem>
                                                        <asp:ListItem Value="F">Female</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row agileits ">
                                                <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                    Date Of Birth:
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtDOB" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtDOB" runat="server" Placeholder="Date Of Birth" class="text-box-dark agileits " TabIndex="6"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtDOB" runat="server" />

                                                    <%-- <input required=""  runat="server" id="" type="text" value="Address 1"  onblur="if (this.value == '') {this.value = 'Address 1';}" name="address1" />--%>
                                                </div>
                                                <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                    Email Id:
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtEmailId" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmailId" ValidationGroup="Cust" ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator>
                                                    <asp:TextBox ID="txtEmailId" runat="server" Placeholder="Email Id" class="text-box-dark agileits " TabIndex="11"></asp:TextBox>


                                                    <%--<input required="" class="text-box-dark agileits " id="" runat="server" type="password" value="Password" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Password';}" name="register-password" />--%>
                                                </div>
                                                
                                            </div>
                                            <div class="row agileits ">
                                                 <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                    Nationality:<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlNationality" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                                    <asp:DropDownList ID="ddlNationality" runat="server" TabIndex="7" class="text-box-dark agileits" CssClass="form-control" Style="font-size: 12px; border-radius: 0; padding: 17px 12px !important; margin-bottom: 2%;">
                                                    </asp:DropDownList>

                                                    <%--  <input required="" runat="server" id="" type="text" value="Address 2" onblur="if (this.value == '') {this.value = 'Address 2';}" name="address2" />--%>
                                                </div>

                                                <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                    Passport Number:<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtPassportNo" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtPassportNo" runat="server" Placeholder="Passport Number" class="text-box-dark agileits " TabIndex="12"></asp:TextBox>

                                                    <%--<select class="text-box-dark agileits" name="title" id="title-name" style="padding: 1%; margin-top: 1%;">
                                            <option selected="selected" value="Title">Country</option>
                                            <option value="India">India</option>
                                            <option value="USA">USA</option>
                                            <option value="UK">UK</option>
                                            <option value="Norway">Norway</option>
                                        </select>--%>
                                                </div>
                                               
                                            </div>

                                            <div class="row agileits ">
                                                 <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                    Passport Issue Date:<asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtPassportIssueDate" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtPassportIssueDate" runat="server" Placeholder="Passport Issue Date" class="text-box-dark agileits " TabIndex="13"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtPassportIssueDate" runat="server" />
                                                </div>
                                                <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                    Passport Expiry Date:<asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtPassportExpiryDate" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtPassportExpiryDate" runat="server" Placeholder="Passport Expiry Date" class="text-box-dark agileits " TabIndex="14"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" TargetControlID="txtPassportExpiryDate" runat="server" />
                                                    <%--<select class="text-box-dark agileits" name="title" id="title-name" style="padding: 1%; margin-top: 1%;">
                                            <option selected="selected" value="Title">Country</option>
                                            <option value="India">India</option>
                                            <option value="USA">USA</option>
                                            <option value="UK">UK</option>
                                            <option value="Norway">Norway</option>
                                        </select>--%>
                                                </div>
                                               
                                            </div>
                                            <div class="row agileits ">
                                                 <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                    Birth Place:<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtBirthPlace" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtBirthPlace" runat="server" Placeholder="Birth Place" class="text-box-dark agileits " TabIndex="8"></asp:TextBox>
                                                    <%-- <input required=""  id="txtCity" runat="server" type="text" value="City"  onblur="if (this.value == '') {this.value = 'City';}" name="city" />--%>
                                                </div>
                                                <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                    Permanent Address:<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtPermAdd" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtPermAdd" runat="server" Placeholder="Permanent Address" class="text-box-dark agileits " TabIndex="9"></asp:TextBox>
                                                    <%--<input required=""  type="text" value="State" runat="server"  onblur="if (this.value == '') {this.value = 'State';}" name="state" />--%>
                                                </div>
                                            </div>
                                            <div class="row agileits ">
                                               
                                                 <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                    Visa No:<asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtVisaNo" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtVisaNo" runat="server" Placeholder="Visa No" class="text-box-dark agileits " TabIndex="15"></asp:TextBox>

                                                </div>
                                                <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                    Visa Expiry Date:<asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtVisaExpiryDate" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtVisaExpiryDate" runat="server" Placeholder="Visa Expiry Date" class="text-box-dark agileits " TabIndex="16"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" TargetControlID="txtVisaExpiryDate" runat="server" />
                                                    <%--<select class="text-box-dark agileits" name="title" id="title-name" style="padding: 1%; margin-top: 1%;">
                                            <option selected="selected" value="Title">Country</option>
                                            <option value="India">India</option>
                                            <option value="USA">USA</option>
                                            <option value="UK">UK</option>
                                            <option value="Norway">Norway</option>
                                        </select>--%>
                                                </div>
                                               
                                            </div>
                                            
                                            
                                            
                                            <div class="row agileits ">
                                                 <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                    Date Of Entry In India:<asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtIndiaEntryDate" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtIndiaEntryDate" runat="server" Placeholder="Date Of Entry In India" class="text-box-dark agileits " TabIndex="17"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender5" TargetControlID="txtIndiaEntryDate" runat="server" />
                                                </div>
                                                <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                    Proposed Stay In India:<asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtProStayInIndia" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtProStayInIndia" runat="server" Placeholder="Proposed Stay In India" class="text-box-dark agileits " TabIndex="18"></asp:TextBox>

                                                    <%--<select class="text-box-dark agileits" name="title" id="title-name" style="padding: 1%; margin-top: 1%;">
                                            <option selected="selected" value="Title">Country</option>
                                            <option value="India">India</option>
                                            <option value="USA">USA</option>
                                            <option value="UK">UK</option>
                                            <option value="Norway">Norway</option>
                                        </select>--%>
                                                </div>
                                               
                                            </div>
                                            <div class="row agileits ">
                                                <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                    Purpose Of Visit:<asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtVisitPurpose" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtVisitPurpose" runat="server" Placeholder="Purpose Of Visit" class="text-box-dark agileits " TabIndex="20"></asp:TextBox>

                                                    <%--<select class="text-box-dark agileits" name="title" id="title-name" style="padding: 1%; margin-top: 1%;">
                                            <option selected="selected" value="Title">Country</option>
                                            <option value="India">India</option>
                                            <option value="USA">USA</option>
                                            <option value="UK">UK</option>
                                            <option value="Norway">Norway</option>
                                        </select>--%>
                                                </div>
                                                <div class="col-sm-6 agileits" style="padding-bottom: 15px; display:none;">
                                                    Room No./Type:<asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtRoomDetails" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtRoomDetails" runat="server" Placeholder="Room No./Type" class="text-box-dark agileits " TabIndex="21"></asp:TextBox>

                                                </div>
                                            </div>
                                           
                                            <div class="row agileits ">

                                                <div style="margin-left: 2%;">
                                                    <asp:Label ID="Label12" runat="server" Text="Employed In India:   "></asp:Label>

                                                    <asp:RadioButton ID="radEmpYes" Text=" Yes " GroupName="grpEmp" runat="server" Width="66px" />


                                                    <asp:RadioButton ID="radEmpNo" Text=" No " GroupName="grpEmp" runat="server" Width="58px" />
                                                </div>

                                            </div>
                                             <div class="row agileits ">

                                                <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                    Special Message:<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtMessage" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtMessage" runat="server" Placeholder="Special Message" class="text-box-dark agileits " TabIndex="10"></asp:TextBox>

                                                    <%-- <input required="" type="text" value="Postcode" id="" runat="server" onblur="if (this.value == '') {this.value = 'Postcode';}" name="postcode" />--%>
                                                </div>
                                                <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                    Meal Preference:<asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtMealPref" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtMealPref" runat="server" Placeholder="Meal Preference" class="text-box-dark agileits " TabIndex="22"></asp:TextBox>

                                                    <%--<select class="text-box-dark agileits" name="title" id="title-name" style="padding: 1%; margin-top: 1%;">
                                            <option selected="selected" value="Title">Country</option>
                                            <option value="India">India</option>
                                            <option value="USA">USA</option>
                                            <option value="UK">UK</option>
                                            <option value="Norway">Norway</option>
                                        </select>--%>
                                                </div>
                                            </div>
                                            <div class="row agileits ">
                                               
                                                <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                    Allergies/Precautions:<asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtAllerges" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtAllerges" runat="server" Placeholder="Allergies/Precautions" class="text-box-dark agileits " TabIndex="23"></asp:TextBox>

                                                </div>
                                                <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                    Arr. Date and Time:
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtArrivaldate" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtArrivaldate" runat="server" Placeholder="Arr. Date and Time" class="text-box-dark agileits " TabIndex="19"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender6" TargetControlID="txtArrivaldate" runat="server" ClearTime="False" />
                                                </div>
                                            </div>
                                            <div class="row agileits ">
                                                 
                                                 <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                    Arr. Airport:<asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtarrivalairport" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtarrivalairport" runat="server" Placeholder="Arr. Airport" class="text-box-dark agileits " TabIndex="28"></asp:TextBox>
                                                    <%--<select class="text-box-dark agileits" name="title" id="title-name" style="padding: 1%; margin-top: 1%;">
                                            <option selected="selected" value="Title">Country</option>
                                            <option value="India">India</option>
                                            <option value="USA">USA</option>
                                            <option value="UK">UK</option>
                                            <option value="Norway">Norway</option>
                                        </select>--%>
                                                </div>
                                               
                                                <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                    Arr. Flight No:
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtVehicalno" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtVehicalno" runat="server" Placeholder="Arr. Flight No" class="text-box-dark agileits " TabIndex="24"></asp:TextBox>

                                                    <%--<select class="text-box-dark agileits" name="title" id="title-name" style="padding: 1%; margin-top: 1%;">
                                            <option selected="selected" value="Title">Country</option>
                                            <option value="India">India</option>
                                            <option value="USA">USA</option>
                                            <option value="UK">UK</option>
                                            <option value="Norway">Norway</option>
                                        </select>--%>
                                                </div>

                                            </div>
                                            <div class="row agileits ">
                                                
                                                <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                    Dep. Date and Time:
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtDeparturedate" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtDeparturedate" runat="server" Placeholder="Dep. Date and Time" class="text-box-dark agileits " TabIndex="26"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender7" TargetControlID="txtDeparturedate" runat="server" ClearTime="False" />
                                                    <%--<select class="text-box-dark agileits" name="title" id="title-name" style="padding: 1%; margin-top: 1%;">
                                            <option selected="selected" value="Title">Country</option>
                                            <option value="India">India</option>
                                            <option value="USA">USA</option>
                                            <option value="UK">UK</option>
                                            <option value="Norway">Norway</option>
                                        </select>--%>
                                                </div>
                                                  <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                    Dep. Airport:<asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtDepairpot" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtDepairpot" runat="server" Placeholder="Dep. Airport" class="text-box-dark agileits " TabIndex="27"></asp:TextBox>
                                                </div>

                                            </div>
                                            <div class="row agileits ">
                                               
                                              
                                                 <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                    Dep. Flight No:<asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtVehicalno" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtDepartureVehicalno" runat="server" Placeholder="Dep. Flight No" class="text-box-dark agileits " TabIndex="25"></asp:TextBox>
                                                </div>
                                                </div>

                                            </div>
                                        </div>
                                        <%--<strong>Terms & Conditions</strong>--%>


                                        <div class="ak-btn-cont ak-btn-booking-cont">

                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Cust" CausesValidation="true" class="btn btn-primary agileits  wow fadeInLeft button1" CssClass="btn btn-info font16" Width="100%" OnClick="btnSubmit_Click" TabIndex="16" />
                                            <%-- <asp:Button ID="Button1" runat="server" Text="Button" class="btn btn-primary agileits  wow fadeInLeft button1" OnClick="Button1_Click" />--%>
                                            <%--<button type="submit" name="register" id="submit-register" class="btn btn-primary agileits  wow fadeInLeft button1"><span class="glyphicon agileits  glyphicon-arrow-left" aria-hidden="true"></span>Submit</button>--%>
                                            <%--<button type="button" id="register-close" class="btn btn-primary agileits  wow fadeInRight button2">Close<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button>--%>
                                        </div>


                                        <div class="ak-btn-cont ak-btn-booking-cont">

                                            <asp:Label ID="lblErrorMsg" runat="server" Text=" "></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div id="Div1" class="box box-primary booking-online" style="width: 80%; padding: 20px !important; background: #fff; box-shadow: 2px 2px 8px #cacaca; border-radius: 5px; overflow-x: scroll; margin: 0 auto;">
                        <asp:GridView ID="GridRoomPaxDetail" runat="server" class="table table-bordered" ShowHeaderWhenEmpty="true" ShowFooter="false" AutoGenerateColumns="False" Style="font-size: 15px; min-width: 500px;" OnRowCommand="GridRoomPaxDetail_RowCommand" OnRowEditing="GridRoomPaxDetail_RowEditing" Caption="Guest details entered">

                            <Columns>
                                <asp:TemplateField HeaderText="Guest Name">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("guestname") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cabin Details">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("RoomDetails") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Gender">
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("Gender") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date Of Birth">
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Convert.ToDateTime(Eval("DateOfBirth")).ToString("d MMMM, yyyy") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nationality">
                                    <ItemTemplate>
                                        <asp:Label ID="Label5" runat="server" Text='<%# Eval("Nationality") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Passport No">
                                    <ItemTemplate>
                                        <asp:Label ID="Label6" runat="server" Text='<%# Eval("PassportNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Visa Expiry Date">
                                    <ItemTemplate>
                                        <asp:Label ID="Label7" runat="server" Text='<%# Convert.ToDateTime( Eval("VisaExpiryDate")).ToString("d MMMM, yyyy")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Arrival Date Time">
                                    <ItemTemplate>
                                        <asp:Label ID="Label8" runat="server" Text='<%#  Convert.ToDateTime(Eval("ArrivalDateTime")).ToString("d MMMM, yyyy") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Arrival Airport">
                                    <ItemTemplate>
                                        <asp:Label ID="Label9" runat="server" Text='<%# Eval("arrivalairport") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Flight No">
                                    <ItemTemplate>
                                        <asp:Label ID="Label9234" runat="server" Text='<%# Eval("arrivalvehiaclno") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Departure Date">
                                    <ItemTemplate>
                                        <asp:Label ID="Label10" runat="server" Text='<%# Convert.ToDateTime(Eval("departuredate")).ToString("d MMMM, yyyy") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Departure Airport">
                                    <ItemTemplate>
                                        <asp:Label ID="Label11" runat="server" Text='<%# Eval("departureairport") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Flight No">
                                    <ItemTemplate>
                                        <asp:Label ID="Labelasdf9" runat="server" Text='<%# Eval("departurevehicalno") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton2" runat="server" Text="Edit" CommandName="Edit" CommandArgument='<%# Eval("TouristNo") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <HeaderStyle CssClass="GridHeader" Font-Bold="True" Font-Size="15px" />
                            <AlternatingRowStyle BackColor="White" />
                            <RowStyle BackColor="#F9F9F9" />
                        </asp:GridView>
                    </div>
                </div>

            </div>

        </div>







        <!-- // $$$$$$$$%%%%%%%%%%%%%%%%%%%%% pakages Details &&&&&&&&&&&&&&&&&&&&&&&&&&&&&& -->
        <!-- Default-JavaScript -->
        <script type="text/javascript" src="js/jquery-2.1.4.min.js"></script>
        <!-- Bootstrap-JavaScript -->
        <script type="text/javascript" src="js/bootstrap.min.js"></script>
        <!-- Animate.CSS-JavaScript -->
        <script src="js/wow.min.js"></script>
        <script type="js/index.js">
        </script>
        <script>
            new WOW().init();
        </script>
        <!-- Date-Picker-JavaScript -->
        <script src="js/jquery-ui.js"></script>

        <script>
            $(function () {
                //localStorage.setItem('activeTab12', 'null');

                $('#btnSubmit,#Button1').click(function () {
                    if (this.id == 'btnSubmit') {
                        localStorage.setItem('activeTab12', 'btnSubmit');
                    }
                    else if (this.id == 'Button1') {
                        //alert('Submit 2 clicked');
                        localStorage.setItem('activeTab12', 'Button1');
                    }
                });
                var activeTab = localStorage.getItem('activeTab12');
                if (activeTab == 'btnSubmit') {
                    localStorage.clear('activeTab12');
                    localStorage.clear("get");
                    $('#collapseOne').removeClass('panel-collapse collapse');
                }
                if (activeTab == 'Button1') {
                    localStorage.clear('activeTab12');
                    localStorage.clear("get");
                    $('#collapseTwo').removeClass('panel-collapse collapse');
                }
                $('.panel-heading').click(function () {

                    var spanChng1 = $(this).children('.panel-title').children('img');
                    console.log(spanChng1);

                    //if (localStorage.getItem("get") == null) {
                    if ($(this).hasClass('collapsed')) {
                        localStorage.setItem("get", "get1");
                        spanChng1.css({
                            "background": "url(../../images/next-arrow.png)",
                            "transform": "rotate(270deg)"
                        });
                    } else {
                        localStorage.setItem("get", "get1");
                        spanChng1.css({
                            "background": "url(../../images/next-arrow.png)",
                            "transform": "rotate(90deg)"
                        });
                    }
                    //}
                });
            });
        </script>


    </form>
</body>
</html>
