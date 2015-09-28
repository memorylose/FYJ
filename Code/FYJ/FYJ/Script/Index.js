function UploadChange(value, files) {
    var objUrl = getObjectURL(files);
    console.log("objUrl = " + objUrl);
    if (objUrl) {
        $('#imageList').append('<div style="background-image:url(\'' + objUrl + '\');" class="user-up-img"><img src="/Image/user_close.png" class="user-image-icon" onclick="CloseUserImg(this)" /></div>');
    }
}
function getObjectURL(file) {
    var url = null;
    if (window.createObjectURL != undefined) { // basic
        url = window.createObjectURL(file);
    } else if (window.URL != undefined) { // mozilla(firefox)
        url = window.URL.createObjectURL(file);
    } else if (window.webkitURL != undefined) { // webkit or chrome
        url = window.webkitURL.createObjectURL(file);
    }
    return url;
}
function CloseUserImg(obj) {
    $(obj).parent().hide();
}
function Submit() {
    //$(".user-up-img").each(function () {
    //    alert($(this).css("background-image"));
    //});
    $.ajax({
        url: '../AjaxHandler/ArticleHandler.ashx',
        type: 'post',
        data: 'Method=AddArticle',
        async: false, //默认为true 异步   
        error: function () {
        },
        success: function (data) {
            $("#" + divs).html(data);
        }
    });
}
var index = 1;
function AddFile() {
    var ul = document.getElementById("FileList");
    var inputDiv = document.createElement("div");
    inputDiv.setAttribute("Id", "div" + index);
    var file = document.createElement("input");
    file.setAttribute("type", "file");
    file.setAttribute("id", "file" + index);
    file.setAttribute("name", "file" + index);
    file.setAttribute("onchange", "UploadChange(this.value, this.files[0])");

    var btnDel = document.createElement("input");
    btnDel.setAttribute("type", "button");
    btnDel.setAttribute("value", "删除");
    btnDel.setAttribute("Id", index);
    btnDel.onclick = function () {
        inputDiv.removeChild(file);
        inputDiv.removeChild(btnDel);
        ul.removeChild(inputDiv);
    }
    inputDiv.appendChild(file);
    inputDiv.appendChild(btnDel);
    ul.appendChild(inputDiv);

    $('#file' + index).click();
    index++;
}