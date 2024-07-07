$(document).ready(function () {
    //request for add product
    $("#myForm").on('click', "#btnAddProduct", function () {
        var name = $("#name").val();
        var category = $("#category").val();
        var price = $("#price").val();
        var stock = $("#stock").val();

        var productData = {
            Name: name,
            Category: category,
            Price: price,
            Stock: stock
        }

        //var productData = "{ 'Name':'" + name
        //    + "','Category':'" + category
        //    + "','Price':'" + price
        //    + "','Stock':'" + stock
        //    + "'}";

        makeRequest('/Home/Add', 'post', productData);
    });
    //request for edit product
    $("body").on('click', "#btnEditProduct", function () {
        var id = $("#id").val();
        var name = $("#name").val();
        var category = $("#category").val();
        var price = $("#price").val();
        var stock = $("#stock").val();

        var productData = {
            ProductId: id,
            Name: name,
            Category: category,
            Price: price,
            Stock: stock
        }
        makeRequest("/Home/Edit", 'post', productData);
    });
    //request for delete product
    $("body").on('click', "#btnDeleteProduct", function () {
        var id = $("#id").val();/*
        var name = $("#name").val();
        var category = $("#category").val();
        var price = $("#price").val();
        var stock = $("#stock").val();*/
/*
        var productData = {
            ProductId: id,
            Name: name,
            Category: category,
            Price: price,
            Stock: stock
        }*/
        makeRequest("/Home/Delete"+ id , 'post', null);
    });
});