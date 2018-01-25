<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DepartureSearch1.aspx.cs" Inherits="Cruise_Booking_DepartureSearch1" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Resorts a Hotels and Restaurants </title>
    <meta charset="UTF-8">
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
    <!-- <link rel="stylesheet" type="text/css" href="cruise.css"> -->
    <!-- Custom-Stylesheet-Links -->
    <!-- Bootstrap-CSS -->
    <link rel="stylesheet" href="css/Newcss/bootstrap.min.css" type="text/css" media="all">

    <!-- Animate.CSS -->
    <link rel="stylesheet" href="css/Newcss/animate.css" type="text/css" media="all">
    <!-- Index-Page-CSS -->
    <link rel="stylesheet" href="css/Newcss/style.css" type="text/css" media="all">

    <!-- //Custom-Stylesheet-Links -->
    <link rel="stylesheet" type="text/Newcss/css" href="cruise.css">
    <!-- Fonts -->
    <!-- Body-Font -->
    <link rel="stylesheet" href="//fonts.googleapis.com/css?family=Open+Sans:400,300,600,700,800" type="text/css">
    <!-- Logo-Font -->
    <link rel="stylesheet" href="//fonts.googleapis.com/css?family=Cinzel+Decorative:400,900,700" type="text/css">
    <!-- Navbar-Font -->
    <link rel="stylesheet" href="//fonts.googleapis.com/css?family=Montserrat:400,700" type="text/css">
