
// 蔡何  2015.12.3
$(document).ready(function () {

    // 提取communityID
    var v_communityID = GetQueryString("communityID");

    //v_communityID = 'cm02';    // !!!
    // 获取社区基本信息
    $.get("../community/getCommunityInfo.ashx", { communityID: v_communityID }, function (cInfo) {
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
                //已经登录
                $.get("../community/isFollowedThisCommunity.ashx", { communityID: v_communityID }, function (isFollowed) {
                    //判断是否已经加入该社区
                   // alert(isFollowed);
                    if (isFollowed = "True") {
                        // 显示发表动态的框框
                        $("#say").show();
                    } else {
                        $("#say").hide();
                    }

                });


                // 有头像
                //加载头像
                var jsonValue = jQuery.parseJSON(data);
                if (jsonValue.isLogin == "true") {
                    var html = "<a id=\"goToWrite\" class=\"goToWrite\" href=\"javascript:clickToTop()\" title=\"back to write\"></a>";
                    $(".sideBar").append(html); //



                    $("#div_signIn").hide();
                    $("#div_headImg_User").show();
                    document.getElementById("login_headImg").src = jsonValue.headImgUrl;
                    document.getElementById("login_name").innerHTML = jsonValue.last_name;
                } else {
                    // 未登录的情况
                    $("#say").hide();
                    var html = "<a id=\"goToTop\" class=\"goTop\" href=\"javascript:clickToTop()\" title=\"go to top\"><img src=\"../../common/image/right_bg.png\" style=\"margin-top: 5px;margin-left: 5px;width: 35px;height: 35px\"></a>";
                    $(".sideBar").append(html);
                    $("#div_headImg_User").hide();
                }
            } else {
                //没有头像 隐藏头像div
                $("#say").hide();
                var html = "<a id=\"goToTop\" class=\"goTop\" href=\"javascript:clickToTop()\" title=\"go to top\"><img src=\"../../common/image/right_bg.png\" style=\"margin-top: 5px;margin-left: 5px;width: 35px;height: 35px\"></a>";
                $(".sideBar").append(html);
                $("#div_headImg_User").hide();
            }
        });

    };
    //获取用户头像 没有就显示登录button
    getHeaderImg();

    // 获取这个社区的用户头像
    $.get("../community/getCommunityUsersHeadImg.ashx", { communityID: v_communityID }, function (cInfo) {
        var json = jQuery.parseJSON(cInfo);
        var div_usersImg = document.getElementById("div_usersImg");
        var tempStr = "";
        //alert(cInfo);
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


    // 获取该社区的所有content数据
    $.get("../community/getCommunityActivesInfoAndComment.ashx", { communityID: v_communityID }, function (allContentsAndComments) {
        //alert(allContentsAndComments);
        //$("#article").html(allContentsAndComments);
        //alert("ok");
       var contents = jQuery.parseJSON(allContentsAndComments).contents;
       //alert(contents.length);
        //alert(json.contents[0].contentID);
       // var comments = contents[0].comments;
        // alert(comments.length);
        //每一个评论错误的的字段的 ID 为 commentError_ + contentID
        //每一个评论输入框的ID 为 newComment_ + contentID
        //每一个按钮的comment 的id 为 contentID
        // 每一个评论的DIV 的ID 为 commentDiv_ + contentID
        var appentContent = "";
        for (var i = 0 ; i < contents.length ; i++) {
            appentContent += "<div  id=\""+i+"\" class=\"box\"><div class=\"contentTitle\"><a id=\"cUserHead\" href=\"#\"><div class=\"imgHead\">" +
        "<img src=\"" + contents[i].submitUserHeadImg + "\">" +
         "</div></a><span id=\"cUserName\" >" +
         "<div class=\"name\">" + contents[i].submitUserName + "</div>" +
          "</span><div id=\"sTime\" class=\"time\">" + contents[i].submitTime + "</div>" +
          "</div><div class=\"clear\"></div><div class=\"content\"><div class=\"article\">" +
          contents[i].text + "</div><div class=\"line\"></div><div class=\"image\">" +
          "<img src=\"" + contents[i].textPicUrl + "\"></div></div>" +
         "<div class=\"comment\" id=\"commentDiv_"+contents[i].contentID+"\"><div class=\"plusArea\"><div class=\"plus\">+1</div></div>" +
         "<div class=\"line\"></div><textarea id=\"newComment_" + contents[i].contentID + "\" class=\"commentArea\" placeholder=\"what new thing to share?\"></textarea>" +
          "<button onclick=\"javascript:submitComment(this.id)\" class=\"commentbtn\" id=\""+contents[i].contentID+"\"><span>comment</span></button><div class=\"error\" id=\"commentError_" + contents[i].contentID + "\"></div>" ;

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
            
            var leftHeight = $(".leftView").outerHeight(true);
            var rightHeight = $(".rightView").outerHeight(true);
            //alert("right" + rightHeight);
            //alert("left" + leftHeight);

            if (leftHeight < rightHeight)
            {
                $(".leftView").append(appentContent).fadeIn("slow");
            } else {
                $(".rightView").append(appentContent).fadeIn("slow");
            }
            //动画效果
           // alert("o");
           /* $("#" + i).hide();
            $("#" + i).delay(500 * i * 0.2).fadeIn("slow");*/
           
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