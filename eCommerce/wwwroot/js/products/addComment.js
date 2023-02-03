$(document).ready(function () {

    var RateClick = function (event) {
        var starSelected = event.target;
        var rating = $(starSelected).data("rating");

        $("#lblRating").val(rating);

        for (var i = 1; i <= rating; i++) {
            $("#star" + i).attr('class', 'fas fa-star rating-star');
        }

        for (var i = rating + 1; i <= 5; i++) {
            $("#star" + i).attr('class', 'far fa-star rating-star');
        }
    };

    var RateOver = function (event) {
        var starSelected = event.target;
        var rating = $(starSelected).data("rating");

        for (var i = 1; i <= rating; i++) {
            $("#star" + i).attr('class', 'fas fa-star rating-star');
        }
    };

    var RateOut = function (event) {
        var starSelected = event.target;
        var rating = $(starSelected).data("rating");

        for (var i = 1; i <= rating; i++) {
            $("#star" + i).attr('class', 'far fa-star rating-star');
        }
    };

    var RateSelected = function () {
        var rating = $("#lblRating").val();

        for (var i = 1; i <= rating; i++) {
            $("#star" + i).attr('class', 'fas fa-star rating-star')
        }
    };

    //main
    $("#star1").on('click', RateClick);
    $("#star2").on('click', RateClick);
    $("#star3").on('click', RateClick);
    $("#star4").on('click', RateClick);
    $("#star5").on('click', RateClick);

    $("#star1").on('mouseover', RateOver);
    $("#star2").on('mouseover', RateOver);
    $("#star3").on('mouseover', RateOver);
    $("#star4").on('mouseover', RateOver);
    $("#star5").on('mouseover', RateOver);

    $("#star1").on('mouseout', RateOut);
    $("#star2").on('mouseout', RateOut);
    $("#star3").on('mouseout', RateOut);
    $("#star4").on('mouseout', RateOut);
    $("#star5").on('mouseout', RateOut);

    $("#stars").on('mouseout', RateSelected);
});