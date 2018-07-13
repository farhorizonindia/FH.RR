<%@ Page Language="C#" AutoEventWireup="true" Inherits="Cruise_booking_CruiseBooking" CodeFile="CruiseBooking.aspx.cs" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <link href='http://fonts.googleapis.com/css?family=Source+Sans+Pro:400,700' rel='stylesheet' type='text/css'>

    <link rel="stylesheet" href="css/css/reset.css">
    <!-- CSS reset -->
    <link rel="stylesheet" href="css/css/style.css">
<<<<<<< HEAD

   
=======
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955
    <!-- Resource style -->
    <script src="js/modernizr.js"></script>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <meta name="author" content="Pandaw Cruises Ltd" />

    <!--<link href="../../css/style.css" rel="stylesheet" />
    <link href="../../css/newcss.css" rel="stylesheet" />-->

    <link href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" media="all" href="../../style.css" />
    <link href="../../css/newcss.css" rel="stylesheet" />
    <%--<script type="text/javascript" src="../../js/jquery-1.11.1.min.js"></script>
    <script type="text/javascript" src="../../js/bootstrap.min.js"></script>--%>

    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.0.3/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <script src="js/QueryString.js" type="text/javascript"></script>
    <title>Cruise Booking Application</title>
    <meta name="description" content="" />
    <script src="js/expedition.js" type="text/javascript"></script>
    <style>
        .polygonbooked:hover {
            background: url("http://www.shortstack.com/wp-content/uploads/2016/04/shortstack-podcast-profile-2.jpg") no-repeat; /* <–copy the URL for your image and paste it here */
        }

        .button-link {
            padding: 10px 15px;
            background: #4479BA;
            color: #FFF;
            font-size: medium;
        }

        th {
            text-align: center;
        }

        .auto-style2 {
            height: 17px;
        }

        tr {
            text-align: center;
        }

        h1 {
            font-size: 34px;
        }

        .rightalign {
            text-align: right;
        }

        caption {
            color: black;
            font-weight: bold;
        }

        .auto-style3 {
            height: 31px;
        }

        area {
     outline: none;
     border:#000 solid 4px;
     background:#000;
     font-size:28px;
}
area:focus {outline: none!important; border:0!important; }

        a {
    outline: none;
    outline-color:transparent;
    outline-style: auto;
    outline-width: 0px;
}

        a:focus {
    outline: none;
    outline-color:transparent;
    outline-style: auto;
    outline-width: 0px;
}

    </style>
    <script>

   
        function disp_confirm() {

            $("html, body").delay(500).animate({
                scrollTop: $('#divscrollto').offset().top
            }, 500);
            setTimeout(function () {
                $("#lblmessage").html("");
            }, 5000);

        }
        function blockArea() {
            $('area[alt="Not Available"]').css('cursor', 'not-allowed');
        }
    </script>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <script type="application/x-javascript"> addEventListener("load", function() { setTimeout(hideURLbar, 0); }, false); function hideURLbar(){ window.scrollTo(0,1); } </script>
    <!-- //Meta-Tags -->

    <!-- Custom-Stylesheet-Links -->
    <!-- Bootstrap-CSS -->
    <link rel="stylesheet" href="css/Newcss/bootstrap.min.css" type="text/css" media="all">
    <!-- Index-Page-CSS -->
    <link rel="stylesheet" href="css/Newcss/style.css" type="text/css" media="all">
    <!-- Index-Page-CSS -->
    <link rel="stylesheet" href="css/Newcss/jquery-ui.css" type="text/css" media="all">
    <!-- Animate.CSS -->
    <link rel="stylesheet" href="css/Newcss/animate.css" type="text/css" media="all">
    <!-- //Custom-Stylesheet-Links -->
<<<<<<< HEAD
        <link  rel="stylesheet" id="csspath" runat="server" type="text/css" media="all"/>
=======

