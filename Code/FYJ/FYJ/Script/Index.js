﻿function UploadChange(value, files, i_index) {
    var objUrl = getObjectURL(files);
    console.log("objUrl = " + objUrl);
    if (objUrl) {
        $('#imageList').append('<div style="background-image:url(\'' + objUrl + '\');" class="user-up-img" id="imgdiv_' + i_index + '"><img src="/Image/user_close.png" class="user-image-icon" onclick="CloseUserImg(this)" /></div>');
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
    var fileIndex = $(obj).parent().attr('id').substring(7);
    $('#div' + fileIndex).remove();
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
    file.setAttribute("onchange", "UploadChange(this.value, this.files[0]," + index + ")");
    inputDiv.appendChild(file);
    ul.appendChild(inputDiv);
    $('#file' + index).click();
    index++;
}

$(document).ready(function () {
    $.ajax({
        url: 'AjaxHandler.ashx',
        dataType: 'json',
        data: { Method: 'GetBlog', Count: '0' },
        beforeSend: function (XMLHttpRequest) {
            $("#beginLoading").css('display', 'block');
        },
        success: function (msg) {
            $(msg).each(function (i) {
                var divs = '';
                divs += '<div class="row" id="rowId">';
                divs += '<div class="col-md-2 c-img-div"><a href=""><img src="SampleImage/s1.jpg" class="user-img" /></a></div>';
                divs += '<div class="col-md-10 col-pad-left">';
                divs += '<div class="c-img"></div>';
                divs += '<div class="c-div">';
                divs += '<div class="c-title"><a href="" id="txtTitle">' + msg[i].Title + '</a></div>';
                divs += '<div class="row c-content">';
                divs += '<div class="col-md-4">';
                divs += '<a href=""> <img src="SampleImage/s3.png" class="u-img" /></a>';
                divs += '</div>';
                divs += '<div class="col-md-8 word-p">';
                divs += '<a href="" class="content-a" id="txtContent">' + msg[i].Contents + '</a>';
                divs += '</div>';
                divs += '</div>';
                divs += '<div class="c-content-op">';
                divs += '<div class="op-word">2015/08/16</div>';
                divs += '<div class="op-word">来自360浏览器</div>';
                divs += '<a href="" class="op-user">删除</a>';
                divs += '<a href="" class="op-user">编辑</a>';
                divs += '<a href="" class="op-user">评论(10)</a>';
                divs += '<a href="" class="op-user">转发(2)</a>';
                divs += '</div>';
                divs += '</div>';
                divs += '';
                divs += '<div class="c-div-b"></div>';
                divs += '</div>';
                divs += '';
                divs += '</div>';

                $("#a_content").append(divs);
                $('#hdCount').prop('value', '0');
                $("#beginLoading").css('display', 'none');
            });
        },
        complete: function (msg) {
            $("#beginLoading").css('display', 'none');
        }
    });

    $(window).scroll(function () {
        if ($(document).scrollTop() >= $(document).height() - $(window).height()) {
            var count = $('#hdCount').val();
            count = parseInt(count) + 10;

            $.ajax({
                url: 'AjaxHandler.ashx',
                dataType: 'json',
                data: { Method: 'GetBlog', Count: count },
                beforeSend: function (XMLHttpRequest) {
                    $("#divLoading").css('display', 'block');
                    //setTimeout("alert('5 seconds!')", 5000)
                },
                success: function (msg) {
                    $(msg).each(function (i) {
                        var divs = '';
                        divs += '<div class="row" id="rowId">';
                        divs += '<div class="col-md-2 c-img-div"><a href=""><img src="SampleImage/s1.jpg" class="user-img" /></a></div>';
                        divs += '<div class="col-md-10 col-pad-left">';
                        divs += '<div class="c-img"></div>';
                        divs += '<div class="c-div">';
                        divs += '<div class="c-title"><a href="" id="txtTitle">' + msg[i].Title + '</a></div>';
                        divs += '<div class="row c-content">';
                        divs += '<div class="col-md-4">';
                        divs += '<a href=""> <img src="SampleImage/s3.png" class="u-img" /></a>';
                        divs += '</div>';
                        divs += '<div class="col-md-8 word-p">';
                        divs += '<a href="" class="content-a" id="txtContent">' + msg[i].Contents + '</a>';
                        divs += '</div>';
                        divs += '</div>';
                        divs += '<div class="c-content-op">';
                        divs += '<div class="op-word">2015/08/16</div>';
                        divs += '<div class="op-word">来自360浏览器</div>';
                        divs += '<a href="" class="op-user">删除</a>';
                        divs += '<a href="" class="op-user">编辑</a>';
                        divs += '<a href="" class="op-user">评论(10)</a>';
                        divs += '<a href="" class="op-user">转发(2)</a>';
                        divs += '</div>';
                        divs += '</div>';
                        divs += '';
                        divs += '<div class="c-div-b"></div>';
                        divs += '</div>';
                        divs += '';
                        divs += '</div>';

                        $("#testAjax").append(divs);
                        //$(divs).appendTo($("#testAjax"));
                        $('#hdCount').prop('value', count);
                        $("#divLoading").css('display', 'none');
                    });
                },
                complete: function (msg) {
                    // alert('远程调用成功，状态文本值：'+textStatus); 
                    $("#divLoading").css('display', 'none');
                }
            });
        }
    });
});