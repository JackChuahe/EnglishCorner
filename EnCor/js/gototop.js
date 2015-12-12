/**
 * Created by qdhlo on 2015/12/3.
 */
$(document).ready(function () {
    $("#goToTop").hide();//隐藏go to top按钮
    $("#goToWrite").hide();
    $(function () {
        $(window).scroll(function () {
            if ($(this).scrollTop() > 200) {//当window的scrolltop距离大于1时，go to top按钮淡出，反之淡入
                $("#goToTop").fadeIn();
                $("#goToWrite").fadeIn();
            } else {
                $("#goToTop").fadeOut();
                $("#goToWrite").fadeOut();
            }
        });
    });

    // 给go to top按钮一个点击事件
    $("#goToTop").click(function () {
        $("html,body").animate({scrollTop: 0}, 800);//点击go to top按钮时，以800的速度回到顶部，这里的800可以根据你的需求修改
        return false;
    });
    $("#goToWrite").click(function () {
        $("html,body").animate({scrollTop: 0}, 800);//点击go to top按钮时，以800的速度回到顶部，这里的800可以根据你的需求修改
        return false;
    });
});

