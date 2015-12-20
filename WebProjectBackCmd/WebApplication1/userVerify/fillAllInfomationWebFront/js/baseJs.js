
//用户完善和修改信息的界面
// 蔡何 2015.12.18
$(document).ready(function () {
    //先对用户的submit事件进行监听
    $("#submit").on("click", function () {
        var  monthArray = new Array(0,31,28,31,30,31,30,31,31,30,31,30,31);
        //判断时间是否合适
        var v_gender = $("#gender").val();
        var v_year = $("#year").val();
        var v_month = $("#month").val();
        var v_day = $("#day").val();


        if (v_year >= 1970 && v_year <= 2015) {
            if ((v_year % 4 == 0 && v_year % 100 != 0) || v_year % 400 == 0) {
                monthArray[2] = 29;
            } else {
                monthArray[2] = 28;
            }
            if (v_month >= 1 && v_month <= 12) {
                if (v_day <= monthArray[v_month] && v_day >= 1) {
                    //天数填写正确
                } else {
                    //天数填写错误
                    alert("Day Wrong !");

                }
            } else {
                //月份填写错误
                alert("Month Wrong !");
                return false;
            }
           // alert(monthArray);

        } else {
            //年份填写错误
            alert("Year Wrong !");
            return false;
        }
        var v_birthday = v_year + "-" + v_month + "-" + v_day;
        //alert(birthday);
       // alert( gender+"  " +year + "  " + month + "  " + day);
        // alert("OK");
        $("#submitLoading").fadeIn("fast");
        $("#mask2").fadeIn("fast");
        $.post("../fillAllInfomation/setProfileInfo.ashx", {birthday:v_birthday,gender:v_gender}, function (isOK) {
         //   alert("s");
            if (isOK == "True") {
                //更新成功!跳转至welcome页面
               // alert("Success to update Information");
                document.getElementById("imge_attention").src = "../../common/image/V.png";
                $("#attention").html("submit success!");
                setTimeout(function () {
                    $("#mask2").fadeOut("fast");
                    $("#submitLoading").fadeOut("fast");
                    window.location = "../../communityConversation/communitySquareWebFront/Communities.html";//跳转
                }, 1500);
               // window.location = "";
            } else {
                //更新资料失败
                //窗口刷新
                document.getElementById("imge_attention").src = "../../common/image/X.png";
                $("#attention").html("failed to update");
                setTimeout(function () {
                    $("#mask2").fadeOut("fast");
                    $("#submitLoading").fadeOut("fast");
                    window.location.reload();//跳转
                }, 1500);
                window.location.reload();//刷新当前页面
            }
        });
    });

});