>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955
    <!-- Fonts -->
    <!-- Body-Font -->
    <link rel="stylesheet" href="//fonts.googleapis.com/css?family=Open+Sans:400,300,600,700,800" type="text/css">
    <!-- Logo-Font -->
    <link rel="stylesheet" href="//fonts.googleapis.com/css?family=Cinzel+Decorative:400,900,700" type="text/css">
    <!-- Navbar-Font -->
    <link rel="stylesheet" href="//fonts.googleapis.com/css?family=Montserrat:400,700" type="text/css">
    <!-- //Fonts -->
</head>
<body onbeforeunload="HandleBackFunctionality()" class="bg-img1">
    <form id="form1" runat="server">
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
<<<<<<< HEAD

                      
                </div>
                
                <div id="navbar" class="navbar-collapse agileits  navbar-right collapse">
                      <asp:DropDownList id="ddlCurrency"
                AppendDataBoundItems="True"
                runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCurrency_SelectedIndexChanged" >
             
                             <asp:ListItem  Value="USD">INR</asp:ListItem>
                             <asp:ListItem  Value="INR">USD</asp:ListItem>
           </asp:DropDownList>

                     

=======
                </div>

                <div id="navbar" class="navbar-collapse agileits  navbar-right collapse">
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955
                    <asp:Label ID="lblUsername" runat="server" Font-Bold="true" ForeColor="Red" Text=" "></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="button-link" OnClick="LinkButton1_Click">Logout</asp:LinkButton>
                    <ul class="nav agileits  navbar-nav" runat="server" id="navlogin">
                        <li class="dropdown">
                            <a id="lblLoginas" runat="server" href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">LOGIN <span class="caret"></span></a>
                            <ul class="dropdown-menu">
                                <li><a runat="server" id="lnkLogin" href="agentLogin1.aspx">Partner</a></li>
                                <li><a runat="server" id="lnkCustomerRegis" href="../Masters/NewRegister.aspx">Customer </a>


                                </li>
                            </ul>
                        </li>
                    </ul>
                </div>

            </div>
        </nav>
        <!-- //Navbar -->

        <br />
        <br />
        <br />
        <br />

        <br />
        <section style="height: 74px; padding-top: 0px;">


            <nav>
                <ol class="cd-breadcrumb triangle">
                    <li><em>Search</em></li>
                    <li><em>Packages</em></li>
                    <li><em>Choose Date</em></li>
                    <li class="current"><em>Select Cabin</em></li>
                    <%--<li><a href="#0">Reservation Details & Check Out</a></li>--%>
                    <li><em>Details & Check Out</em></li>
                </ol>
            </nav>
        </section>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>


        <asp:UpdatePanel runat="server" ID="upd1">
            <ContentTemplate>
                <asp:TextBox ID="txtBookingRef" runat="server" Style="display: none"></asp:TextBox>

                <%--                <div class="container">
                    <div class="row">
                        <div id="headings1" runat="server" class="col-md-12 text-center White-Box">
                            <h2>Select Cabin Category 
              <span class=" pull-right">
                  <asp:Label ID="lblUsername" runat="server" Font-Bold="true" ForeColor="Red" Text=" "></asp:Label>
                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  <%--<asp:LinkButton ID="lnkLogout" runat="server" CssClass="button-link" BackColor="#4479BA" >Logout</asp:LinkButton>--%>
                <%-- </span>
                            </h2>
                            <div class=" clearfix"></div>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                </div>--%>

                <div class="m2">
                    <div class="booking-online-box White-Box2 padding2">
                        <div class=" col-md-12" style="margin: 0 auto; float: none;">
                            <div class="booking-online-left1">
                                <div id="CruisesInformation" class="booking-online">
                                    <table id="ljl" runat="server" visible="false">
                                        <tr>
                                            <td style="border: thin solid #fff; color: #fff; font-family: 'Montserrat', sans-serif; font-size: 16px">Please Choose No Of Guest </td>
                                            <td>
                                                <asp:TextBox ID="txtPassengers" Visible="true" runat="server" placeholder="No of Guests" OnTextChanged="txtPassengers_TextChanged"></asp:TextBox>
                                                <asp:DropDownList ID="ddlpax" runat="server" Visible="false" AutoPostBack="True" OnSelectedIndexChanged="ddlpax_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">-occupancy -</asp:ListItem>
                                                    <asp:ListItem Value="1" Text="1 Person"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="2 Person"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="3 Person"></asp:ListItem>


                                                </asp:DropDownList></td>
                                            <td>
                                                <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" /></td>
                                        </tr>
                                    </table>
                                    <br />

                                </div>
                                <div id="div1" runat="server" class="booking-online" visible="false">
                                    <table id="tblSelectSection" style="">
                                        <tr>
                                            <td colspan="4" style="font-weight: bold; font-size: 16px; font-family: 'Times New Roman', Times, serif;" class="auto-style2"></td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="font-family: 'Times New Roman'; font-style: italic; font-size: 13px;">Please enter the cabin configuration and then select the cabin no on the deck plan to book</td>

                                        </tr>
                                        <tr style="background-color: #EFF3FB">
                                            <td>Bed Configuration</td>
                                            <td>
                                                <asp:DropDownList ID="ddlConvert" runat="server">
                                                    <asp:ListItem Value="1">Double</asp:ListItem>
                                                    <asp:ListItem Value="0">Twin</asp:ListItem>
                                                </asp:DropDownList></td>
                                            <td>Occupancy  </td>
                                            <td>
                                                <asp:DropDownList ID="ddlpax1rm" runat="server">
                                                    <asp:ListItem>-Occupancy-</asp:ListItem>
                                                    <asp:ListItem Text="1 person" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="2 person" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="3 person" Value="3"></asp:ListItem>
                                                </asp:DropDownList></td>
                                        </tr>
                                    </table>
                                    <%-- <asp:CheckBoxList ID="ddlrooms" runat="server" RepeatDirection="Horizontal"></asp:CheckBoxList>--%>
                                </div>
                                <br />

                                <div style="width: 80%; margin: 0 auto; font-size: 16px;">
                                    <table id="PackageDetails" class="table table-bordered">
                                        <thead class="GridHeader" style="font-size: 15px; font-weight: bold;">
                                            <tr>
                                                <th class="text-center">Package Name
                                                </th>
                                                <th class="text-center">Check In Date
                                                </th>
                                                <th class="text-center">Check Out Date
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblPackageName" runat="server" Text=" "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCheckInDate" runat="server" Text=" "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCheckOutDate" runat="server" Text=" "></asp:Label>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <div class="text-center" style="text-align: center; margin-bottom: 17px;">
                                        <asp:DropDownList ID="ddlAgentRef" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlAgentRef_SelectedIndexChanged" Style="padding: 13px 50px; background: #ECECEC !important; font-weight: bold; font-size: 0.9em; font-family: 'Montserrat', sans-serif; border: 1px solid #ddd;">
                                        </asp:DropDownList>
                                    </div>
                                    <%-- <asp:DropDownList ID="ddlAgentRef" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlAgentRef_SelectedIndexChanged"></asp:DropDownList>--%>
                                </div>
                                <div style="width: 58%; overflow-x: scroll; margin: 0 auto;">
