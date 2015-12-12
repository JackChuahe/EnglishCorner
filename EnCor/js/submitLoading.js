/**
 * Created by qdhlo on 2015/12/12.
 */
$(function ($) {
    $("#submit").hover(function () {
        $(this).stop().animate({
            opacity: '1'
        }, 600);
    }, function () {
        $(this).stop().animate({
            opacity: '0.6'
        }, 1000);
    }).on('click', function () {
        //$("body").append("");
        $("#mask2").addClass("mask").fadeIn("slow");
        $("#submitLoading").fadeIn("slow");
    });

});