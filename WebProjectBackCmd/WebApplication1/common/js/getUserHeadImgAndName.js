$(document).ready(function () {
    // 判断是否有头像和用户名
    function getHeaderImg() {
        $.get("../../common/HttpRequest/getHeadImgAndUserEmail.ashx", {}, function (data) {
            if (data.length > 0) {
                // 有头像
                //加载头像
                var jsonValue = jQuery.parseJSON(data);
                if (jsonValue.isLogin == "true") {

                    $("#div_signIn").hide();
                    $("#div_headImg_User").show();
                    document.getElementById("login_headImg").src = jsonValue.headImgUrl;
                    document.getElementById("login_name").innerHTML = jsonValue.last_name;
                } else {
                    // 未登录的情况
                    $("#div_headImg_User").hide();
                }
            } else {
                //没有头像 隐藏头像div
                $("#div_headImg_User").hide();
            }
        });

    };
    //获取用户头像 没有就显示登录button
    getHeaderImg();
});