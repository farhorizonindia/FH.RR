<%@ page language="C#" autoeventwireup="true" inherits="Cruise_PackageSearchResults" CodeFile="~/Cruise/booking/PackageSearchResults.aspx.cs" enableeventvalidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <meta name="author" content="" />
    <base href="#" />
    <link rel="icon" type="image/png" href="/favicon.ico" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="css/style.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.0.3/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/1.11.3/jquery.min.js" type="text/javascript"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.5/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="js/QueryString.js" type="text/javascript"></script>
  
    
    <title>Cruise Search Results</title>
    <meta name="description" content="" />
    <script src="js/results.js"></script>
    
    <link href="../../css/newcss.css" rel="stylesheet" />
     <style>
         ul
{
    list-style-type: none;
    padding:0px;
}
         li
         {
             padding:0px;
             margin:0px;
         }
         h3
         {
             font-size:21px;
         }
     </style>
    </head>
    <body class="bg-img1" style="font-family:'Times New Roman';font-style:italic">
    <form method="post"  id="form1" runat="server">
      <%--<div class="aspNetHidden">
<input type="hidden" name="__VIEWSTATE" id="__VIEWSTATE" value="GI+bAOeA8ZHmQtNiNt2SN2pkCHIgN2OZX5ChHT9zDB+cErXXVYE/y30xkbLY8wjDRt9+tXiiqsCuFB/5q7Lwu4jxujHlJDU1kdHD/ghWll27bp5stB9JhE7Tc7c8jxJqv0Wp67k+xdiamZ2vxRhu/qt+AvNXhf4cFdpYq0d7AAQ=" />
</div>
<div class="aspNetHidden">
	<input type="hidden" name="__VIEWSTATEGENERATOR" id="__VIEWSTATEGENERATOR" value="63301E89" />--%>
      </div>
      <div class="sitecontainer whiteBackground1">
        <section>
        
        <div class="container">
          <div class="row">
            <div class="col-md-12 text-center White-Box">
            
            
              <h2>Packages <span class=" pull-right" ><asp:LinkButton ID="LinkButton1" runat="server" CssClass="button-link" BackColor="#4479BA" OnClick="LinkButton1_Click">Logout</asp:LinkButton></span></h2>
              
              
              <div class=" clearfix"> </div>
              
              
            </div>
          </div>
          </div>
          
          <div class="row ">
            
            <div class="col-md-12 text-center White-Box2">
              <div id="Offers" class="insideSkin">
                <%try %>
                <%{ %>
                <% for(int i=0;i<dtres.Rows.Count;i++)
                                   
                               {
                                   %>
                <div class="col-md-12 text-left noSideMargin noSidePadding bottomWhiteBar imageOffersFader">

                    <div class=" col-sm-4"><% Response.Write("<img src= " + dtres.Rows[i]["Img"].ToString() + " class='img-responsive noLeftPadding' border='0' alt='' />");%></div>
                    <div class=" col-sm-8">

                        <h3  style="font-family:'Times New Roman';font-style:italic;font-weight:bold">
                    <%Response.Write( dtres.Rows[i]["PackageName"].ToString()); %>
                  </h3>

                         <span class="darkGrey1">
                             <ul>
                             <li>Vessel:MV Mahabaahu</li>
                  <%
                     
                                   Response.Write("<li>" + dtres.Rows[i]["BFrom"].ToString() + " to " + dtres.Rows[i]["BTo"].ToString() + "</li>");
                                   Response.Write("<li> " + dtres.Rows[i]["NoOfNights"].ToString() + "  Nights/" +(Convert.ToInt32( dtres.Rows[i]["NoOfNights"].ToString())+1).ToString() + " Days </li> ");
                  
                    
                     %>
                                 </ul>
                 </span>
                        <div class="darkGrey1" style="font-family:'Times New Roman';font-style:italic">
                  <%Response.Write(dtres.Rows[i]["PackageDescription"].ToString()); %>

  <% PackageId = dtres.Rows[i]["PackageId"].ToString(); %>
                            <div class="pull-right">
                                 <%Response.Write("<a href='" + dtres.Rows[i]["ItineraryLink"].ToString() + "' class='btn btn-info btnWidth100 btnFont' >Itinerary</a>");%>
                    <%Response.Write("<a href='DepartureSearch.aspx?PackId=" + PackageId + "&CheckinDep="+CheckinDep+"&CheckoutDep="+CheckoutDep+" '  class='btn btn-info btnWidth100 btnFont' >Book Now</a>"); %>
                               
                    </div>
<div class="clearfix"></div>
                 </div>

                    </div>



                </div>
                <%
                            
                            } %>
                <%} %>
                <%catch(Exception sqe) %>
                <%{ %>
                <%} %>
              </div>
            </div>
          </div>
        </section>
        
        <!--ZOOMSTOP--> 
        
      </div>
    </form>
</body>
</html>
