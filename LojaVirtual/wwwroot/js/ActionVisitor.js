$(document).ready(function () {
    ChangeOrdination();
    ScrollOrdination();
});

function ScrollOrdination() {
    if (window.location.hash.length > 0)
    {
        var hash = window.location.hash;

        if (hash == "#productPosition")
        {
            window.scrollBy(0, 550);
        }
    }
};

function ChangeOrdination() {
    $("#ordinationList").change(function () {
        var Page = 1;
        var Search = "";
        var Ordination = $(this).val();

        var QueryString = new URLSearchParams(window.location.search);

        if (QueryString.has("page"))
        {
            Page = QueryString.get("page");
        }

        if (QueryString.has("search"))
        {
            Search = QueryString.get("search");
        }

        var URL = window.location.protocol + "//" + window.location.host + window.location.pathname;

        var URLWithParameters = URL + "?page=" + Page + "&search=" + Search + "&ordination=" + Ordination + "#productPosition";

        window.location.href = URLWithParameters;
    });
};