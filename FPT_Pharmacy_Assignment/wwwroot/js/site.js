// wwwroot/js/site.js
document.addEventListener("DOMContentLoaded", function () {
    var priceCheckboxes = document.querySelectorAll('input[type="checkbox"]');
    priceCheckboxes.forEach(function (checkbox) {
        checkbox.addEventListener('change', filterData);
    });

    var searchInput = document.getElementById('searchInput');
    if (searchInput) {
        searchInput.addEventListener('input', filterData);
    }
});

function filterData() {
    var priceFilters = document.querySelectorAll('input[type="checkbox"]:checked');
    var searchInput = document.getElementById('searchInput');
    var searchTerm = searchInput ? searchInput.value.toLowerCase() : '';
    var products = document.querySelectorAll('.col-md-4');

    products.forEach(function (product) {
        var price = parseFloat(product.querySelector('.card-text').innerText.replace('Price: ', '').replace('$', ''));
        var productName = product.querySelector('.card-title').innerText.toLowerCase();

        var displayProduct = true;

        // Price filtering
        priceFilters.forEach(function (checkbox) {
            var priceFilter = checkbox.value;

            if (priceFilter === 'price1' && !(price >= 0 && price <= 50)) {
                displayProduct = false;
            }
            if (priceFilter === 'price2' && !(price > 50 && price <= 200)) {
                displayProduct = false;
            }
            if (priceFilter === 'price3' && !(price > 200 && price <= 500)) {
                displayProduct = false;
            }
            if (priceFilter === 'price4' && !(price > 200 && price <= 500)) {
                displayProduct = false;
            }
            if (priceFilter === 'price5' && !(price > 500 && price <= 1000)) {
                displayProduct = false;
            }
            if (priceFilter === 'price6' && !(price > 1000 && price <= 2000)) {
                displayProduct = false;
            }
        });

        // Name search
        if (searchTerm && !productName.includes(searchTerm)) {
            displayProduct = false;
        }

        if (displayProduct) {
            product.style.display = 'block';
        } else {
            product.style.display = 'none';
        }
    });
}