<<<<<<< HEAD
                                  
                                    <%--  <asp:ImageMap ID="ImageMap1" runat="server" hidefocus="true"  ImageUrl="~/images/aspnet_imagemap.png"
                                        OnClick="ImageMap1_Click" Style="width: auto;">--%>

                                      <asp:ImageMap ID="ImageMap1" runat="server" hidefocus="true"  
                                        OnClick="ImageMap1_Click" Style="width: auto;">


=======
                                    <asp:ImageMap ID="ImageMap1" runat="server" hidefocus="true"  ImageUrl="~/images/aspnet_imagemap.png"
                                        OnClick="ImageMap1_Click" Style="width: auto;">
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955
                                        <%-- <asp:PolygonHotSpot Coordinates="346,15,374,16,375,59,347,56" AlternateText="210"
                                            HotSpotMode="PostBack" PostBackValue="210" />
                                        <asp:PolygonHotSpot Coordinates="373,13,413,14,410,58,371,58" AlternateText="208"
                                            HotSpotMode="PostBack" PostBackValue="208" />
                                        <asp:PolygonHotSpot Coordinates="416,14,450,15,450,58,426,57,412,55" AlternateText="206"
                                            HotSpotMode="PostBack" PostBackValue="206" />
                                        <asp:PolygonHotSpot Coordinates="455,15,491,15,489,55,455,57" AlternateText="204"
                                            HotSpotMode="PostBack" PostBackValue="204" />
                                        <asp:PolygonHotSpot Coordinates="495,15,527,14,532,60,494,56" AlternateText="202"
                                            HotSpotMode="PostBack" PostBackValue="202" />
                                        <asp:PolygonHotSpot Coordinates="202,145,236,145,237,188,204,188" AlternateText="114"
                                            HotSpotMode="PostBack" PostBackValue="114" />
                                        <asp:PolygonHotSpot Coordinates="239,146,270,147,270,187,240,188" AlternateText="112"
                                            HotSpotMode="PostBack" PostBackValue="112" />
                                        <asp:PolygonHotSpot Coordinates="274,154,327,156,325,176,316,177,314,188,273,188"
                                            AlternateText="110" HotSpotMode="PostBack" PostBackValue="110" />
                                        <asp:PolygonHotSpot Coordinates="346,152,395,154,395,187,348,186" AlternateText="108"
                                            HotSpotMode="PostBack" PostBackValue="108" />
                                        <asp:PolygonHotSpot Coordinates="400,154,450,154,449,187,398,187" AlternateText="106"
                                            HotSpotMode="PostBack" PostBackValue="106" />
                                        <asp:PolygonHotSpot Coordinates="451,154,502,152,500,187,450,189" AlternateText="104"
                                            HotSpotMode="PostBack" PostBackValue="104" />
                                        <asp:PolygonHotSpot Coordinates="501,153,519,153,545,153,548,146,567,152,579,161,587,171,590,178,543,178,542,188,503,187"
                                            AlternateText="102" HotSpotMode="PostBack" PostBackValue="102" />
                                        <asp:PolygonHotSpot Coordinates="224,204,269,204,269,239,226,235" AlternateText="111"
                                            HotSpotMode="PostBack" PostBackValue="111" />
                                        <asp:PolygonHotSpot Coordinates="272,201,317,205,316,216,324,216,330,238,272,240"
                                            AlternateText="109" HotSpotMode="PostBack" PostBackValue="109" />
                                        <asp:PolygonHotSpot Coordinates="348,202,398,201,398,237,352,237" AlternateText="107"
                                            HotSpotMode="PostBack" PostBackValue="107" />
                                        <asp:PolygonHotSpot Coordinates="399,203,425,205,445,205,448,236,419,239,400,237"
                                            AlternateText="105" HotSpotMode="PostBack" PostBackValue="105" />
                                        <asp:PolygonHotSpot Coordinates="451,204,501,204,500,239,453,236" AlternateText="103"
                                            HotSpotMode="PostBack" PostBackValue="103" />
                                        <asp:PolygonHotSpot Coordinates="502,202,542,202,542,211,590,212,588,223,579,234,562,243,549,244,540,239,524,237,508,239,502,239"
                                            AlternateText="101" HotSpotMode="PostBack" PostBackValue="101" />
                                        <asp:PolygonHotSpot Coordinates="345,81,352,81,353,74,372,74,372,116,344,113" AlternateText="209"
                                            HotSpotMode="PostBack" PostBackValue="209" />
                                        <asp:PolygonHotSpot Coordinates="375,73,410,72,413,115,375,115" AlternateText="207"
                                            HotSpotMode="PostBack" PostBackValue="207" />
                                        <asp:PolygonHotSpot Coordinates="414,73,451,72,452,115,416,115" AlternateText="205"
                                            HotSpotMode="PostBack" PostBackValue="205" />
                                        <asp:PolygonHotSpot Coordinates="453,71,491,71,493,115,452,115" AlternateText="203"
                                            HotSpotMode="PostBack" PostBackValue="203" />
                                        <asp:PolygonHotSpot Coordinates="493,72,529,73,532,116,494,115"  AlternateText="201"
                                            HotSpotMode="PostBack" PostBackValue="201" />--%>
                                    </asp:ImageMap>
                                </div>

                                <br />
                                <div style="width: 80%; padding: 20px !important; background: #fff; box-shadow: 2px 2px 8px #cacaca; border-radius: 5px; /* margin-left: -136px; */
    margin: 0 auto; overflow-x: scroll;"
                                    class="tbl_CB">
                                    <asp:GridView class="table table-bordered" ID="gdvRoomCategories" runat="server" Style="width: 100%;"
                                        CellPadding="4" ForeColor="#333333" GridLines="Both" Font-Size="12px" AutoGenerateColumns="false">

                                        <Columns>

                                            <asp:TemplateField HeaderText="Cabin category" HeaderStyle-Width="30%" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="RoomId" Text='<%#Eval("Cabin Category") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Price per person single use">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lbltws" Text='<%#Eval("Price Per Person Single Use") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Price per person sharing">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblsc" Text='<%#Eval("Price Per Person sharing") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--  <asp:TemplateField HeaderText="Tax %">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblsc" Text='<%#Eval("Tax Value").ToString()%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        </Columns>
                                        <AlternatingRowStyle BackColor="White" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="#5bc0de" Font-Bold="True" ForeColor="Black" />
                                        <HeaderStyle CssClass="GridHeader" Font-Bold="True" Font-Size="15px" HorizontalAlign="Left" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#F9F9F9" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                    </asp:GridView>
                                    <br />

                                    <strong>
                                        <div>
                                            <asp:Label ID="lblTax" runat="server" Text=" "></asp:Label>
                                        </div>
                                    </strong>
                                </div>

