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
            removeCartItem(productId).then(function (response) {
                if (quantity === 1) {
                    item.parent().parent().fadeOut();

                    return;
                }
                var updatedQuantity = --quantity;
                item.data('quantity', updatedQuantity);
                $(`#quantity-${productId}`).text(updatedQuantity);
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
