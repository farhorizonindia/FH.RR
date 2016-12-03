<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Itinerary.aspx.cs" Inherits="Cruise_booking_Itinerary" %>

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
</head>
 <body class="bg-img1">
    <form method="post"  id="form1" runat="server">


    <div>

         <div class="sitecontainer whiteBackground1">
        <section>
  <div class="row ">
            
            <div class="col-md-12 text-center White-Box2">
              <div id="Offers" class="insideSkin">

  <%
      for (int i = 0; i < dtreturn.Rows.Count; i++)
      {
        %>
       <div class="col-md-12 text-left noSideMargin noSidePadding bottomWhiteBar imageOffersFader">
            <center> <h2 style="font-weight: bold;color: #2492e2;"> <%Response.Write(dtreturn.Rows[i]["Night"].ToString() + " " + dtreturn.Rows[i]["City"].ToString()); %>
        </h2> </center>
           <h3> <%Response.Write(dtreturn.Rows[i]["CDescription"].ToString());%></h3>
        </div>

        <% 
      }
            %>  
       
</div>
                </div>
      </div>
            </section>
             </div>
        

    </div>
    </form>
</body>
</html>
