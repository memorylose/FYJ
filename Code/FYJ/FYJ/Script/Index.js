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