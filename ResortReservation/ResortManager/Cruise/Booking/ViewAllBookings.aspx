<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewAllBookings.aspx.cs" Inherits="Cruise_booking_ViewAllBookings" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
   
    <title></title>
    <link rel="stylesheet" type="text/css" media="all" href="../../css/calendar-blue2.css"
        title="win2k-cold-1" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../../css/pageheader.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../../style.css" />
    <link rel="stylesheet" type="text/css" media="all" href="../../bootstrap.min.css" />
     <link href="css/style.css" rel="stylesheet" />

    <script type="text/javascript" src="../../js/calendar/calendar.js"></script>
    <script type="text/javascript" src="../../js/calendar/calendar-en.js"></script>
    <script type="text/javascript" src="../../js/calendar/calendar-setup.js"></script>
    <script type="text/javascript" src="../../js/popups.js"></script>
    <script type="text/javascript" src="../../js/global.js"></script>
    <script type="text/javascript" src="../../js/client/booking.js"></script>
    <script type="text/javascript" src="../../js/jquery-1.11.1.min.js"></script>
    <script type="text/javascript" src="../../js/bootstrap.min.js"></script>    
    
    <link href="../../css/newcss.css" rel="stylesheet" />
    <style>
        tr
        {
           text-align:center;
           height:25px
        }
        th
        {
              text-align:center;
        }

        .tbl-main
        {


        }

        .tbl-main table
        { width:60%; margin:0 auto; float:none;
             background-color:#eaeaea; border:1px solid #ddd; padding:10px; 

        }

        .tbl-main table tr td
        { width:33.3%;  font-size:17px;  color:#333; 
        }
         .tbl-main table tr td table
        { width:100%;   
        }




         .tbl-main2 table
        { width:60%; margin:0 auto; float:none;
             background-color:#eaeaea; border:1px solid #ddd; padding:10px; 

        }

        .tbl-main2 table tr td
        { width:25%;  font-size:17px;  color:#333; 
        }

      .tbl-main2 table tr td select {   border: solid 1px #CCCCCC;
            background-color: #FFFFFF;
            text-align: left !important;
            padding: 7px 12px;
            font-weight: 400;
            line-height: 1.42857143; width:100%;
        }

      
            .tbl-main2 table tr td input[type=submit]
            {     color: #fff;
                    background-color: #5bc0de;
                    border-color: #46b8da;
                    display: inline-block;
                    padding: 6px 12px;
                    margin-bottom: 0;
                    font-size: 14px;
                    font-weight: 400;
                    line-height: 1.42857143;
                    text-align: center;
                    white-space: nowrap;
                    vertical-align: middle;
                    -ms-touch-action: manipulation;
                    touch-action: manipulation;
                    cursor: pointer;
                    -webkit-user-select: none;
                    -moz-user-select: none;
                    -ms-user-select: none;
                    user-select: none;
                    background-image: none;
                    border: 1px solid transparent;
                    border-radius: 4px; float:right;
            }

        .tbl-main4
        {width:60%; margin:0 auto; float:none;
             background-color:#eaeaea;
        }

            .tbl-main4 table
        { width:60%; margin:0 auto; float:none;
             background-color:#eaeaea; 
             border:1px solid #ddd; padding:10px; 

        }

        .tbl-main4 table tr td
        { width:33.3%; 
          font-size:17px;  
          color:#333; 
        }


        .tbl-main4 table tr td select {   
            border: solid 1px #CCCCCC;
            background-color: #FFFFFF;
            text-align: left !important;
            padding: 7px 12px;
            font-weight: 400;
            line-height: 1.42857143; width:100%;
        }


      
            .tbl-main5 table
        { width:60%; margin:0 auto; float:none;
             background-color:#eaeaea; 
             border:1px solid #ddd; padding:10px; 

        }

        .tbl-main5 table tr td
        { width:27.3%; 
          font-size:17px;  
          color:#333; 
        }



        .tbl-main5 table tr td input[type=text] {   border: solid 1px #CCCCCC;
            background-color: #FFFFFF;
            text-align: left !important;
            padding: 7px 12px;
            font-weight: 400;
            line-height: 1.42857143; 
            width: 72%;  
        }


      .tbl-main5 table tr td .datebutton
        { background-color: #5bc0de;
    border-color: #46b8da;
    height: 34px;
    width: 30px;
    margin-left: -6px;
    color: #fff;
        }


       .tbl-main5 table tr td input[type=submit]
            {     color: #fff;
                    background-color: #5bc0de;
                    border-color: #46b8da;
                    display: inline-block;
                    padding: 6px 12px;
                    margin-bottom: 0;
                    font-size: 14px;
                    font-weight: 400;
                    line-height: 1.42857143;
                    text-align: center;
                    white-space: nowrap;
                    vertical-align: middle;
                    -ms-touch-action: manipulation;
                    touch-action: manipulation;
                    cursor: pointer;
                    -webkit-user-select: none;
                    -moz-user-select: none;
                    -ms-user-select: none;
                    user-select: none;
                    background-image: none;
                    border: 1px solid transparent;
                    border-radius: 4px; width:100%;     
                    margin-top: 15px;
            }
        .tbl-main5 table tr td select {   
            border: solid 1px #CCCCCC;
            background-color: #FFFFFF;
            text-align: left !important;
            padding: 7px 12px;
            font-weight: 400;
            line-height: 1.42857143; width:100%;
        }


        .header-part{ padding:20px;
                      background-color: #5bc0de;
                    border-color: #46b8da; margin-bottom:30px; text-align:center;
        }


         .header-part h2{ color:#fff; font-size:30px;
        }

        .auto-style1
        {
            width: 33%;
        }
          .button-link {
    padding: 10px 15px;
    background: #4479BA;
    color: #FFF;
    font-size:medium;
}

    </style>
</head>
<body class="bg-img">
    <form id="form1" runat="server">
           <div class="sitecontainer whiteBackground1">
           <div class="container">
            <div class="row">
            <div class="col-md-12 text-center White-Box" >
            
            
              <h2>Your Bookings <span class=" pull-right"> <asp:LinkButton ID="LinkButton1" runat="server" CssClass="button-link" OnClick="LinkButton1_Click">Logout</asp:LinkButton></span></h2>
              
              
              <div class=" clearfix"> </div>
              
              
            </div>
          </div> 
          </div>
               <center>
    <div style="width: 100%">
       

        <asp:GridView ID="gdvAllBookings" ForeColor="#333333" style="margin-bottom:30px;width: 100%" Font-Size="12px" runat="server" AllowPaging="True" OnPageIndexChanging="gdvAllBookings_PageIndexChanging" PageSize="50" >
               <AlternatingRowStyle BackColor="White" />
                      <EditRowStyle BackColor="#2461BF" />
                      <FooterStyle BackColor="#5bc0de" Font-Bold="True" ForeColor="Black" />
                      <HeaderStyle CssClass="GridHeader" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                      <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                      <RowStyle BackColor="#EFF3FB" />
                      <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                      <SortedAscendingCellStyle BackColor="#F5F7FB" />
                      <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                      <SortedDescendingCellStyle BackColor="#E9EBEF" />
                      <SortedDescendingHeaderStyle BackColor="#4870BE" />

        </asp:GridView>


    </div>
  <asp:Button ID="Button1" runat="server" CssClass="btn btn-info" Text="Back" OnClick="Button1_Click"  />
    </center>
               </div>


    </form>
</body>
</html>
