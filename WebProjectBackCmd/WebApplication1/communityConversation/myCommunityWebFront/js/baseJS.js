
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

    // 判断是否有头像和用户名  判断用户是否有登录 若没有登录 则跳转到社区广场界面
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
                    //window.location = "Verify.html?userEmail=" + $("#email").val();
                }
            } else {
                //没有头像 隐藏头像div
                $("#div_headImg_User").hide();
                //window.location = "Verify.html?userEmail=" + $("#email").val();
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
    // 提取相关信息
   


    // 获取该社区的所有content数据
    
    $.get("../myCommunity/getUserCommunityActiveAndComment.ashx", {  }, function (allContentsAndComments) {
        //alert(allContentsAndComments);
        var contents = jQuery.parseJSON(allContentsAndComments).contents;
        //alert(json.contents.length);
        //alert(json.contents[0].contentID);
       // var comments = contents[0].comments;
        // alert(comments.length);
        //每一个评论错误的的字段的 ID 为 commentError_ + contentID
        //每一个评论输入框的ID 为 newComment_ + contentID
        //每一个按钮的comment 的id 为 contentID
        // 每一个评论的DIV 的ID 为 commentDiv_ + contentID
        var appentContent = "";
        for (var i = 0 ; i < contents.length ; i++) {
            appentContent += "<div class=\"box\"><div class=\"contentTitle\"><a id=\"cUserHead\" href=\"#\"><div class=\"imgHead\">" +
        "<img src=\"" + contents[i].submitUserHeadImg + "\">" +
         "</div></a><a id=\"cUserName\" href=\"#\">" +
         "<div class=\"name\">" + contents[i].submitUserName + "</div>" +
          "</a><div id=\"sTime\" class=\"time\">" + contents[i].submitTime + "</div>" +
          "</div><div class=\"clear\"></div><div class=\"content\"><div class=\"article\">" +
          contents[i].text + "</div><div class=\"line\"></div><div class=\"image\">" +
          "<img src=\"" + contents[i].textPicUrl + "\"></div></div>" +
         "<div class=\"comment\" id=\"commentDiv_"+contents[i].contentID+"\"><div class=\"plusArea\"><div class=\"plus\">+1</div></div>" +
         "<div class=\"line\"></div><textarea id=\"newComment_" + contents[i].contentID + "\" class=\"commentArea\" placeholder=\"what new thing to share?\"></textarea>" +
          "<div class=\"error\" id=\"commentError_"+contents[i].contentID+ "\"></div>" +
         "<a  onclick=\"javascript:submitComment(this.id)\" class=\"commentbtn\" id=\"" + contents[i].contentID + "\">" +
          "<span>comment</span></a>";

            //
            var comments = contents[i].comments;
            //评论
            for (var j = 0; j < comments.length ; j++)
            {
                appentContent += "<div class=\"line\"></div><div class=\"oldComments\"><div class=\"oldimg\"><a href=\"#\">" +
                "<img src=\"" + comments[j].commentUserHeadImg + "\"></a></div>" +
                "<div class=\"oldname\">" +
                "<a href=\"#\">" + comments[j].commentUserName + "</a><span>   " + comments[j].commentSubmitTime +"</br>"+ comments[j].commentContent + "</span></div>" +
                "<div class=\"clear\"></div></div>" + "<div style=\"height: 13px\"></div>";
            }

            // 整个content div结束部分
            //<div style=\"height: 10px\"></div>
            appentContent += "</div></div></div>";
            
            var leftHeight = $("#leftView").outerHeight(true);
            var rightHeight = $("#rightView").outerHeight(true);
            //alert("right" + rightHeight);
            //alert("left" + leftHeight);

            if (leftHeight < rightHeight)
            {
                $("#leftView").append(appentContent);
            } else {
                $("#rightView").append(appentContent);
            }
           
            appentContent = "";

        }

        /////////////////////////////////////////////
     /*   "<div class=\"box\"><div class=\"contentTitle\"><a id=\"cUserHead\" href=\"#\"><div class=\"imgHead\">"+
        "<img src=\""+ submitUserHeadImg + "\">"+
         "</div></a><a id=\"cUserName\" href=\"#\">"+
         "div class=\"name\">"+ submitUserName +"</div>"+
          "</a><div id=\"sTime\" class=\"time\">by"+ userSubmitTime +"</div>"+
          "</div><div class=\"clear\"></div><div class=\"content\"><div class=\"article\">"+
          submitContent + "</div><div class=\"line\"></div><div class=\"image\">"+
          "<img src=\""+contentPicUrl+"\"></div></div>"+
         "<div class=\"comment\"><div class=\"plusArea\"><div class=\"plus\">+1</div></div>"+
         "<div class=\"line\"></div><textarea id=\"newComment_"+ contentID + "\" class=\"commentArea\" placeholder=\"what new thing to share?\"></textarea>"+
          "<div class=\"error\">error</div>" +
         "<a  href=\"#\" class=\"commentbtn\" id=\""+contentID+"\">"+
          "<span>comment</span></a>";

        //评论
        "<div class=\"line\"></div><div class=\"oldComments\"><div class=\"oldimg\"><a href=\"#\">"+
        "<img src=\""+ commentUserHeadImg + "\"></a></div>"+
        "<div class=\"oldname\">"+
        "<a href=\"#\">" + commentUserName + "</a><span>"+ commentContent+"</span></div>"+
        "<div class=\"clear\"></div></div>";


        // 整个content div结束部分
        "<div style=\"height: 10px\"></div></div></div>";*/


    });

});