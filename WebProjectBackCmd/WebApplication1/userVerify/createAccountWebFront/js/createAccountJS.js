$(document).ready(function () {

    // 清空所有的数据
    function init() {
        document.getElementById("nameInputError").innerHTML = "";
        document.getElementById("nameInputError").innerHTML = "";
        document.getElementById("emailInputError").innerHTML = "";
        document.getElementById("passwordInputError").innerHTML = "";
        document.getElementById("captchaError").innerHTML = "";
        document.getElementById("confirmPasswordInputError").innerHTML = "";
        document.getElementById("password").value = "";
        document.getElementById("ConfirmPassword").value = "";
    };

    // 改变验证码
    function changePic(){
        var myDate = new Date();
        document.getElementById("confirmPic").src = "../createAccount/CAPTCHA.ashx?" + "td=" + myDate.getMilliseconds();
    };
  
    //验证是否是中文
    function verifyChinese(data) {
        var filter = /[u4e00-u9fa5]/;
        return (filter.test(data));
        
    };

    // 判断是否有头像和用户名
    $.get("../../common/HttpRequest/getHeadImgAndUserEmail.ashx", {}, function (data) {
        if (data.length > 0) {
            // 有头像
            //加载头像
            var jsonValue = jQuery.parseJSON(data);
            if (jsonValue.isLogin == "true") {
                $("#div_signIn").hide();
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

    changePic();

    var isEmailExists = false;
 
    //监听当email输入框失去焦点的时候且不为空
    $("#email").blur(function () {   
        //进行邮箱校验
        if ($("#email").val().length > 0){ // 如果输入框中的值不为null
        var email = document.getElementById("email").value;
        var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            //检测是否有中文
        if (!verifyChinese($("#email").val())) {
            //邮箱有中文的情况
            document.getElementById("emailInputError").innerHTML = "Only English Or Number!";
            return false;
        }
        if (filter.test(email)) {
            //alert("是邮箱格式");
            //访问后台
            document.getElementById("emailInputError").innerHTML = "";
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
                    //alert("该邮箱已经存在");
                    document.getElementById("emailInputError").innerHTML = "The Email has already exists";
                }
            });
        } else {
            // 提示用户这不是邮箱格式
            // alert("不是邮箱格式");
            document.getElementById("emailInputError").innerHTML = "Wrong Email Format";
        }
        }
    });

    // 当firstname姓名输入消失焦点的时候
    $("#FirstName").blur(function () {

        document.getElementById("nameInputError").innerHTML = "";
        if ($("#FirstName").val().length > 0) {
            if (verifyChinese($("#FirstName").val())) {
                //如果没有中文
            }else{
                //如果有中文
                document.getElementById("nameInputError").innerHTML = "Only English Or Number !";
            }
        }
    });

    //当lastname输入失去焦点
    $("#LastName").blur(function () {
        document.getElementById("nameInputError").innerHTML = "";
        if ($("#LastName").val().length > 0) {
            if (verifyChinese($("#LastName").val())) {
                //如果没有中文
            } else {
                //如果有中文
                document.getElementById("nameInputError").innerHTML = "Only English Or Number !";
            }
        }
    });
    //当第一个密码框失去焦点的时候
    $("#password").blur(function () {
        document.getElementById("passwordInputError").innerHTML = "";
    });
    //当第二个密码框失去焦点的时候
    $("#ConfirmPassword").blur(function () {
        document.getElementById("confirmPasswordInputError").innerHTML = "";
    });
    //当验证码框失去焦点的时候
    $("#confirmCode").blur(function () {
        document.getElementById("captchaError").innerHTML = "";
    });
    //当点击登陆按钮后
    $("#Next").on("click", function () {
        //检查是否有未填写的数据
        var flag = true;
        if (document.getElementById("FirstName").value.length == 0) {
            // 提示用户 不能缺少某项内容
            document.getElementById("nameInputError").innerHTML = "name can't be null";
            flag = false;;
        }
        if (document.getElementById("LastName").value.length == 0) {
            document.getElementById("nameInputError").innerHTML = "name can't be null";
            flag = false;
        }
        if (document.getElementById("email").value.length == 0) {
            document.getElementById("emailInputError").innerHTML = "Email can't be null";
            flag = false;
        }
        if (document.getElementById("password").value.length == 0) {
            document.getElementById("passwordInputError").innerHTML = "password can't be null";
            flag = false;
        }
        if (document.getElementById("ConfirmPassword").value.length == 0){
            document.getElementById("confirmPasswordInputError").innerHTML = "password Confirm can't be null";
            flag = false;
        }
        if (document.getElementById("confirmCode").value.length == 0) {
            document.getElementById("captchaError").innerHTML = "verifidity code can't be null";
            flag = false;
        }

        // 验证邮箱是否有中文字符
        if (!verifyChinese($("#email").val())) {

            //邮箱有中文的情况
            document.getElementById("emailInputError").innerHTML = "Only English Or Number!";
            return false;
        }
        if (!flag) return false;
        //检验邮箱是否符合格式规范
        var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (filter.test(document.getElementById("email").value)) {
            //alert("email right");
            //邮箱正确的情况下
        } else {
            // 提示用户 邮箱格式错误
            // 邮箱不正确
            document.getElementById("emailInputError").innerHTML = "Wrong Email Format";
            return false;
        }
        // 判断邮箱是否已经存在了
        if (isEmailExists) {
            document.getElementById("emailInputError").innerHTML = "The Email has already exists!";
            return false;
        }
        // 检验用户两次密码输入的一致性
        if (document.getElementById("password").value == document.getElementById("ConfirmPassword").value) {
            // 两次密码相等
           // alert("password equal");
        } else {
            // 两次密码不相等
            // 提示用户密码两次不相等
            document.getElementById("confirmPasswordInputError").innerHTML = "The two passwords are not equal";
            return false;
        }

        //验证验证码是否输入正确
        $.get("../createAccount/getCaptchValue.ashx", {}, function (captcha) {

            if (document.getElementById("confirmCode").value != captcha) {
                // 验证码不正确正确
                document.getElementById("captchaError").innerHTML = "verifidity code Wrong!";
                changePic();
                return false;
            } else {
                //在验证码正确的情况下才进行服务器访问
                //准备为密码加token和加token
                $.get("../createAccount/setServerToken.ashx", {}, function (token) {
                    // 向服务器插入数据
                    //alert(token);
                    var tempPwd = token + $("#password").val();
                    var rawPwd = $.base64.btoa(tempPwd);
                    $.post("../createAccount/createTheAccount.ashx", { first_name: $("#FirstName").val(), last_name: $("#LastName").val(), userEmail: $("#email").val(), pwd: rawPwd }, function (isInsertOK) {
                        //alert(isInsertOK);
                        if (isInsertOK == "True") {
                            window.location = "Verify.html?userEmail=" + $("#email").val();
                            // 插入成功!下面进行下一步 激活页面
                        } else {
                            //插入失败
                            alert("Create Account Failed ! Please try again !");
                            init();
                            changePic();
                        }
                    });

                });
            }
        });

       
    });

    });
