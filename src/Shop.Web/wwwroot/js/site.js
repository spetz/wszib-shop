// Write your JavaScript code.

(function () {
    function init() {
        $('.add-to-cart').click(function() {
            var productId = $(this).data('id');
            addCartItem(productId);
        })
    }
    init();

    function addCartItem(productId) {
        $.post(`cart/items/${productId}`, response => {
            console.log(`Product with id: ${productId} was added to the cart.`);
        }); 
    }
})();