<<<<<<< HEAD
                                <br />
                                <br />
=======
                                <br />
                                <br />
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955
                                <div id="Div1" class="box box-primary booking-online pax-details" style="width: 80%; padding: 20px !important; background: #fff; box-shadow: 2px 2px 8px #cacaca; border-radius: 5px; overflow-x: scroll; margin: 0 auto;">
                                    <table>
                                        <tr>
                                            <td></td>
                                        </tr>
                                    </table>
                                    <div id="divscrollto" style="text-align:center">
                                        <asp:Label ID="lblmessage" runat="server" ForeColor="Green"  Text=""></asp:Label>
                                        <asp:GridView ID="GridRoomPaxDetail" class="table table-bordered" ShowHeaderWhenEmpty="true" ShowFooter="false" AutoGenerateColumns="False" runat="server" Caption="Cabin Details(Amount in Indian Rupees)" Style="font-size: 15px; min-width: 500px;" OnRowDeleting="GridRoomPaxDetail_RowDeleting" OnRowCommand="GridRoomPaxDetail_RowCommand" OnRowDataBound="GridRoomPaxDetail_RowDataBound" Font-Size="12px">
                                        <HeaderStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Cabin No">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="RoomId" Text='<%#Eval("RoomNumber") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Category">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="RoomCategoryId" Visible="false" Text=""></asp:Label>
                                                    <asp:Label runat="server" ID="lbcategoryName" Text='<%#Eval("categoryName") %>'></asp:Label>
                                                    <asp:DropDownList Visible="false" ID="ddlCategoryType" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlCategoryType_SelectedIndexChanged"></asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bed Config">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="BedConfig" Text="'" Visible="false"></asp:Label>
                                                    <asp:DropDownList ID="ddlbedconfiguration" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlbedconfiguration_SelectedIndexChanged"></asp:DropDownList>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Guests">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="Pax" Text='<%#Eval("Pax") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblCurr" Text='<%#Eval("Currency") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Right">
