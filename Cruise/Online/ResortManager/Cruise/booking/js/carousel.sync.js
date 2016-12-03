$(document).ready(function () {

        var pauseCarousels = function () {
            $(".carousel").carousel("pause");
        }
        var startCarousels = function (carousel) {
            $(carousel).carousel("start");
            alert(carousel);
        }
        //setTimeout(pauseCarousels, 2500);

        $("#mainCarousel .carousel").carousel("pause");
        $("#mainCarousel .carousel").carousel("start");

        //setTimeout(startCarousels, 3000, "#burmaCarousel");
        //setTimeout(startCarousels, 4000, "#vietnamCarousel");
        //setTimeout(startCarousels, 5000, "#chinaCarousel");
        //setTimeout(startCarousels, 6000, "#specialsCarousel");
        //setTimeout(startCarousels, 3000, "#amazonCarousel");
        //setTimeout(startCarousels, 4000, "#thailandCarousel");
        //setTimeout(startCarousels, 5000, "#indiaCarousel");
        //setTimeout(startCarousels, 6000, "#burmacoastalCarousel");

    });