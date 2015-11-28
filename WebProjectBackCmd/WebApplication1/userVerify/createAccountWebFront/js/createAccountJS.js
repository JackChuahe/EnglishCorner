$(document).ready(function () {
    var myDate = new Date();
    var isEmailExists = false;
    document.getElementById("confirmPic").src = "../createAccount/CAPTCHA.ashx?" + "td=" + myDate.getMilliseconds();
    //监听当email输入框失去焦点的时候且不为空
    $("#email").blur(function () {
        //alert("ok");
        //进行邮箱校验
        if ($("#email").val().length > 0){ // 如果输入框中的值不为null
        var email = document.getElementById("email").value;
        var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (filter.test(email)) {
            //alert("是邮箱格式");
            //访问后台
            $.get("../createAccount/isAccountExists.ashx", { userEmail: $("#email").val() }, function (isExists) {
                //alert(isExists);
                if (isExists == 'False') {
                    //该账号还不存在
                    //alert(isExists);
                    isEmailExists = false;
                } else {
                    // 该账号已经存在
                    // 提示用户账号已经存在
                    isEmailExists = true;
                    alert("该邮箱已经存在");
                }
            });
        } else {
            // 提示用户这不是邮箱格式
           // alert("不是邮箱格式");
        }
        }
    });



    //当点击登陆按钮后
    $("#sign_In").on("click", function () {
        //检查是否有未填写的数据
        if (document.getElementById("FirstName").value.length == 0) {
            // 提示用户 不能缺少某项内容
            alert("lack of 1");
            return false;
        }
        if (document.getElementById("LastName").value.length == 0) {
            alert("lack of 2");
            return false;
        }
        if (document.getElementById("email").value.length == 0) {
            alert("lack of 3");
            return false;
        }
        if (document.getElementById("password").value.length == 0) {
            alert("lack of 4");
            return false;
        }
        if (document.getElementById("ConfirmPassword").value.length == 0) {
            alert("lack of 5");
            return false;
        }
        if (document.getElementById("confirmCode").value.length == 0) {
            alert("lack of 6");
            return false;
        }

        //检验邮箱是否符合格式规范
        var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (filter.test(document.getElementById("email").value)) {
            //alert("email right");
            //邮箱正确的情况下
        } else {
            // 提示用户 邮箱格式错误
            // 邮箱不正确
            alert("email wrong");
            return false;
        }
        // 判断邮箱是否已经存在了
        if (isEmailExists) {
            alert("该邮箱已经存在");
            return false;
        }
        // 检验用户两次密码输入的一致性
        if (document.getElementById("password").value == document.getElementById("ConfirmPassword").value) {
            // 两次密码相等
           // alert("password equal");
        } else {
            // 两次密码不相等
            // 提示用户密码两次不相等
            alert("password no equal");
            return false;
        }

        //验证验证码是否输入正确
        
        $.get("../createAccount/getCaptchValue.ashx", { }, function (captcha) {
            //alert(captcha);
            if (document.getElementById("confirmCode").value != captcha) {
                alert("验证码不正确!");
                return false;
            }

            //准备为密码加token和加token
            $.get("../createAccount/setServerToken.ashx", {}, function (token) {
                // 向服务器插入数据
                //alert(token);
                var tempPwd = token + $("#password").val();
                var rawPwd =  $.base64.btoa(tempPwd);
                $.post("../createAccount/createTheAccount.ashx", { first_name: $("#FirstName").val(), last_name: $("#LastName").val(), userEmail: $("#email").val(), pwd: rawPwd }, function (isInsertOK) {
                    alert(isInsertOK);
                });

            });
           
        });

    });
});