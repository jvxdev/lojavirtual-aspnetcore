$(document).ready(function () {
    ScrollOrdination();
    ChangeOrdination();
    ChangeMainProductImage();
    ChangeAmountProductKart();
    ActionPlannerProduct();
});

function numberToReal(numero) {
    var numero = numero.toFixed(2).split('.');
    numero[0] = "R$ " + numero[0].split(/(?=(?:...)*$)/).join('.');
    return numero.join(',');
}

function ChangeAmountProductKart() {
    $("#order .btn-secondary").click(function () {
        if ($(this).hasClass("btn-less")) {
            ActionPlannerProduct("decrease", $(this));
        };

        if ($(this).hasClass("btn-more")) {
            ActionPlannerProduct("increase", $(this));
        };
    });
}

function ActionPlannerProduct(operation, button) {
    HiddeErrorMessage();

    /*
     * ========== CARREGAMENTO DOS VALORES ==========
     */

    var father = button.parent().parent();
    var productId = father.find(".input-product-id").val();
    var productStock = parseInt(father.find(".input-product-stock").val());
    var productUnitaryPrice = parseFloat(father.find(".input-product-unitary-price").val());

    var inputProductAmountKart = father.find(".input-product-amount-kart");
    var inputProductPrice = button.parent().parent().parent().parent().parent().find(".price");

    var oldProductAmountKart = parseInt(inputProductAmountKart.val());

    var product = new ProductAmountAndPrice(productId, productStock, productUnitaryPrice, oldProductAmountKart, 0, inputProductAmountKart, inputProductPrice);

    /*
     * ========== CHAMADA DE MÉTODOS ==========
     */

    ChangeProductKartValuesInput(product, operation);
}

function ChangeProductKartValuesInput(product, operation) {
    if (operation == "increase") {
        /*if (product.oldProductAmountKart == product.productStock) {
            alert("Desculpe! Estamos sem estoque para a quantidade desejada!");
        } else */{
            product.newProductAmountKart = product.oldProductAmountKart + 1;
            UpdateAmountAndValue(product);

            AjaxAmountProductAlteration(product);
        }
    } else if (operation == "decrease") {
        /*if (product.oldProductAmountKart == 1) {
            alert("Não é possível selecionar uma quantidade menor que 1! Clique em remover caso não queira mais comprar este produto.")
        } else */{
            product.newProductAmountKart = product.oldProductAmountKart - 1;
            UpdateAmountAndValue(product);

            AjaxAmountProductAlteration(product);
        }
    }
}

function AjaxAmountProductAlteration(product) {
    $.ajax({
        type: "GET",
        url: "/ShoppingKart/ChangeAmount?Id=" + product.productId + "&Amount=" + product.newProductAmountKart,
        error: function (data) {
            ShowErrorMessage(data.responseJSON.message);

            product.newProductAmountKart = product.oldProductAmountKart;
            UpdateAmountAndValue(product);
        },
        success: function (data) {
        }
    });
}

function ShowErrorMessage(message) {
    $(".alert-danger").css("display", "block");
    $(".alert-danger").text(message);
}

function HiddeErrorMessage(message) {
    $(".alert-danger").css("display", "none");
}

function UpdateAmountAndValue(product) {
    product.inputProductAmountKart.val(product.newProductAmountKart);

    var result = product.productUnitaryPrice * product.newProductAmountKart;
    product.inputProductPrice.text(numberToReal(result));
}

function ChangeMainProductImage() {
    $(".img-small-wrap img").click(function () {
        var Path = $(this).attr("src");
        $(".img-big-wrap img").attr("src", Path);
        $(".img-big-wrap a").attr("href", Path);
    });
}

function ScrollOrdination() {
    if (window.location.hash.length > 0)
    {
        var hash = window.location.hash;

        if (hash == "#productPosition")
        {
            window.scrollBy(0, 530);
        }
    }
}

function ChangeOrdination() {
    $("#ordinationList").change(function () {
        var Page = 1;
        var Search = "";
        var Ordination = $(this).val();
        var Fragment = "#productPosition";

        var QueryString = new URLSearchParams(window.location.search);

        if (QueryString.has("page")) {
            Page = QueryString.get("page");
        }

        if (QueryString.has("search")) {
            Search = QueryString.get("search");
        }

        if ($("#breadcrumb").length > 0) {
            Fragment = "";
        }

        var URL = window.location.protocol + "//" + window.location.host + window.location.pathname;

        var URLWithParameters = URL + "?page=" + Page + "&search=" + Search + "&ordination=" + Ordination + Fragment;

        window.location.href = URLWithParameters;
    });
}

/*
 * ========= CLASSES =========
 */

class ProductAmountAndPrice {
    constructor(productId, productStock, productUnitaryPrice, oldProductAmountKart, newProductAmountKart, inputProductAmountKart, inputProductPrice) {
        this.productId = productId;
        this.productStock = productStock;
        this.productUnitaryPrice = productUnitaryPrice;

        this.oldProductAmountKart = oldProductAmountKart;
        this.newProductAmountKart = newProductAmountKart;

        this.inputProductAmountKart = inputProductAmountKart;
        this.inputProductPrice = inputProductPrice;
    }
}