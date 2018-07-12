<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/Cruise/Booking/SummarizedDetails1.aspx.cs" Inherits="Cruise_Booking_SummarizedDetails" EnableEventValidation="false" %>

<!DOCTYPE html>



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

    <script type = "text/javascript" >
        $("#btnPayProceed").click( function()
        {
            function preventBack(){window.history.forward(0);}

            setTimeout("preventBack()", 0);

            window.onunload=function(){null};
        }

</script>
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
                    <a class="navbar-brand agileits headerTextFont" href="searchproperty.aspx">Booking System</a>

                     <asp:DropDownList id="ddlCurrency"
                AppendDataBoundItems="True"
                runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCurrency_SelectedIndexChanged"  >
             
                             <asp:ListItem  Value="USD">INR</asp:ListItem>
                             <asp:ListItem  Value="INR">USD</asp:ListItem>
           </asp:DropDownList>
                </div>

                <div id="navbar" class="navbar-collapse agileits  navbar-right collapse">
                    <asp:Label ID="lblUsername" runat="server" Font-Bold="true" ForeColor="Red" Text=" "></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="button-link" OnClick="LinkButton1_Click">Logout</asp:LinkButton>
                    <ul class="nav agileits  navbar-nav" runat="server" id="navlogin">
                        <li class="dropdown">
                            <a id="lblLoginas" runat="server" href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">LOGIN <span class="caret"></span></a>
                            <ul class="dropdown-menu">
                                <li><a runat="server" id="lnkLogin" href="agentLogin.aspx">Partner</a></li>
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
        
        <section style="width: 100%; height: 74px; padding: 0 !important;">


            <nav>
                <ol class="cd-breadcrumb triangle">
                    <li><em>Search</em></li>
                    <li><em>Packages</em></li>
                    <li><em>Choose Date</em></li>
                    <li><em>Select Cabin</em></li>
                    <%--<li><a href="#0">Reservation Details & Check Out</a></li>--%>
                    <li class="current"><em>Details & Check Out</em></li>
                </ol>
            </nav>
        </section>
        <div class="container-fluid">
            <div class="yours-details">
                <div class="row" runat="server">
                    <div class="col-sm-7">
                        <div class="panel panel-default" runat="server" id="pnllogin">
                            <div class="panel-heading" role="tab" id="headingTwo" data-toggle="collapse" data-parent="#accordion" href="#collapse" aria-expanded="true" aria-controls="collapseTwo">
                                <!-- <h4 class="panel-title">
                              <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                Collapsible Group Item #2
                              </a>
                            </h4> -->
                                <div class="panel-title">

                                    <span></span>
                                    <label style="margin: 0;">
                                        SIGN IN
                                    </label>
                                    <img src="../../images/next-arrow.png" style="width: 16px; float: right; margin-top: 4px; transform: rotate(270deg);" />
                                </div>
                            </div>
                            <div id="collapse" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingTwo">
                                <div class="panel-body">
                                    <div class="ak-main-login payment-online-form-left agileits">
                                        <h4>LOGIN</h4>
                                        <form class="ak-form" method="post" action="#">

                                            <ul class="agileits ">
                                                <li class="agileits">
                                                    <asp:TextBox ID="txtCustMailId" runat="server" Placeholder="Enter Email Id"></asp:TextBox>
                                                </li>
                                                <li class="agileits">
                                                    <asp:TextBox ID="txtCustPass" Placeholder="Enter Your Password" runat="server" class="text-box-dark agileits " TextMode="Password" onblur="if (this.value == '') {this.value = 'Your Password';}"></asp:TextBox>
                                                    <%-- <input required="" id="txtCustPass" runat="server" class="text-box-dark agileits " type="password" value="Your Password" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Your Password';}" name="password" />--%></li>
                                            </ul>
                                            <div class="ak-btn-cont ak-btn-booking-cont login-btn">
                                                <asp:Button runat="server" class="btn btn-primary agileits button2" CssClass="btn btn-info btnWidth100 btnFont" ID="btnCustLogin" ValidationGroup="CustLogin" Text="Login" OnClick="btnCustLogin_Click" />
                                                <asp:Button runat="server" class="btn btn-primary agileits button2" CssClass="btn btn-info btnWidth100 btnFont" ID="Button2" Text="Forgot Password" OnClick="btnCustLogin_Click1" />
                                                <!-- <button type="button" id="register-show" class="btn btn-primary agileits button1"><span class="glyphicon agileits  glyphicon-arrow-left" aria-hidden="true"></span> Register</button> -->
                                                <%-- <button type="submit" id="login" class="btn btn-primary agileits button2">Login<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button>--%>
                                            </div>
                                        </form>
                                    </div>
                                </div>
                            </div>
                        </div>
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


                                <div class="panel-heading collapsed" role="tab" id="headingOne" runat="server"
                                    data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="false" aria-controls="collapseOne" style="background-color: #f5f5f5; border-color: #ddd;">
                                    <div class="panel-title">
                                        <span></span>
                                        <label style="margin-bottom: 0;">REGISTER</label>
                                        <img src="../../images/next-arrow.png" style="width: 16px; float: right; margin-top: 4px; transform: rotate(90deg);" />
                                    </div>
                                </div>

                                <div id="collapseOne" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingOne">
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

                                                    <div style="width: 100%">

                                                        <div style="width: 86%; float: right;">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtFirstName" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                                            <asp:TextBox ID="txtFirstName" runat="server" Placeholder="First Name" class="text-box-dark agileits " TabIndex="1"></asp:TextBox>

                                                        </div>

                                                        <div style="width: 50%;">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddltitle" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                                            <asp:DropDownList ID="ddltitle" runat="server" class="text-box-dark agileits" Style="margin-bottom: 0px; font-size: 12px; width: 26%; margin-top: 0%; border: 1px solid #9e9e9e; height: 52px;">

                                                                <asp:ListItem>Mr</asp:ListItem>
                                                                <asp:ListItem>Mrs</asp:ListItem>
                                                                <asp:ListItem>Miss</asp:ListItem>
                                                                <asp:ListItem>Ms</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <%--<input required=""  id="" runat="server" type="text" value="First Name" onblur="if (this.value == '') {this.value = 'First Name';}" name="firstname" />--%>
                                                </div>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtLastName" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                                <div class="col-sm-6 agileits" style="padding-bottom: 15px;">

                                                    <asp:TextBox ID="txtLastName" runat="server" Placeholder="Last Name" Font-Size="Larger" class="text-box-dark agileits " TabIndex="2"></asp:TextBox>
                                                    <%--<input required="" id="" runat="server" type="text" value="Last Name" onblur="if (this.value == '') {this.value = 'Last Name';}" name="lastname" />--%>
                                                </div>
                                            </div>
                                            <div class="row agileits ">

                                                <div class="col-sm-6 agileits">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtMailAddress" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                                    <%--<input required="" id="" runat="server" type="text" value="Email Address" onblur="if (this.value == '') {this.value = 'Email Address';}" name="register-email" />--%>
                                                    <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtMailAddress" ValidationGroup="Cust" ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator>
                                                    <asp:TextBox ID="txtMailAddress" runat="server" Placeholder="Email Address" class="text-box-dark agileits " TabIndex="3"></asp:TextBox>

                                                    <%-- <asp:TextBox ID="txtMailAddress" runat="server" PlaceHolder="Email Address" class="form-control"></asp:TextBox>--%>
                                                </div>

                                                <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtTelephone" ValidationGroup="Cust"></asp:RequiredFieldValidator>

                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid" ControlToValidate="txtTelephone" ValidationGroup="Cust" ValidationExpression="^([0-9]{8,15})$"></asp:RegularExpressionValidator>
                                                    <asp:TextBox ID="txtTelephone" runat="server" Placeholder="Contact No" MaxLength="10" TabIndex="4"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row agileits ">
                                                <div class="col-sm-6 agileits" style="padding-bottom: 15px;">


                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*" InitialValue="0" ForeColor="Red" ControlToValidate="ddlCountry" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                                    <asp:DropDownList ID="ddlCountry" class="text-box-dark agileits" runat="server" CssClass="form-control" Style="font-size: 12px; border-radius: 0; padding: 17px 12px !important;" TabIndex="6">
                                                    </asp:DropDownList>

                                                    <%-- <input required=""  runat="server" id="" type="text" value="Address 1"  onblur="if (this.value == '') {this.value = 'Address 1';}" name="address1" />--%>
                                                </div>
                                                <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtState" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtState" runat="server" Placeholder="State" class="text-box-dark agileits " TabIndex="7"></asp:TextBox>

                                                    <%--  <input required="" runat="server" id="" type="text" value="Address 2" onblur="if (this.value == '') {this.value = 'Address 2';}" name="address2" />--%>
                                                </div>
                                            </div>
                                            <div class="row agileits ">
                                                <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtCity" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtCity" runat="server" Placeholder="City" class="text-box-dark agileits " TabIndex="8"></asp:TextBox>
                                                    <%-- <input required=""  id="txtCity" runat="server" type="text" value="City"  onblur="if (this.value == '') {this.value = 'City';}" name="city" />--%>
                                                </div>
                                                <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtAddress1" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtAddress1" runat="server" Placeholder="Address1" class="text-box-dark agileits " TabIndex="9"></asp:TextBox>
                                                    <%--<input required=""  type="text" value="State" runat="server"  onblur="if (this.value == '') {this.value = 'State';}" name="state" />--%>
                                                </div>
                                            </div>
                                            <div class="row agileits ">
                                                <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlCountry" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtaddress2" runat="server" Placeholder="Address2" class="text-box-dark agileits " TabIndex="10"></asp:TextBox>

                                                    <%-- <input required="" type="text" value="Postcode" id="" runat="server" onblur="if (this.value == '') {this.value = 'Postcode';}" name="postcode" />--%>
                                                </div>
                                                <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtPostcode" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                                 
                                                    <asp:TextBox ID="txtPostcode" runat="server" class="text-box-dark agileits " Placeholder="Post Code" MaxLength="6" TabIndex="11"></asp:TextBox>


                                                    <%--<input required="" class="text-box-dark agileits " id="" runat="server" type="password" value="Password" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Password';}" name="register-password" />--%>
                                                </div>
                                            </div>
                                            <div class="row agileits ">
                                                <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtpassword" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtpassword" runat="server" Placeholder="password" class="text-box-dark agileits " TextMode="Password" TabIndex="13"></asp:TextBox>

                                                    <%--<select class="text-box-dark agileits" name="title" id="title-name" style="padding: 1%; margin-top: 1%;">
                                            <option selected="selected" value="Title">Country</option>
                                            <option value="India">India</option>
                                            <option value="USA">USA</option>
                                            <option value="UK">UK</option>
                                            <option value="Norway">Norway</option>
                                        </select>--%>
                                                </div>
                                                <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtConfirm" ValidationGroup="Cust"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="comparePasswords"
                                                        runat="server"
                                                        ControlToCompare="txtpassword"
                                                        ControlToValidate="txtConfirm"
                                                        ErrorMessage="Your passwords do not match up!"
                                                        Display="Dynamic" ValidationGroup="Cust" ForeColor="Red" />
                                                    <asp:TextBox ID="txtConfirm" runat="server" Placeholder="Confirm password" class="text-box-dark agileits " TextMode="Password" TabIndex="14"></asp:TextBox>

                                                    <%--<select class="text-box-dark agileits" name="title" id="title-name" style="padding: 1%; margin-top: 1%;">
                                            <option selected="selected" value="Title">Country</option>
                                            <option value="India">India</option>
                                            <option value="USA">USA</option>
                                            <option value="UK">UK</option>
                                            <option value="Norway">Norway</option>
                                        </select>--%>
                                                </div>
                                            </div>



                                        </div>
                                        <%--<strong>Terms & Conditions</strong>--%>
                                        <div style="width: 100%; height: 100px; overflow: auto; margin-bottom: 1%; margin-top: 2%;" runat="server" visible="false" id="Divmvmahabahu1">
                                            <strong>Terms and Conditions:</strong>
                                            <br />
                                            <p>Information contained in the ADVENTURE RESORTS & CRUISES brochure, website or advertising materials is not an offer or a contract. The transportation of guests and baggage on the MV MAHABAAHU river cruise vessel is provided solely by ADVENTURE RESORTS & CRUISES and is governed by the terms and conditions printed on the Guest Booking Contract. For complete information on terms and conditions, itinerary, liability of ADVENTURE RESORTS & CRUISES, and all sections mentioned below, refer to the Guest Booking Contract below.</p>
                                            <br />
                                            <br />
                                            <strong>PAYMENT AND DEPOSIT POLICY:</strong>
                                            <br />
                                            A deposit for cruise fare of 25% per person is required to secure a confirmed booking. For Charter bookings this deposit is NON REFUNDABLE.
                                            <br />
                                            <br />
                                            90 DAYS PRIOR TO DATE OF SAILING: 75% Per Person
                                            <br />
                                            <br />
                                            <p>
                                                For the Christmas and New Year holiday, the full balance must be paid 120 days prior.
All reservations are subject to cancellation if payments are not received by the due date. Upon full payment by the participant(s) of the amount specified as the Full Fare (Invoice to be referred), ADVENTURE RESORTS & CRUISES agrees to arrange for the provision of the services as described herein and as modified by any supplementary marketing materials.
                                            </p>
                                            <br />
                                            <br />
                                            <p>Upon receipt of your deposit or full payment or upon the issuance of the Guest Booking Contract directly or by a Booking agent or upon receipt of a confirmation letter or final invoice from us, both the Guest and the Carrier will be fully bound by all of the terms and conditions of the Guest Booking Contract.</p>
                                            <br />
                                            <br />
                                            <strong>RESERVATIONS</strong>
                                            <br />
                                            Bank wire transfer which is subject to bank wire fee
                                            <br />
                                            PayPal system with additional 5% Transaction fee
                                            <br />
                                            Through Amex, Master and Visa credit card with an additional 3% Transaction fee
                                            <br />
                                            <br />
                                            <strong>CANCELLATION POLICY</strong>
                                            <br />
                                            <p>All cancellations must be in writing. The following cancellation terms will be applicable for all written cancellations received prior to departure up to the scheduled time of departure.</p>
                                            <br />
                                            <br />
                                            <strong>CANCELLATION TERMS FOR CHARTER BOOKINGS</strong>
                                            <br />
                                            <p>25% of the price will be charged for cancellation after the deposit has been paid 90-75 days prior, 50% of the charter cost will be charged 74-59 days prior, 75% of the charter cost will be charged 60 days and less, 100% of the charter cost will be charged</p>
                                            <br />
                                            <br />
                                            <p><strong>CANCELLATION TERMS FOR F.I.T. AND GROUP DEPARTURES APPLICABLE ON THE FULL FARE:</strong></p>
                                            <br />
                                            <p>120 to 90 days prior, 25% per person of the fare paid will be charged 89-76 days prior, 50% per person will be charged 75-46 days prior, 75% per person will be charged 45 days and less, 100% per person will be charged *Full Fare is defined as the full cost of any cruise package as mentioned in the price chart. Note: for Christmas and New Year Departures, 100% cancellation is charged 90 days and under from the sailing date.</p>
                                            <br />
                                            <br />
                                            <strong>REVISIONS/CHANGES</strong>
                                            <br />
                                            Once a cruise booking has a full deposit, all changes are subject to a INR 3,400 charge per change, provided the said change is prior to 90 days before date of departure/sailing. The following situations are considered as Revisions and penalties will apply:
                                            <br />
                                            <br />
                                            Changes to departure date
                                            <br />
                                            <br />
                                            Substitutions of itinerary
                                            <br />
                                            <br />
                                            Substitution of another person for original booked guest(s)
                                            <br />
                                            <br />
                                            Changing to a promotional fare
                                            <br />
                                            <br />
                                            <strong>DOCUMENTS</strong>
                                            <br />

                                            Documents will be issued approximately 3 weeks prior to departure.
                                            <br />
                                            <br />

                                            <strong>TRANSFERS</strong>
                                            <br />
                                            Only round trip airport-to-ship transfers are included.
                                            <br />
                                            <br />
                                            <strong>TRAVEL DOCUMENTATION/VISAS</strong>
                                            <br />
                                            <p>
                                                All guests must have passports valid for six months following disembarkation and necessary visas when boarding. Guests are advised to check with their Booking agent to determine which documents they must obtain. Due to airline security measures, your passport name must match your airline ticket name or you may be denied boarding. Adventure Resorts and Cruises accepts no responsibility for obtaining required visas or for advising guests of visa or other immigration requirements.
                                            </p>
                                            <br />
                                            <br />
                                            <strong>CHILDREN/MINORS</strong>
                                            <br />
                                            <p>
                                                We accept children onboard sharing a cabin with parents or guardians. They will be the sole responsibility of the parents/ guardians and must never be left unattended. For Children of 15 years and above, a separate cabin will need to be booked.
                                            </p>
                                            <br />
                                            <br />
                                            <strong>DIFFERENTLY ABLED GUESTS</strong>
                                            <br />
                                            <p>
                                                We ask that you advise us at the time of booking of any disability or special needs you may have while on your Adventure Resorts & Cruises itinerary. Most transportation services, including the motorcoaches, are not equipped with elevators or wheelchair ramps. Adventure Resorts & Cruises India will make reasonable attempts to accommodate the special needs of travelers with disabilities, but we are not responsible in the event we are unable to do so, or there is a denial of service by vessel operators, air carriers, hotels, restaurants or other independent suppliers. We regret that we cannot provide individual assistance to a guest for walking, dining, getting on and off vessels, motor coaches and other vehicles, or other personal needs. A qualified and physically able companion must accompany travelers who need such assistance and must assume full responsibility for their wellbeing. The guest assumes the full risk of use and of any prohibitions imposed by vendors.
                                            </p>
                                            <br />
                                            <br />
                                            <strong>TIPS</strong>

                                            <br />
                                            <p>
                                                It is customary to give cruise gratuities, subject to your satisfaction of services rendered. Gratuities on board and on land are not included in your Full Fare.
                                            </p>
                                        </div>
                                        <table style="margin-left: 33%; margin-top: 1%; margin-bottom: 1%;">
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="chkRegTerm" runat="server" TabIndex="17" /></td>
                                                <td>I agree to the  <a href="http://www.mahabaahucruiseindia.com/cruise-policy/" target="_blank">Terms and Conditions.</a></td>
                                            </tr>
                                        </table>
                                        <div class="ak-btn-cont ak-btn-booking-cont">

                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Cust" CausesValidation="true" class="btn btn-primary agileits  wow fadeInLeft button1" CssClass="btn btn-info font16" Width="100%" OnClick="btnSubmit_Click" TabIndex="16" />
                                            <%-- <asp:Button ID="Button1" runat="server" Text="Button" class="btn btn-primary agileits  wow fadeInLeft button1" OnClick="Button1_Click" />--%>
                                            <%--<button type="submit" name="register" id="submit-register" class="btn btn-primary agileits  wow fadeInLeft button1"><span class="glyphicon agileits  glyphicon-arrow-left" aria-hidden="true"></span>Submit</button>--%>
                                            <%--<button type="button" id="register-close" class="btn btn-primary agileits  wow fadeInRight button2">Close<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button>--%>
                                        </div>


                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--<div class="payment-option">--%>
                        <div>
                            <div id="dvpayment" runat="server" visible="false">
                                <label class="text-left">PAYMENT OPTIONS</label>
                                <p style="padding-bottom: 2%;">Your credit card will be used to guarantee your booking for late arrival - it will not be charged.</p>
                                <form>
                                    <label>CARD DETAILS</label>

                                    <ul class="agileits list-unstyled ">
                                        <li class="agileits  wow fadeInLeft">
                                            <asp:TextBox ID="txtNamenCard" runat="server" class="text-box-dark agileits " Placeholder="Name On Card" onfocus="this.value = '';"></asp:TextBox>
                                            <%--<input required="" id="" runat="server" type="text" value="Name On Card" onblur="if (this.value == '') {this.value = 'Name On Card';}" name="Name On Card" />--%></li>
                                    </ul>
                                    <div class="row">
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtcardnumber" runat="server" class="text-box-dark agileits " Placeholder="Card Number" onfocus="this.value = '';"></asp:TextBox>
                                            <%--<input required=""  id="" runat="server" type="text" value="Card Number"  onblur="if (this.value == '') {this.value = 'Card Number';}" name="Card Number" />--%>
                                        </div>
                                        <div class="col-sm-4">

                                            <div class="book-pag-frm2 agileits ">
                                                <asp:TextBox ID="datepicker2" runat="server" class="date agileits " onfocus="this.value = '';" Placeholder="Expiry date"></asp:TextBox>
                                                <%--<input id="" type="text" runat="server" value=" Expiry Date" onblur="if (this.value == '') {this.value = 'Expiry Date';}" required="" />--%>
                                            </div>

                                        </div>
                                    </div>

                                </form>
                            </div>
                            <!-- ####################### -->


                            <div class="panel panel-default" id="dvBilling" runat="server" visible="false">
                                <div class="panel-heading collapsed" role="tab" id="headingTwo" data-toggle="collapse" data-parent="#accordion" href="#collapse1" aria-expanded="false" aria-controls="collapseTwo">
                                    <!-- <h4 class="panel-title">
                                  <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                    Collapsible Group Item #2
                                  </a>
                                </h4> -->
                                    <div class="panel-title">

                                        <span></span>
                                        <label style="padding-left: 4%;">
                                            I want to enter a billing address.
                                        </label>
                                    </div>
                                </div>
                                <div id="collapse1" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingTwo">
                                    <div class="panel-body">
                                        <asp:TextBox ID="txtBilingAddress" runat="server" Placeholder="Billing Adddress" class="text-box-dark agileits " onfocus="this.value = '';"></asp:TextBox>
                                        <%-- <input required=""  id="" runat="server" type="text" value="Email Address"  onblur="if (this.value == '') {this.value = 'Email Address';}" name="register-email" />--%>
                                    </div>
                                </div>
                            </div>



                             <asp:Panel ID="pnlguestlogin" runat="server" Visible="false">
                                <table id="table12" runat="server" >
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtguestverify" placeholder="Enter Code" runat="server"></asp:TextBox>
                                            <asp:Button ID="btnguestverify" runat="server" Text="Verify" class="btn btn-primary agileits  wow fadeInLeft button1" OnClick="btnguestverify_Click"   />

                                        </td>
                                        <td></td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="hdfguestcode" runat="server" />
                            </asp:Panel>


                            <div class="panel panel-default" runat="server" id="dvRefrence">
                                <div class="panel-heading collapsed" role="tab" id="Div1" runat="server"
                                    data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo" style="background-color: #f5f5f5; border-color: #ddd;">
                                    <div class="panel-title">
                                        <%--<span></span>--%>
                                        <label style="margin-bottom: 0;">Continue As a Guest</label>
                                        <img src="../../images/next-arrow.png" style="width: 16px; float: right; margin-top: 4px; transform: rotate(90deg);" />
                                    </div>
                                </div>

                                <%--                                <div class="panel-heading collapsed" role="tab" id="headingTwo" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                    <!-- <h4 class="panel-title">
                                  <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                    Collapsible Group Item #2
                                  </a>
                                </h4> -->
                                    <div class="panel-title">
                                        <span></span>
                                        <label style="padding-left: 4%;">
                                            
                                        </label>
                                    </div>
                                </div>--%>
                                <%-- <div id="collapseOne" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingOne">
                                    <div class="top-form panel-body" runat="server" id="Div2">
                                  <%--      <%-- <h4>Register</h4>--%>
                                <%--<div class="ak-form">--%>

                                <div id="collapseTwo" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingTwo">
                                    <div class="top-form panel-body">
                                        <div class="ak-form">

                                            <ul class="agileits ">
                                                <li class="agileits">

                                                    <%-- <select class="text-box-dark agileits" name="title" id="title-name" style="padding: 1%;">
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

                                                    <div style="width: 100%">

                                                        <div style="width: 86%; float: right;">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtFirstname1" ValidationGroup="Cust1"></asp:RequiredFieldValidator>
                                                            <asp:TextBox ID="txtFirstname1" runat="server" Placeholder="First Name" class="text-box-dark agileits " TabIndex="1"></asp:TextBox>

                                                        </div>

                                                        <div style="width: 50%;">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlList1" ValidationGroup="Cust1"></asp:RequiredFieldValidator>
                                                            <asp:DropDownList ID="ddlList1" runat="server" class="text-box-dark agileits" Style="margin-bottom: 0px; font-size: 12px; width: 26%; margin-top: 0%; border: 1px solid #9e9e9e; height: 52px;">

                                                                <asp:ListItem>Mr</asp:ListItem>
                                                                <asp:ListItem>Mrs</asp:ListItem>
                                                                <asp:ListItem>Miss</asp:ListItem>
                                                                <asp:ListItem>Ms</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <%--<input required=""  id="" runat="server" type="text" value="First Name" onblur="if (this.value == '') {this.value = 'First Name';}" name="firstname" />--%>
                                                </div>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtLastanme1" ValidationGroup="Cust1"></asp:RequiredFieldValidator>
                                                <div class="col-sm-6 agileits" style="padding-bottom: 15px;">

                                                    <asp:TextBox ID="txtLastanme1" runat="server" Placeholder="Last Name" Font-Size="Larger" class="text-box-dark agileits " TabIndex="2"></asp:TextBox>
                                                    <%--<input required="" id="" runat="server" type="text" value="Last Name" onblur="if (this.value == '') {this.value = 'Last Name';}" name="lastname" />--%>
                                                </div>
                                            </div>
                                            <div class="row agileits ">

                                                <div class="col-sm-6 agileits">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtEmailid1" ValidationGroup="Cust1"></asp:RequiredFieldValidator>
                                                    <%--<input required="" id="" runat="server" type="text" value="Email Address" onblur="if (this.value == '') {this.value = 'Email Address';}" name="register-email" />--%>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtMailAddress" ValidationGroup="Cust" ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator>
                                                    <asp:TextBox ID="txtEmailid1" runat="server" Placeholder="Email Address" class="text-box-dark agileits " TabIndex="3"></asp:TextBox>

                                                    <%-- <asp:TextBox ID="txtMailAddress" runat="server" PlaceHolder="Email Address" class="form-control"></asp:TextBox>--%>
                                                </div>

                                                <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtMobilephone1" ValidationGroup="Cust1"></asp:RequiredFieldValidator>

                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="Invalid" ControlToValidate="txtTelephone" ValidationGroup="Cust1" ValidationExpression="^([0-9]{8,15})$"></asp:RegularExpressionValidator>
                                                    <asp:TextBox ID="txtMobilephone1" runat="server" Placeholder="Contact No" MaxLength="10" TabIndex="4"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row agileits ">
                                                <div class="col-sm-6 agileits" style="padding-bottom: 15px;">


                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ErrorMessage="*" InitialValue="0" ForeColor="Red" ControlToValidate="ddlCountry1" ValidationGroup="Cust1"></asp:RequiredFieldValidator>
                                                    <asp:DropDownList ID="ddlCountry1" class="text-box-dark agileits" runat="server" CssClass="form-control" Style="font-size: 12px; border-radius: 0; padding: 17px 12px !important;" TabIndex="6">
                                                    </asp:DropDownList>

                                                    <%-- <input required=""  runat="server" id="" type="text" value="Address 1"  onblur="if (this.value == '') {this.value = 'Address 1';}" name="address1" />--%>
                                                </div>
                                                <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtState1" ValidationGroup="Cust1"></asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtState1" runat="server" Placeholder="State" class="text-box-dark agileits " TabIndex="7"></asp:TextBox>

                                                    <%--  <input required="" runat="server" id="" type="text" value="Address 2" onblur="if (this.value == '') {this.value = 'Address 2';}" name="address2" />--%>
                                                </div>
                                            </div>
                                            <div class="row agileits ">
                                                <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtCity1" ValidationGroup="Cust1"></asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtCity1" runat="server" Placeholder="City" class="text-box-dark agileits " TabIndex="8"></asp:TextBox>
                                                    <%-- <input required=""  id="txtCity" runat="server" type="text" value="City"  onblur="if (this.value == '') {this.value = 'City';}" name="city" />--%>
                                                </div>
                                                <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtAddress11" ValidationGroup="Cust1"></asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtAddress11" runat="server" Placeholder="Address1" class="text-box-dark agileits " TabIndex="9"></asp:TextBox>
                                                    <%--<input required=""  type="text" value="State" runat="server"  onblur="if (this.value == '') {this.value = 'State';}" name="state" />--%>
                                                </div>
                                            </div>
                                            <div class="row agileits ">
                                                <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtadress22" ValidationGroup="Cust1"></asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtadress22" runat="server" Placeholder="Address2" class="text-box-dark agileits " TabIndex="10"></asp:TextBox>

                                                    <%-- <input required="" type="text" value="Postcode" id="" runat="server" onblur="if (this.value == '') {this.value = 'Postcode';}" name="postcode" />--%>
                                                </div>
                                                <div class="col-sm-6 agileits" style="padding-bottom: 15px;">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtPostcode1" ValidationGroup="Cust1"></asp:RequiredFieldValidator>
                                                   
                                                    <asp:TextBox ID="txtPostCode1" runat="server" class="text-box-dark agileits " Placeholder="Post Code" MaxLength="6" TabIndex="11"></asp:TextBox>


                                                    <%--<input required="" class="text-box-dark agileits " id="" runat="server" type="password" value="Password" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Password';}" name="register-password" />--%>
                                                </div>
                                            </div>
                                            <%--<strong>Terms & Conditions</strong>--%>
                                            <div visible="false" style="width: 100%; height: 100px; overflow: auto; margin-bottom: 1%; margin-top: 2%;" runat="server" id="dvmvMahabahu">
                                                <strong>Terms and Conditions:</strong>
                                                <br />
                                                <p>Information contained in the ADVENTURE RESORTS & CRUISES brochure, website or advertising materials is not an offer or a contract. The transportation of guests and baggage on the MV MAHABAAHU river cruise vessel is provided solely by ADVENTURE RESORTS & CRUISES and is governed by the terms and conditions printed on the Guest Booking Contract. For complete information on terms and conditions, itinerary, liability of ADVENTURE RESORTS & CRUISES, and all sections mentioned below, refer to the Guest Booking Contract below.</p>
                                                <br />
                                                <br />
                                                <strong>PAYMENT AND DEPOSIT POLICY:</strong>
                                                <br />
                                                A deposit for cruise fare of 25% per person is required to secure a confirmed booking. For Charter bookings this deposit is NON REFUNDABLE.
                                            <br />
                                                <br />
                                                90 DAYS PRIOR TO DATE OF SAILING: 75% Per Person
                                            <br />
                                                <br />
                                                <p>
                                                    For the Christmas and New Year holiday, the full balance must be paid 120 days prior.
All reservations are subject to cancellation if payments are not received by the due date. Upon full payment by the participant(s) of the amount specified as the Full Fare (Invoice to be referred), ADVENTURE RESORTS & CRUISES agrees to arrange for the provision of the services as described herein and as modified by any supplementary marketing materials.
                                                </p>
                                                <br />
                                                <br />
                                                <p>Upon receipt of your deposit or full payment or upon the issuance of the Guest Booking Contract directly or by a Booking agent or upon receipt of a confirmation letter or final invoice from us, both the Guest and the Carrier will be fully bound by all of the terms and conditions of the Guest Booking Contract.</p>
                                                <br />
                                                <br />
                                                <strong>RESERVATIONS</strong>
                                                <br />
                                                Bank wire transfer which is subject to bank wire fee
                                            <br />
                                                PayPal system with additional 5% Transaction fee
                                            <br />
                                                Through Amex, Master and Visa credit card with an additional 3% Transaction fee
                                            <br />
                                                <br />
                                                <strong>CANCELLATION POLICY</strong>
                                                <br />
                                                <p>All cancellations must be in writing. The following cancellation terms will be applicable for all written cancellations received prior to departure up to the scheduled time of departure.</p>
                                                <br />
                                                <br />
                                                <strong>CANCELLATION TERMS FOR CHARTER BOOKINGS</strong>
                                                <br />
                                                <p>25% of the price will be charged for cancellation after the deposit has been paid 90-75 days prior, 50% of the charter cost will be charged 74-59 days prior, 75% of the charter cost will be charged 60 days and less, 100% of the charter cost will be charged</p>
                                                <br />
                                                <br />
                                                <p><strong>CANCELLATION TERMS FOR F.I.T. AND GROUP DEPARTURES APPLICABLE ON THE FULL FARE:</strong></p>
                                                <br />
                                                <p>120 to 90 days prior, 25% per person of the fare paid will be charged 89-76 days prior, 50% per person will be charged 75-46 days prior, 75% per person will be charged 45 days and less, 100% per person will be charged *Full Fare is defined as the full cost of any cruise package as mentioned in the price chart. Note: for Christmas and New Year Departures, 100% cancellation is charged 90 days and under from the sailing date.</p>
                                                <br />
                                                <br />
                                                <strong>REVISIONS/CHANGES</strong>
                                                <br />
                                                Once a cruise booking has a full deposit, all changes are subject to a INR 3,400 charge per change, provided the said change is prior to 90 days before date of departure/sailing. The following situations are considered as Revisions and penalties will apply:
                                            <br />
                                                <br />
                                                Changes to departure date
                                            <br />
                                                <br />
                                                Substitutions of itinerary
                                            <br />
                                                <br />
                                                Substitution of another person for original booked guest(s)
                                            <br />
                                                <br />
                                                Changing to a promotional fare
                                            <br />
                                                <br />
                                                <strong>DOCUMENTS</strong>
                                                <br />

                                                Documents will be issued approximately 3 weeks prior to departure.
                                            <br />
                                                <br />

                                                <strong>TRANSFERS</strong>
                                                <br />
                                                Only round trip airport-to-ship transfers are included.
                                            <br />
                                                <br />
                                                <strong>TRAVEL DOCUMENTATION/VISAS</strong>
                                                <br />
                                                <p>
                                                    All guests must have passports valid for six months following disembarkation and necessary visas when boarding. Guests are advised to check with their Booking agent to determine which documents they must obtain. Due to airline security measures, your passport name must match your airline ticket name or you may be denied boarding. Adventure Resorts and Cruises accepts no responsibility for obtaining required visas or for advising guests of visa or other immigration requirements.
                                                </p>
                                                <br />
                                                <br />
                                                <strong>CHILDREN/MINORS</strong>
                                                <br />
                                                <p>
                                                    We accept children onboard sharing a cabin with parents or guardians. They will be the sole responsibility of the parents/ guardians and must never be left unattended. For Children of 15 years and above, a separate cabin will need to be booked.
                                                </p>
                                                <br />
                                                <br />
                                                <strong>DIFFERENTLY ABLED GUESTS</strong>
                                                <br />
                                                <p>
                                                    We ask that you advise us at the time of booking of any disability or special needs you may have while on your Adventure Resorts & Cruises itinerary. Most transportation services, including the motorcoaches, are not equipped with elevators or wheelchair ramps. Adventure Resorts & Cruises India will make reasonable attempts to accommodate the special needs of travelers with disabilities, but we are not responsible in the event we are unable to do so, or there is a denial of service by vessel operators, air carriers, hotels, restaurants or other independent suppliers. We regret that we cannot provide individual assistance to a guest for walking, dining, getting on and off vessels, motor coaches and other vehicles, or other personal needs. A qualified and physically able companion must accompany travelers who need such assistance and must assume full responsibility for their wellbeing. The guest assumes the full risk of use and of any prohibitions imposed by vendors.
                                                </p>
                                                <br />
                                                <br />
                                                <strong>TIPS</strong>

                                                <br />
                                                <p>
                                                    It is customary to give cruise gratuities, subject to your satisfaction of services rendered. Gratuities on board and on land are not included in your Full Fare.
                                                </p>

                                            </div>
                                            <table style="margin-left: 33%; margin-top: 1%; margin-bottom: 1%;">
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chkTerm" runat="server" TabIndex="11" /></td>
                                                    <td>I agree to the  <a href="http://www.mahabaahucruiseindia.com/cruise-policy/" target="_blank">Terms and Conditions</a></td>
                                                </tr>
                                            </table>
                                            <div style="width: 16rem !important; margin: 0 auto;">
                                                <asp:Button ID="Button1" runat="server" Text="Submit" CausesValidation="true" TabIndex="12" Style="width: 100%;" class="btn btn-primary agileits  wow fadeInLeft button1" CssClass="btn btn-info font16" ValidationGroup="Cust1" OnClick="Button1_Click" />
                                            </div>
                                        </div>

                                        <%-- <asp:Button ID="Button1" runat="server" Text="Button" class="btn btn-primary agileits  wow fadeInLeft button1" OnClick="Button1_Click" />--%>
                                        <%--<button type="submit" name="register" id="submit-register" class="btn btn-primary agileits  wow fadeInLeft button1"><span class="glyphicon agileits  glyphicon-arrow-left" aria-hidden="true"></span>Submit</button>--%>
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-default" id="divspcl" runat="server" visible="false">
                                <div class="panel-heading collapsed" role="tab" id="headingTwo" data-toggle="collapse" data-parent="#accordion" href="#collapseThree" aria-expanded="false" aria-controls="collapseTwo">
                                    <!-- <h4 class="panel-title">
                                  <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                    Collapsible Group Item #2
                                  </a>
                                </h4> -->
                                    <div class="panel-title">
                                        <span></span>
                                        <label style="padding-left: 4%;">
                                            I have special requests.
                                        </label>
                                    </div>
                                </div>
                                <div id="collapseThree" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingTwo">
                                    <div class="panel-body">
                                        <p style="padding: 2%;">Let us know if there is anything else we can help you with, we are always happy to help where ever we can.</p>
                                        <textarea class="form-control" rows="3" id="specialrequest" runat="server"></textarea>
                                    </div>
                                </div>
                            </div>
                            <div class="ak-btn-cont ak-btn-booking-cont" runat="server" id="dvbtn" visible="false">
                                <asp:Button ID="buttonUpdate" runat="server" Text="Update" Visible="false" class="btn btn-primary agileits  wow fadeInLeft button1" CssClass="btn btn-info font16" OnClick="buttonUpdate_Click" />
                                <%-- <asp:Button ID="Button1" runat="server" Text="Button" class="btn btn-primary agileits  wow fadeInLeft button1" OnClick="Button1_Click" />--%>
                                <%--<button type="submit" name="register" id="submit-register" class="btn btn-primary agileits  wow fadeInLeft button1"><span class="glyphicon agileits  glyphicon-arrow-left" aria-hidden="true"></span>Submit</button>--%>
                                <%--<button type="button" id="register-close" class="btn btn-primary agileits  wow fadeInRight button2">Close<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button>--%>
                            </div>
                            <asp:Panel ID="customerLogin" runat="server">
                                <table id="tableVerify" runat="server" visible="false">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtCode" placeholder="Enter Code" runat="server"></asp:TextBox>
                                            <asp:Button ID="btnVerify" runat="server" Text="Verify" class="btn btn-primary agileits  wow fadeInLeft button1" OnClick="btnVerify_Click" />

                                        </td>
                                        <td></td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="hfVCode" runat="server" />
                            </asp:Panel>
                            <div style="width: 100%; float: none;">
                                <asp:Panel ID="pnlFullDetails" class="CSSTableGenerator" Width="100%" runat="server" Style="box-shadow: 3px 3px 9px 2px rgba(0,0,0,0.4); padding-top: 1%; border: 1px solid rgba(0,0,0,0.5) !important; border-radius: 4px; padding-bottom: 35px;">
                                    <div id="BookRef" runat="server" style="width: 90%; margin: auto; padding-top: 5%; padding-bottom: 5px;">
                                        <table id="tbl-booking-name" style="border: 1px solid #fff; width: 100%;">
                                            <tr>
                                                <td class="auto-style5" style="padding: 0px 4px 0px 8px; font-weight: bold;">Enter Booking Reference Name.</td>
                                                <td style="text-align: left">
                                                    <asp:TextBox ID="txtBookRef" runat="server" Width="100%"></asp:TextBox>

                                                    <asp:RequiredFieldValidator ID="ReqBookRef" runat="server" ControlToValidate="txtBookRef" ErrorMessage="*" ValidationGroup="Pay"></asp:RequiredFieldValidator></td>
                                            </tr>
                                        </table>
                                    </div>
                                    <h4 style="font-size: 17px; padding-top: 14px; padding-bottom: 14px; font-family: 'Montserrat', sans-serif;">Payment Details</h4>

                                    <table class="table table-bordered" id="tbl-full-detail" style="width: 90%; margin: 0 auto; border: 1px solid #ddd;">
                                        <tr style="background-color: #ECECEC !important;">
                                            <td style="font-weight: bold; width: 27%; padding-right: 12%; padding-top: 10px;">Invoice To</td>
                                            <td style="padding-left: 30px !important; padding-right: 30px !important;">
                                                <asp:Label ID="lblAgentName" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr style="background-color: #f9f9f9 !important;">
                                            <td style="font-weight: bold; padding-right: 7%; padding-top: 10px;">
                                                <asp:Label ID="lblBilling" runat="server" Text="Billing Address "></asp:Label></td>
                                            <td style="padding-left: 30px !important; padding-right: 30px !important;">
                                                <asp:Label ID="lblBillingAddress" runat="server" Text=""></asp:Label></td>

                                        </tr>
                                        <%-- <tr style="background-color: #ECECEC !important; height: 40px;">
                                            <td style="font-weight: bold; padding-right: 6%; padding-top: 10px;">
                                                <asp:Label ID="Label3" runat="server" Text="Special Request  "></asp:Label></td>
                                            <td style="">
                                                <asp:Label ID="lblSpecialRequest" runat="server" Text=""></asp:Label></td>

                                        </tr>--%>
                                        <tr style="background-color: #ECECEC !important;">
                                            <td style="font-weight: bold; padding-right: 5%; padding-top: 10px;">
                                                <asp:Label ID="lbPayment" runat="server" Text="Payment Method "></asp:Label></td>
                                            <td style="padding-left: 30px !important; padding-right: 30px !important;">
                                                <asp:Label ID="lbPaymentMethod" runat="server" Text=""></asp:Label><asp:HiddenField ID="hdnfPhoneNumber" runat="server" />
                                            </td>

                                        </tr>

                                        <tr style="display: none;">
                                            <td>&nbsp;</td>
                                            <td>
                                                <asp:HiddenField ID="hdnfCreditLimit" runat="server" />
                                            </td>
                                        </tr>
                                    </table>



                                    <asp:Panel ID="panelwithoutCreditAgent" Style="padding-top: 15px" Width="100%" runat="server" Font-Size="Medium">
                                        <div>

                                            <table class="table table-bordered" style="width: 90%; margin: 0 auto; border: 1px solid #ddd;">
                                                <tr style="background-color: #ECECEC !important;">
                                                    <td style="font-weight: bold; width: 27%; padding-right: 8%; padding-top: 10px;" class="auto-style3">Total amount</td>
                                                    <td style="padding-left: 30px !important; padding-right: 30px !important;" class="auto-style4">
                                                        <asp:Label ID="lbltotAmt" runat="server" Text="Label"></asp:Label></td>

                                                </tr>
                                                <tr style="background: #f9f9f9 !important;" id="getdiscount" runat="server">
                                                    <td style="font-weight: bold; padding-right: 17%; padding-top: 10px;">Discount(<asp:Label ID="lblDiscountper" runat="server" Text=" "></asp:Label>)</td>
                                                    <td style="padding-left: 30px !important; padding-right: 30px !important;">
                                                        <asp:Label ID="lblDiscount" runat="server" Text=" "></asp:Label>
                                                    </td>

                                                </tr>
                                                <tr style="background: #ECECEC !important;">
                                                    <td style="font-weight: bold; padding-right: 17%; padding-top: 10px;">Taxable Amount</td>
                                                    <td style="padding-left: 30px !important; padding-right: 30px !important;">
                                                        <asp:Label ID="lbltaxin" runat="server" Text=" "></asp:Label>
                                                    </td>

                                                </tr>
                                                <tr style="background: #f9f9f9 !important;">
                                                    <td style="font-weight: bold; padding-right: 19%; padding-top: 10px;">GST <%# Session["gettaxpercentage"].ToString() %></td>
                                                    <td style="padding-left: 30px !important; padding-right: 30px !important;">
                                                        <asp:Label ID="lblTax" runat="server" Text=" "></asp:Label>
                                                    </td>

                                                </tr>
                                                <tr style="background: #f9f9f9 !important;" runat="server" id="lvlt" visible="false">
                                                    <td style="font-weight: bold; padding-right: 17%; padding-top: 10px;">Total</td>
                                                    <td style="padding-left: 30px !important; padding-right: 30px !important;">
                                                        <asp:Label ID="lblalltotal" runat="server" Text=" "></asp:Label>
                                                    </td>

                                                </tr>

                                                <tr style="background-color: #ECECEC !important;">
                                                    <td style="font-weight: bold; padding-right: 17%; padding-top: 10px;">Gross</td>
                                                    <td style="padding-left: 30px !important; padding-right: 30px !important;">
                                                        <asp:Label ID="lblGross" runat="server" Text=" "></asp:Label>
                                                    </td>

                                                </tr>
                                                <tr style="background: #f9f9f9 !important;">
                                                    <td style="font-weight: bold; padding-right: 4%; padding-top: 10px;">Advance Amount</td>
                                                    <td style="padding-left: 30px !important; padding-right: 30px !important;">
                                                        <asp:Label ID="lblCurrency" runat="server" Text=" "></asp:Label><asp:Label ID="txtPaidAmt" runat="server"></asp:Label>
                                                        <asp:Label ID="lbl25" runat="server" Text=" "></asp:Label></td>

                                                </tr>
                                                <tr style="background-color: #ECECEC !important;" id="trbalanceamount" runat="server">
                                                    <td style="font-weight: bold; padding-right: 4%; padding-top: 10px;">Balance Amount</td>
                                                    <td style="padding-left: 30px !important; padding-right: 30px !important;">
                                                        <asp:Label ID="lblBalanceAmt" runat="server" Text=" "></asp:Label>
                                                    </td>

                                                </tr>
                                                <tr style="background: #f9f9f9 !important;" id="trbalancedate" runat="server">
                                                    <td style="font-weight: bold; padding-right: 8%; padding-top: 10px;">Balance Payment Date</td>
                                                    <td style="padding-left: 30px !important; padding-right: 30px !important;">
                                                        <asp:Label ID="lblBalancedate" runat="server" Text=" "></asp:Label>
                                                    </td>

                                                </tr>
                                            </table>

                                            <br />
                                            <div style="padding-left: 30%;">
                                                <asp:CheckBox ID="chkterms" Visible="false" runat="server" OnCheckedChanged="chkterms_CheckedChanged" />
                                                <asp:Label ID="Label3" Visible="false" Style="padding-left: 2%;" runat="server" Text="I agree to the terms and condition"></asp:Label>
                                                <asp:Label ID="lblT" runat="server" Text="By proceeding I accept the"></asp:Label>
                                                <%--<asp:LinkButton ID="lbtnTandC" Style="padding-left: 2%;" Font-Underline="True"  runat="server" Text="Terms & Conditions"  OnClick="lbtnTandC_Click"></asp:LinkButton>--%>
                                                <a id="linkTandCReg" runat="server" target="_blank">terms and conditions</a>
                                            </div>
                                        </div>
                                        <br />
                                    </asp:Panel>
                                </asp:Panel>
                                <div style="height: 40px"></div>
                                <%--    <asp:Panel ID="fhtfr"  Width="70%" runat="server">
            <div>
                  <p> Amount</p>
                     <p><asp:TextBox ID="txtPaidAmt" runat="server"></asp:TextBox>
                         <asp:Label ID="lblCurrency" runat="server" Text="Label"></asp:Label>
                     </p>
                            </div>

                 
                    <br />
    </asp:Panel>--%>
                                <%--    <div style="height: 40px"></div>--%>
                                <center>
                      
                        </center>
                            </div>
                            <%--  <div class="ak-btn-cont ak-btn-booking-cont check-room">
                                <button type="submit" name="register" id="submit-register" class="btn btn-primary agileits  wow fadeInLeft button1"><span class="glyphicon agileits  glyphicon-arrow-left" aria-hidden="true"></span>Add another room</button>
                                <button type="button" id="register-close" class="btn btn-primary agileits  wow fadeInRight button2 pull-right">Reveiw<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button>
                            </div>--%>

                            <div id="pnlBookButton" width="70%" runat="server" class="text-center">
                                <asp:Button ID="btnPayProceed" runat="server" Text="Proceed For Payment" CssClass="btn btn-info btnWidth100 btnFont" Font-Size="Medium" OnClick="btnPayProceed_Click" />
                                <asp:Label ID="lblPaymentErr" runat="server" ForeColor="#FF3300"></asp:Label>
                                <asp:HiddenField ID="hftxtpaidamt" runat="server" />
                                <asp:HiddenField ID="hdnfTotalPaybleAmt" runat="server" />
                                <br />
                                <asp:Label ID="lblBookingLockFound" runat="server" ForeColor="#FF3300" Visible="false"></asp:Label>
                                <br />
                                <asp:HyperLink ID="lnkBackToCruiseBooking" runat="server" CssClass="applink" Text="Back To Rooms Selection" Visible="false"></asp:HyperLink>
                            </div>

                            <!-- ####################### -->
                        </div>
                </div>
                <div class="col-sm-5 left-col" style="box-shadow: 3px 3px 9px 2px rgba(0,0,0,0.4);">
                    <h4 class="text-left">Your Reservation</h4>
                    <br />
                    <div class="col-sm-12 top-div" style="padding-left: 0; padding-right: 0;">
                        <asp:Image ID="Image1" Width="100%" runat="server" />
                        <%-- <img src='/<%# Session["forimage"].ToString() %>' class="img-responsive" alt="room-" runat="server" id="imgcruise">--%>
                    </div>
                    <br />
                    <div class="col-sm-12" style="margin-bottom: 15px;">
                        <label style="font-size: 18px; margin: 4px 0px 5px 0px;">Rate Includes</label>
                        <p style="text-align: justify; font-size: 14px;">
                            <asp:Label ID="lblPakagedescrip" runat="server" Text=" "></asp:Label><%# Session["Packagedesc"].ToString() %>
                        </p>

                    </div>
                    <br />
                    <div class="col-sm-6">
                        <label>Check In:</label>

                        <%--<img src="images/down.png" class="img-responsive" alt="">--%>
                        <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                        <%-- <select class="form-control" id="sel1">
                                                                        <option>1</option>
                                                                        <option>2</option>
                                                                        <option>3</option>
                                                                        <option>4</option>
                                                                        <option>5</option>
                                                                        <option>6</option>
                                                                    </select>--%>
                    </div>
                    <div class="col-sm-6">
                        <label>Check Out:</label>

                        <%--<img src="images/down.png" class="img-responsive" alt="">--%>
                        <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
                        <%-- <select class="form-control" id="sel1">
                                                                        <option>1</option>
                                                                        <option>2</option>
                                                                        <option>3</option>
                                                                        <option>4</option>
                                                                        <option>5</option>
                                                                        <option>6</option>
                                                                    </select>--%>
                    </div>
                    <br />
                    <div class="row room-detail">
                        <%
                            if (dtrpax != null)
                            {
                        %>
                        <% for (int i = 0; i < dtrpax.Rows.Count; i++)
                            {
                                try
                                {
                        %>
                        <h3 style="font-size: 22px; padding-bottom: 11px; margin-top: 3%;"><%Response.Write((dtrpax.Rows[i]["categoryName"]).ToString()); %></h3>


                        <div class="col-sm-12 middle-div">
                            <div class="row">
                                <div class="col-sm-12 top-div">
                                    <div class="row" style="padding-top: 25px;">
                                        <!-- <div class="col-sm-3">
                                            <label>Room</label>
                                            <p>Pool Facing Dlx Cottage</p>
                                        </div> -->

                                        <div class="col-sm-6">
                                            <label>Room Type:</label>

                                            <%--<img src="images/down.png" class="img-responsive" alt="">--%>
                                            <%Response.Write((dtrpax.Rows[i]["RoomType"]).ToString()); %>
                                            <%-- <asp:Label ID="Label2" runat="server" Text='<%# Eval("RoomType") %>'></asp:Label>--%>
                                            <%-- <select class="form-control" id="sel1">

                                                                        <option>No</option>
                                                                        <option>Yes</option>
                                                                    </select>--%>
                                        </div>
                                        <div class="col-sm-6" style="width: 30%;">
                                            <label>Guests:</label>

                                            <%--<img src="images/down.png" class="img-responsive" alt="">--%>
                                            <%Response.Write((dtrpax.Rows[i]["Pax"]).ToString()); %>
                                            <%-- <asp:Label ID="Label1" runat="server" Text='<%# Eval("Pax") %>'></asp:Label>--%>
                                            <%-- <select class="form-control" id="sel1">
                                                                        <option>1</option>
                                                                        <option>2</option>
                                                                        <option>3</option>
                                                                        <option>4</option>
                                                                        <option>5</option>
                                                                        <option>6</option>
                                                                    </select>--%>
                                        </div>
                                        <div class="col-sm-6" style="width: 30%;">
                                            <label>Room No:</label>

                                            <%--<img src="images/down.png" class="img-responsive" alt="">--%>
                                            <%Response.Write((dtrpax.Rows[i]["RoomNumber"]).ToString()); %>
                                            <%-- <asp:Label ID="Label1" runat="server" Text='<%# Eval("Pax") %>'></asp:Label>--%>
                                            <%-- <select class="form-control" id="sel1">
                                                                        <option>1</option>
                                                                        <option>2</option>
                                                                        <option>3</option>
                                                                        <option>4</option>
                                                                        <option>5</option>
                                                                        <option>6</option>
                                                                    </select>--%>
                                        </div>
                                        <%-- <div class="col-sm-4">
                                                                <label>Room</label>
                                                                <p>1</p>
                                                            </div>--%>
                                    </div>
                                </div>

                            </div>
                            <!-- middle-div -->
                            <!-- <div class="row">
                                <div class="col-sm-12 middle-div">
                                    <p><strong>Detail:</strong>Stone Cottages (201 - 208)</p>
                                </div>

                            </div> -->
                            <!-- middle-div -->

                        </div>
                        <div class="col-sm-12 bottom-div">
                            <div class="row" style="margin: 0;">
                                <!-- <div class="col-sm-3">
                                    <label>Detail</label>
                                    <p>Stone Cottages (201 - 208)</p>
                                </div> -->


                                <div class="col-sm-6" style="float: left;">
                                    <p style="font-size: 18px;" class="text-left">Price Per Person: </p>

                                    <%--<p style="font-size: 18px;" class="text-left">Discount(<%# Eval("Discount") %>):  </p>--%>
                                    <%
                                        if (dtrpax.Rows[i]["Discount"].ToString() == "0%")
                                        {
                                            //Response.Write("<span style='color:#f99646'> " + dtres.Rows[i]["Availability"].ToString() + " </span>");
                                        }
                                        else
                                        {
                                            //<p style="font-size: 18px;" class="text-left">Gross: </p>
                                            Response.Write(" <p style='font-size: 18px;' class='text-left'>Gross: </p>");
                                            Response.Write(" <p style='font-size: 18px;' class='text-left'>Discount(" + dtrpax.Rows[i]["Discount"].ToString() + "):</p>");
                                        }


                                    %>
                                    <%--<p style="font-size: 18px;" class="text-left">Discount( <%Response.Write((dtrpax.Rows[i]["Discount"]).ToString()); %>):  </p>--%>
                                    <p style="font-size: 18px;" class="text-left">Taxable amount:  </p>
                                    <%


                                        Response.Write(" <p style='font-size: 18px;' class='text-left'>GST (" + dtrpax.Rows[i]["Tax"].ToString() + "%):</p>");



                                    %>
                                    <%--   <p style="font-size: 18px;" class="text-left">GST@<%# Session["gettaxpercentage"].ToString() %>: </p>--%>

                                    <%--<p style="font-size: 18px;" class="text-left">Gross:  </p>--%>

                                    <%--<button class="btn btn-primary wow agileits  fadeInUp pull-right" id="book-resort">Add to Cart<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button>--%>
                                </div>
                                <div class="col-sm-6">
                                    <p style="font-size: 18px; " class="text-right">
                                        <%-- <strong>INR <%# Eval("Pricewithouttax1") %></strong>--%>

                                        <% if (ddlCurrency.Text != "USD")
    {
        currency((dtrpax.Rows[i]["Pricewithouttax1"]).ToString(), "INR");
        
                                                 Response.Write(" <p style='font-size: 18px;' class='text-right'><strong>USD " +  ViewState["Comman"] + " </strong></p>");
    }
    else
    {
                                                  Response.Write(" <p style='font-size: 18px;' class='text-right'><strong>INR " + dtrpax.Rows[i]["Pricewithouttax1"].ToString() + " </strong></p>");
         
    } %>
                                    </p>
                                    <%--<p style="font-size: 18px;" class="text-right"><strong>INR   <%Response.Write((dtrpax.Rows[i]["Total"]).ToString()); %></strong> </p>--%>
                                    <%
                                        if (dtrpax.Rows[i]["Discountprice"].ToString() == "0")
                                        {
                                            //Response.Write("<span style='color:#f99646'> " + dtres.Rows[i]["Availability"].ToString() + " </span>");
                                        }
                                        else
                                        {
                                            if (ddlCurrency.Text != "USD")
                                            {
                                                currency(dtrpax.Rows[i]["Total"].ToString(), "INR");
                                                Response.Write(" <p style='font-size: 18px;' class='text-right'><strong>USD " +  ViewState["Comman"] + " </strong></p>");

                                                 currency(dtrpax.Rows[i]["Discountprice"].ToString(), "INR");
                                                  Response.Write(" <p style='font-size: 18px;' class='text-right'><strong>USD " + ViewState["Comman"] + " </strong></p>");
                                            }
                                            else
                                            {
                                                Response.Write(" <p style='font-size: 18px;' class='text-right'><strong>INR " + dtrpax.Rows[i]["Total"].ToString() + " </strong></p>");
                                                 Response.Write(" <p style='font-size: 18px;' class='text-right'><strong>INR " + dtrpax.Rows[i]["Discountprice"].ToString() + " </strong></p>");
                                            }
                                           
                                        }


                                    %>
                                    <%--<p style="font-size: 18px;" class="text-right"><strong>INR  <%Response.Write((dtrpax.Rows[i]["Discountprice"]).ToString()); %></strong> </p>--%>

                                    <%
                                        if(ddlCurrency.Text != "USD")
                                        {
                                              currency(dtrpax.Rows[i]["taxablepamt"].ToString(), "INR");
                                                Response.Write(" <p style='font-size: 18px;' class='text-right'><strong>USD " +  ViewState["Comman"] + " </strong></p>");

                                                 currency(dtrpax.Rows[i]["Tax1"].ToString(), "INR");
                                                  Response.Write(" <p style='font-size: 18px;' class='text-right'><strong>USD " + ViewState["Comman"] + " </strong></p>");
                                        }
                                        else
                                        {
                                             Response.Write(" <p style='font-size: 18px;' class='text-right'><strong>INR " + dtrpax.Rows[i]["taxablepamt"].ToString() + " </strong></p>");
                                                 Response.Write(" <p style='font-size: 18px;' class='text-right'><strong>INR " + dtrpax.Rows[i]["Tax1"].ToString() + " </strong></p>");
                                        }
                                         %>
                                  <%--  <p style="font-size: 18px;" class="text-right"><strong>INR  <%Response.Write((dtrpax.Rows[i]["taxablepamt"]).ToString()); %></strong> </p>
                                    <p style="font-size: 18px;" class="text-right"><strong>INR  <%Response.Write((dtrpax.Rows[i]["Tax1"]).ToString()); %></strong> </p>--%>

                                    <%-- <p style="font-size: 18px;" class="text-right"><strong><%# Eval("Totalprice") %></strong> </p>--%>

                                    <%--<button class="btn btn-primary wow agileits  fadeInUp pull-right" id="book-resort">Add to Cart<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button>--%>
                                </div>
                                <div class="col-sm-12 gross-value" style="margin: 0;">
                                    <div class="col-sm-6">

                                        <p style="font-size: 18px; margin-left: -6%;" class="text-left">Total: </p>
                                        <%--<button class="btn btn-primary wow agileits  fadeInUp pull-right" id="book-resort">Add to Cart<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button>--%>
                                    </div>
                                    <div class="col-sm-6">
                                        <%
                                            if(ddlCurrency.Text != "USD")
                                            {
                                                 currency(dtrpax.Rows[i]["Totalprice"].ToString(), "INR");
                                                Response.Write(" <p style='font-size: 18px; margin-right: -7%;' class='text-right'><strong>USD " +  ViewState["Comman"] + " </strong></p>");
                                            }
                                            else
                                            {
                                                  Response.Write(" <p style='font-size: 18px; margin-right: -7%;' class='text-right'><strong>INR " + dtrpax.Rows[i]["Totalprice"].ToString() + " </strong></p>");
                                            }
                                             %>

                                      <%--  <p style="font-size: 18px; margin-right: -7%;" class="text-right"><strong>INR  <%Response.Write((dtrpax.Rows[i]["Totalprice"]).ToString()); %></strong> </p>--%>
                                        <%--<button class="btn btn-primary wow agileits  fadeInUp pull-right" id="book-resort">Add to Cart<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button>--%>
                                    </div>
                                </div>



                            </div>


                        </div>

                        <br />
                        <%
                                    }
                                    catch
                                    {
                                    }
                                }
                            } %>

                        <asp:GridView ID="GridRoomPaxDetail" Visible="false" runat="server" ForeColor="#333333" GridLines="None" Font-Size="Medium" AutoGenerateColumns="False" Width="100%">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <h3 style="font-size: 22px; padding-bottom: 11px; margin-top: 3%;"><%# Eval("categoryName") %></h3>

                                        <div class="col-sm-12 middle-div">
                                            <div class="row">
                                                <div class="col-sm-12 top-div">
                                                    <div class="row" style="padding-top: 25px;">
                                                        <!-- <div class="col-sm-3">
                                            <label>Room</label>
                                            <p>Pool Facing Dlx Cottage</p>
                                        </div> -->

                                                        <div class="col-sm-6">
                                                            <label>Room Type:</label>

                                                            <%--<img src="images/down.png" class="img-responsive" alt="">--%>
                                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("RoomType") %>'></asp:Label>
                                                            <%-- <select class="form-control" id="sel1">

                                                                        <option>No</option>
                                                                        <option>Yes</option>
                                                                    </select>--%>
                                                        </div>
                                                        <div class="col-sm-6" style="width: 30%;">
                                                            <label>Guests:</label>

                                                            <%--<img src="images/down.png" class="img-responsive" alt="">--%>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Pax") %>'></asp:Label>
                                                            <%-- <select class="form-control" id="sel1">
                                                                        <option>1</option>
                                                                        <option>2</option>
                                                                        <option>3</option>
                                                                        <option>4</option>
                                                                        <option>5</option>
                                                                        <option>6</option>
                                                                    </select>--%>
                                                        </div>
                                                        <div class="col-sm-6" style="width: 30%;">
                                                            <label>Room No:</label>

                                                            <%--<img src="images/down.png" class="img-responsive" alt="">--%>
                                                            <asp:Label ID="lblRoomNo" runat="server" Text='<%# Eval("RoomNumber") %>'></asp:Label>
                                                            <%-- <select class="form-control" id="sel1">
                                                                        <option>1</option>
                                                                        <option>2</option>
                                                                        <option>3</option>
                                                                        <option>4</option>
                                                                        <option>5</option>
                                                                        <option>6</option>
                                                                    </select>--%>
                                                        </div>
                                                        <%-- <div class="col-sm-4">
                                                                <label>Room</label>
                                                                <p>1</p>
                                                            </div>--%>
                                                    </div>
                                                </div>

                                            </div>
                                            <!-- middle-div -->
                                            <!-- <div class="row">
                                <div class="col-sm-12 middle-div">
                                    <p><strong>Detail:</strong>Stone Cottages (201 - 208)</p>
                                </div>

                            </div> -->
                                            <!-- middle-div -->

                                        </div>
                                        <div class="col-sm-12 bottom-div">
                                            <div class="row" style="margin: 0;">
                                                <!-- <div class="col-sm-3">
                                    <label>Detail</label>
                                    <p>Stone Cottages (201 - 208)</p>
                                </div> -->


                                                <div class="col-sm-6" style="float: left;">

                                                    <p style="font-size: 18px;" class="text-left">Price Per Person: </p>
                                                    <p style="font-size: 18px;" class="text-left">Gross: </p>
                                                    <p style="font-size: 18px;" class="text-left">Discount(<%# Eval("Discount") %>):  </p>
                                                    <p style="font-size: 18px;" class="text-left">Taxable amount:  </p>
                                                    <p style="font-size: 18px;" class="text-left">GST <%# Session["gettaxpercentage"].ToString() %>: </p>

                                                    <%--<p style="font-size: 18px;" class="text-left">Gross:  </p>--%>

                                                    <%--<button class="btn btn-primary wow agileits  fadeInUp pull-right" id="book-resort">Add to Cart<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button>--%>
                                                </div>
                                                <div class="col-sm-6">
                                                    <p style="font-size: 18px;" class="text-right">
                                                       
                                                        <strong>INR <%# Eval("Pricewithouttax1") %></strong>
                                                    </p>
                                                    <p style="font-size: 18px;" class="text-right"><strong>INR  <%# Eval("Total") %></strong> </p>
                                                    <p style="font-size: 18px;" class="text-right"><strong>INR <%# Eval("Discountprice") %></strong> </p>
                                                    <p style="font-size: 18px;" class="text-right"><strong>INR <%# Eval("taxablepamt") %></strong> </p>
                                                    <p style="font-size: 18px;" class="text-right"><strong>INR <%# Eval("Tax1") %></strong> </p>

                                                    <%-- <p style="font-size: 18px;" class="text-right"><strong><%# Eval("Totalprice") %></strong> </p>--%>

                                                    <%--<button class="btn btn-primary wow agileits  fadeInUp pull-right" id="book-resort">Add to Cart<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button>--%>
                                                </div>
                                                <div class="col-sm-12 gross-value" style="margin: 0;">
                                                    <div class="col-sm-6">

                                                        <p style="font-size: 18px;" class="text-left">Total: </p>
                                                        <%--<button class="btn btn-primary wow agileits  fadeInUp pull-right" id="book-resort">Add to Cart<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button>--%>
                                                    </div>
                                                    <div class="col-sm-6">

                                                        <p style="font-size: 18px;" class="text-right"><strong>INR <%# Eval("Totalprice") %></strong> </p>
                                                        <%--<button class="btn btn-primary wow agileits  fadeInUp pull-right" id="book-resort">Add to Cart<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button>--%>
                                                    </div>
                                                </div>



                                            </div>


                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </div>
                    <div class="col-sm-12 bottom-btn" style="padding-left: 0; padding-right: 0;">
                        <div class="row">
                            <div>
                                <div class="total-value col-sm-12">
                                    <asp:Label ID="Label6" runat="server" Style="float: left; padding-left: 2%;" Text="Total"></asp:Label>

                                    <asp:Label ID="lblnetAmount" runat="server" Text=" "></asp:Label>
                                </div>
                            </div>



                        </div>
                    </div>
                    <div class="col-sm-12 bottom-btn" style="padding-left: 0; padding-right: 0;">
                        <div class="row">
                            <div>
                                <div class="total-value col-sm-12">



                                    <asp:LinkButton ID="lnkbtnAdd" runat="server" CssClass="button-link" OnClick="lnkbtnAdd_Click">Add Room</asp:LinkButton>
                                </div>
                            </div>



                        </div>
                    </div>
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

        <%--   <script>
            $(function () {
                $("#datepicker,#datepicker1,#datepicker2").datepicker({ dateFormat: 'MM-yy' });


                $('.panel-default').click(function () {
                    var spanChng = $(this).children('.panel-heading').children('.panel-title').children('span');
                    console.log(spanChng);
                    if ($(this).children('.panel-heading').hasClass('collapsed')) {
                        spanChng.css({
                            "background": "url(images/remove.png)",
                            "background-size": "3.6rem",
                            "background-repeat": "no-repeat",
                            "background-position": "53%"
                        });
                    } else {
                        spanChng.css({
                            "background": "",
                            "background-size": "3.6rem",
                            "background-repeat": "no-repeat",
                            "background-position": "53%"
                        });
                    }
                });

            });
        </script>--%>
    </form>
</body>
</html>
