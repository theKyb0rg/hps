$(function () {
    $('.opacityNone').animate(
        { opacity: 100 }, // what we are animating
        100000, // how fast we are animating
        'easeOutExpo', // the type of easing
        function () { // the callback
            //alert('done');
        });
})