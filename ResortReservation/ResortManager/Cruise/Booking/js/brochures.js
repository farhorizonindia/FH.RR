$(document).ready(function () {

    /*
    // Enter address manually
    */
    $('#enterAddress').click(function (e) {
        $("#ContentPlaceHolder1_addressSearch").addClass("hide");
        $("#ContentPlaceHolder1_addressField").removeClass("hide");
        e.preventDefault();
    });

});