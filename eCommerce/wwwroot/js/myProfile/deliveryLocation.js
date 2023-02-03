$(document).ready(function () {

    var deleteDeliveryLocation = function (event) {
        var btn = event.target;

        var deliveryLocationId = $(btn).data('delivery-location-id');
        var deleteDeliveryLocationUrl = $('#deleteDeliveryLocationUrl').data('delete-delivery-location-url');

        $(btn).removeAttr('enabled');
        $(btn).attr('disabled', 'disabled');

        $.ajax({
            type: 'POST',
            url: deleteDeliveryLocationUrl,
            data: {
                deliveryLocationId: deliveryLocationId
            },
            success: function (response) {
                if (response && response.flag) {
                    location.reload(true);
                }
                else {
                    alert("Error at deleting");
                }

                $(btn).removeAttr('disabled');
                $(btn).attr('enabled', 'enabled');
            },
            error: function (error) {
            }
        });
    }

    // main
    $('.deleteDeliveryLocationBtn').on('click', deleteDeliveryLocation);
});