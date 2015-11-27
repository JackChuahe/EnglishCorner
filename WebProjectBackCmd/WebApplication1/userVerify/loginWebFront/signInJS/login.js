/**
 * Created by JackCai on 26/11/2015.
 */
$(document).ready(function () {
    var error = document.getElementById("Error");
    // 监听Email输入框事件
    $("#email").blur(function () {
        var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        var useremail = document.getElementById("email").value;

        if (filter.test(useremail)) {
            error.innerHTML = "";
            //获取头像开始
            
            $.get("../login/getHeadImg.ashx", { userEmail: $("#email").val() }, function (imgUrl) {
                //alert(imgUrl);
                if (imgUrl.length > 0) {
                   // alert(imgUrl);
                    var img = document.getElementById("headImg");
                   // alert(img.src);
                    img.src = imgUrl;
                }
            });
        } else {

            error.innerHTML = "Email format error";
        }
    });

    // 登录
    $("#sign_In").on("click", function () {

        var pwd = document.getElementById("password").value;
        var email = document.getElementById("email").value;
        var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (filter.test(email)) {
            // ..
        } else {
            // 如果邮箱格式不正确
            error.innerHTML = "Email format error";
            return false;
        }

        if (pwd.length > 0) {
            // 访问后台 token
            $.get("../login/setServerToken.ashx", {}, function (token) {
                var tempPwd = token + pwd;
                //base64 编码
                var encodePwd = $.base64.btoa(tempPwd);

                // 向后台传入用户名和密码
                $.post("../login/verifyUserLogin.ashx", { userEmail: email, password: encodePwd }, function (isRight) {
                    //alert(isRight);
                    if (isRight == 'True') {

                        // 登录成功！
                        //... 跳转到另外的页面

                    } else {
                        alert("Wrong password or Email !Please check!");
                    }
                });
                //alert(encode);

            });

        } else {
            //如果密码为空
            error.innerHTML = "password can not be null";
        }

    });
    
});