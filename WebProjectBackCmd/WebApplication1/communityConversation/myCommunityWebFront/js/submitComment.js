

// 提交评论
function submitComment(contentID) {

    ////////////////////////////////////////////
    var textID = "#newComment_" + contentID;
    var commentContent = $(textID).val();
    var errorWarning = "commentError_" + contentID;  // 获取错误输出位置

    // 首先判断此人是否登录过
    $.get("../../common/HttpRequest/getHeadImgAndUserEmail.ashx", {}, function (data) {
        if (data.length > 0) {
            //登录过
            var jsonValue = jQuery.parseJSON(data);
            if (jsonValue.isLogin == "true") {
                //登录过的
                if (commentContent.trim().length > 0)  // 若长度大于0
                {
                    //判断是否有中文字符
                    if (verifyChinese(commentContent.trim())) {//若没有中文字符 就开始向后台发送数据
                        var date = new Date();
                        var insertContent = "<div class=\"line\"></div><div class=\"oldComments\"><div class=\"oldimg\"><a href=\"#\">" +
                            "<img src=\"" + jsonValue.headImgUrl + "\"></a></div>" +
                            "<div class=\"oldname\">" +
                            "<a href=\"#\">" + jsonValue.last_name + "</a><span>   " + date.getFullYear() + "-" + date.getMonth() + "-0" + date.getDay() + " " + date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds() + "</br>" + commentContent + "</span></div>" +
                            "<div class=\"clear\"></div></div>" + "<div style=\"height: 13px\"></div>";
                        //$("#" + contentID).after(insertContent);
                        //$("#" + contentID).append(insertContent);
                        $("#commentDiv_" + contentID).append(insertContent);
                        document.getElementById(errorWarning).innerHTML = "";


                        //向后台传入数据
                        $.get("../community/submitComment.ashx", { v_contentID: contentID, text: commentContent }, function (isOK) {
                          alert(isOK);
                        });
                    } else {
                        // 有中文字符的情况
                        document.getElementById(errorWarning).innerHTML = "  Invalid to use non-English !";
                    }
                }
                $(textID).val("");
                //alert(commentContent);
            } else {
                //没登录过
                $(textID).val("");
                alert("You didn't SignIn,Please SignIn.");
                return false;
            }
        } else {
            //没登录过
            $(textID).val("");
            alert("You didn't SignIn,Please SignIn.");
            return false;
      
        }
    });




    

};

//验证是否是中文
function verifyChinese(data) {
    var filter = /[u4e00-u9fa5]/;
    return (filter.test(data));

};