$(document).ready(function () {


    var addToCart = function (event) {
        var btn = event.target;

        var productId = $(btn).data('product-id');
        var quantityToBuy = $('#quantityToBuy').val();

        var addToCartUrl = $('#addProductToCartUrl').data('add-product-to-cart-url');

        $(btn).removeAttr('enabled');
        $(btn).attr('disabled', 'disabled');

        $.ajax({
            type: 'POST',
            url: addToCartUrl,
            data: {
                productId: productId,
                quantityToBuy: quantityToBuy
            },
            success: function (response) {
                if (response && response.flag) {
                    
                }
                else {
                    alert("Error");
                }

                $(btn).removeAttr('disabled');
                $(btn).attr('enabled', 'enabled');
            },
            error: function (error) {
            }
        });
    };

    var changeQuantityToBuy = function (event) {
        var inputBtn = event.target;
        document.getElementById("quantityToBuy").value = inputBtn.value;
    };


    var deleteCommentAdmin = function (event) {
        var btn = event.target;
        var productCommentId = $(btn).data('product-comment-id');
        var deleteProductCommentUrl = $('#deleteProductCommentUrl').data('delete-product-comment-url');

        var divToDelete = $(btn).closest(".divToDelete");

        $(btn).removeAttr('enabled');
        $(btn).attr('disabled', 'disabled');

        $.ajax({
            type: 'POST',
            url: deleteProductCommentUrl,
            data: {
                productCommentId: productCommentId
            },
            success: function (response) {
                if (response && response.flag) {
                    $(divToDelete).fadeOut(425, function () {
                        $(divToDelete).remove();
                    });
                }
                else {
                    alert("Error at deleting comment");
                }

                $(btn).removeAttr('disabled');
                $(btn).attr('enabled', 'enabled');
            },
            error: function (error) {
            }
        });
    };

    // main
    $('#addToCart').on('click', addToCart);
    $('#quantityToBuy').on('blur', changeQuantityToBuy);
    $('.deleteProductCommentBtn').on('click', deleteCommentAdmin);
});