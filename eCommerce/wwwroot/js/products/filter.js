$(document).ready(function () {

    var applyFilters = function (event) {
        var btn = event.target; 
        $(btn).removeAttr('enabled');
        $(btn).attr('disabled', 'disabled');

        var categoryId = $(btn).data("category-id");
        var applyFiltersUrl = $("#applyFiltersUrl").data("apply-filters-url");


        var manufacturersFilter = [];

        $(".manufacturerInput").each(function (index, element) {
            if (element.checked) {
                manufacturersFilter.push(element.id);
            }
            
        });

        var radioButtonChecked = $("input[name='price']:checked").attr("id");

        var minPriceFilter, maxPriceFilter;
        if (radioButtonChecked == "all") {
            minPriceFilter = parseFloat(0);
            maxPriceFilter = parseFloat(-1);
        }
        else if (radioButtonChecked == "600")
        {
            minPriceFilter = parseFloat(600);
            maxPriceFilter = parseFloat(-1);
        }
        else {
            var valuesSplitted = radioButtonChecked.split("_");
            minPriceFilter = parseFloat(valuesSplitted[0]);
            maxPriceFilter = parseFloat(valuesSplitted[1]);
        }
        
        
        var filters = {
            "ManufacturersFilter": manufacturersFilter,
            "MinPriceFilter": minPriceFilter,
            "MaxPriceFilter": maxPriceFilter
        };
 
        var data = {
            "categoryId": categoryId,
            "filtersModel": filters
        };

        $.ajax({
            type: "POST",
            url: applyFiltersUrl,
            data: data,
            success: function (response) {
                $('#productList').empty();
                $('#productList').html(response);

                $(btn).removeAttr('disabled');
                $(btn).attr('enabled', 'enabled');
            },
            error: function () {
                alert("Nothing match your criteria!");
            }
        });
    };

    //main
    $("#applyFilters").on("click", applyFilters);
});