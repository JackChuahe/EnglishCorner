
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
        var birthday = v_year + "-" + v_month + "-" + v_day;
        //alert(birthday);
       // alert( gender+"  " +year + "  " + month + "  " + day);
      
        $.post("../fillAllInfomation/setProfileInfo.ashx", { gender: v_gender, birthday: v_birthday }, function (isOK) {
            if (isOK == "True") {
                //更新成功!跳转至welcome页面
                alert("Success to update Information");
               // window.location = "";
            } else {
                //更新资料失败
                //窗口刷新
                alert("failed to update Information");
                window.location.reload();//刷新当前页面
            }
        });
    });

});