</head>
<body>
      <div class="loaderbody"><div class="loader"><img src="../../images/loading1.gif" alt="Loading..." /> Please Wait</div></div>k
    <form id="form1" runat="server">
        <div class="header agileits " id="home">

            <!-- Navbar -->
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
                                    <li><a runat="server" id="lnkLogin" href="agentLogin1.aspx">Partner</a></li>
                                    <li class="dropdown-submenu"><a runat="server" id="lnkCustomerRegis" href="../Masters/NewRegister.aspx">Customer </a>
                                        <ul class="dropdown-menu">
                                            <li><a href="#">Sign Up</a></li>
                                            <li><a href="#">Login</a></li>
                                            <%--      </li>
                                    <li><a runat="server" id="lnkCustLogin" href="CustomerLogin.aspx">Customer Login</a>

                                    </li>
                                    <li><a runat="server" id="lnkView" href="ViewAllBookings.aspx">Customer Login</a>

                                    </li>--%>
                                        </ul>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </div>

                </div>
            </nav>
            <!-- //Navbar -->

            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>


            <asp:UpdatePanel runat="server" ID="upd1" updatemode="Conditional">
               <%-- <Triggers>
            <asp:AsyncPostBackTrigger controlid="UpdateButton2" eventname="Click" />
        </Triggers>--%>
                <ContentTemplate>
                    <!-- Ventures -->
                    <div class="ak-ventures ventures agileits ">
                        <div class="container">
                            <div class="row">
                                <div class="col-sm-4">
                                    <h2 style="text-align: left; padding-top: 17px;">Select your travel Date  </h2>
                                </div>
                                <div class="col-sm-8">
                                    <section style="width: 100%; height: 74px; padding-top: 0; border-bottom: none;">


                                        <nav>
                                            <ol class="cd-breadcrumb triangle" style="float: right;">
                                                <li><em>Search</em></li>
                                                <li><em>Packages</em></li>
                                                <li class="current"><em>Choose Date</em></li>
                                                <li><em>Select Cabin</em></li>
                                                <%--<li><a href="#0">Reservation Details & Check Out</a></li>--%>
                                                <li><em>Details & Check Out</em></li>
                                            </ol>
                                        </nav>
                                    </section>
                                </div>
                            </div>



                            <div class="row">
                                <div class="col-md-12  White-Box_sub">
                                    <div class="col-sm-12 agileits ak-venture-banner location-grids location-grids-1 wow slideInLeft">
                                        <h3 style="color: #000; font-weight: normal; font-size: 24px; margin-bottom: 0; padding-bottom: 14px; border-bottom: 1px solid;"><%Response.Write(dtres.Rows[0]["NamePack"].ToString()); %></h3>
                                        <p style="text-align: justify; padding-top: 15px;">
                                            Vessel : M V Mahabaahu<br />
                                            <br />
                                            <asp:Label ID="lblFrmTo" runat="server" Text="Label"></asp:Label><br />
                                            <br />

                                            <asp:Label ID="lblnights" runat="server" Text="Label"></asp:Label>
                                            <br />
                                            <br />
                                            <asp:Label ID="lblPackDesc" runat="server" Text="Label"></asp:Label>
                                        </p>
                                    </div>
                                </div>
                            </div>


                            <section >
                                <div class="col-md-12 ventures-grids agileits">

                                    <div id="bookingTop" class="row White-Box2 text-center padding-main" >
                                        <div id="Div3" class="insideSkin collapse in" aria-expanded="true">
                                            <div id="Div4" class="topbotPadding">
                                                <div id="Div5" class="botBorderWhite">
                                                    <div class="col-md-12 pricePanelBox topBorderWhite font14 noLeftPadding noRightPadding" style="border: thin solid #ddd; padding: 14px; font-weight: bold;">
                                                        <div class="col-md-2 text-left noLeftPadding noRightPadding topMargin10">Boarding Date </div>
                                                        <div class="col-md-2 text-left noLeftPadding noRightPadding topMargin10">De-Boarding Date </div>
                                                        <div class="col-md-1 text-left topMargin10 noSidePadding">Nights </div>
                                                        <div class="col-md-3 text-right topMargin10" style="text-align: -webkit-center;">Starting price Per person </div>
                                                        <div class="col-md-1 text-right topMargin10">Availability </div>
                                                        <div class="col-md-2 text-left topMargin10">
                                                            <div class="col-sm-12">
                                                                <div class="col-sm-4">
                                                                    <asp:Label ID="lblSuite" runat="server" Text="Suite" ToolTip="Suite"></asp:Label>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <asp:Label ID="lblSwb" runat="server" Text="Swb" ToolTip="Superior with Balcony"></asp:Label>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <asp:Label ID="lblSwob" runat="server" Text="Swob" ToolTip="Superior without Balcony"></asp:Label>
                                                                </div>
                                                            </div>


                                                        </div>

                                                        <div class="col-md-1 text-right noRightPadding"></div>
                                                    </div>
                                                    <div style="height: 30px"></div>
                                                    <br />
                                                    <% for (int i = 0; i < dtres.Rows.Count; i++)
                                                        {
                                                            try
                                                            {
                                                    %>
                                                    <div class="col-md-12 pricePanelBox topBorderWhite font14 noLeftPadding noRightPadding" style="border: thin solid #ddd; padding: 10px;">
                                                        <div class="col-md-2 text-left noLeftPadding noRightPadding topMargin10">
                                                            <%Response.Write(Convert.ToDateTime(dtres.Rows[i]["CheckInDate"]).ToString("dddd, MMMM d, yyyy")); %>
                                                        </div>
                                                        <div class="col-md-2 text-left noLeftPadding noRightPadding topMargin10">
                                                            <%Response.Write(Convert.ToDateTime(dtres.Rows[i]["CheckInDate"]).AddDays(Convert.ToInt32(dtres.Rows[i]["NoOfNights"])).ToString("dddd, MMMM d, yyyy")); %>
                                                        </div>
                                                        <div class="col-md-1 text-left topMargin10 noSidePadding">
                                                            <%Response.Write(dtres.Rows[i]["NoOfNights"].ToString()); %>
                      Nights
                                                        </div>
                                                        <div class="col-md-3 text-right" style="text-align: -webkit-center;">

                                                            <%Response.Write(dtres.Rows[i]["Currency"].ToString()); %>
                                                            <span class="offerPrice">
                                                                <strong>
                                                                    <%if (dtres.Rows[i]["Rate"].ToString() == "0.00")
                                                                        {
                                                                            Response.Write("0.00");
                                                                        }
                                                                        else
                                                                        {
                                                                            Response.Write(Convert.ToDouble(dtres.Rows[i]["Rate"]).ToString("##,0"));
                                                                        }
                                                                    %>  
                                                                </strong>
                                                            </span>
                                                            <br />
                                                            <br />
                                                            <strong>
                                                                <%if (dtres.Rows[i]["Discount %"].ToString() != "0")
                                                                    {
                                                                        Response.Write("<span style='color:#4f81bd'> " + dtres.Rows[i]["Discount %"].ToString() + "% Off </span>");
                                                                        //            <asp:Label ID="lblDiscount" runat="server" Text=" "></asp:Label>
                                                                        //Response.Write(dtres.Rows[i]["Discount %"].ToString());
                                                                        //<asp:Label ID="lblPercent" runat="server"></asp:Label>
                                                                    }
                                                                    else
                                                                    {

                                                                    }
                                                                %>  
                                                            </strong>
                                                        </div>
                                                        <% PackageId = Session["PackId"].ToString(); %>
                                                        <% PackageName = dtres.Rows[i]["NamePack"].ToString(); %>
                                                        <% NoOfNight = dtres.Rows[i]["NoOfNights"].ToString(); %>
                                                        <% CheckinDate = (Convert.ToDateTime(dtres.Rows[i]["CheckInDate"].ToString()).ToShortDateString()).ToString(); %>
                                                        <div class="col-md-1 text-right topMargin10">
                                                            <%
                                                                if (dtres.Rows[i]["Availability"].ToString() == "Limited Availability")
                                                                {
                                                                    Response.Write("<span style='color:#f99646'> " + dtres.Rows[i]["Availability"].ToString() + " </span>");
                                                                }
                                                                if (dtres.Rows[i]["Availability"].ToString() == "Available")
                                                                {
                                                                    Response.Write("<span style='color:#4f81bd'> " + dtres.Rows[i]["Availability"].ToString() + " </span>");
                                                                }

                                                                if (dtres.Rows[i]["Availability"].ToString() == "Sold Out")
                                                                {
                                                                    Response.Write("<span style='color:Red'> " + dtres.Rows[i]["Availability"].ToString() + " </span>");
                                                                }
                                                            %>
                                                        </div>

                                                        <div class="col-md-2 text-right" style="text-align: -webkit-center;">

                                                            <div class="col-sm-12">
                                                                <div class="col-sm-4"><%Response.Write("<span style='color:#f99646'> " + dtres.Rows[i]["Suite"].ToString() + " </span>"); %></div>
                                                                <div class="col-sm-4"><%Response.Write("<span style='color:#f99646'> " + dtres.Rows[i]["Swb"].ToString() + " </span>"); %></div>
                                                                <div class="col-sm-4"><%Response.Write("<span style='color:#f99646'> " + dtres.Rows[i]["Swob"].ToString() + " </span>"); %></div>
                                                            </div>

                                                        </div>
                                                        <div class="col-md-1 text-right noRightPadding  topMargin10" style="float: right;">

                                                            <%
                                                                if (dtres.Rows[i]["Availability"].ToString() != "Sold Out")
                                                                {
                                                                    Response.Write("<a href='CruiseBooking.aspx?PackId=" + dtres.Rows[i]["packageId"].ToString() + "&PackageName=" + dtres.Rows[i]["NamePack"].ToString() + "&NoOfNights=" + dtres.Rows[i]["NoOfNights"].ToString() + "&CheckIndate=" + dtres.Rows[i]["CheckInDate"].ToString() + "&CheckOutdate=" + dtres.Rows[i]["CheckOutDate"].ToString() + "&Discount=" + dtres.Rows[i]["Discount %"].ToString() + "&DepartureId=" + dtres.Rows[i]["Id"].ToString() + "'  class='btn btn-info font16 topMargin10 botMargin10 step2Btn' style='padding-left:12px;padding-right:12px;' data-departureid='5597' >Select</a>");
                                                                }

                                                            %>
                                                        </div>
                                                    </div>
                                                    <div style="height: 30px"></div>
                                                    <br />
                                                    <%
                                                            }
                                                            catch
                                                            {
                                                            }
                                                        } %>
                                                </div>
                                                <div class=" clearfix"></div>
                                                <div class="pull-left" style="font-weight: bold">The price is based on the minimum price for the available cabin</div>
                                            </div>

                                        </div>
                                    </div>

                                    <%--<div class="row">
                            <table class="table table-striped table-bordered">
                                <thead style="font-weight: bold;">
                                    <tr>
                                        <th>Boarding Date</th>
                                        <th>De-Boarding Date</th>
                                        <th>Nights</th>
                                        <th>Price</th>
                                        <th>Availability</th>
                                    </tr>
                                </thead>
                                <tbody >
                                    <% for (int i = 0; i < dtres.Rows.Count; i++)
                                        {
                                            try
                                            {
                                    %>
                                    <tr>
                                        <td><%Response.Write(Convert.ToDateTime(dtres.Rows[i]["CheckInDate"]).ToString("dddd, MMMM d, yyyy")); %></td>
                                        <td><%Response.Write(Convert.ToDateTime(dtres.Rows[i]["CheckInDate"]).AddDays(Convert.ToInt32(dtres.Rows[i]["NoOfNights"])).ToString("dddd, MMMM d, yyyy")); %></td>
                                        <td><%Response.Write(dtres.Rows[i]["NoOfNights"].ToString()); %>
                      Nights</td>
                                        <td>Starting at <em><%Response.Write(dtres.Rows[i]["Currency"].ToString()); %></em> <strong><%if (dtres.Rows[i]["Rate"].ToString() == "0.00")
                                                                                                                                        {
                                                                                                                                            Response.Write("0.00");
                                                                                                                                        }
                                                                                                                                        else
                                                                                                                                        {
                                                                                                                                            Response.Write(Convert.ToDouble(dtres.Rows[i]["Rate"]).ToString("##,0"));
                                                                                                                                        }
                                        %>  </strong>,pp based on Standard Room
                                            <br />
                                            <br />
                                            <strong>
                                                <asp:Label ID="lblDiscount" runat="server" Text=" "></asp:Label>
                                                <%Response.Write(dtres.Rows[i]["Discount %"].ToString()); %>
                                                <asp:Label ID="lblPercent" runat="server" Text=" "></asp:Label></strong>
                                        </td>

                                        <td>
                                            <span class="text-primary"><em><%
                                                                               if (dtres.Rows[i]["Availability"].ToString() == "Limited Availability")
                                                                               {
                                                                                   Response.Write("<span style='color:#f99646'> " + dtres.Rows[i]["Availability"].ToString() + " </span>");
                                                                               }
                                                                               if (dtres.Rows[i]["Availability"].ToString() == "Available")
                                                                               {
                                                                                   Response.Write("<span style='color:#4f81bd'> " + dtres.Rows[i]["Availability"].ToString() + " </span>");
                                                                               }

                                                                               if (dtres.Rows[i]["Availability"].ToString() == "Sold Out")
                                                                               {
                                                                                   Response.Write("<span style='color:Red'> " + dtres.Rows[i]["Availability"].ToString() + " </span>");
                                                                               }
                                            %> &nbsp;</em> </span>
                                            <%
                                                if (dtres.Rows[i]["Availability"].ToString() != "Sold Out")
                                                {
                                                    Response.Write("<a href='CruiseBooking.aspx?PackId=" + dtres.Rows[i]["packageId"].ToString() + "&PackageName=" + dtres.Rows[i]["NamePack"].ToString() + "&NoOfNights=" + dtres.Rows[i]["NoOfNights"].ToString() + "&CheckIndate=" + dtres.Rows[i]["CheckInDate"].ToString() + "&CheckOutdate=" + dtres.Rows[i]["CheckOutDate"].ToString() + "&Discount=" + dtres.Rows[i]["Discount %"].ToString() + "&DepartureId=" + dtres.Rows[i]["Id"].ToString() + "'  class='btn btn-info font16 topMargin10 botMargin10 step2Btn'  data-departureid='5597' >Select</a>");
                                                }

                                            %><%--<a href="#">
                                                <button class="btn btn-primary wow agileits  fadeInLeft">Select<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button>
                                            </a>--%>
                                    <%--</td>

                                    </tr>
                                    <%
                                            }
                                            catch
                                            {
                                            }
                                        } %>
                                </tbody>
                            </table>
                        </div>--%>
                                </div>
                            </section>
                        </div>
                    </div>

                    </ContentTemplate>
               <%-- <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lblSuite" />
                </Triggers>--%>
            </asp:UpdatePanel>

            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="200">
                <ProgressTemplate>
                    <iframe id="pgrIFrame" frameborder="0" marginheight="0" marginwidth="0" scrolling="no"
                        src="javascript:'<html></html>';" style="position: absolute;"></iframe>
                    <asp:Panel ID="Panel1" runat="server" Height="150%" Style="margin-left: 0px; margin-bottom: 700px;" Width="100%">
                        <div style="position: relative; top: 200px; left: 200px; padding-left: 150px;">
                            <div style="padding-right: 50px;">
                                <asp:Image ID="image2" runat="server" Height="40px" Width="40px" ImageUrl="~/images/loading1.gif" />
                                Please Wait.... 
                            </div>
                        </div>
                    </asp:Panel>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <cc1:alwaysvisiblecontrolextender id="AlwaysVisibleControlExtender1" runat="server"
                targetcontrolid="Panel1" horizontaloffset="300" verticaloffset="150"></cc1:alwaysvisiblecontrolextender>
                    <!-- Footer -->
                    <%--  <div class="footer agileits ">
                <div class="container">

                    <div class="col-md-6 col-sm-6 agileits  footer-grids">
                        <div class="col-md-4 col-sm-4 footer-grid agileits  footer-grid-1 wow fadeInUp">
                            <ul class="agileits ">--%>
                    <%-- <li class="agileits ">5 Star Hotels</li>
                                <li class="agileits ">Beach Resorts</li>
                                <li class="agileits ">Beach Houses</li>
                                <li class="agileits ">Water Houses</li>--%>
                    <%-- </ul>
                        </div>
                        <div class="col-md-4 col-sm-4 footer-grid agileits  footer-grid-2 wow fadeInUp">
                            <ul class="agileits ">--%>
                    <%--<li class="agileits "><a href="gallery.html">Bahamas</a></li>
                                <li class="agileits "><a href="gallery.html">Hawaii</a></li>
                                <li class="agileits "><a href="gallery.html">Miami</a></li>
                                <li class="agileits "><a href="gallery.html">Ibiza</a></li>--%>
                    <%--  </ul>
                        </div>
                        <div class="col-md-4 col-sm-4 footer-grid agileits  footer-grid-3 wow fadeInUp">
                            <ul class="agileits ">--%>
                    <%--  <li class="agileits "><a href="about.html">About</a></li>
                                <li class="agileits "><a href="cuisines.html">Cuisines</a></li>
                                <li class="agileits "><a href="gallery.html">Gallery</a></li>
                                <li class="agileits "><a href="booking.html">Contact</a></li>--%>
                    <%--</ul>
                        </div>
                        <div class="clearfix"></div>
                    </div>

                    <div class="col-md-6 col-sm-6 footer-grids agileits  social wow fadeInUp">
                        <ul class="social-icons">--%>
                    <%--  <li class="agileits "><a href="#" class="facebook agileits " title="Go to Our Facebook Page"></a></li>
                            <li class="agileits "><a href="#" class="twitter agileits " title="Go to Our Twitter Account"></a></li>
                            <li class="agileits "><a href="#" class="googleplus agileits " title="Go to Our Google Plus Account"></a></li>
                            <li class="agileits "><a href="#" class="instagram agileits " title="Go to Our Instagram Account"></a></li>
                            <li class="agileits "><a href="#" class="youtube agileits " title="Go to Our Youtube Channel"></a></li>--%>
                    <%-- </ul>
                    </div>

                    <div class="col-md-6 col-sm-6 footer-grids agileits  copyright wow fadeInUp">
                        <p>&copy; 2017 Resorts. All Rights Reserved | Design by</p>
                    </div>
                    <div class="clearfix"></div>

                </div>
            </div>--%>
                

            <!-- //Footer -->
            <!-- // $$$$$$$$%%%%%%%%%%%%%%%%%%%%% pakages Details &&&&&&&&&&&&&&&&&&&&&&&&&&&&&& -->

            <!-- Necessary-JavaScript-Files-&-Links -->

            <!-- Default-JavaScript -->
            <script type="text/javascript" src="js/jquery-2.1.4.min.js"></script>
            <!-- Bootstrap-JavaScript -->
            <script type="text/javascript" src="js/bootstrap.min.js"></script>

            <!-- Animate.CSS-JavaScript -->
            <script src="js/wow.min.js"></script>
            <script>
                new WOW().init();
            </script>
            <!-- //Animate.CSS-JavaScript -->

            <script type="text/javascript">
                $('.fix');

                $(document).ready(function () {
                    $('.loaderbody').css('display', 'none');
                    $('.btn-info').click(function () {
                        $('.loaderbody').css('display', 'block');
                    });
                });
            </script>
    </form>
</body>
</html>
