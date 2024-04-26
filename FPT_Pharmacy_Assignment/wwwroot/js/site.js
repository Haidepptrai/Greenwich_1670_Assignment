$(document).ready(function () {
    // Filter products based on price range
    $('#filterForm input[type="checkbox"]').change(function () {
        var selectedPrices = [];
        $('#filterForm input[type="checkbox"]:checked').each(function () {
            selectedPrices.push($(this).val());
        });

        // AJAX call to fetch filtered products
        $.ajax({
            url: '@Url.Action("FilterProducts", "Products")',
            type: 'POST',
            data: { prices: selectedPrices },
            success: function (data) {
                $('#productList').html(data);
            }
        });
    });
});
