<%@ Page Language="C#" AutoEventWireup="true" CodeFile="available.aspx.cs" Inherits="Cruise_Booking_available" EnableEventValidation="false" %>

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
    <!-- Modernizr -->
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
    <link rel="stylesheet" href="css/Newcss/animate.css" type="text/css" media="all">
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
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

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
                                    <li><a runat="server" id="lnkCustomerRegis" href="../Masters/NewRegister.aspx">Customer </a>


                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </div>

                </div>
            </nav>
            <!-- //Navbar -->


            <div class="ak-available-container">

                <div class="container available">
                    <div class="row">
                        <div class="col-sm-12 col-md-5">
                            <h2 class="text-left">
                                <asp:Label ID="lblAccomname" runat="server" Text=""></asp:Label></h2>
                        </div>
                        <div class="col-sm-12 col-md-7">
                            <section style="padding-top: 0; border-bottom: none;">


                                <nav>
                                    <ol class="cd-breadcrumb triangle">
                                        <li><em>Search</em></li>
                                        <li class="current"><em>Available Rooms</em></li>
                                        <li><em>Book and Pay</em></li>
                                        <%--<li><a href="#">Project</a></li>--%>
                                    </ol>
                                </nav>
                            </section>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="updatepanel1" runat="server">
                        <ContentTemplate>
                            <h4 class="text-left" style="padding-bottom: 6px; font-size: 30px; color: #1dc8d9;">
                                <asp:Label ID="lblRoom" runat="server" Text=" "></asp:Label></h4>
                            <div class="row">
                                <div class="room-detail col-sm-8" runat="server" id="dvclass">
                                    <%--<a href="images/awards.jpg"></a>--%>
                                    <asp:GridView ID="gdvHotelRoomRates" runat="server" ForeColor="#333333" GridLines="None" Font-Size="Medium" AutoGenerateColumns="False" OnRowDataBound="gdvHotelRoomRates_RowDataBound" Width="100%" OnSelectedIndexChanged="gdvHotelRoomRates_SelectedIndexChanged">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton2" runat="server" CommandArgument='<%#Eval("RoomCategoryId") %>'></asp:LinkButton>
                                                    <asp:HiddenField ID="hfrctId" runat="server" Value='<%#Eval("RoomCategoryId") %>' />

                                                </ItemTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hfrtype" runat="server" Value='<%#Eval("RoomTypeId") %>' />

                                                </ItemTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hfMaxGuests" runat="server" Value='<%#Eval("MaxGuests") %>' />

                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-CssClass="ClassHotelRoomrates">
                                                <ItemTemplate>
                                                    <div class="col-sm-7">

                                                        <img src='<%# Eval("ImagePath") %>' class="img-responsive" style="max-width: 619px; max-height: 300px;" alt="room-">
                                                    </div>



                                                    <div class="col-sm-5">
                                                        <div class="row">
                                                            <div class="col-sm-12 top-div" style="padding-bottom: 10px;">
                                                                <div class="row">
                                                                    <!-- <div class="col-sm-3">
							<label>Room</label>
							<p>Pool Facing Dlx Cottage</p>
						</div> -->
                                                                    <h3><%# Eval("RoomCategory") %></h3>
                                                                    <div class="col-sm-4" style="margin-right: 1%; padding-left: 0;">

                                                                        <div class="form-group">
                                                                            <img src="images/down.png" class="img-responsive" alt="">
                                                                            <asp:Label ID="Label1" Visible="false" runat="server" Text=""></asp:Label>

                                                                            <asp:DropDownList ID="ddlConvert" class="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlConvert_SelectedIndexChanged">
                                                                                <asp:ListItem Value="1">Single</asp:ListItem>
                                                                                <asp:ListItem Value="2">Twin</asp:ListItem>
                                                                                <asp:ListItem Value="3">Double</asp:ListItem>
                                                                                <asp:ListItem Value="4">Triple</asp:ListItem>
                                                                                <%--<asp:ListItem>No</asp:ListItem>--%>
                                                                            </asp:DropDownList>
                                                                            <%--<select class="form-control" id="sel1">

                                                            <option>No</option>
                                                            <option>Yes</option>
                                                        </select>--%>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-4">

                                                                        <div class="form-group" style="width: 50%;">
                                                                            <%--<img src="images/down.png" class="img-responsive" alt="">--%>
                                                                            <asp:DropDownList class="form-control" Enabled="false" ID="ddlGuests" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlGuests_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                            <%-- <select class="form-control" id="sel1">
                                                            <option>1</option>
                                                            <option>2</option>
                                                            <option>3</option>
                                                            <option>4</option>
                                                            <option>5</option>
                                                            <option>6</option>
                                                        </select>--%>
                                                                        </div>
                                                                    </div>



                                                                    <div class="col-sm-12" style="padding-top: 10px; text-align: justify; padding-left: 0;">

                                                                        <p>


                                                                            <%# Eval("RoomDescription") %><%-- avg.per night--%>
                                                                        </p>
                                                                    </div>
                                                                    <div class="col-sm-12" style="padding-top: 20px; text-align: justify; padding-left: 0;">
                                                                        <label style="font-size: 18px; margin-bottom: 5px;">Rate Includes</label>
                                                                        <p><%# Eval("description") %></p>

                                                                    </div>
                                                                    <div class="col-sm-12" style="padding-top: 13px;">

                                                                        <p style="font-size: 24px; font-family: 'Montserrat', sans-serif;" class="text-right">
                                                                            <strong>INR  <%# Eval("Amt", "{0:0,00}") %>
                                                                                <%-- <%# Eval("Amtc") %>--%></strong><%-- avg.per night--%>
                                                                        </p>
                                                                    </div>
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
                                                        <div class="row" style="padding: 0; margin: 0;">
                                                            <!-- <div class="col-sm-3">
							<label>Detail</label>
							<p>Stone Cottages (201 - 208)</p>Session["GetDescription"]
						</div> -->
                                                            <%--<div class="col-sm-4">
                                                        <label>Room</label>
                                                        <p><%# Eval("RoomCategory") %></p>
                                                    </div>--%>

                                                            <div class="col-sm-12 text-center">

                                                                <asp:Button ID="btnBook" class="btn btn-primary wow agileits  fadeInUp pull-right" runat="server" OnClick="btnBook_Click" Text="Add Room" Style="padding: 8px 0px;" />
                                                                <%--<button class="btn btn-primary wow agileits  fadeInUp pull-right" id="book-resort">Add to Cart<span class="glyphicon agileits  glyphicon-arrow-right" aria-hidden="true"></span></button>--%>
                                                            </div>

                                                        </div>


                                                    </div>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                </div>
                                <div class="col-sm-4" style="padding-left: 3px;" id="dvselectedroom" runat="server">
                                    <div class="RoomRatesBox White-Box2" runat="server" id="DivRmRate">
                                        <div class="container DivRmRate-selectRoom" style="padding: 20px !important; background: #fff; box-shadow: 2px 2px 8px #cacaca; border-radius: 5px; width: 100%; border: 1px solid;">
                                            <h3 style="color: #000; font-size: 24px; padding-bottom: 7px; border-bottom: 1px solid; padding-top: 7px; padding-bottom: 14px;">Selected Rooms</h3>

                                            <div style="margin-left: -20px; margin-right: -20px;">


                                                <asp:GridView ID="gdvSelectedRooms" runat="server" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" Font-Size="Medium" OnRowDataBound="gdvSelectedRooms_RowDataBound" Style="width: 100%;"
                                                    OnRowCommand="gdvSelectedRooms_RowCommand">
                                                    <AlternatingRowStyle BackColor="White" />

                                                    <Columns>
                                                        <%--<asp:BoundField DataField="Rooms" HeaderText="Rooms" ItemStyle-HorizontalAlign="Left" />--%>

                                                        <asp:TemplateField>
                                                            <ItemTemplate>

                                                                <asp:HiddenField ID="hdnRooCatId" runat="server" Value='<%#Eval("RoomCategoryId") %>' />
                                                            </ItemTemplate>

                                                        </asp:TemplateField>

                                                        <%-- <asp:BoundField DataField="categoryName" HeaderText="Name" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="Pax" HeaderText="Pax" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="Price" HeaderText="Price" ItemStyle-HorizontalAlign="Left" />

                            <asp:BoundField DataField="Nights" HeaderText="Nights" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="Tax" HeaderText="Tax" ItemStyle-HorizontalAlign="Left" />

                            <asp:BoundField DataField="Inclusivetax" HeaderText="Total" ItemStyle-HorizontalAlign="Left" />--%>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <div style="height: 1px; width: 104%; background-color: #9E9E9E; margin-left: -2%;">
                                                                </div>
                                                                <div style="height: 7px;"></div>
                                                                <table class="table table-striped" style="margin-bottom: 0; border-color: #000;" border="1">

                                                                    <%--<tr style="visibility: hidden;">
                                                                <td style="font-family: sans-serif; padding-left: 11px;">Rooms</td>
                                                                <td style="height: 1px; width: 26%;"></td>
                                                                <td><%#Eval("Rooms") %></td>
                                                            </tr>--%>

                                                                    <tr>
                                                                        <td style="font-family: sans-serif; padding-left: 11px; padding-left: 11px; font-weight: normal; border-color: #000;">Name</td>

                                                                        <td><%#Eval("categoryName") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="font-family: sans-serif; padding-left: 11px; font-weight: normal">Night/s</td>

                                                                        <td><%#Eval("Nights") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="font-family: sans-serif; padding-left: 11px; font-weight: normal">Room Price</td>

                                                                        <td><%#Eval("Price") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="font-family: sans-serif; padding-left: 11px; font-weight: normal">Gross</td>

                                                                        <td><%#Eval("Total") %></td>
                                                                    </tr>
                                                                    <%-- <tr>
                                                                <td style="font-family: sans-serif; padding-left: 11px; font-weight: normal">Pax</td>

                                                                <td><%#Eval("Pax") %></td>
                                                            </tr>--%>





                                                                    <tr>
                                                                        <td style="font-family: sans-serif; padding-left: 11px; font-weight: normal">Tax</td>

                                                                        <td><%#Eval("Tax") %></td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td style="font-family: sans-serif; padding-left: 11px; font-weight: normal">Total</td>

                                                                        <td><%#Eval("Inclusivetax") %></td>
                                                                    </tr>

                                                                </table>
                                                                <div style="text-align: center; padding-bottom: 10px; padding-top: 10px;">
                                                                    <asp:ImageButton ImageUrl="~/Cruise/Booking/images/closetrash (2).png" ID="imgbtnDelete" CommandName="Remove" runat="server" CssClass="select-btn-delete" />
                                                                </div>
                                                                <asp:HiddenField ID="hdnRmno" runat="server" Value='<%#Eval("RoomNo") %>' />
                                                                <asp:HiddenField ID="hdnCurrency" runat="server" Value='<%#Eval("Currency") %>' />
                                                                <asp:HiddenField ID="hdnConv" runat="server" Value='<%#Eval("ConvDouble") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate></ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                    <EditRowStyle BackColor="#7C6F57" />
                                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle CssClass="GridHeader" Font-Bold="True" ForeColor="Black" HorizontalAlign="Right" />
                                                    <PagerStyle BackColor="#666666" ForeColor="White" />
                                                    <RowStyle BackColor="#ECECEC" />
                                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                                                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                                    <SortedDescendingHeaderStyle BackColor="#15524A" />
                                                </asp:GridView>

                                            </div>
                                            <div class="DivRmRate-selectRoom-bottom" style="padding-top: 19px;">
                                                <div class=" clear"></div>
                                                <div>
                                                    <div style="float: right">
                                                        <h2 style="font-size: 18px; font-weight: bold;">Total :
                                        <asp:Label runat="server" ID="lblCurrency"></asp:Label>
                                                            <asp:Label runat="server" ID="lblRmRate"></asp:Label></h2>
                                                    </div>

                                                </div>

                                                <div id="selected-room-btn" style="padding-top: 28px;">
                                                    <asp:Button ID="Button2" runat="server" CssClass="btn btn-info rightMargin15 font16 pricePanelBtn" Text="Proceed with Payment" OnClick="Button2_Click" Width="100%" />
                                                </div>
                                            </div>
                                            <div class=" clear"></div>

                                        </div>


                                    </div>
                                </div>
                            </div>
                            </div>
                <div id="ak-booking-select">
                </div>
                            <div class="RoomRatesBox" style="display: none">
                                <h2>Meal Plan Rates</h2>
                                <asp:GridView ID="gdvMealplans" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="gdvMealplans_SelectedIndexChanged" Font-Size="Medium">
                                    <AlternatingRowStyle BackColor="White" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5bc0de" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>

                            </div>

                            <div class="RoomRatesBox" style="display: none">
                                <asp:Label ID="lblMealTotal" runat="server" Text=""></asp:Label>
                                <asp:GridView ID="gdvHotelServiceRates" runat="server" Caption="Service Rates" Width="50%" CellPadding="4" ForeColor="#333333" GridLines="None" Font-Size="Medium">
                                    <AlternatingRowStyle BackColor="White" />
                                    <EditRowStyle BackColor="#7C6F57" />
                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5bc0de" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#E3EAEB" />
                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                    <SortedDescendingHeaderStyle BackColor="#15524A" />

                                </asp:GridView>


                                <div class="Hotel-Booking-Tbl-2">
                                    <table>
                                        <tr>

                                            <td>Date
                        <asp:DropDownList ID="ddlDates" runat="server"></asp:DropDownList></td>

                                            <td>Plan
                        <asp:DropDownList ID="ddlMealPlan" runat="server"></asp:DropDownList></td>
                                            <td>
                                                <asp:Button ID="btnAddmealplan" runat="server" Text="Add" OnClick="btnAddmealplan_Click" /></td>

                                        </tr>

                                    </table>
                                </div>
                            </div>

                            <div class="RoomRatesBox">
                                <%--          <h2>Meal Plan Rates</h2>--%>
                                <asp:GridView ID="gdvselectedMealplan" runat="server" Width="50%" CellPadding="4" Font-Size="Medium" ForeColor="#333333" GridLines="None">
                                    <AlternatingRowStyle BackColor="White" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5bc0de" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>
                            </div>


                            <div class="RoomRatesBox">
                                <%--<asp:GridView ID="gdvHotelServiceRates" runat="server" Caption="Service Rates" Width="50%" CellPadding="4" ForeColor="#333333" GridLines="None" Font-Size="Medium">
                <AlternatingRowStyle BackColor="White" />
                <EditRowStyle BackColor="#7C6F57" />
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5bc0de" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#E3EAEB" />
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                <SortedAscendingHeaderStyle BackColor="#246B61" />
                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                <SortedDescendingHeaderStyle BackColor="#15524A" />

            </asp:GridView>--%>


                                <%--    <asp:Label ID="lblMealTotal" runat="server" Text=""></asp:Label>--%>


                                <%--<div class="Hotel-Booking-Tbl-2">        
               


                  
            <table>
                <tr>
                   
                    <td>Date
                        <asp:DropDownList ID="ddlDates" runat="server"></asp:DropDownList></td>
                   
                    <td>Plan
                        <asp:DropDownList ID="ddlMealPlan" runat="server"></asp:DropDownList></td>
                    <td>
                        <asp:Button ID="btnAddmealplan" runat="server" Text="Add" OnClick="btnAddmealplan_Click" /></td>

                </tr>

            </table>
               


                  
                  </div>--%>
                            </div>


                            </div>


        </div>
                        </ContentTemplate>
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

                    <ajaxToolkit:AlwaysVisibleControlExtender ID="AlwaysVisibleControlExtender1" runat="server" TargetControlID="Panel1" HorizontalOffset="300" VerticalOffset="150" UseAnimation="False" ScrollEffectDuration="0.1" BehaviorID="AlwaysVisibleControlExtender1" />


                    <%-- <!-- Footer -->
                    <div class="footer agileits ">
                        <div class="container">

                            <div class="col-md-6 col-sm-6 agileits  footer-grids">
                                <div class="col-md-4 col-sm-4 footer-grid agileits  footer-grid-1 wow fadeInUp">
                                    <ul class="agileits ">
                                        <%--  <li class="agileits ">5 Star Hotels</li>
                            <li class="agileits ">Beach Resorts</li>
                            <li class="agileits ">Beach Houses</li>
                            <li class="agileits ">Water Houses</li>--%>
                    <%--  </ul>
                                </div>
                                <div class="col-md-4 col-sm-4 footer-grid agileits  footer-grid-2 wow fadeInUp">
                                    <ul class="agileits ">--%>
                    <%-- <li class="agileits "><a href="gallery.html">Bahamas</a></li>
                            <li class="agileits "><a href="gallery.html">Hawaii</a></li>
                            <li class="agileits "><a href="gallery.html">Miami</a></li>
                            <li class="agileits "><a href="gallery.html">Ibiza</a></li>--%>
                    <%--</ul>
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
                    <%-- <li class="agileits "><a href="#" class="facebook agileits " title="Go to Our Facebook Page"></a></li>
                        <li class="agileits "><a href="#" class="twitter agileits " title="Go to Our Twitter Account"></a></li>
                        <li class="agileits "><a href="#" class="googleplus agileits " title="Go to Our Google Plus Account"></a></li>
                        <li class="agileits "><a href="#" class="instagram agileits " title="Go to Our Instagram Account"></a></li>
                        <li class="agileits "><a href="#" class="youtube agileits " title="Go to Our Youtube Channel"></a></li>--%>
                    <%-- </ul>--%>
                    <%-- </div>

                            <div class="col-md-6 col-sm-6 footer-grids agileits  copyright wow fadeInUp">
                                <p>&copy; 2017 Resorts. All Rights Reserved | Design by</p>
                            </div>
                            <div class="clearfix"></div>

                        </div>
                    </div>--%>
                    <!-- //Footer -->
                    <!-- // $$$$$$$$%%%%%%%%%%%%%%%%%%%%% pakages Details &&&&&&&&&&&&&&&&&&&&&&&&&&&&&& -->
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
                    <script type="text/javascript">
                        $('#book-resort').click(function (event) {
                            $('#ak-booking-select').html('<div class="container select-room"><button id="ak-booking-del" class="btn btn-default btn-lg"aria-label="Left Align"type=button><span aria-hidden=true class="glyphicon first-span glyphicon-remove-sign"></span></button><div class=select><div class="agileits wow location-grids location-grids-1 select-down slideInDown"><div class=table-responsive><table class="table table-bordered table-striped"><h3>Selected Rooms</h3><tr class=first-row><th>Rooms<th>Name<th>Pax<th>Price<th>Nights<th>Total<tr class=second-row><td>1<td>Deluxe Room<td>2<td>INR 15000<td>1<td>INR 15000</table></div></div><div class="agileits wow location-grids location-grids-1 col-md-2 slideInUp total">Total : INR 15000</div><button class="agileits wow btn btn-primary fadeInUp"><a href="booking-details.html">CONTINUE</a><span aria-hidden=true class="agileits glyphicon glyphicon-arrow-right"></span></button></div></div>');
                        });
                        $('.ak-available-container').on('click', '#ak-booking-del', function () {
                            $('#ak-booking-select').html('');
                        });
                    </script>
                    <!-- //Animate.CSS-JavaScript -->
    </form>
</body>
</html>
