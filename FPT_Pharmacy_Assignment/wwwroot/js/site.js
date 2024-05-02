$(document).ready(function () {
    $('input[name="priceFilter"]').change(function () {
        var selectedFilters = [];
        $('input[name="priceFilter"]:checked').each(function () {
            selectedFilters.push($(this).val());
        });
        $.ajax({
            url: '/Products/FilterProducts', // Update the URL according to your routing configuration
            type: 'POST',
            data: { prices: selectedFilters },
            success: function (response) {
                // Update product list with filtered products
                $('#productList').empty();
                response.forEach(function (product) {
                    $('#productList').append('<div class="col-lg-4 col-md-6 align-self-center mb-30 properties-items col-md-6 rac str"> <div class="card"> <div class="card-img-container d-flex justify-content-center align-items-center" style="height: 300px;"> <img src="/uploads/products/' + product.ImageFile + '" class="card-img-top" alt="' + product.Name + '"> </div> <div class="card-body"> <h5 class="card-title">' + product.Name + '</h5> <p class="card-text">Price: ' + product.Price + ' $</p> <p class="card-text">Quantity: ' + product.StockLevel + '</p> <a href="/Products/Details/' + product.ProductId + '" class="btn btn-primary">Details</a> </div><div class="main-button"> <a href="">Add to cart</a> </div> </div> </div>');
                });
            }
        });
    });
});
