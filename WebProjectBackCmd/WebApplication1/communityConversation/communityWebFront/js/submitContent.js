$(function ($) {
    $("#submitContentBtn").hover(function () {
        $(this).stop().animate({
            opacity: '1'
        }, 600);
    }, function () {
        $(this).stop().animate({
            opacity: '0.6'
        }, 1000);
    }).on('click', function () {


        var text = $("#sayInput").val();

        if (text.trim().length == 0) {
            //没有书写任何内容
            $("#say_error").html("You can't submit a content with nothing");
            return false;
        }
        //alert(text);
        //首先判断是否有超过字数
        if (text.length > 500) {

            $("#say_error").html("The content no more than 500 characters");
            return false;
        } else {
            $("#say_error").html("");
        }

        //判断是否有中文字符
        if (!verifyChinese(text)) {
            //若有中文
            $("#say_error").html("Invalid to use non-English characters");
            return false;
        }

        //获取当前社区的ID communityID
        var v_communityID = GetQueryString("communityID");

        v_communityID = "cm02";
        //发送到后台
        //$("body").append("");
        $("#mask2").addClass("mask").fadeIn("fast");
        $("#submitLoading").fadeIn("fast");

        $.get("../community/releaseContent.ashx", { communityID: v_communityID, text: text }, function (data) {

            if (data == "False") {
                //alert("error! server error! faild to submit content");
                document.getElementById("imge_attention").src = "../../common/image/X.png";
                $("#attention").html("comit failed!");

            } else {

            }
            $("#sayInput").val("");
            document.getElementById("imge_attention").src = "../../common/image/V.png";
            $("#attention").html("submit success!");
            setTimeout(function () {
                $("#mask2").fadeOut("fast");
                $("#submitLoading").fadeOut("fast");
                window.location.reload();//刷新当前页面
            }, 3000);

            //alert("ok");
        });

    });

    //监听input artcle 点击事件
    $("#sayInput").on("click", function () {
        $("#say_error").html("");
    });

    // 提取get方法的内容 获取communityID 
    function GetQueryString(name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return unescape(r[2]); return null;
    }


    //验证是否是中文
    function verifyChinese(data) {
        var filter = /[u4e00-u9fa5]/;
        return (filter.test(data));

    };

});

/*
$(document).ready(function () {
    $("#sayInput").on("click", function () {
        $("#say_error").html("");
    });


    //当用户点击提交按钮后
    $("#submitContentBtn").on("click", function () {
        var text = $("#sayInput").val();

        if (text.trim().length == 0) {
            //没有书写任何内容
            $("#say_error").html("You can't submit a content with nothing");
            return false;
        }
        //alert(text);
        //首先判断是否有超过字数
        if (text.length > 500) {

            $("#say_error").html("The content no more than 500 characters");
            return false;
        } else {
            $("#say_error").html("");
        }

        //判断是否有中文字符
        if (!verifyChinese(text)) {
            //若有中文
            $("#say_error").html("Invalid to use non-English characters");
            return false;
        }

        //获取当前社区的ID communityID
        var v_communityID = GetQueryString("communityID");

        v_communityID = "cm02";
        //发送到后台
        $.get("../community/releaseContent.ashx", { communityID: v_communityID, text: text }, function (data) {
            if (data == "False") {
                alert("error! server error! faild to submit content");
            }
            window.location = "Community.html";

           
        });
    });


    // 提取get方法的内容 获取communityID 
    function GetQueryString(name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return unescape(r[2]); return null;
    }


    //验证是否是中文
    function verifyChinese(data) {
        var filter = /[u4e00-u9fa5]/;
        return (filter.test(data));

    };

});*/