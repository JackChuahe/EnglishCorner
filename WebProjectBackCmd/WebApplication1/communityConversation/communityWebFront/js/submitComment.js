

// 提交评论
function submitComment(contentID) {
    var textID = "#newComment_" + contentID;
    var commentContent = $(textID).val();
    if (commentContent.trim().length > 0)
    {
    var insertContent = "<div class=\"line\"></div><div class=\"oldComments\"><div class=\"oldimg\"><a href=\"#\">" +
                "<img src=\"" + "../../UserHeadImg/default.png" + "\"></a></div>" +
                "<div class=\"oldname\">" +
                "<a href=\"#\">" + "James Lius" + "</a><span>   " + new Date().getDate() + "</br>" + "why do some people choose this phone? " + "</span></div>" +
                "<div class=\"clear\"></div></div>" + "<div style=\"height: 13px\"></div>";
    $("#" + contentID).after(insertContent);
    }
    $(textID).val("");
    //alert(commentContent);
}