$(function () {
    "use strict";

    $(document).ready(function () {


        $('.imageOffersFader').hover(function () {
            $(this).find('.blackBack img').fadeTo(500, 1);
        }, function () {
            $(this).find('.blackBack img').fadeTo(500, 0.8);
        });



        

    });

});