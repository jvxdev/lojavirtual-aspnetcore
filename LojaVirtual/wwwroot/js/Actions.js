﻿$(document).ready(function () {
    $(".btn-danger").click(function (e) {
        var resultado = confirm("Tem certeza que deseja realizar essa operação?");
        if (resultado == false) {

        e.preventDefault();
        }
    });
        $('.money').mask('000.000.000.000.000,00', { reverse: true });
        $('#cpf').mask('000.000.000-00');
        $('#phone').mask('(00) 00000-0000');

    AjaxUploadProductImage();
});

function AjaxUploadProductImage() {
    $(".img-upload").click(function () {
        $(this).parent().find(".input-upload").click();
    });

    $(".btn-delete-image").click(function () {
        var hiddenInput = $(this).parent().find("input[name=image]");
        var Image = $(this).parent().find(".img-upload");
        var btnDelete = $(this).parent().find(".btn-delete-image");
        var inputUpload = $(this).parent().find(".input-upload");

        $.ajax({
            type: "GET",
            url: "/Collaborator/Image/Delete?path=" + hiddenInput.val(),
            error: function () {
            },
            success: function () {
                Image.attr("src", "/img/img-example.png");
                btnDelete.addClass("btn-hidden");
                hiddenInput.val("");
                inputUpload.val("");
            }
        })
    });

    $(".input-upload").change(function () {
        var Binary = $(this)[0].files[0];
        var Form = new FormData();
        Form.append("file", Binary);

        var hiddenInput = $(this).parent().find("input[name=image]");
        var Image = $(this).parent().find(".img-upload");
        var btnDelete = $(this).parent().find(".btn-delete-image");


        //Loading IMG
        Image.attr("src", "/img/loading.gif")

        //AJAX Request
        $.ajax({
            type: "POST",
            url: "/Collaborator/Image/Upload",
            data: Form,
            contentType: false,
            processData: false,
            error: function () {
                alert("Sinto muito. Algo de errado aconteceu.");
                Image.attr("src", "/img/img-example.png");
            },
            success: function (data) {
                var path = data.path;

                Image.attr("src", path);
                hiddenInput.val(path);
                btnDelete.removeClass("btn-hidden");
            }
        });
    });
}