$(document).ready(function () {
    $("#loadingImg").hide();
    $("#fileButton").click(function () {
        var files = $("#fileInput").get(0).files;
        var fileData = new FormData();
        for (var i = 0; i < files.length; i++) {
            fileData.append("fileInput", files[i]);
        }
        $.ajax({
            type: "POST",
            url: "/Admin/AjaxFileUpload/UploadFiles",
            dataType: "json",
            contentType: false,
            // Not to set any content header
            processData: false,
            // Not to process data
            data: fileData,
            success: function (result, status, xhr) {
                console.log(result);
                $("#Image").val(result);
            },
            error: function (xhr, status, error) {
                alert(status);
            }
        });
    });
    $(document).ajaxStart(function () {
        $("#loadingImg").show();
        $("#fileButton").prop('disabled', true);
    });
    $(document).ajaxStop(function () {
        $("#loadingImg").hide();
        $("#fileButton").prop('disabled', false);
        // $("#fileInput").val();
    });
});

function uploadfile() {
    var files = $("#fileInput").get(0).files;
    var fileData = new FormData();
    for (var i = 0; i < files.length; i++) {
        fileData.append("fileInput", files[i]);
    }
    $.ajax({
        type: "POST",
        url: "/Admin/AjaxFileUpload/UploadFiles",
        dataType: "json",
        contentType: false,
        // Not to set any content header
        processData: false,
        // Not to process data
        data: fileData,
        success: function (result, status, xhr) {
            console.log(result);
            $("#Image").val(result);
        },
        error: function (xhr, status, error) {
            alert(status);
        }
    });
}