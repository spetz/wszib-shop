// Write your JavaScript code.

(function () {
    function init() {
        $('.add-to-cart').click(function() {
            var productId = $(this).data('id');
            addCartItem(productId);
        })

        $('.remove-from-cart').click(function () {
            var item = $(this);
            var productId = item.data('id');
            var quantity = parseInt(item.data('quantity'));
            var itemsCount = parseInt($('#items-count').text());
            var totalPrice = parseFloat($('#total-price').text());
            var productTotalPrice = parseFloat($(`#total-price-${productId}`).text());
            var productUnitPrice = parseFloat(item.data('unit-price'));

            $('#total-price').text(totalPrice - productUnitPrice);

            removeCartItem(productId).then(function (response) {
                itemsCount -= 1;
                if (itemsCount === 0) {
                    $('#purchase').fadeOut();
                }
                if (quantity === 1) {
                    item.parent().parent().fadeOut();
                    $('#items-count').text(itemsCount);

                    return;
                }
                var updatedQuantity = --quantity;
                item.data('quantity', updatedQuantity);
                $(`#quantity-${productId}`).text(updatedQuantity);
                $(`#total-price-${productId}`).text(productTotalPrice - productUnitPrice);
            });
        })
    }
    init();

    function addCartItem(productId) {
        $.post(`cart/items/${productId}`, response => {
            console.log(`Product with id: ${productId} was added to the cart.`);
        }); 
    }

    //Return 'promise'
    function removeCartItem(productId) {
        return $.ajax({
            type: 'DELETE',
            url: `cart/items/${productId}`
        });
    }
})();
