const decreaseButton = document.getElementById("button-addon1");
const increaseButton = document.getElementById("button-addon2");
const quantityInput = document.querySelector(".form-control");

const addToCartButton = document.getElementById("add-to-cart-btn");
addToCartButton.addEventListener("click", addToCart);

// Get the maximum stock level from the server
const stockLevelElement = document.getElementById("stock-level");
const maxStockLevel = parseInt(stockLevelElement.getAttribute('data-stock-level'));
// Add event listeners to the buttons
decreaseButton.addEventListener("click", decreaseQuantity);
increaseButton.addEventListener("click", increaseQuantity);

// Function to decrease the quantity
function decreaseQuantity() {
    let currentQuantity = parseInt(quantityInput.value);
    if (currentQuantity > 1) {
        currentQuantity--;
        quantityInput.value = currentQuantity;
    }
}

// Function to increase the quantity
function increaseQuantity() {
    let currentQuantity = parseInt(quantityInput.value);
    if (currentQuantity < maxStockLevel) {
        currentQuantity++;
        quantityInput.value = currentQuantity;
    }
}

function addToCart() {
    const productInfo = document.getElementById('productInfo');
    const productId = parseInt(productInfo.getAttribute('data-product-id'));
    const productName = productInfo.getAttribute('data-name');
    const productPrice = parseFloat(productInfo.getAttribute('data-pricing'));
    const productQuantity = parseInt(document.getElementById('productQuantity').value);

    $.ajax({
        url: '/Products/AddToCart',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({
            productId: productId,
            productName: productName,
            price: productPrice,
            quantity: productQuantity
        }),
        success: function (response) {
            toastr.success(response.message)
        },
        error: function () {
            toastr.error('Error adding product to cart');
        }
    });
}