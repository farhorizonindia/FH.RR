<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReviewBookingDetails.aspx.cs" Inherits="Cruise_booking_ReviewBookingDetails" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
    <meta name="author" content="Pandaw Cruises Ltd">
    
    <link rel="icon" type="image/png" href="/favicon.ico">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.5/css/bootstrap.min.css" rel="stylesheet" type="text/css">
    <link href="css/style.css" rel="stylesheet">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.0.3/css/font-awesome.min.css" rel="stylesheet" type="text/css">

    <script async="" src="//www.google-analytics.com/analytics.js"></script>
    <script async="" src="//www.google-analytics.com/analytics.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/1.11.3/jquery.min.js" type="text/javascript"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.5/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="js/QueryString.js" type="text/javascript"></script>
    <script src="js/pandaw.js" type="text/javascript"></script>

    <title>Cruise Booking App</title>

    <meta name="description" content="">

    <script src="https://cdnjs.cloudflare.com/ajax/libs/datatables/1.9.4/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="js/expedition.js" type="text/javascript"></script>

</head>

<body>

    <form method="post" runat="server"  id="form1">
        <div class="sitecontainer whiteBackground">
            <div class="col-md-12 spacerGradient"></div>
            <script type="text/javascript">
                Sys.WebForms.PageRequestManager._initialize('ctl00$ContentPlaceHolder1$ScriptManager1', 'form1', [], [], [], 90, 'ctl00');
               
            </script>
            <section id="HeaderSection">

                <div class="row noSideMargin">
                </div>

            </section>
            <section>

                <div class="row">
                    <div class="col-md-12 text-center topbotPadding">
                        <h2 class="goldLine font24"><span class="darkGold">Cruise Booking Application</span></h2>

                    </div>
                    <div class="col-md-12 text-center noTopPadding">
                        <div class="insideSkin">
                            <div class="goldLineSubHeader"><%Response.Write(Request.QueryString["PackName"]); %></div>
                            <div class="darkGold font16 topbotPadding20"><%Response.Write(Request.QueryString["NoOfNights"]); %> NIGHTS </div>

                        </div>
                    </div>
                </div>
            </section>

            <section>

                <div id="bookingTop" class="row noSideMargin backgroundPaperDark innerDropShadow text-center topbotPadding">

                    <div class="col-md-12 text-center bottomPadding50">

                    <h2 class="goldLine paperDarkLine"><span>CHECK AVAILABILITY &amp; BOOK ONLINE</span></h2>
                    </div>
                    <a id="departureList" href="#areaBooking" class="btn btn-info noMargin font16 btnWidth200" role="button" data-toggle="collapse" aria-expanded="true" aria-controls="areaBooking" style="display: none;"><span class="pull-left">Choose your dates</span><span class="caret pull-right topMargin10"></span></a>

                    <div id="areaBooking" class="insideSkin collapse in" style="display: block;" aria-expanded="true">
                        <div id="Step1" class="topbotPadding" style="display: none;">

                            <div class="col-md-12 text-center bottomPadding20">

                                <strong>Please Note</strong>: Prices shown are per person in USD$ and include all discounts.

                            </div>



                            <div id="pricePanelRow" class="botBorderWhite">
                                <p class="bottomPadding30 font24"><i class="fa fa-cog fa-spin font24"></i>Loading Dates &amp; Prices...</p>
                            </div>

                        </div>

                        <div id="Step2" class="bottomPadding30" style="display: none;">

                            <h3 class="bottomPadding30">SELECT GUESTS &amp; STATEROOMS</h3>

                            <div id="departureInformation"></div>

                            <div class="col-md-6">

                                <p class="bold">Choose a vacant stateroom by clicking on the deckplan below. Click once to select a stateroom for two people, again for single occupancy and once more to clear your selection.</p>

                                <div id="CabinSelect" style="text-align: left; position: relative; margin-left: auto; margin-right: auto; width: 490px;" class="hidden">

                                    <img id="Deckplan" src="." border="0" alt="" width="490">
                                </div>

                            </div>

                            <div class="col-md-6">

                                <div id="CruisesInformation" class="box box-primary" style="width: 490px; margin-left: auto; margin-right: auto; background-image: url(/images/template/content-background-dark.jpg); background-repeat: repeat;">

                                    <div class="box-header">

                                        <h3 class="box-title noTopPadding">PRICES (PER TRAVELLER)</h3>
                                        <div id="Cruises_Description">* PRICES BELOW INCLUDE ANY DISCOUNT</div>

                                    </div>

                                    <div class="box-body table-responsive no-padding">

                                        <div id="Cruises_PriceTable_wrapper" class="dataTables_wrapper" role="grid">
                                            <div id="Div1" class="dataTables_wrapper" role="grid">
                                                <table id="Cruises_PriceTable" class="table dataTable" width="100%" style="width: 100%;">
                                                    <thead>
                                                        <tr role="row">
                                                            <th class="sorting_disabled" role="columnheader" rowspan="1" colspan="1" style="width: 54px;">STATEROOM TYPE</th>
                                                            <th class="sorting_disabled" role="columnheader" rowspan="1" colspan="1" style="width: 14px;">PRICE PER PERSON</th>
                                                        </tr>
                                                    </thead>

                                                    <tbody role="alert" aria-live="polite" aria-relevant="all">
                                                        <tr class="odd">
                                                            <th class=" "></th>
                                                            <th class=" "></th>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>

                                    </div>

                                </div>
                                <!-- Cruise Prices - END -->


                                <p>&nbsp;</p>


                                <!-- Selected Cabins - START -->
                                <div id="SelectedInformation" class="box box-primary" style="width: 490px; margin-left: auto; margin-right: auto; background-image: url(/images/template/content-background-dark.jpg); background-repeat: repeat;">

                                    <div class="box-header">

                                        <h3 class="box-title">YOUR CURRENT SELECTION</h3>

                                    </div>

                                    <div class="box-body table-responsive no-padding">

                                        <div id="Selected_PriceTable_wrapper" class="dataTables_wrapper" role="grid">
                                            <div id="Div2" class="dataTables_wrapper" role="grid">
                                                <table id="Selected_PriceTable" class="table dataTable" width="100%" style="width: 100%;">
                                                    <thead>
                                                        <tr role="row">
                                                            <th class="sorting_disabled" role="columnheader" rowspan="1" colspan="1" style="width: 44px;">STATEROOM TYPE</th>
                                                            <th class="sorting_disabled" role="columnheader" rowspan="1" colspan="1" style="width: 0px;">TRAVELLERS</th>
                                                            <th class="sorting_disabled" role="columnheader" rowspan="1" colspan="1" style="width: 14px;">SUB TOTAL</th>
                                                            <th class="sorting_disabled" role="columnheader" rowspan="1" colspan="1" style="width: 0px;"></th>
                                                            <th class="sorting_disabled" role="columnheader" rowspan="1" colspan="1" style="width: 0px;"></th>
                                                        </tr>
                                                    </thead>

                                                    <tbody role="alert" aria-live="polite" aria-relevant="all">
                                                        <tr class="odd">
                                                            <th class=" "></th>
                                                            <th class=" "></th>
                                                            <th class=" "></th>
                                                            <th class=" "></th>
                                                            <th class=" "></th>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>

                                    </div>

                                </div>

                                <div class="text-right rightMargin10"><a class="btn btn-info rightMargin15 font16 pricePanelBtn">Back</a><a class="btn btn-info noMargin font16 step3Btn">Continue</a></div>

                            </div>

                        </div>

                        <div id="Step3" class="topbotPadding" style="display: none;">

                            <h3 class="bottomPadding30">WOULD YOU LIKE TO ADD AN OPTIONAL EXTENSION?</h3>

                            <div id="preTourHeading" class="col-md-12 text-center extensionsBar darkGold hidden">PRE CRUISE</div>

                            <div id="preTourRow" class="btn-group bottomPadding30" data-toggle="buttons">

                                <div class="col-md-12 noSidePadding topPadding20">

                                    <div class="col-md-4 noSidePadding">

                                        <img src="[EXTENSIONIMAGE]" border="0" alt="[EXTENSIONALT]" class="img-responsive">
                                    </div>

                                    <div class="col-md-8">

                                        <div class="box-header">

                                            <h2 class="box-title">[EXTENSIONNAME]</h2>

                                        </div>

                                        <div class="box-body table-responsive no-padding extensionTable">

                                            <table id="[PRODUCTID]" class="table exensionPriceTable" width="100%">
                                                <thead>
                                                    <tr>
                                                        <th>Option</th>
                                                        <th>Price per Traveller</th>
                                                        <th>Travellers</th>
                                                        <th>Sub Total</th>
                                                        <th></th>
                                                    </tr>
                                                </thead>
                                                <tbody></tbody>
                                            </table>

                                        </div>

                                        <table class="buttonTable pull-right">
                                            <tbody>
                                                <tr>
                                                    <td class="bold topPadding10 rightPadding20">Add this tour</td>
                                                    <td><a class="btn btn-info font16 topMargin10 botMargin10 [CLASS]" data-productid="[PRODUCTID]" data-type="[DATATYPE]" data-extensiontype="[EXTENSIONTYPE]">Yes Please</a></td>
                                                </tr>
                                            </tbody>
                                        </table>

                                        <div class="puller"></div>


                                    </div>

                                </div>

                            </div>

                            <div id="postTourHeading" class="col-md-12 text-center extensionsBar darkGold hidden">POST CRUISE</div>

                            <div id="postTourRow" class="btn-group" data-toggle="buttons"></div>

                            <div class="text-right rightMargin10 topbotPadding"><a class="btn btn-info noMargin font16 step4Btn">Continue</a></div>

                        </div>

                        <div id="Step4" class="topbotPadding" style="display: none;">

                            <h3 class="bottomPadding30">WOULD YOU LIKE TO ANY OPTIONAL ADDITIONS?</h3>

                            <div id="addonsTourHeading" class="col-md-12 text-center extensionsBar darkGold hidden">CRUISE ADDONS</div>

                            <div id="addOnsRow" class="btn-group bottomPadding30" data-toggle="buttons">

                                <div class="col-md-12 noSidePadding topPadding20">

                                    <div class="col-md-4 noSidePadding">

                                        <img src="[EXTENSIONIMAGE]" border="0" alt="[EXTENSIONALT]" class="img-responsive">
                                    </div>

                                    <div class="col-md-8">

                                        <h2>[EXTENSIONNAME]</h2>

                                        <div class="box-body table-responsive no-padding">

                                            <table class="table exensionPriceTable" width="100%">
                                                <thead>
                                                    <tr>
                                                        <th>Option</th>
                                                        <th>Price per Traveller</th>
                                                        <th>Travellers</th>
                                                        <th>Sub Total</th>
                                                    </tr>
                                                </thead>
                                                <tbody></tbody>
                                            </table>

                                        </div>

                                    </div>

                                </div>

                            </div>

                            <div class="text-right rightMargin10 topbotPadding"><a class="btn btn-info noMargin font16 step5Btn">Continue</a></div>

                        </div>

                        <div id="Step5" class="topbotPadding">

                            <h3 class="bottomPadding30">REVIEW YOUR BOOKING DETAILS</h3>

                            <div class="col-md-12 text-center topbotPadding">

                                <h2 class="goldLine font20 letterStyling paperDarkLine"><span>EXPEDITIONS</span></h2>

                            </div>

                            <div class="insideSkin">

                                <div class="text-right rightMargin10 bottomPadding30">
                                    <input type="button" name="ctl00$ContentPlaceHolder1$btnClearBooking" value="Clear Booking" onclick="javascript: __doPostBack('ctl00$ContentPlaceHolder1$btnClearBooking', '')" id="ContentPlaceHolder1_btnClearBooking" class="btn btn-info font16"></div>

                                <div class="puller"></div>

                                <div class="col-md-12">

                                    <div class="box-body table-responsive no-padding expeditionsSummary">

                                        <table class="table" width="100%">
                                            <thead>
                                                <tr>
                                                    <th class="col-md-6 text-left bold">EXPEDITION NAME</th>
                                                    <th class="col-md-3 text-center bold">NIGHTS</th>
                                                    <th class="col-md-3 text-center bold">DATE</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td class="col-md-6 text-left normal"><%Response.Write(Request.QueryString["PackName"].ToString()); %></td>
                                                    <td class="col-md-3 text-center normal"><%Response.Write(Request.QueryString["NoOfNights"].ToString()); %></td>
                                                    <td class="col-md-3 text-center normal"><%Response.Write(Request.QueryString["CheckinDate"].ToString()); %></td>
                                                </tr>
                                            </tbody>
                                        </table>

                                        <div class="puller"></div>
                                    </div>

                                </div>

                            </div>

                            <div class="col-md-12 text-center topbotPadding">

                                <h2 class="goldLine font20 letterStyling paperDarkLine"><span>STATEROOM</span></h2>

                            </div>

                            <div class="insideSkin">

                                <div class="col-md-12">

                                    <div class="box-body table-responsive no-padding stateroomsSummary">

                                        <table class="table" width="100%">
                                            <thead>
                                                <tr>
                                                    <th class="col-md-6 text-left bold">STATEROOM TYPE</th>
                                                    <th class="col-md-3 text-center bold">TRAVELLERS</th>
                                                    <th class="col-md-3 text-center bold">SUB TOTAL</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                               <%for(LoopCounter=0; LoopCounter<dtGetBookedRooms.Rows.Count; LoopCounter++) %>
                                                <%{ %>
                                                <tr>
                                                    <td class="col-md-6 text-left normal"><%Response.Write(dtGetBookedRooms.Rows[LoopCounter]["categoryName"].ToString()); %></td>
                                                    <td class="col-md-3 text-center normal"><%Response.Write(dtGetBookedRooms.Rows[LoopCounter]["Pax"].ToString()); %></td>
                                                    <td class="col-md-3 text-center normal"><%Response.Write(dtGetBookedRooms.Rows[LoopCounter]["Price"].ToString()); %></td>
                                                </tr>
                                                <%} %>
                                            </tbody>
                                        </table>
                                        <div class="puller"></div>
                                    </div>

                                </div>

                            </div>

                            <div id="extensionsArea" class="hide">

                                <div class="col-md-12 text-center topbotPadding">

                                    <h2 class="goldLine font20 letterStyling paperDarkLine"><span>EXTENSIONS</span></h2>

                                </div>

                                <div class="insideSkin">

                                    <div class="col-md-12">

                                        <div class="box-body table-responsive no-padding extensionsSummary">

                                            <table class="table" width="100%">
                                                <thead>
                                                    <tr>
                                                        <th class="col-md-6 text-left bold">DESCRIPTION</th>
                                                        <th class="col-md-3 text-center bold">TRAVELLERS</th>
                                                        <th class="col-md-3 text-center bold">SUB TOTAL</th>
                                                    </tr>
                                                </thead>

                                            </table>

                                            <div class="puller"></div>
                                        </div>

                                    </div>

                                </div>

                            </div>

                            <div id="addonsArea" class="hide">

                                <div class="col-md-12 text-center topbotPadding">

                                    <h2 class="goldLine font20 letterStyling paperDarkLine"><span>ADD ONS</span></h2>

                                </div>

                                <div class="insideSkin">

                                    <div class="col-md-12">

                                        <div class="box-body table-responsive no-padding addonsSummary">

                                            <table class="table" width="100%">
                                                <thead>
                                                    <tr>
                                                        <th class="col-md-6 text-left bold">DESCRIPTION</th>
                                                        <th class="col-md-3 text-center bold">TRAVELLERS</th>
                                                        <th class="col-md-3 text-center bold">SUB TOTAL</th>
                                                    </tr>
                                                </thead>

                                            </table>

                                            <div class="puller"></div>
                                        </div>

                                    </div>

                                </div>

                            </div>

                            <div id="surchargeArea" class="hide">

                                <div class="col-md-12 text-center topbotPadding">

                                    <h2 class="goldLine font20 letterStyling paperDarkLine"><span>SURCHARGES</span></h2>

                                </div>

                                <div class="insideSkin">

                                    <div class="col-md-12">

                                        <div class="box-body table-responsive no-padding surchargesSummary">

                                            <table class="table" width="100%">
                                                <thead>
                                                    <tr>
                                                        <th class="col-md-6 text-left bold">DESCRIPTION</th>
                                                        <th class="col-md-3 text-center bold">TRAVELLERS</th>
                                                        <th class="col-md-3 text-center bold">SUB TOTAL</th>
                                                    </tr>
                                                </thead>

                                            </table>

                                            <div class="puller"></div>
                                        </div>

                                    </div>

                                </div>

                            </div>

                            

                            </div>

                            <div class="insideSkin">

                            </div>
                        


                            <div class="text-right rightMargin10 topbotPadding insideSkin">
                                <asp:Button  runat="server" Text="Continue"  id="btncontinue" OnClick="btncontinue_Click"   />

                            </div>
                    </div>

                </div>

            </section>

            <section>
            </section>

            <input type="hidden" name="ctl00$ContentPlaceHolder1$HF_CartToken" id="ContentPlaceHolder1_HF_CartToken">

            <!--ProductId: 31 -->
            <!--Route: Upstream -->


            <!--ZOOMSTOP-->


            <footer>

                <div class="row noSideMargin backgroundPaperDark innerDropShadow">
                </div>

            </footer>

        </div>

    </form>






    </div></body></html>