$(document).ready(function () {

    var removeFromModalCart = function (event) {
        var btn = event.target;
        var productId = $(btn).data('product-id'); 
        var divToDelete = $(btn).closest(".generated");

        var removeProductCartUrl = $('#removeProductCartUrl').data('remove-action-url')

        $(btn).removeAttr('enabled');
        $(btn).attr('disabled', 'disabled');

        $.ajax({
            type: 'POST',
            url: removeProductCartUrl,
            data: {
                productId: productId
            },
            success: function (response) {
                if (response && response.flag) {
                    $(divToDelete).remove();
                }
                else {
                    alert("Error");
                }

                $(event.target).removeAttr('disabled');
                $(event.target).attr('enabled', 'enabled');
            },
            error: function (error) {
            }
        });
    };

    var displayCartModal = function (cartModelList) {
        var isGenerated = false;

        var generatedElements = document.getElementsByClassName('generated').length;
        if (!generatedElements) {
            $.each(cartModelList, function (index, value) {

                var htmlCode = $([
                    "<div class='generated mb-4 border border-secondary rounded'>",
                    "   <div class='row'>",
                    "       <div class='col-3 mt-3'>",
                    "           <img src=" + value.productImage + " class='card-img' style='height:120px;width:auto;'/>",
                    "       </div>",
                    "       <div class='col-8 mt-4'",
                    "           <p class='card-text mb-1'>Name: " + value.productName + "</p>",
                    "           <p class='card-text mb-1'>Price: " + value.productPrice + "</p>",
                    "           <p class='card-text'>Quantity: " + value.quantityBuy + "</p>",
                    "       </div>",
                    "  </div>",
                    "  <div class='row'>",
                    "       <div class='col-12 mt-0 mb-2 d-flex justify-content-center'>",
                    "           <button type='button' data-product-id='" + value.productId + "' class='removeProductBtn btn btn-primary'>Remove Product</button>",
                    "       </div>",
                    "  </div>",
                    "</div>"
                ].join("\n"));
                

                $(".insertModalBody").append(htmlCode);
            });
            isGenerated = true;
        }

        if (!isGenerated) {
            if (cartModelList.length !== generatedElements) {
                $('.generated').remove();
                displayCartModal(cartModelList);
            }
        }
    };

    var openCartModal = function (event) {
        var btn = event.target;

        $(btn).removeAttr('enabled');
        $(btn).attr('disabled', 'disabled');

        var cartUrl = $('#cartUrl').data('cart-action-url');

        $.ajax({
            type: 'GET',
            url: cartUrl,
            data: {
            },
            success: function (response) {
                if (response && response.flag) {
                    displayCartModal(response.cartModelList);
                    $('.removeProductBtn').on('click', removeFromModalCart);
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



    var removeFromCart = function (event) {
        var btn = event.target;
        var productId = $(btn).data('product-id');
        var toDelete = $(btn).closest(".toDelete");

        $(btn).removeAttr('enabled');
        $(btn).attr('disabled', 'disabled');
        
        var removeFromCartUrl = $('#removeFromCartUrl').data('remove-from-cart-url');

        $.ajax({
            type: 'POST',
            url: removeFromCartUrl,
            data: {
                productId : productId
            },
            success: function (response) {
                if (response && response.flag) {
                    $(toDelete).fadeOut(425, function () {
                        $(toDelete).remove();
                        location.reload(true);
                    });
                }
                else {
                    alert("Error");
                }

                $(event.target).removeAttr('disabled');
                $(event.target).attr('enabled', 'enabled');
            },
            error: function (error) {
            }
        });
    };

    var timer, delay = 400;
    var changeQuantityToBuy = function (event) {
        var inputBtn = event.target;

        clearTimeout(timer);
        timer = setTimeout(function () {
            var quantityToBuy = inputBtn.value;
            var productId = $(inputBtn).data('product-id');

            var removeFromCartUrl = $('#updateQuantityToBuy').data('update-quantity-to-buy-url');
            $.ajax({
                type: 'POST',
                url: removeFromCartUrl,
                data: {
                    productId: productId,
                    quantityToBuy : quantityToBuy
                },
                success: function (response) {
                    if (response && response.flag) {
                        location.reload(true);
                    }
                    else {
                        alert("Error");
                    }
                },
                error: function (error) {
                }
            });
        }, delay);

    };


    //main
    $('.cartBtn').on('click', openCartModal);
    $('.deleteFromCart').on('click', removeFromCart);
    $('#quantityToBuy').on('blur', changeQuantityToBuy);
});
