$(document).ready(function () {
    ScrollOrdination();
    ChangeOrdination();
    ChangeMainProductImage();
    ChangeAmountProductKart();
    ChangeUnitaryProductAmount();
});

function ChangeAmountProductKart() {
    $("#order .btn-secondary").click(function () {
        var father = $(this).parent().parent();
        if ($(this).hasClass("btn-less")) {
            ChangeUnitaryProductAmount("decrease", $(this));
            //var id = father.find(".input-product-id").val();
        };

        if ($(this).hasClass("btn-more")) {
            ChangeUnitaryProductAmount("increase", $(this));
        };
    });
}

function ChangeUnitaryProductAmount(Operation, Button) {

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
};

function ChangeOrdination() {
    $("#ordinationList").change(function () {
        var Page = 1;
        var Search = "";
        var Ordination = $(this).val();
        var Fragment = "#productPosition";

        var QueryString = new URLSearchParams(window.location.search);

        if (QueryString.has("page"))
        {
            Page = QueryString.get("page");
        }

        if (QueryString.has("search"))
        {
            Search = QueryString.get("search");
        }

        if ($("#breadcrumb").length > 0)
        {
            Fragment = "";
        }

        var URL = window.location.protocol + "//" + window.location.host + window.location.pathname;

        var URLWithParameters = URL + "?page=" + Page + "&search=" + Search + "&ordination=" + Ordination + Fragment;

        window.location.href = URLWithParameters;
    });

    
};