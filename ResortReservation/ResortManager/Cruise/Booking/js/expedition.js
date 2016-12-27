$(function () {
    "use strict";

    $(document).ready(function () {

        var departureId = 0;
        var cartToken = "";
        var cabinAllocation = "";
        var preExtension = "";
        var postExtension = "";
        var selectedAddOns = "";

        var puller = "<div class='puller'></div>";
        var currencySymbol = "US$";
        var singlePassengers = 0;
        var twinPassengers = 0;
        var pricePanelRow = $("#pricePanelRow").html();
        $("#pricePanelRow").html("<p class='bottomPadding30 font24'><i class='fa fa-cog fa-spin font24'></i> Loading Dates &amp; Prices...</p>");


        /*
        // Set all the carousels
        */
        $('.carousel').carousel({
            interval: 6000
        })


        /*
        // Itinerary Tabs
        // Show the selected product itinerary
        */
        $('ul.ItineraryTabs li').click(function (e) {
            var tab_id = $(this).attr('data-tab');
            $('ul.ItineraryTabs li').removeClass('current');
            $('.tab-itinerary').removeClass('current');
            $(this).addClass('current');
            $("#_" + tab_id).addClass('current');
            e.preventDefault();
        });


        /*
        // Price Panel
        // Load the departure dates and lead in prices
        */
        function LeadInPricePanel() {
            //PageMethods.LeadInPricePanel(productId, LeadInPricePanelOK, LeadInPricePanelErr);
        }

        function LeadInPricePanelOK(result) {
            var puller = "<div class='puller'></div>";
            var pricePanel = "";
            var pricePanelRows = "";
            //var pricePanelRow = $("#pricePanelRow").html();
            //$("#pricePanelRow").html("<i class='icon-spinner icon-spin icon-large'></i> Loading Dates &amp; Prices...");
            var pricePanelItems = jQuery.parseJSON(result);
            if (pricePanelItems.length == 0) {
                pricePanelRows = '<p class="bottomPadding20">All departures are sold out. We are currently working on new departures to be added soon. Please sign up for the newsletter below to ensure we can email details of these tours when they are launched. In addition you can email us to <a href="mailto:information@pandaw.com?Subject=Waiting%20List%20Request">information@pandaw.com</a> to be put on a waiting list for the current season. Thank you.</p>';
            } else {
                for (var i = 0; i < pricePanelItems.length; i++) {
                    pricePanel = pricePanelRow;
                    pricePanel = pricePanel.replace("[DEPARTUREDATE]", pricePanelItems[i]["DepartureDate"]);
                    pricePanel = pricePanel.replace("[PRODUCTNAME]", pricePanelItems[i]["Name"] + "<br/><strong>" + pricePanelItems[i]["DiscountMessage"] + "</strong>");
                    pricePanel = pricePanel.replace("[DIRECTION]", pricePanelItems[i]["Direction"]);
                    pricePanel = pricePanel.replace("[SHIP]", pricePanelItems[i]["Ship"]);
                    pricePanel = pricePanel.replace("[PRICE]", pricePanelItems[i]["FinalPrice"]);
                    pricePanel = pricePanel.replace("[AVAILBILITY]", pricePanelItems[i]["Availability"]);
                    pricePanel = pricePanel.replace("[PRODUCTID]", pricePanelItems[i]["ProductId"]);
                    pricePanelRows += pricePanel;
                };
            }
            $("#pricePanelRow").html(pricePanelRows + puller);
            setStep2();
        }

        function LeadInPricePanelErr(result) {
            alert(result);
        }

        function cabinsSelected() {
            var cabinSelected = 0;
            $(".singleCabin").each(function (i, obj) {
                cabinSelected += 1;
            });
            $(".doubleCabin").each(function (i, obj) {
                cabinSelected += 1;
            });
            return cabinSelected;
        }


        /*
        // Booking Steps
        // Takes the user through the booking steps
        */
        function setStep2() {
            $(".step2Btn").on("click", function () {
                departureId = $(this).data("departureid");
                $("#departureList").fadeOut(1000);
                $("#Step1").fadeOut(1000);
                $("#Step2").delay(1000).fadeIn(1000);
                bookingTop();
                DepartureInformation(departureId);
                Deckplan(departureId);
                CruiseInformation(departureId);
            });
        }

        $(".step3Btn").on("click", function () {

            var selectedCabins = cabinsSelected();
            if (selectedCabins == 0) {
                alert("Please select your cabin.");
            } else {
                //$("#Step2").fadeOut(1000);
                //$("#Step3").delay(1000).fadeIn(1000);
                Extensions(departureId);
                bookingTop();
            }
        });

        function setStep3() {
            $(".preSelect").on("click", function (e) {
                if ($(this).hasClass("selectActive")) {
                    $(this).html("Yes Please");
                    $(this).removeClass('selectActive');
                } else {
                    $(".preSelect").html("Yes Please");
                    $(".preSelect").removeClass("selectActive");
                    $(this).html("No Thanks");
                    $(this).addClass('selectActive');
                }
            });
            $(".postSelect").on("click", function (e) {
                if ($(this).hasClass("selectActive")) {
                    $(this).html("Yes Please");
                    $(this).removeClass('selectActive');
                } else {
                    $(".postSelect").html("Yes Please");
                    $(".postSelect").removeClass("selectActive");
                    $(this).html("No Thanks");
                    $(this).addClass('selectActive');
                }
            });
        }

        $(".step4Btn").on("click", function () {
            //$("#Step3").fadeOut(1000);
            //$("#Step4").delay(1000).fadeIn(1000);
            AddOns(productId, departureId);
            bookingTop();
        });

        $(".step5Btn").on("click", function () {
            $("#Step4").fadeOut(1000);
            $("#Step5").delay(1000).fadeIn(1000);
            bookingTop();
            createCart();
        });

        $(".step6Btn").on("click", function () {
            $("#Step5").fadeOut(1000);
            $("#Step6").delay(1000).fadeIn(1000);
            bookingTop();
        });

        $(".step7Btn").on("click", function () {
            $("#Step5").fadeOut(1000);
            $("#Step7").delay(1000).fadeIn(1000);
            bookingTop();
        });

        $(".pricePanelBtn").on("click", function () {
            $("#Step2").fadeOut(1000);
            $("#Step1").delay(1000).fadeIn(1000);
            $("#departureList").delay(1000).fadeIn(1000);
            bookingTop();
        });


        
        


        //$(".step8Btn").on("click", function () {
        //    $("#Step7").fadeOut(1000);
        //    $("#Step8").delay(1000).fadeIn(1000);
        //    bookingTop();
        //    $("#form1").submit();
        //});




        function bookingTop() {
            $('html,body').animate({
                scrollTop: $("#bookingTop").offset().top
            }, 1000);
        }

        function showPricePanel() {
            bookingTop();
            $('#areaBooking').collapse('show');
        }


        /*
        // Deckplan
        // Loads the relevent deckplan for the selected date
        */
        function Deckplan(DepartureId) {
            $(".cabins").remove();
            $("#CabinVessel").addClass("hidden");
            $("#CabinSelect").addClass("hidden");
            $("#CabinSelecting").removeClass("hidden");
            var deckplanRaw = '';
            PageMethods.Deckplan(DepartureId, DeckplanOK, DeckplanErr, DepartureId);
        }

        function DeckplanOK(result, DepartureId) {
            var item = jQuery.parseJSON(result);
            var deckplan = "/images/" + item[0]['Deckplan'];
            var productName = item[0]['Product'] + ' (' + item[0]['Itinerary'] + ')<br/>onboard the ' + item[0]['Vessel'] + ' departing ' + item[0]['DepartureDate'];
            $("#Deckplan").attr("src", deckplan);
            $("#CabinVessel h3").html(productName);
            var cabinAvailable = "<div title='[INFO]' data-cabintype='[CABINATTRIBUTE]' data-productid='[PRODUCTID]' data-facilityid='[FACILITYID]' data-cabinno='[CABINNO]' style='position:absolute;left:[PLANX]px;top:[PLANY]px;width:[WIDTH]px;height:[HEIGHT]px;text-align:left;font-weight:bold;color:#000000;' class='cabins availableCabin'>[CABIN]</div>";
            var cabinSingleAvailable = "<div title='[INFO]' data-cabintype='[CABINATTRIBUTE]' data-productid='[PRODUCTID]' data-facilityid='[FACILITYID]' data-occupancy='[OCCUPANCY]' style='position:absolute;left:[PLANX]px;top:[PLANY]px;width:[WIDTH]px;height:[HEIGHT]px;text-align:left;font-weight:bold;color:#000000;' class='cabins singleOnlyCabin availableSingleCabin'>[CABIN]</div>";
            var cabinBooked = "<div style='position:absolute;left:[PLANX]px;top:[PLANY]px;width:[WIDTH]px;height:[HEIGHT]px;text-align:left;font-weight:bold;color:#C4C4C4;' class='cabins bookedCabin'>[CABIN]</div>";
            var setCabinSelect = "";
            var cabinAttributes = "";
            var cabinIds = [];
            var dummyCabins = [];
            var takenCabins = 0;
            var cabinCount = 0;
            for (var i = 0; i < item.length; i++) {
                if (item[i]['IsAvailable'] == "1") {
                    if (item[i]['Occupancy'].replace(" ", "") == "Single") {
                        setCabinSelect = cabinSingleAvailable;
                        cabinAttributes = item[i]['Location'] + " ";
                        cabinAttributes = cabinAttributes + item[i]['CabinType'] + " ";
                    } else {
                        setCabinSelect = cabinAvailable;
                        cabinAttributes = item[i]['Location'] + " ";
                        cabinAttributes = cabinAttributes + item[i]['CabinType'] + " ";
                    }
                } else {
                    setCabinSelect = cabinBooked;
                    cabinAttributes = "";
                    takenCabins += 1;
                }
                setCabinSelect = setCabinSelect.replace("[PLANX]", item[i]['PlanX']);
                setCabinSelect = setCabinSelect.replace("[PLANY]", item[i]['PlanY']);
                setCabinSelect = setCabinSelect.replace("[WIDTH]", item[i]['PlanWidth']);
                setCabinSelect = setCabinSelect.replace("[HEIGHT]", item[i]['PlanHeight']);
                setCabinSelect = setCabinSelect.replace("[CABIN]", item[i]['Cabin']);
                setCabinSelect = setCabinSelect.replace("[CABINNO]", item[i]['Cabin']);
                setCabinSelect = setCabinSelect.replace("[CABINATTRIBUTE]", cabinAttributes);
                setCabinSelect = setCabinSelect.replace("[PRODUCTID]", DepartureId);
                setCabinSelect = setCabinSelect.replace("[FACILITYID]", item[i]['FacilitiesId']);
                setCabinSelect = setCabinSelect.replace("[OCCUPANCY]", item[i]['Occupancy']);
                setCabinSelect = setCabinSelect.replace("[INFO]", item[i]['Info']);
                $(setCabinSelect).appendTo("#CabinSelect");
                var cabinId = [item[i]['Cabin']];
                cabinIds.push(cabinId);
                //if (item[i]['Location'] != 'Suite') {
                //    dummyCabins.push(cabinId);
                //}
                var cabinType = item[i]['Location'].toLowerCase();
                if (cabinType.indexOf("suite") == -1) {
                    dummyCabins.push(cabinId);
                }
                cabinCount += 1;
            };
            $("#CabinVessel").removeClass("hidden");
            $("#CabinSelecting").addClass("hidden");
            $("#CabinSelect").removeClass("hidden");
            $(".availableCabin").click(function () {
                selectCabin(this);
                getCabinSelection();
            });
            $(".availableSingleCabin").click(function () {
                selectSingleCabin(this);
                getCabinSelection();
            });
            var emptyDeparture = true;
            $(".bookedCabin").each(function (i, obj) {
                emptyDeparture = false;
            });
            if (loggedIn == "False") {
                if (emptyDeparture == true && cabinCount > 5 || takenCabins < 3 && cabinCount > 5) {
                    for (i = takenCabins; i < 3; i++) {
                        // Get a random cabin number
                        var randomCabin = dummyCabins[Math.floor(Math.random() * cabinIds.length)];
                        // Get reference to that cabin on the deckplab
                        var dummyBooking = $("*[data-cabinno='"+randomCabin+"']");
                        // Apply a dummy booking
                        $(dummyBooking).removeClass("availableCabin").addClass("bookedCabin");
                        $(dummyBooking).unbind("click");
                        // Remove the dummy booking cabin number form array
                        dummyCabins = $.grep(dummyCabins, function (value) {
                            return value != randomCabin;
                        });
                    }
                }
            }        
            $(".availableCabin[title]").tooltip();
            $(".availableSingleCabin[title]").tooltip();
        }

        function DeckplanErr(result, BookingId, ProductId) {
            $("#LoadingDeckplan").html(result);
        }

        //$('#Cruises_PriceTable').dataTable({
        //    "bPaginate": false,
        //    "bLengthChange": false,
        //    "bFilter": false,
        //    "bSort": false,
        //    "bProcessing": false,
        //    "bInfo": false,
        //    "scrollX": false,
        //    "iDisplayLength": 100,
        //    "language": {
        //        "decimal": ".",
        //        "thousands": ","
        //    }
        //});

        function CruiseInformation(DepartureId) {
            PageMethods.PricePanel(DepartureId, CruiseInformationOK, CruiseInformationErr);
        }

        function CruiseInformationOK(result) {
            if ($("#Cruises_PriceTable").length <= 0)
                return;

            var dataTable = $("#Cruises_PriceTable").dataTable();
            var CruiseInformationArray = [];
            var item = jQuery.parseJSON(result)
            for (var i = 0; i < item.length; i++) {
                var arrayRow = [item[i]['CabinName'],
                    item[i]['Currency'] + item[i]['PricePerPerson']];
                CruiseInformationArray.push(arrayRow);
            };
            dataTable.fnClearTable();
            dataTable.fnAddData(CruiseInformationArray, true);
            SelectedInformation(result);
        }

        function CruiseInformationErr(result) {
            alert(result);
        }

        $('#Selected_PriceTable').dataTable({
            "bPaginate": false,
            "bLengthChange": false,
            "bFilter": false,
            "bSort": false,
            "bProcessing": false,
            "bInfo": false,
            "scrollX": false,
            "iDisplayLength": 100,
            "language": {
                "decimal": ".",
                "thousands": ","
            }
        });


        /*
        // Cabin Selection
        // Functions to support cabin selection
        */
        function SelectedInformation(result) {
            var dataTable = $("#Selected_PriceTable").dataTable();
            var SelectedInformationArray = [];
            var item = jQuery.parseJSON(result)
            for (var i = 0; i < item.length; i++) {
                var arrayRow = [item[i]['CabinName'],
                    '0',
                    '0',
                    item[i]['Occupancy'],
                    item[i]['ProductsAttributesId']];
                SelectedInformationArray.push(arrayRow);
            };
            dataTable.fnClearTable();
            dataTable.fnAddData(SelectedInformationArray, true);
        }

        function selectCabin(cabin) {
            if ($(cabin).hasClass("singleCabin")) {
                $(cabin).removeClass("singleCabin");
                $(cabin).removeClass("doubleCabin");
                $(cabin).removeClass("selectedCabin");
            } else if ($(cabin).hasClass("doubleCabin")) {
                $(cabin).removeClass("doubleCabin");
                $(cabin).addClass("singleCabin");
                $(cabin).addClass("selectedCabin");
            } else {
                $(cabin).addClass("doubleCabin");
                $(cabin).removeClass("singleCabin");
                $(cabin).addClass("selectedCabin");
            }
            var cabinNumber = $(cabin).html();
        }

        function selectSingleCabin(cabin) {
            if ($(cabin).hasClass("singleCabin")) {
                $(cabin).removeClass("singleCabin");
            } else {
                $(cabin).addClass("singleCabin");
                $(cabin).addClass("current");
            }
            var cabinNumber = $(cabin).html();
        }

        function cabinType(cabin) {
            var cabinOccupancy = "";
            if ($(cabin).hasClass("singleCabin")) {
                cabinOccupancy = "Single Use";
            } else if ($(cabin).hasClass("doubleCabin")) {
                cabinOccupancy = "Sharing";
            } else {
                cabinOccupancy = "";
            }
            var selectedCabinType = $(cabin).data("cabintype") + cabinOccupancy;
            return selectedCabinType.replace("  ", " ");
        }

        function allocationPricing(cabins) {
            $("#Selected_PriceTable tbody tr").css("font-weight", "normal");
            $("#Selected_PriceTable tbody tr td:nth-child(3)").html(currencySymbol + "0.00");
            $("#Selected_PriceTable tbody tr td:nth-child(2)").html("0");
            $.each(cabins, function (index, value) {
                // Highlight in bold the selected cabin row
                var priceRow = $("#Selected_PriceTable td").filter(function () {
                    return $(this).text() == value;
                }).parent("tr").css("font-weight", "bold");
                // Get the cabin price
                var priceColumn = $("#Cruises_PriceTable td").filter(function () {
                    return $(this).text() == value
                }).closest("tr").children("td:nth-child(2)").text();
                // Get any existing pricing for the selected cabin type
                var existingPricing = $("#Selected_PriceTable td").filter(function () {
                    return $(this).text() == value;
                }).closest("tr").children("td:nth-child(3)").text();
                // Get occupancy for cabin
                var occupancy = $("#Selected_PriceTable td").filter(function () {
                    return $(this).text() == value;
                }).closest("tr").children("td:nth-child(4)").text();
                // Get existing selected occupancy for cabin
                var exisingOccupancy = $("#Selected_PriceTable td").filter(function () {
                    return $(this).text() == value;
                }).closest("tr").children("td:nth-child(2)").text();
                // Clan input for calculations
                //priceColumn = priceColumn.replace(/US\$/g, '');
                var currencySymbol = priceColumn.match(/US\$|AU\$|GB\£/g);
                priceColumn = priceColumn.replace(/US\$|AU\$|GB\£/g, '');
                priceColumn = priceColumn.replace(/,/g, '');
                //existingPricing = existingPricing.replace(/US\$/g, '');
                existingPricing = existingPricing.replace(/US\$|AU\$|GB\£/g, '');
                existingPricing = existingPricing.replace(/,/g, '');
                // Calculate price
                var quotedPrice = parseFloat(priceColumn * occupancy) + parseFloat(existingPricing);
                // Show final price for selected cabin type
                var quoteColumn = $("#Selected_PriceTable td").filter(function () {
                    return $(this).text() == value;
                }).closest("tr").children("td:nth-child(3)").html(datatableNumberFormat(quotedPrice, 2, ',', '.', currencySymbol));
                // Calculate total occupancy for cabin selection
                var quotedOccupancy = parseFloat(exisingOccupancy) + parseFloat(occupancy);
                // Show final occupancy for selected cabin
                var occupancyColumn = $("#Selected_PriceTable td").filter(function () {
                    return $(this).text() == value;
                }).closest("tr").children("td:nth-child(2)").html(quotedOccupancy);
            })
            getTravellingPassengers();
        }

        function getCabinPrice(cabin) {
            var priceColumn = $("#Cruises_PriceTable td").filter(function () {
                return $(this).text() == cabin;
            }).closest("tr").children("td:nth-child(2)").html();
            return priceColumn;
        }

        function getCabinAttributeId(cabin) {
            var priceColumn = $("#Selected_PriceTable td").filter(function () {
                return $(this).text() == cabin;
            }).closest("tr").children("td:nth-child(5)").html();
            return priceColumn;
        }

        function getCabinSelection() {
            var cabins = [];
            var cabinPrice = "0";
            $(".singleCabin").each(function (i, obj) {
                var cabinRow = [cabinType(obj)];
                cabins.push(cabinRow);
            });
            $(".doubleCabin").each(function (i, obj) {
                var cabinRow = [cabinType(obj)];
                cabins.push(cabinRow);
            });
            allocationPricing(cabins);
        }

        function getTravellingPassengers() {
            singlePassengers = 0;
            twinPassengers = 0;
            $('#Selected_PriceTable tbody tr').each(function (i, row) {
                var occupancy = 0;
                var travellers = 0;
                var totalTravellers = 0;
                occupancy = $(this).children("td:nth-child(4)").text();
                travellers = $(this).children("td:nth-child(2)").text();
                totalTravellers = travellers * 1;
                if (occupancy == 1) {
                    singlePassengers += totalTravellers;
                }
                if (occupancy == 2) {
                    twinPassengers += totalTravellers;
                }
            });
        }

        function datatableNumberFormat(number, decPlaces, thouSeparator, decSeparator, sign) {
            var i = parseInt(number = Math.abs(+number || 0).toFixed(decPlaces)) + "";
            var j = (j = i.length) > 3 ? j % 3 : 0;
            var column = ""; //"<span class='hidden'>" + number + "</span>";
            return column + sign + (j ? i.substr(0, j) + thouSeparator : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thouSeparator) + (decPlaces ? decSeparator + Math.abs(number - i).toFixed(decPlaces).slice(2) : "");
        }


        /*
        // Extensions List
        // Load the relevent optional extensions for this product
        */
        function Extensions(DepartureId) {
            var attributeIds = "";
            var cabinName = "";
            $(".singleCabin").each(function (i, obj) {
                cabinName = cabinType(obj);
                attributeIds += getCabinAttributeId(cabinName) + ",";
            });
            $(".doubleCabin").each(function (i, obj) {
                cabinName = cabinType(obj);
                attributeIds += getCabinAttributeId(cabinName) + ",";
            });
            attributeIds += "0";
            PageMethods.Extensions(DepartureId, attributeIds, ExtensionsOK, ExtensionsErr);
        }

        function ExtensionsOK(result) {
            var extensions = "";
            var extensionPricePanel = "";
            var preExtensionRows = "";
            var postExtensionRows = "";
            var extensionRow = $("#preTourRow").html();
            var extensionItems = jQuery.parseJSON(result);
            if (extensionItems.length == 0) {
                $("#preTourRow").html("");
                $("#postTourRow").html("");
                if ($("#Step2").is(":visible")) {
                    $("#Step2").fadeOut(1000);
                }
                //$("#Step2").fadeOut(1000);
                //$("#Step4").delay(1000).fadeIn(1000);
                AddOns(productId, departureId);
                bookingTop();
            } else {
                for (var i = 0; i < extensionItems.length; i++) {
                    extensions = extensionRow;
                    extensions = extensions.replace("[EXTENSIONIMAGE]", extensionItems[i]["HeaderImage"]);
                    extensions = extensions.replace("[EXTENSIONALT]", extensionItems[i]["Name"]);
                    extensions = extensions.replace("[EXTENSIONNAME]", extensionItems[i]["Name"]);
                    extensions = extensions.replace("[DATATYPE]", extensionItems[i]["Type"]);
                    extensions = extensions.replace("[EXTENSIONTYPE]", extensionItems[i]["ExtensionType"]);
                    extensions = extensions.replace(/\[PRODUCTID\]/g, extensionItems[i]["ProductId"]);
                    var pricePanel = extensionItems[i]["PricePanel"];
                    var item = jQuery.parseJSON(pricePanel)
                    extensionPricePanel = "";
                    for (var ii = 0; ii < item.length; ii++) {
                        if (item[ii]['Occupancy'] == "1" && singlePassengers > 0) {

                        }
                        extensionPricePanel += "<tr>" +
                            "<td>" + item[ii]['Option'] + "</td>" +
                            "<td>" + datatableNumberFormat(item[ii]['PricePerPerson'], 2, ',', '.', currencySymbol) + "</td>";
                        if (item[ii]['Occupancy'] == "1") {
                            extensionPricePanel += "<td>" + singlePassengers + "</td>" +
                                "<td>" + datatableNumberFormat((item[ii]['PricePerPerson'] * singlePassengers), 2, ',', '.', currencySymbol) + "</td>";
                        }
                        if (item[ii]['Occupancy'] == "2") {
                            extensionPricePanel += "<td>" + twinPassengers + "</td>" +
                                "<td>" + datatableNumberFormat((item[ii]['PricePerPerson'] * twinPassengers), 2, ',', '.', currencySymbol) + "</td>";
                        }
                        extensionPricePanel += "<td>" + item[ii]['ProductAttributeId'] + "</td>";
                        extensionPricePanel += "</tr>";
                    };
                    extensions = extensions.replace("<tbody></tbody>", extensionPricePanel);

                    if (extensionItems[i]["Type"] == "Pre") {
                        extensions = extensions.replace("[CLASS]", "preSelect");
                        preExtensionRows += extensions;
                        $("#preTourHeading").removeClass("hidden");
                    }
                    if (extensionItems[i]["Type"] == "Post") {
                        extensions = extensions.replace("[CLASS]", "postSelect");
                        postExtensionRows += extensions;
                        $("#postTourHeading").removeClass("hidden");
                    }
                };
                $("#preTourRow").html(preExtensionRows + puller);
                $("#postTourRow").html(postExtensionRows + puller);
                $("#Step2").fadeOut(1000);
                $("#Step3").delay(1000).fadeIn(1000);
                setStep3();
            }
        }

        function ExtensionsErr(result) {
            alert(result);
        }


        /*
        // AddOns List
        // Load the relevent optional addons for this product
        */
        function AddOns(ProductId, DepartureId) {
            PageMethods.AddOns(ProductId, DepartureId, AddOnsOK, AddOnsErr);
        }

        function AddOnsOK(result) {
            var puller = "<div class='puller'></div>";
            var addOns = "";
            var addOnsPricePanel = "";
            var addOnsRows = "";
            var addOnRow = $("#addOnsRow").html();
            var addOnsItems = jQuery.parseJSON(result);
            if (addOnsItems.length == 0) {
                $("#addOnsRow").html("");
                if ($("#Step2").is(":visible")) {
                    $("#Step2").fadeOut(1000);
                }
                if ($("#Step3").is(":visible")) {
                    $("#Step3").fadeOut(1000);
                }
                //$("#Step3").fadeOut(1000);
                $("#Step5").delay(1000).fadeIn(1000);
                bookingTop();
                createCart();
            } else {
                for (var i = 0; i < addOnsItems.length; i++) {
                    addOns = addOnRow;
                    addOns = addOns.replace("[EXTENSIONIMAGE]", addOnsItems[i]["HeaderImage"]);
                    addOns = addOns.replace("[EXTENSIONALT]", addOnsItems[i]["Name"]);
                    addOns = addOns.replace("[EXTENSIONNAME]", addOnsItems[i]["Name"]);
                    addOns = addOns.replace("[PRODUCTID]", addOnsItems[i]["ProductId"]);
                    var pricePanel = addOnsItems[i]["PricePanel"];
                    var item = jQuery.parseJSON(pricePanel)
                    addOnsPricePanel = "";
                    for (var ii = 0; ii < item.length; ii++) {
                        addOnsPricePanel += "<tr>" +
                            "<td>" + item[ii]['Option'] + "</td>" +
                            "<td>" + datatableNumberFormat(item[ii]['PricePerPerson'], 2, ',', '.', currencySymbol) + "</td>";
                        if (addOnsItems[i]['ActionType'] == "Quantity") {
                            addOnsPricePanel += "<td><select id='Addon_" + item[ii]['ProductId'] + "' class='form-control addonSelectMenus' data-productid='" + item[ii]['ProductId'] + "' data-attrid='" + item[ii]['ProductAttributeId'] + "'>";
                            for (var iii = 0; iii < (singlePassengers + twinPassengers + 1) ; iii++) {
                                addOnsPricePanel += "<option value='" + iii + "'>" + iii + " passengers</option>"
                            }
                            addOnsPricePanel += "</select></td>";
                        }
                        if (addOnsItems[i]['ActionType'] == "Boolean") {
                            addOnsPricePanel += "<td><a class='btn btn-info font16 topMargin2 [CLASS]' data-productid='" + item[ii]['ProductId'] + "' data-attrid='" + item[ii]['ProductAttributeId'] + "' data-type='[DATATYPE]'>Yes Please</a></td>";
                        }
                        addOnsPricePanel += "<td>" + datatableNumberFormat((0), 2, ',', '.', currencySymbol) + "</td>";
                        addOnsPricePanel += "</tr>";
                    };
                    addOns = addOns.replace("<tbody></tbody>", addOnsPricePanel);
                    if (addOnsItems[i]["ActionType"] == "Quantity" || addOnsItems[i]["ActionType"] == "Boolean") {
                        addOns = addOns.replace("[CLASS]", "addOnSelect");
                        addOns = addOns.replace("[PRODUCTID]", addOnsItems[i]["ProductId"]);
                        addOns = addOns.replace("[DATATYPE]", "AddOn");
                        addOns = addOns.replace("[ATTRID]", addOnsItems[i]["ProductId"]);
                        addOnsRows += addOns;
                        $("#addonsTourHeading").removeClass("hidden");
                    }
                };
                $("#addOnsRow").html(addOnsRows + puller);
                $(".addonSelectMenus").change(function () {
                    addonPrice(this);
                });
                $(".addOnSelect").on("click", function (e) {
                    if ($(this).hasClass("selectActive")) {
                        $(this).html("Yes Please");
                        $(this).removeClass('selectActive');
                    } else {
                        $(this).html("No Thanks");
                        $(this).addClass('selectActive');
                    }
                });
                $("#Step2").fadeOut(1000);
                $("#Step3").fadeOut(1000);
                $("#Step4").delay(1000).fadeIn(1000);
            }
        }

        function AddOnsErr(result) {
            alert(result);
        }

        function addonPrice(addonSelection) {
            var currentIndex = $(addonSelection).parent().index();
            var priceIndex = (currentIndex - 1);
            var addonPrice = $(addonSelection).closest("tr").children("td:nth-child(2)").text();
            var addons = $(addonSelection).val();
            addonPrice = addonPrice.replace(/US\$|AU\$|GB\£/g, '');
            addonPrice = addonPrice.replace(/,/g, '');
            var quotedPrice = parseFloat(addonPrice * addons);
            $(addonSelection).closest("tr").children("td:nth-child(4)").html(datatableNumberFormat(quotedPrice, 2, ',', '.', currencySymbol));
        }


        /*
        // Add To Cart
        // Collate all selected products and add to the cart
        */
        function createCart() {
            var allocation = "";
            var cabinName = "";
            var attributeId = "";
            var facilityId = "";
            var preExtensions = "";
            var postExtensions = "";
            var riverStays = "";
            var extensionType = "";

            $(".singleCabin").each(function (i, obj) {
                cabinName = cabinType(obj);
                attributeId = getCabinAttributeId(cabinName);
                facilityId = $(obj).data("facilityid");
                cabinAllocation += attributeId + ":" + facilityId + ',';
            });
            $(".doubleCabin").each(function (i, obj) {
                cabinName = cabinType(obj);
                attributeId = getCabinAttributeId(cabinName);
                facilityId = $(obj).data("facilityid");
                cabinAllocation += attributeId + ":" + facilityId + ',';
            });
            $(".preSelect").each(function (i, obj) {
                if ($(this).hasClass("selectActive")) {
                    preExtension = $(this).data("productid");
                    extensionType = $(this).data("extensiontype");
                    var tableId = "#" + preExtension + " tbody tr";
                    $(tableId).each(function (i, row) {
                        if ($(this).children("td:nth-child(3)").text() != "0") {
                            if (extensionType == "RiverStay") {
                                riverStays += preExtension + ":" + $(this).children("td:nth-child(5)").text() + ",";
                            } else {
                                preExtensions += preExtension + ":" + $(this).children("td:nth-child(5)").text() + ",";
                            }
                        }
                        
                    });
                }
            });
            $(".postSelect").each(function (i, obj) {
                if ($(this).hasClass("selectActive")) {
                    postExtension = $(this).data("productid");
                    extensionType = $(this).data("extensiontype");
                    var tableId = "#" + postExtension + " tbody tr";
                    $(tableId).each(function (i, row) {
                        if ($(this).children("td:nth-child(3)").text() != "0") {
                            if (extensionType == "RiverStay") {
                                riverStays += postExtension + ":" + $(this).children("td:nth-child(5)").text() + ",";
                            } else {
                                postExtensions += postExtension + ":" + $(this).children("td:nth-child(5)").text() + ",";
                            }
                        }
                    });
                }
            });
            $("select").each(function () {
                selectedAddOns += $(this).data("productid") + ':' + $(this).data("attrid") + ':' + this.value + ',';
            });
            $(".addOnSelect").each(function () {
                if ($(this).hasClass('selectActive')) {
                    selectedAddOns += $(this).data("productid") + ':' + $(this).data("attrid") + ':' + '1' + ',';
                } else {
                    selectedAddOns += $(this).data("productid") + ':' + $(this).data("attrid") + ':' + '0' + ',';
                }
            });
            //alert("cabinAllocation: " + cabinAllocation + " riverStays: " + riverStays + " preExtension: " + preExtension + " postExtension: " + postExtension + " selectedAddOns: " + selectedAddOns + " Last extensionType: " + extensionType);
            AddToCart(departureId, cabinAllocation, riverStays, preExtensions, postExtensions, selectedAddOns, "", 1);
        }

        function AddToCart(DepartureId, CabinAllocation, RiverStays, PreExtensions, PostExtensions, SelectedAddOns, GUID, CurrencyId) {
            PageMethods.AddToCart(DepartureId, CabinAllocation, RiverStays, PreExtensions, PostExtensions, SelectedAddOns, GUID, CurrencyId, AddToCartOK, AddToCartErr);
        }

        function AddToCartOK(result) {
            cartToken = JSON.stringify(result);
            cartToken = cartToken.replace(/"/g, "");
            $("#ContentPlaceHolder1_HF_CartToken").val(cartToken);
            BookingSummary(cartToken);
            
        }

        function AddToCartErr(result) {
            alert(JSON.stringify(result));
        }


        /*
        // Assign Customer
        // Accosiates the Cart items to the customer once logged in
        */
        function assignCustomer(GUID) {
            PageMethods.AssignCustomer(GUID, assignCustomerOK, assignCustomerErr);
        }

        function assignCustomerOK(result) {
        }

        function assignCustomerErr(result) {
            alert(JSON.stringify(result));
        }



        /*
        // Cart Summary
        // Returns financials for cart value
        */
        function cartValue(GUID) {
            PageMethods.CalculateSummary(GUID, "", cartValueOK, cartValueErr);
        }

        function cartValueOK(result) {
            var cartValues = jQuery.parseJSON(result);
            for (var i = 0; i < cartValues.length; i++) {
                $("#PublicRateValue").html("US$" + cartValues[i]["BookingValue"]);
                $("#CommissionValue").html("US$" + cartValues[i]["Commission"]);
                $("#Tax1Value").html("US$" + cartValues[i]["Tax"]);
                $("#Tax2Value").html("US$" + cartValues[i]["Tax"]);
                $("#Total1Value").html("US$" + cartValues[i]["Total"]);
                $("#Total2Value").html("US$" + cartValues[i]["PublicTotal"]);
                $("#DepositDue1Value").html("US$" + cartValues[i]["Deposit"]);
                $("#DepositDue2Value").html("US$" + cartValues[i]["PublicDeposit"]);
                $("#DepositPercent").html("(" + cartValues[i]["DepositPercentage"] + ")");


                //$("#PublicRateValue").html(datatableNumberFormat(cartValues[i]["BookingValue"], 2, ",", ".", "US$"));
                //$("#CommissionValue").html(datatableNumberFormat(cartValues[i]["Commission"], 2, ",", ".", "US$"));
                //$("#Tax1Value").html(datatableNumberFormat(cartValues[i]["Tax"], 2, ",", ".", "US$"));
                //$("#Tax2Value").html(datatableNumberFormat(cartValues[i]["Tax"], 2, ",", ".", "US$"));
                //$("#Total1Value").html(datatableNumberFormat(cartValues[i]["Total"], 2, ",", ".", "US$"));
                //$("#Total2Value").html(datatableNumberFormat(cartValues[i]["PublicTotal"], 2, ",", ".", "US$"));
                //$("#DepositDue1Value").html(datatableNumberFormat(cartValues[i]["Deposit"], 2, ",", ".", "US$"));
                //$("#DepositDue2Value").html(datatableNumberFormat(cartValues[i]["PublicDeposit"], 2, ",", ".", "US$"));
                //$("#DepositPercent").html("(" + cartValues[i]["DepositPercentage"] + ")");
            }
            //cartToken = cartToken.replace(/"/g, "");    
        }

        function cartValueErr(result) {
            alert(JSON.stringify(result));
        }


        /*
        // Booking Summary
        // Returns the full shopping cart
        */
        function BookingSummary(GUID) {
            assignCustomer(GUID);
            PageMethods.BookingSummary(GUID, BookingSummaryOK, BookingSummaryErr, GUID);
        }

        function BookingSummaryOK(result, GUID) {
            var summaryItems = jQuery.parseJSON(result);
            var travellingPassengers = 0;
            var singleAccommodation = 0;
            var twinAccommodation = 0;
            var expeditionsRows = "";
            var stateroomsRows = "";
            var extensionsRows = "";
            var addonsRows = "";
            var productName = "";
            var extensionCounter = 0;
            var addonCounter = 0;
            var expeditionsRow = $(".expeditionsSummary").html();
            var stateroomsRow = $(".stateroomsSummary").html();
            var extensionsRow = $(".extensionsSummary").html();
            var addonsRow = $(".addonsSummary").html();
            singlePassengers = 0;
            twinPassengers = 0;
            for (var i = 0; i < summaryItems.length; i++) {
                if (summaryItems[i]["ProductType"] == "Cruise") {
                    if (summaryItems[i]["Occupancy"] == 1) {
                        singleAccommodation += 1;
                        singlePassengers += 1;
                    }
                    if (summaryItems[i]["Occupancy"] == 2) {
                        twinAccommodation += 1;
                        twinPassengers += 2;
                    }
                }
            }
            travellingPassengers = (singlePassengers + twinPassengers);
            for (var i = 0; i < summaryItems.length; i++) {
                if (summaryItems[i]["ProductType"] == "Cruise") {
                    if (productName != summaryItems[i]["Name"]) {
                        expeditionsRows += "<tr>";
                        expeditionsRows += "<td class='col-md-6 text-left normal'>" + summaryItems[i]["Name"] + "</td>";
                        expeditionsRows += "<td class='col-md-3 text-center normal'>" + summaryItems[i]["Duration"] + "</td>";
                        expeditionsRows += "<td class='col-md-3 text-center normal'>" + summaryItems[i]["DepartureDate"] + "</td>";
                        expeditionsRows += "</tr>";
                    }
                    productName = summaryItems[i]["Name"];
                }
                if (summaryItems[i]["ProductType"] == "Cruise") {
                    stateroomsRows += "<tr>";
                    stateroomsRows += "<td class='col-md-6 text-left normal'>" + summaryItems[i]["Description"] + "</td>";
                    stateroomsRows += "<td class='col-md-3 text-center normal'>" + summaryItems[i]["Occupancy"] + "</td>";
                    stateroomsRows += "<td class='col-md-3 text-center normal'>" + datatableNumberFormat(summaryItems[i]["Price"], 2, ",", ".", "US$") + "</td>";
                    stateroomsRows += "</tr>";
                }
                if (summaryItems[i]["ProductType"] == "PreTour" || summaryItems[i]["ProductType"] == "PostTour" || summaryItems[i]["ProductType"] == "RiverStay") {
                    extensionCounter += 1;
                    extensionsRows += "<tr>";
                    extensionsRows += "<td class='col-md-6 text-left normal'>" + summaryItems[i]["Name"] + ' ' + summaryItems[i]["Description"] + "</td>";
                    extensionsRows += "<td class='col-md-3 text-center normal'>";
                    if (summaryItems[i]["Occupancy"] == 1) {
                        extensionsRows += singlePassengers;
                    }
                    if (summaryItems[i]["Occupancy"] == 2) {
                        extensionsRows += twinPassengers;
                    }
                    extensionsRows += "</td>";
                    extensionsRows += "<td class='col-md-3 text-center normal'>";
                    if (summaryItems[i]["Occupancy"] == 1) {
                        extensionsRows += datatableNumberFormat((summaryItems[i]["Price"] * singleAccommodation), 2, ",", ".", "US$");
                    }
                    if (summaryItems[i]["Occupancy"] == 2) {
                        extensionsRows += datatableNumberFormat((summaryItems[i]["Price"] * twinAccommodation), 2, ",", ".", "US$");
                    }
                    extensionsRows += "</td>";
                    extensionsRows += "</tr>";
                }
                if (summaryItems[i]["ProductType"] == "AddOn") {
                    addonCounter += 1;
                    addonsRows += "<tr>";
                    addonsRows += "<td class='col-md-6 text-left normal'>" + summaryItems[i]["Name"] + "</td>";
                    if (summaryItems[i]["ActionType"] == "Boolean") {
                        addonsRows += "<td class='col-md-3 text-center normal'>" + (singlePassengers + twinPassengers) + "</td>";
                    } else {
                        addonsRows += "<td class='col-md-3 text-center normal'>" + summaryItems[i]["Quantity"] + "</td>";
                    }
                    addonsRows += "<td class='col-md-3 text-center normal'>" + datatableNumberFormat(summaryItems[i]["Price"], 2, ",", ".", "US$") + "</td>";
                    addonsRows += "</tr>";
                }
            }
            expeditionsRows = expeditionsRow.replace("<tbody></tbody>", expeditionsRows);
            stateroomsRows = stateroomsRow.replace("<tbody></tbody>", stateroomsRows);
            extensionsRows = extensionsRow.replace("<tbody></tbody>", extensionsRows);
            addonsRows = addonsRow.replace("<tbody></tbody>", addonsRows);
            $(".expeditionsSummary").html(expeditionsRows + puller);
            $(".stateroomsSummary").html(stateroomsRows + puller);
            $(".extensionsSummary").html(extensionsRows + puller);
            $(".addonsSummary").html(addonsRows + puller);
            if (extensionCounter == 0) {
                $("#extensionsArea").addClass("hide");
            }
            if (addonCounter == 0) {
                $("#addonsArea").addClass("hide");
            }
            Surcharges(GUID, travellingPassengers);
            cartValue(GUID);
        }

        function BookingSummaryErr(result, GUID) {
            alert(JSON.stringify(result));
        }


        /*
        // Surcharges
        // Returns any surcharges for this booking
        */
        function Surcharges(GUID, travellingPassengers) {
            PageMethods.Surcharges(GUID, SurchargesOK, SurchargesErr, travellingPassengers);
        }

        function SurchargesOK(result, travellingPassengers) {
            var surchargeCounter = 0;
            var summaryItems = jQuery.parseJSON(result);
            var surchragesRows = "";
            var surchargesRow = $(".surchargesSummary").html();
            for (var i = 0; i < summaryItems.length; i++) {
                if (summaryItems[i]["PortTaxes"] && summaryItems[i]["PortTaxes"] > 0) {
                    surchargeCounter += 1;
                    surchragesRows += "<tr>";
                    surchragesRows += "<td class='col-md-6 text-left normal'>Port Taxes</td>";
                    surchragesRows += "<td class='col-md-3 text-center normal'>" + travellingPassengers + "</td>";
                    surchragesRows += "<td class='col-md-3 text-center normal'>" + datatableNumberFormat((summaryItems[i]["PortTaxes"] * travellingPassengers), 2, ",", ".", "US$") + "</td>";
                    surchragesRows += "</tr>";
                    
                }
                if (summaryItems[i]["FuelTaxes"] && summaryItems[i]["FuelTaxes"] > 0) {
                    surchargeCounter += 1;
                    surchragesRows += "<tr>";
                    surchragesRows += "<td class='col-md-6 text-left normal'>Fuel Surcharge</td>";
                    surchragesRows += "<td class='col-md-3 text-center normal'>" + travellingPassengers + "</td>";
                    surchragesRows += "<td class='col-md-3 text-center normal'>" + datatableNumberFormat((summaryItems[i]["FuelTaxes"] * travellingPassengers), 2, ",", ".", "US$") + "</td>";
                    surchragesRows += "</tr>";
                }
            }
            surchragesRows = surchargesRow.replace("<tbody></tbody>", surchragesRows);
            $(".surchargesSummary").html(surchragesRows + puller);
            if (surchargeCounter == 0) {
                $("#surchargeArea").addClass("hide");
            }
        }

        function SurchargesErr(result, travellingPassengers) {
            alert(JSON.stringify(result));
        }



        /*
        // Departure Information
        // Returns information about the selected departure
        */
        function DepartureInformation(DepartureId) {
            PageMethods.DepartureInformation(DepartureId, DepartureInformationOK, DepartureInformationErr);
        }

        function DepartureInformationOK(result) {
            var departureItems = jQuery.parseJSON(result);
            var departureInformationText = "";
            departureInformationText += "<span>";
            departureInformationText += departureItems[0]["Name"];
            departureInformationText += " departing ";
            departureInformationText += departureItems[0]["DepartureDate"];
            departureInformationText += " ";
            departureInformationText += departureItems[0]["Direction"];
            departureInformationText += " on board the ";
            departureInformationText += departureItems[0]["Ship"];
            departureInformationText += "</span>";
            $("#departureInformation").html(departureInformationText);
        }

        function DepartureInformationErr(result) {
            alert(JSON.stringify(result));
        }



        /*
        // Relevant Booking Terms
        */
        $('a.Terms').click(function () {
            window.open(this.href, "_blank", "toolbar=no, scrollbars=yes, resizable=no, width=600, height=600");
            return false;
        });



        /* Format number */
        function datatableNumberFormat(number, decPlaces, thouSeparator, decSeparator, sign) {
            var minus = "";
            try {
                if (number) {
                    if (String(number).charAt(0) !== "-") {
                        minus = "";
                    }
                    //minus = number.charAt(0);
                    //if (minus !== "-") {
                    //    minus = "";
                    //}
                }
                var i = parseInt(number = Math.abs(+number || 0).toFixed(decPlaces)) + "";
                var j = (j = i.length) > 3 ? j % 3 : 0;
                var column = ""; //"<span class='hidden'>" + minus + number + "</span>";
                return column + sign + minus + (j ? i.substr(0, j) + thouSeparator : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thouSeparator) + (decPlaces ? decSeparator + Math.abs(number - i).toFixed(decPlaces).slice(2) : "");
            }
            catch (err) {
                return number;
            }
        }
        

        /*
        // Page Load
        // Functions to run when the page loads
        */
        LeadInPricePanel();
        if (bookNow == 'True') {
            setTimeout(showPricePanel, 500);
        }

        if (CartToken) {
            BookingSummary(CartToken);
        }


    });

});