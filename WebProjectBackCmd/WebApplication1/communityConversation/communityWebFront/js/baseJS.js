
// 蔡何  2015.12.3
$(document).ready(function () {
    // 获取社区基本信息
    $.get("../community/getCommunityInfo.ashx", { communityID: "cm01" }, function (cInfo) {
        var json = jQuery.parseJSON(cInfo);
        document.getElementById("title").innerHTML = json.communityName;
        document.getElementById("article").innerHTML = json.communityDesc;
        document.getElementById("pic").src = json.headImgUrl;
       // alert(json.headImgUrl);
        //alert(json.communityName + "   " + json.communityDesc + "  " + json.headImgUrl);
    });

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

    // 获取这个社区的用户头像
    $.get("../community/getCommunityUsersHeadImg.ashx", { communityID: "cm01" }, function (cInfo) {
        var json = jQuery.parseJSON(cInfo);
        var div_usersImg = document.getElementById("div_usersImg");
        var tempStr = "";
        for (var i = 0 ; i < json.headImages.length - 1 ; i++) {
           // var tempJson = json.headImages[0].url;
            tempStr += "<div style=\"width: 34px;height: 34px;margin: 3px;float: left\"><img src=\"" + json.headImages[i].url + "\" height=\"34px\" width=\"34px\" style=\"border-radius: 4px\"></div>";
        }
        div_usersImg.innerHTML = tempStr;
    });



    // 提取get方法的内容 获取communityID 
    function GetQueryString(name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return unescape(r[2]); return null;
    }
    // 提取邮箱
    var communityID = GetQueryString("communityID");
    // 获取该社区的所有content数据
    $.get("../community/getCommunityActivesInfoAndComment.ashx");

});