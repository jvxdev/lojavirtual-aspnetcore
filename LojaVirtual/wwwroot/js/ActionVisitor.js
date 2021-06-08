$(document).ready(function () {
    ScrollOrdination();
    ChangeOrdination();
    ChangeMainProductImage();
    ChangeAmountProductKart();

    Mask();
    AjaxBuscarCEP();
    ActionCalcularFreteBtn();
    AjaxCalcularFrete(false);

    AjaxDeliveryAddressCalcularFrete();

    OrderBtnImprimir();
});

function OrderBtnImprimir() {
    $(".btn-imprimir").click(function () {
        window.print();
    })
}

function AjaxDeliveryAddressCalcularFrete() {
    $("input[name=deliveryAddress]").change(function () {

        $.cookie("ShoppingKart.DeliveryAddress", $(this).val(), { path: "/" });

        var cep = DeleteMask($(this).parent().find("input[name=cep]").val());

        DeliveryAddressCardsClear();
        ClearValues();
        DeliveryAddressCardsLoading();

        $(".btn-proceed").addClass("disabled");

        $.ajax({
            type: "GET",
            url: "/ShoppingKart/CalcularFrete?cepDestino=" + cep,
            error: function (data) {
                ShowErrorMessage("Desculpe! Tivemos um erro ao tentar obter o frete... " + data.Message);

                DeliveryAddressCardsClear();
            },
            success: function (data) {
                DeliveryAddressCardsClear();

                for (var i = 0; i < data.valuesList.length; i++) {
                    var tipoFrete = data.valuesList[i].tipoFrete;
                    var valor = data.valuesList[i].valor;
                    var prazo = data.valuesList[i].prazo;

                    $(".card-title")[i].innerHTML = "<label for='" + tipoFrete + "'>" + tipoFrete + "</label>";
                    $(".card-text")[i].innerHTML = "<label for='" + tipoFrete + "'>Prazo de até " + prazo + " dia(s).</label>";
                    $(".card-footer")[i].innerHTML = "<input type=\"radio\" name=\"frete\" id='" + tipoFrete + "' value=\"" + tipoFrete + "\" /> <strong><label for='" + tipoFrete + "'>" + numberToReal(valor) + "</label></strong>";

                    console.info($.cookie("ShoppingKart.tipoFrete") + " - " + tipoFrete);
                    console.info($.cookie("ShoppingKart.tipoFrete") == tipoFrete);

                    if ($.cookie("ShoppingKart.tipoFrete") != undefined && $.cookie("ShoppingKart.tipoFrete") == tipoFrete) {
                        $(".card-footer input[name=frete]").eq(i).attr("checked", "checked");

                        TipoFreteBackgroundStyle($(".card-footer input[name=frete]").eq(i));

                        $(".btn-proceed").removeClass("disabled");
                    }
                }

                $(".card-footer").find("input[name=frete]").change(function () {
                    $.cookie("ShoppingKart.tipoFrete", $(this).val(), { path: '/' });
                    $(".btn-proceed").removeClass("disabled");

                    TipoFreteBackgroundStyle($(this));
                });

                /*
                $(".container-frete").html(html);

                $(".container-frete").find("input[type=radio]").change(function () {

                    $.cookie("ShoppingKart.tipoFrete", $(this).val());
                    $(".btn-proceed").removeClass("disabled");

                    var valorFrete = parseFloat($(this).parent().find("input[type=hidden]").val());

                    $(".frete").text(numberToReal(valorFrete));

                    var subtotal = parseFloat($(".subtotal").text().replace("R$", "").replace(".", "").replace(",", "."));

                    var total = valorFrete + subtotal;

                    $(".total").text(numberToReal(total));
                });
                */

                //console.info(data);
            }
        });
    });
}

function TipoFreteBackgroundStyle(obj) {
    $(".card-body").css("background-color", "white");
    $(".card-footer").css("background-color", "rgba(0,0,0,.03)");

    obj.parent().parent().find(".card-body").css("background-color", "#CACACA");
    obj.parent().parent().find(".card-footer").css("background-color", "#CACACA");

    UpdateValues();
}

function UpdateValues() {
    var product = parseFloat($(".product-text").text().replace("R$", "").replace(".", "").replace(",", "."));
    var frete = parseFloat($(".card-footer input[name=frete]:checked").parent().find("label").text().replace("R$", "").replace(".", "").replace(",", "."));

    var total = product += frete;

    $(".frete-text").text(numberToReal(frete));
    $(".total-text").text(numberToReal(total));
}

function ClearValues() {
    $(".frete-text").text("-");
    $(".total-text").text("-");
}

function DeliveryAddressCardsLoading() {
    for (var i = 0; i < 3; i++) {
        $(".card-text")[i].innerHTML = "<img src='/img/loading.gif' />";
    }
}

function DeliveryAddressCardsClear() {
    for (var i = 0; i < 3; i++) {
        $(".card-title")[i].innerHTML = "-";
        $(".card-text")[i].innerHTML = "-";
        $(".card-footer")[i].innerHTML = "<h5>-</h5>";
    }
}

