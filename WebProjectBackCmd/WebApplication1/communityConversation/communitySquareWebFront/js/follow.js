﻿


//处理用户点击fellow事件
function fellowToComnunity(v_communityID) {
    //alert(communityID);
    //先判断是否有登录
    $("#mask2").addClass("mask").fadeIn("fast");
    $("#submitLoading").fadeIn("fast");

    $.get("../../common/HttpRequest/getHeadImgAndUserEmail.ashx", {}, function (data) {
        if (data.length > 0) {
            //有登录
            var jsonValue = jQuery.parseJSON(data);
            if (jsonValue.isLogin == "true") {
                //有登录
                
                $.post("../communitySquare/fellowCommunity.ashx", { communityID: v_communityID }, function (isOK) {
                    //alert(isOK);
                    //alert(isOK);
                    if (isOK == '1') {
                        // 加入成功
                        //跳转到相应的社区去
                        document.getElementById("imge_attention").src = "../../common/image/V.png";
                        $("#attention").html("Fellow success!");
                        setTimeout(function () {
                            $("#mask2").fadeOut("fast");
                            $("#submitLoading").fadeOut("fast");
                            window.location = "../communityWebFront/Community.html?communityID="+v_communityID;
                        }, 1000);
                    } else {
                        //操作失败
                        document.getElementById("imge_attention").src = "../../common/image/X.png";
                        $("#attention").html("You Have Fellowed!");
                        setTimeout(function () {
                            $("#mask2").fadeOut("fast");
                            $("#submitLoading").fadeOut("fast");
                        }, 1000);
                    }
                });
            } else {
                // 未登录的情况
                window.location = "../../userVerify/loginWebFront/SignIn.html";

            }
        } else {
            window.location = "../../userVerify/loginWebFront/SignIn.html";
           //未登录的情况
        }
    });

};