

//处理用户点击fellow事件
function fellowToComnunity(communityID) {
    //alert(communityID);
    //先判断是否有登录
    $.get("../../common/HttpRequest/getHeadImgAndUserEmail.ashx", {}, function (data) {
        if (data.length > 0) {
            //有登录
            var jsonValue = jQuery.parseJSON(data);
            if (jsonValue.isLogin == "true") {

            } else {
                // 未登录的情况
                window.location = "";

            }
        } else {
            //没有头像 隐藏头像div
            $("#div_headImg_User").hide();
        }
    });

};