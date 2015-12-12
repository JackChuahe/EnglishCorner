/**
 * Created by qdhlo on 2015/12/3.
 */
$(function () {

    var $wind = $(window);//将浏览器加入缓存中

    var $introduction = $('div.introduction');//将你要改变宽度的div元素加入缓存中
    var $container = $('div.container');
    var win = $wind.width();//首先获取浏览器的宽度

    $win.resize(function () {//浏览器变化宽度的动作。
        var newW = $wind.width();
        var newW= $wind.width();
        var w=$introduction.width();

        $introduction.height();
        $container.width()

    });

});