$(function () {
    "use strict";

    $(document).ready(function () {

        /*
        // Select Country Menu
        // Show the selected country in the menu
        */
        var caretDown = '<span class="caret pull-right topMargin10"></span>';
        var menuLoading = '<span class="bottomPadding30 font16"><i class="fa fa-cog fa-spin font16"></i> Loading...</span>';

        $("#countryFlags ul li a").click(function () {
            $("#countryFlag").html($(this).html());
            $("#countryFlag").val($(this).text());
        });

        Filter(0, 0, '', "Destinations", "#filterDestination");
        Filter(0, 0, '', "Departures", "#filterDate");
        Filter(0, 0, '', "Rivers", "#filterRiver");


        /*
        // Cruise Critic Social Icon Rollover
        */
        $("#CruiseCritic").mouseover(function () {
            $(this).attr("src", "/images/template/CruiseCritic_ro.png");
        })
        .mouseout(function () {
            $(this).attr("src", "/images/template/CruiseCritic.png");
        });


        function trimLength(input, outputLength) {
            if (input.length > outputLength) {
                return input.substr(0, outputLength) + "...";
            } else {
                return input;
            }
        }


        /*
        // Filter
        // Load filter menus with relevent data
        */
        function Filter(_CountryId, _RiverId, _Date, _Type, menuElement) {
            var params = '{"CountryId": "' + _CountryId + '", "RiverId": "' + _RiverId + '", "Departure": "' + _Date + '", "Type": "' + _Type + '"}';
            var menuElementId = "";
            $.ajax({
                url: "Default.aspx/Filter",
                type: "POST",
                data: params,
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    menuElementId = menuElement + "Menu";
                    $(menuElementId).empty();
                    if (menuElement == "#filterDestination") {
                        $("#filterDate").html('<span class="pull-left">All Dates</span>' + caretDown);
                        $("#filterDate").attr("data-filterid", "0");
                        $("#filterRiver").html('<span class="pull-left">All Rivers</span>' + caretDown);
                        $("#filterRiver").attr("data-filterid", "0");
                    }
                    var items = jQuery.parseJSON(data.d)
                    for (var i = 0; i < items.length; i++) {
                        $(menuElementId).append("<li><a id='_" + items[i]['Value'] + "' href='javascript:void(0);'>" + items[i]['Name'] + "</a><li>");
                    }
                    menuElementId = menuElementId + " li a";
                    $(menuElementId).click(function () {
                        $(menuElement).html('<span class="pull-left">' + trimLength($(this).text(), 14) + '</span>' + caretDown);
                        $(menuElement).val(trimLength($(this).text(), 14));
                        var filterId = $(this).attr("id");
                        $(menuElement).attr("data-filterid", filterId.replace("_", ""));
                        runFilter(menuElement);
                    });
                },
                error: function (data) {
                    //alert(JSON.stringify(data));
                },
            })
        }

        function getMenuSelection(menuElement) {
            var filterId = $(menuElement).attr("data-filterid");
            return filterId;
        }

        function runFilter(menuElement) {
            var destinationId = getMenuSelection("#filterDestination")
            var departureId = getMenuSelection("#filterDate")
            var riverid = getMenuSelection("#filterRiver")
            if (menuElement == "#filterDestination") {
                Filter(destinationId, riverid, departureId, "Destinations", "#filterDestination");
                Filter(destinationId, riverid, departureId, "Departures", "#filterDate");
                Filter(destinationId, riverid, departureId, "Rivers", "#filterRiver");
            }
            if (menuElement == "#filterDate") {
                Filter(destinationId, 0, departureId, "Rivers", "#filterRiver");
            }
            if (menuElement == "#filterRiver") {
                Filter(destinationId, riverid, 0, "Departures", "#filterDate");
            }
        }

        $("#Search").click(function () {
            var departureId = getMenuSelection("#filterDate").split("-");
            var destinationId = getMenuSelection("#filterDestination");
            var month = 0;
            var year = 0;
            if (departureId.length > 1) {
                month = departureId[0];
                year = departureId[1];
            }
            var riverid = getMenuSelection("#filterRiver");
            window.location.href = "/river-cruises/search/" + destinationId + "/" + month + "/" + year + "/" + riverid;
        });

        



    });

});

var maxHeight = 400;

$(function () {

    $(".dropdown > li").hover(function () {

        var $container = $(this),
            $list = $container.find("ul"),
            $anchor = $container.find("a"),
            height = $list.height() * 1.1,       // make sure there is enough room at the bottom
            multiplier = height / maxHeight;     // needs to move faster if list is taller

        // need to save height here so it can revert on mouseout            
        $container.data("origHeight", $container.height());

        // so it can retain it's rollover color all the while the dropdown is open
        $anchor.addClass("hover");

        // make sure dropdown appears directly below parent list item    
        $list
            .show()
            .css({
                paddingTop: $container.data("origHeight")
            });

        // don't do any animation if list shorter than max
        if (multiplier > 1) {
            $container
                .css({
                    height: maxHeight,
                    overflow: "hidden"
                })
                .mousemove(function (e) {
                    var offset = $container.offset();
                    var relativeY = ((e.pageY - offset.top) * multiplier) - ($container.data("origHeight") * multiplier);
                    if (relativeY > $container.data("origHeight")) {
                        $list.css("top", -relativeY + $container.data("origHeight"));
                    };
                });
        }

    }, function () {

        var $el = $(this);

        // put things back to normal
        $el
            .height($(this).data("origHeight"))
            .find("ul")
            .css({ top: 0 })
            .hide()
            .end()
            .find("a")
            .removeClass("hover");

    });

    // Add down arrow only to menu items with submenus
    $(".dropdown > li:has('ul')").each(function () {
        $(this).find("a:first").append("<img src='images/down-arrow.png' />");
    });

});