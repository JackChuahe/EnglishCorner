

$(document).ready(function () {
    //从网络获取数据
    $.get("../communitySquare/communitySuqare.ashx", {}, function (data) {

        var communities = jQuery.parseJSON(data).communities;
        var myColor = new Array("red", "dGreen", "orange", "pink", "yellow", "gray", "green", "blue", "brown", "purple");
        //../communityWebFront/Community.html?communityID=  communities[i].communityID +
        for (var i = 0 ; i < communities.length ; i++) {
            var html = "<div id=\"" + i + "\" class=\"box\" >" +
                "<a href=\"../communityWebFront/Community.html?communityID=" + communities[i].communityID + "\"><div class=\"backImg\"><img src=\"" + communities[i].communityPic + "\" /></div></a>" +
                "<div class=\"firstUser\"><img src=\"" + communities[i].headImageUrl + "\" />" +
                "</div><div class=\"clear\"></div><div class=\"disc\"><div style=\"height: 18px\"></div>" +
                "<div class=\"communityName\">" + communities[i].communityName + "</div><div class=\"activator\">" + communities[i].userName + "</div>" +
                "<button class=\"commentbtn\" id=\"" + communities[i].communityID + "\" onclick=\"javascript:fellowToComnunity(this.id)\" value=\"\">Follow</button>" + "</div></div>";


            $(".rightBox").append(html);
            var cIndex = parseInt(10 * Math.random());
            $("#" + i).addClass(myColor[cIndex]);
            /*
            setTimeout(function () {

            }, 3000);*/
            $("#" + i).hide();
           // $("#" + i).delay(500 * i * 0.2).animate({ top: 50 }, 200);
           //  $("#" + i).delay(500 * i * 0.2).slideTop(200);
            $("#" + i).delay(500 * i * 0.2).fadeIn("slow");
        }

        ////////////
            
    });


});