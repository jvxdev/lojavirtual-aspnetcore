$(document).ready(function () {
    ScrollOrdination();
    ChangeOrdination();
    ChangeMainProductImage();
    ChangeAmountProductKart();
    ChangeUnitaryProductAmount();
});

function numberToReal(numero) {
    var numero = numero.toFixed(2).split('.');
    numero[0] = "R$ " + numero[0].split(/(?=(?:...)*$)/).join('.');
    return numero.join(',');
}

function ChangeAmountProductKart() {
    $("#order .btn-secondary").click(function () {
        if ($(this).hasClass("btn-less")) {
            ChangeUnitaryProductAmount("decrease", $(this));
        };

        if ($(this).hasClass("btn-more")) {
            ChangeUnitaryProductAmount("increase", $(this));
        };
    });
}

function ChangeUnitaryProductAmount(Operation, Button) {
    var father = Button.parent().parent();
    var productId = father.find(".input-product-id").val();
    var productStock = parseInt(father.find(".input-product-stock").val());
    var productUnitaryPrice = parseFloat(father.find(".input-product-unitary-price").val());

    var inputProductAmountKart = father.find(".input-product-amount-kart");
    var productAmountKart = parseInt(inputProductAmountKart.val());

    var inputProductPrice = Button.parent().parent().parent().parent().parent().find(".price");

    if (Operation == "increase") {
        if (productAmountKart == productStock) {
            alert("Desculpe! Não temos mais estoque deste produto.");
        } else {

        productAmountKart++;

        inputProductAmountKart.val(productAmountKart);

            var result = productUnitaryPrice * productAmountKart;
            inputProductPrice.text(numberToReal(result));
        }
    } else if (Operation == "decrease") {
        if (productAmountKart == 0) {
            alert("Não é possível selecionar esta quantidade! Clique em remover caso não queira mais comprar este produto.")
        } else {

        productAmountKart--;

        inputProductAmountKart.val(productAmountKart);

            var result = productUnitaryPrice * productAmountKart;
            inputProductPrice.text(numberToReal(result));
        }
    }
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