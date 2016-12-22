<%@ page language="C#" autoeventwireup="true" inherits="Cruise_booking_DepartureSearch" CodeFile="~/Cruise/booking/DepartureSearch.aspx.cs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <meta name="author" content="" />
    <link rel="icon" type="image/png" href="/favicon.ico" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.0.3/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <script src="//www.google-analytics.com/analytics.js"></script>
    <script src="//www.google-analytics.com/analytics.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/1.11.3/jquery.min.js" type="text/javascript"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.5/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="js/QueryString.js" type="text/javascript"></script>
    <link href="css/style.css" rel="stylesheet" />
    <script type="text/javascript">
        (function (i, s, o, g, r, a, m) {
            i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                (i[r].q = i[r].q || []).push(arguments)
            }, i[r].l = 1 * new Date(); a = s.createElement(o),
            m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
        })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');
        ga('create', 'UA-1319647-1', 'auto');
        ga('send', 'pageview');
    </script>
    <title></title>
    <meta name="description" content="">
    <script type="text/javascript">

        var productId = 50;
        var isAgent = 'False';
        var bookNow = 'False';
        var CartToken = '';
        var loggedIn = 'False';

        function creatingBooking() {
            $("#ContentPlaceHolder1_btnComplete").attr('disabled', 'disabled').html("Creating Booking");
        }

    </script>
    <style>
.button-link {
	padding: 10px 15px;
	background: #4479BA;
	color: #FFF;
	font-size: medium;
}
ul {
	list-style-type: none;
	padding: 0px;
}
li {
	padding: 0px;
	margin: 0px;
}
h3 {
	font-size: 21px;
}
li {
	font-size: 16px;
	font-family: 'Times New Roman';
	padding: 0px;
	margin: 0px;
}
</style>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/datatables/1.9.4/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="js/expedition.js" type="text/javascript"></script>
    <title></title>
    <link href="../../css/newcss.css" rel="stylesheet" />
    </head>

    <body class="bg-img2" style="font-family: 'Times New Roman'; font-style: italic">
    <form method="post" id="form1" runat="server">
      <div class="sitecontainer">
        <section>
          <div class="container">
            <div class="row">
              <div class="col-md-12 text-center White-Box">
               
                  <h2>Select your travel Date
                  <span class=" pull-right"  >
                  <asp:LinkButton ID="LinkButton1" runat="server" CssClass="button-link" OnClick="LinkButton1_Click">Logout</asp:LinkButton>
                  </span> 
               	</h2>
              </div>
            </div>
          </div>
          <div class="container">
            <div class="row">
              <div class="col-md-12  White-Box_sub">
                <h3 class="" style="font-family: 'Times New Roman'; font-style: italic; font-weight: bold"><span class=" pull-left">Package :
                  <%Response.Write(dtres.Rows[0]["NamePack"].ToString()); %>
                  </span></h3>
                  <br />
                <ul>
                  <li>
                    <asp:Label ID="Label1" runat="server">Vessel :
                      <%Response.Write(dtres.Rows[0]["AccomName"].ToString()); %>
                    </asp:Label>
                  </li>
                  <li>
                    <asp:Label ID="lblFrmTo" runat="server" Text="Label"></asp:Label>
                  </li>
                  <li>
                    <asp:Label ID="lblnights" runat="server" Text="Label"></asp:Label>
                  </li>
                  <li>
                    <asp:Label ID="lblPackDesc" runat="server" Text="Label"></asp:Label>
                  </li>
                </ul>
              </div>
            </div>
          </div>
        </section>
        <section>
          <div id="bookingTop" class="row White-Box2 text-center padding-main">
            <div id="Div3" class="insideSkin collapse in" aria-expanded="true">
              <div id="Div4" class="topbotPadding">
                <div id="Div5" class="botBorderWhite">
                  <div class="col-md-12 pricePanelBox topBorderWhite font14 noLeftPadding noRightPadding" style="border: thin solid #2492e2; background-color: rgba(149, 190, 222, 0.8) !important; padding: 10px;    font-weight: bold;">
                    <div class="col-md-2 text-left noLeftPadding noRightPadding topMargin10"> Boarding Date </div>
                    <div class="col-md-3 text-left noLeftPadding noRightPadding topMargin10"> De-Boarding Date </div>
                    <div class="col-md-1 text-left topMargin10 noSidePadding"> Nights </div>
                    <div class="col-md-3 text-right topMargin10" style="text-align: -webkit-center;"> Price </div>
                    <div class="col-md-2 text-right topMargin10"> Availability </div>
                    <div class="col-md-1 text-right noRightPadding"> </div>
                  </div>
                  <div style="height: 30px"></div>
                  <br />
                  <% for (int i = 0; i < dtres.Rows.Count; i++)
                                   {
                                       try
                                       {
                                %>
                  <div class="col-md-12 pricePanelBox topBorderWhite font14 noLeftPadding noRightPadding" style="border: thin solid #2492e2; padding: 10px;">
                    <div class="col-md-2 text-left noLeftPadding noRightPadding topMargin10">
                      <%Response.Write(Convert.ToDateTime(dtres.Rows[i]["CheckInDate"]).ToString("dddd, MMMM d, yyyy")); %>
                    </div>
                    <div class="col-md-3 text-left noLeftPadding noRightPadding topMargin10">
                      <%Response.Write(Convert.ToDateTime(dtres.Rows[i]["CheckInDate"]).AddDays(Convert.ToInt32(dtres.Rows[i]["NoOfNights"])).ToString("dddd, MMMM d, yyyy")); %>
                    </div>
                    <div class="col-md-1 text-left topMargin10 noSidePadding">
                      <%Response.Write(dtres.Rows[i]["NoOfNights"].ToString()); %>
                      Nights </div>
                    <div class="col-md-3 text-right"> Starting at
                      <%Response.Write(dtres.Rows[i]["Currency"].ToString()); %>
                      <span class="offerPrice">
                      <%Response.Write(Convert.ToDouble(dtres.Rows[i]["Rate"]).ToString("#.##")); %>
                      </span>pp<br />
                      based on Standard Room <span class="highlightBlue"><strong></strong></span> </div>
                    <% PackageId = Session["PackId"].ToString(); %>
                    <% PackageName = dtres.Rows[i]["NamePack"].ToString(); %>
                    <% NoOfNight = dtres.Rows[i]["NoOfNights"].ToString(); %>
                    <% CheckinDate = (Convert.ToDateTime(dtres.Rows[i]["CheckInDate"].ToString()).ToShortDateString()).ToString(); %>
                    <div class="col-md-2 text-right topMargin10">



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
                    
                    <div class="col-md-1 text-right noRightPadding  topMargin10">
                      <%
                       if (dtres.Rows[i]["Availability"].ToString() != "Sold Out")
                       {
                           Response.Write("<a href='CruiseBooking.aspx?PackId=" + PackageId + "&PackageName=" + PackageName + "&NoOfNights=" + NoOfNight + "&CheckIndate=" + CheckinDate + "&DepartureId=" + dtres.Rows[i]["Id"].ToString() + "'  class='btn btn-info font16 topMargin10 botMargin10 step2Btn'  data-departureid='5597' >Select</a>");
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
                   <div class="pull-left" style="font-weight:bold">The price is based on the minimum price for the available cabin</div>
              </div>
             
            </div>
          </div>
        </section>
        
        <!--ProductId: 50 --> 
        <!--Route: Downstream --> 
        
        <!--ZOOMSTOP--> 
      </div>
      <footer></footer>
    </form>
</body>
</html>
