/**
 * Created by qdhlo on 2015/12/6.
 */


$(function ($) {
    //弹出
    $("#addPic").hover(function () {
        $(this).stop().animate({
            opacity: '1'
        }, 600);
    }, function () {
        $(this).stop().animate({
            opacity: '0.6'
        }, 1000);
    }).on('click', function () {
        //$("body").append("<a id='mask' href='#' title='close this dialog'></a>");
        $("#mask").addClass("mask").fadeIn("slow");
        $("#addImg").fadeIn("slow");
    });

    $("#mask").on('click', function () {
        $("#addImg").fadeOut("slow");
        $("#mask").fadeOut("slow");
        return false;
    });

   /* $("#close_btn").on('click', function () {
        $("#addImg").fadeOut("slow");
        $("#mask").fadeOut("slow");
        return false;
    });*/

    $("#file-3").fileinput({
        showUpload: true,
        showCaption: false,
        uploadUrl: '../fillAllInfomation/reciveHeadImg.ashx',
        browseClass: "btn btn-primary btn-lg",
        fileType: "image",
        previewFileIcon: "<i class='glyphicon glyphicon-king'></i>"
    });

    $(document).ready(function () {
        $("#test-upload").fileinput({
            'showPreview': false,
            'allowedFileExtensions': ['jpg', 'png', 'gif'],
            'elErrorContainer': '#errorBlock'
        });
    });

    /*-------------------------鼠标左键拖动---------------------*/
    /*--------当不需要实现此功能时，可以将这一部分代码删除------------*/
    var objDiv = document.getElementById("addImg");
    var isIE = document.all ? true : false;//判断浏览器类型
    document.onmousedown = function(evnt) {//当鼠标左键按下后执行此函数
        var evnt = evnt ? evnt : event;
        if (evnt.button == (document.all ? 1 : 0)) {
            mouseD = true;//mouseD为鼠标左键状态标志，为true时表示左键被按下
        }
    }

    objDiv.onmousedown = function(evnt) {
        objDrag = this;//objDrag为拖动的对象
        var evnt = evnt ? evnt : event;
        if (evnt.button == (document.all ? 1 : 0)) {
            mx = evnt.clientX;
            my = evnt.clientY;
            objDiv.style.left = objDiv.offsetLeft + "px";
            objDiv.style.top = objDiv.offsetTop + "px";
            if (isIE) {
                objDiv.setCapture();
//objDiv.filters.alpha.opacity = 50;//当鼠标按下后透明度改变
            } else {
                window.captureEvents(Event.MOUSEMOVE);//捕获鼠标拖动事件
//objDiv.style.opacity = 0.5;//当鼠标按下后透明度改变
            }
        }
    }
    document.onmouseup = function() {
        mouseD = false;//左键松开
        objDrag = "";
        if (isIE) {
            objDiv.releaseCapture();
//objDiv.filters.alpha.opacity = 100;//当鼠标左键松开后透明度改变
        } else {
            window.releaseEvents(objDiv.MOUSEMOVE);//释放鼠标拖动事件
//objDiv.style.opacity = 1;//当鼠标左键松开后透明度改变
        }
    }

    document.onmousemove = function(evnt) {
        var evnt = evnt ? evnt : event;
        if (mouseD == true && objDrag) {
            var mrx = evnt.clientX - mx;
            var mry = evnt.clientY - my;
            objDiv.style.left = parseInt(objDiv.style.left) + mrx + "px";
            objDiv.style.top = parseInt(objDiv.style.top) + mry + "px";
            mx = evnt.clientX;
            my = evnt.clientY;
        }
    }
});
