<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchBasedPackage.aspx.cs" Inherits="Cruise_booking_SearchBasedPackage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" /><meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" /><meta name="author" content="Pandaw Cruises Ltd" />
    <base href="https://www.pandaw.com" />
    <link rel="icon" type="image/png" href="/favicon.ico" /><link href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.5/css/bootstrap.min.css" rel="stylesheet" type="text/css" /><link href="../../../../../css/style.css" rel="stylesheet" /><link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.0.3/css/font-awesome.min.css" rel="stylesheet" type="text/css" />

    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/1.11.3/jquery.min.js" type="text/javascript"></script>   
    <script src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.5/js/bootstrap.min.js" type="text/javascript"></script>   
    <script src="js/QueryString.js" type="text/javascript"></script>
    
    

    <script src="js/pandaw.js" type="text/javascript"></script>

    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->

  
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
    <!-- End Google Analytics -->
    <title>Cruise Search Results</title>
    <meta name="description" content="river cruises for your selected search criteria." />

    <script src="js/results.js"></script>

</head>
<body>
    <form method="post" action="/river-cruises/search/0/0/0/0" id="form1">
<div class="aspNetHidden">
<input type="hidden" name="__VIEWSTATE" id="__VIEWSTATE" value="GI+bAOeA8ZHmQtNiNt2SN2pkCHIgN2OZX5ChHT9zDB+cErXXVYE/y30xkbLY8wjDRt9+tXiiqsCuFB/5q7Lwu4jxujHlJDU1kdHD/ghWll27bp5stB9JhE7Tc7c8jxJqv0Wp67k+xdiamZ2vxRhu/qt+AvNXhf4cFdpYq0d7AAQ=" />
</div>

<div class="aspNetHidden">

	<input type="hidden" name="__VIEWSTATEGENERATOR" id="__VIEWSTATEGENERATOR" value="63301E89" />
</div>
        <div class="sitecontainer whiteBackground">
            <div class="col-md-12 spacerGradient">

            </div>
            <section>
                <div class="row">
                    <div class="col-md-12 text-center topbotPadding">

                        <h2 class="goldLine font24"><span>Open Dates Based On packages</span></h2>

                    </div>
                    <div class="col-md-12 text-center noTopPadding">

                        &nbsp;

                    </div>
                </div>
                <div class="row backgroundPaperDark noSideMargin innerDropShadow">

                    <div class="col-md-12 text-center topbotPadding">

                        <div id="Offers" class="insideSkin">

                            <% for(int i=0;i<dtres.Rows.Count;i++)
                               {
                                   %>
                            <div class="col-md-12 text-left noSideMargin noSidePadding bottomWhiteBar imageOffersFader">

                                <div class="blackBack"><img src="/images/product/Classic_Mekong.jpg" class="col-md-3 img-responsive noLeftPadding" border="0" alt="" /></div>
                                
                                <h4 class="darkGrey">Property Name : <%Response.Write(dtres.Rows[i]["AccomName"].ToString()); %>  </h4>
                                <h4 class="darkGrey">From <%Response.Write(dtres.Rows[i]["CheckInDate"].ToString()); %> &nbsp;To &nbsp;<%Response.Write(dtres.Rows[i]["CheckOutDate"].ToString()); %> </h4>
                                <h2 class="latestOffers"><%Response.Write(dtres.Rows[i]["NamePack"].ToString()); %></h2>

                                <span class="darkGrey"><%Response.Write(dtres.Rows[i]["NoOfNights"].ToString()); %> Nights</span>               

                                <div class="pull-right placeDivBottom">

                                   <% PackageId = Request.QueryString["PackId"].ToString(); %>

                                        <span class="pull-right">from US$<span class="offerPrice">1,755</span> pp
                                       <%Response.Write("<a href='CruiseBooking.aspx?PackId=" + PackageId + "'  class='btn btn-info font16 topMargin10 botMargin10 step2Btn'  data-departureid='5597' >Select</a>"); %>
                                </div>
                            </div>

                              <% } %>

                  </div> 
                    </div>
                </div>

            </section>
        </div>
    </form>
</body>
</html>