function AjaxBuscarCEP() {
    $("#CEP").keyup(function () {

        HiddeErrorMessage();

        if ($(this).val().length == 10) {

            var cep = DeleteMask($(this).val());

            $.ajax({
                type: "GET",
                url: "https://viacep.com.br/ws/" + cep + "/json/?callback=callback_name",
                dataType: "jsonp",
                error: function (data) {
                    ShowErrorMessage("Ocorreu um erro durante a busca do CEP. Tente novamente mais tarde!");
                    console.info(data);
                },
                success: function (data) {
                    console.info("OK");
                    console.info(data);

                    if (data.erro == undefined) {
                        $("#State").val(data.uf);
                        $("#City").val(data.localidade);
                        $("#Neighborhood").val(data.bairro);
                        $("#Street").val(data.logradouro);
                    }
                    else {
                        ShowErrorMessage("O CEP informado não existe!");
                    }
                }
            });
        }
    });
}

function Mask() {
    $(".cep").mask("00.000-000");
    $(".cpf").mask("000.000.000-00");
    $(".card-number").mask("0000-0000-0000-0000");
}

function ActionCalcularFreteBtn() {
    $(".btn-calcular-frete").click(function (e) {
        AjaxCalcularFrete(true);
        e.preventDefault();
    });
}

function AjaxCalcularFrete(callByBtn) {
    $(".btn-proceed").addClass("disabled");
    if (callByBtn == false) {
        if ($.cookie('ShoppingKart.CEP') != undefined) {
            if ($(".no-cep").length <= 0) {
                $(".cep").val($.cookie("ShoppingKart.CEP"));
            }
        }
    }

    if ($(".cep").length > 0) {

        var cep = DeleteMask($(".cep").val());
        $.removeCookie("ShoppingKart.tipoFrete");

        if (cep.length == 8) {
            $.cookie('ShoppingKart.CEP', $(".cep").val());

            $(".container-frete").html("<img src='/img/loading.gif' />");
            $(".frete").text("R$ 0,00");
            $(".total").text("R$ 0,00");

            $.ajax({
                type: "GET",
                url: "/ShoppingKart/CalcularFrete?cepDestino=" + cep,
                error: function (data) {
                    ShowErrorMessage("Desculpe! Tivemos um erro ao tentar obter o frete... " + data.Message);
                    console.info(data);
                },
                success: function (data) {
                    console.info(data);
                    html = "";

                    for (var i = 0; i < data.valuesList.length; i++) {
                        var tipoFrete = data.valuesList[i].tipoFrete;
                        var valor = data.valuesList[i].valor;
                        var prazo = data.valuesList[i].prazo;

                        html += "<dl><dd class=\"text-left\"><input type=\"radio\" name=\"frete\" value=\"" + tipoFrete + "\"/><input type=\"hidden\" name=\"valor\" value=\"" + valor + "\"/>   " + tipoFrete + " - <strong>" + numberToReal(valor) + "</strong> (até " + prazo + " dias úteis)</dd></dl>";
                    }

                    $(".container-frete").html(html);

                    $(".container-frete").find("input[type=radio]").change(function () {

                        $.cookie("ShoppingKart.tipoFrete", $(this).val());
                        $(".btn-proceed").removeClass("disabled");

                        var valorFrete = parseFloat($(this).parent().find("input[type=hidden]").val());

                        $(".frete").text(numberToReal(valorFrete));

                        var subtotal = parseFloat($(".subtotal").text().replace("R$", "").replace(".", "").replace(",", "."));

                        var total = valorFrete + subtotal;

                        $(".total").text(numberToReal(total));
                    });

                    //console.info(data);
                }
            });
        } else {
            if (callByBtn == true) {
                $(".container-frete").html("");
                ShowErrorMessage("O campo CEP é obrigatório!");
            }
        }
    }
}

function numberToReal(numero) {
    var numero = numero.toFixed(2).split('.');
    numero[0] = "R$ " + numero[0].split(/(?=(?:...)*$)/).join('.');
    return numero.join(',');
}

function ChangeAmountProductKart() {
    $("#order .btn-amount").click(function (e) {
        e.preventDefault();
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
            AjaxCalcularFrete();
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

    UpdateSubtotal();
}

function UpdateSubtotal() {
    var Subtotal = 0;
    var priceTags = $(".price");

    priceTags.each(function () {
        var reaisValue = parseFloat($(this).text().replace("R$", "").replace(".", "").replace(" ", "").replace(",", "."));

        Subtotal += reaisValue;
    });

    $(".subtotal").text(numberToReal(Subtotal));
}

function ChangeMainProductImage() {
    if ($(".img-small-wrap img").length > 0) {
        $(".img-small-wrap img").click(function () {
            var Path = $(this).attr("src");
            $(".img-big-wrap img").attr("src", Path);
            $(".img-big-wrap a").attr("href", Path);
        });
    }
}

function ScrollOrdination() {
    if (window.location.hash.length > 0) {
        var hash = window.location.hash;

        if (hash == "#productPosition") {
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

function DeleteMask(value) {
    return value.replace(".", "").replace("-", "");
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