<<<<<<< HEAD
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="Price" Text='<%#Eval("pricewithouttax1") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Gross">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="Total" Text='<%#Eval("Total") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Discount">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblDiscount1" Text='<%#Eval("Discount") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
=======
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="Price" Text='<%#Eval("pricewithouttax1") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Gross">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="Total" Text='<%#Eval("Total") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Discount">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblDiscount1" Text='<%#Eval("Discount") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955
                                            <asp:TemplateField HeaderText="Taxable Amt">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblTax11" Text='<%#Eval("taxablepamt") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Tax">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblTax1123" Text='<%#Eval("Tax1") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblTotalprice12" Text='<%#Eval("Totalprice") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Tax" Visible="false">
                                                <ItemTemplate>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:ImageButton Width="25px" ImageUrl="~/Cruise/Booking/images/closetrash (2).png" ID="imgbtnDelete" CommandName="Remove" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="GridHeader" Font-Bold="True" Font-Size="15px" />
                                        <AlternatingRowStyle BackColor="White" />
                                        <RowStyle BackColor="#F9F9F9" />
                                        <%-- <FooterStyle CssClass="rightalign" />--%>
                                    </asp:GridView>
                                    </div>
                                    
                                    <div style="width: 100%;">
                                        <table style="width: 42%; float: right;">
                                            <tr>
                                                <td>
                                                    <div class="row" style="padding: 10px;">
                                                        <div class="col-sm-4">
                                                            <strong style="font-size: large;">Total:</strong>
                                                        </div>
                                                        <div class="col-sm-8">
                                                            <asp:Label ID="lblgetTotal" runat="server" Text=" " Style="font-weight: bold; font-size: large;"></asp:Label></strong>
                                                        </div>
                                                    </div>

                                                </td>

                                            </tr>
                                        </table>
                                    </div>
                                    <p id="pMessages" runat="server" style="margin-left: 71px;">
                                        <table style="width: 100%; color: #F9F9F9; margin: 0 119px 0 0; float: right;">
                                            <tr>
                                                <td style="background-color: #F9F9F9;">
                                                    <asp:Label ID="TotalCabins" runat="server" Font-Bold="True" Visible="false" Font-Size="Medium"></asp:Label></td>
                                                <td style="background-color: #F9F9F9;">
                                                    <asp:Label ID="lblTotalCabins" runat="server" Font-Bold="True" Visible="false" Font-Size="Medium"></asp:Label></td>
                                                <td style="background-color: #F9F9F9;">
                                                    <asp:Label ID="lblTotal" runat="server" Font-Bold="True" Visible="false" Font-Size="Large"></asp:Label></td>
                                                <td style="background-color: #F9F9F9;">
                                                    <asp:Label ID="lblCurr" runat="server" Font-Bold="True" Visible="false" Font-Size="Medium"></asp:Label>
                                                    <asp:Label ID="lblTotAmt" runat="server" Font-Bold="True" Visible="false" Font-Size="Medium"></asp:Label></td>
                                            </tr>
                                        </table>

                                        <div class=" clearfix"></div>
                                    </p>


                                    <div class="text-right btncox" id="ButtonsDiv" runat="server">
                                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-info rightMargin15 font16 pricePanelBtn" Style="border-right: 1px solid #1dc8d9 !important;" Text="Back" OnClick="Button1_Click" />
                                        &nbsp;&nbsp;
                                        <asp:Button ID="Button3" runat="server" CssClass="btn btn-info rightMargin15 font16 pricePanelBtn" Style="border-right: 1px solid #1dc8d9 !important;" Text="Reload" OnClick="Button3_Click" />
                                        &nbsp;&nbsp;
                                   <asp:Button ID="Button2" runat="server" CssClass="btn btn-info rightMargin15 font16 pricePanelBtn" Style="border-right: 1px solid #1dc8d9 !important;" Text="Next" OnClick="Button2_Click" />


                                    </div>
                                </div>

                                <!-- Cruise Prices - END -->

                                <!-- Selected Cabins - START -->


                                <asp:GridView ID="GridView1" Visible="false" runat="server"
                                    OnRowDataBound="GridView1_RowDataBound" ShowFooter="True" CellPadding="4" ForeColor="#333333" GridLines="Both">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                                <asp:HiddenField ID="hfRoomId" runat="server" />
                            </div>
                            <div class="clear"></div>
                        </div>
                        <div class="clear"></div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ImageMap1" />
            </Triggers>
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
        <cc1:AlwaysVisibleControlExtender ID="AlwaysVisibleControlExtender1" runat="server"
            TargetControlID="Panel1" HorizontalOffset="300" VerticalOffset="150"></cc1:AlwaysVisibleControlExtender>
        <!-- Footer -->
        <%-- <div class="footer agileits ">
            <div class="container">

                <div class="col-md-6 col-sm-6 agileits  footer-grids">
                    <div class="col-md-4 col-sm-4 footer-grid agileits  footer-grid-1 wow fadeInUp">
                        <ul class="agileits ">--%>
        <%--  <li class="agileits ">5 Star Hotels</li>
                            <li class="agileits ">Beach Resorts</li>
                            <li class="agileits ">Beach Houses</li>
                            <li class="agileits ">Water Houses</li>--%>
        <%--  </ul>
                    </div>
                    <div class="col-md-4 col-sm-4 footer-grid agileits  footer-grid-2 wow fadeInUp">
                        <ul class="agileits ">--%>
        <%--    <li class="agileits "><a href="gallery.html">Bahamas</a></li>
                            <li class="agileits "><a href="gallery.html">Hawaii</a></li>
                            <li class="agileits "><a href="gallery.html">Miami</a></li>
                            <li class="agileits "><a href="gallery.html">Ibiza</a></li>--%>
        <%-- </ul>
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
        <%--<li class="agileits "><a href="#" class="facebook agileits " title="Go to Our Facebook Page"></a></li>
                        <li class="agileits "><a href="#" class="twitter agileits " title="Go to Our Twitter Account"></a></li>
                        <li class="agileits "><a href="#" class="googleplus agileits " title="Go to Our Google Plus Account"></a></li>
                        <li class="agileits "><a href="#" class="instagram agileits " title="Go to Our Instagram Account"></a></li>
                        <li class="agileits "><a href="#" class="youtube agileits " title="Go to Our Youtube Channel"></a></li>--%>
        <%--  </ul>
                </div>

                <div class="col-md-6 col-sm-6 footer-grids agileits  copyright wow fadeInUp">
                    <p>&copy; 2017 Resorts. All Rights Reserved | Design by</p>
                </div>
                <div class="clearfix"></div>

            </div>
        </div>--%>
        <!-- //Footer -->
        <!-- Custom-JavaScript-File-Links -->

        <!-- Default-JavaScript -->
        <script type="text/javascript" src="js/jquery-2.1.4.min.js"></script>
        <!-- Bootstrap-JavaScript -->
        <script type="text/javascript" src="js/bootstrap.min.js"></script>

        <!-- Animate.CSS-JavaScript -->
        <script src="js/wow.min.js"></script>
        <script type="js/index.js"></script>
        <script>
            new WOW().init();
        </script>
        <!-- //Animate.CSS-JavaScript -->
<<<<<<< HEAD

    
=======
>>>>>>> 06df147e7f6e76b3ddcb27473f8305164d96b955
    </form>

</body>

        <script type="text/javascript" language="javascript">
    
            function HandleBackFunctionality() {
                alert("1");
                if (window.event) {
                    if (window.event.clientX < 40 && window.event.clientY < 0) {
                        alert("Browser back button is clicked...");
                    }
                    else {
                        alert("Browser refresh button is clicked...");
                    }
                }
                else {
                    if (event.currentTarget.performance.navigation.type == 1) {
                        alert("Browser refresh button is clicked...");
                    }
                    if (event.currentTarget.performance.navigation.type == 2) {
                        alert("Browser back button is clicked...");
                    }
                }
            }
    </script>
</html>
