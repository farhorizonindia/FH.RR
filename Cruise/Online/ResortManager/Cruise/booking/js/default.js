$(function () {
    "use strict";

    $(document).ready(function () {


        /*
        // Set all the carousels
        */
        $(".carousel").carousel({
            interval: 8000
        })

        

        $('.imageFader').hover(function () {
            $(this).parent().find('.carousel .carousel-inner .item img').fadeTo(500, 0.7);
        }, function () {
            $(this).parent().find('.carousel .carousel-inner .item img').fadeTo(500, 1);
        });

        $('.imageOffersFader').hover(function () {
            $(this).find('.blackBack img').fadeTo(500, 1);
        }, function () {
            $(this).find('.blackBack img').fadeTo(500, 0.8);
        });



        

    });

});