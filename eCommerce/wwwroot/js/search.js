$(document).ready(function () {

    var search = function (event) {
        var btn = event.target;

        $(btn).removeAttr('enabled');
        $(btn).attr('disabled', 'disabled');

        var searchString = $("#search").val();

        var searchUrl = $("#searchUrl").data("search-url");

        if (searchString != null && searchString.length != 0) {
            $.ajax({
                type: "POST",
                url: searchUrl,
                data: {
                    search: searchString
                },
                success: function (response) {
                    if (response.flag != false) {
                        $('body').html(response);
                    }
                    else if (response.flag == false) {
                        alert("Nothing match your critaria!");
                    }
                },
                error: function () {
                    alert("Nothing match your critaria!");
                }
            });
        }
    }

    // main
    $("#search").on('blur', search);
});