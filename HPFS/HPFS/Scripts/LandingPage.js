//Page Load
$(function () {

    $("#content1").delay(2000).fadeOut(500);
    $("#content2").delay(2500).fadeIn(300);
    $("#mainHeading").delay(3000).fadeIn(600);
    $("#directive").on('click', resetGuide);

    function resetGuide() {
        $("#directive").html('What are you looking for?');
        $(".guideButton").fadeOut(300);
        $(".activeGuide").fadeOut(300);
        $(".activeGuide").attr('class', 'form-control guideButton');
        $("#helpMe").delay(300).fadeIn(250);
        $("#informMe").delay(300).fadeIn(250);
        $("#aboutUs").delay(300).fadeIn(250);
        $("#newUser").delay(300).fadeIn(250);
        $("#speakFrench").delay(300).fadeIn(250);

    }

    $("#helpMe").on("click", function () {

        $("#directive").html("<a href='#'><span class='glyphicon glyphicon-chevron-left' style='font-size: .7em;'></span>Start Over</a>");

        //Out
        $("#informMe").fadeOut(300);
        $("#aboutUs").fadeOut(300);
        $("#newUser").fadeOut(300);
        $("#speakFrench").fadeOut(300);
        $("#helpMe").attr('class', 'form-control activeGuide');

        //In
        $("#emergencyBtn").delay(350).fadeIn(300);
        $("#nonEmergency").delay(350).fadeIn(300);

    });
    $("#informMe").on("click", function () {

        $("#directive").html("<a href='#'><span class='glyphicon glyphicon-chevron-left' style='font-size: .7em;'></span>Start Over</a>");


        //Out
        $("#helpMe").fadeOut(300);
        $("#aboutUs").fadeOut(300);
        $("#newUser").fadeOut(300);
        $("#speakFrench").fadeOut(300);
        $("#informMe").attr('class', 'form-control activeGuide');
        $("#informMe").attr('onclick', 'resetGuide()');

        //In
        $("#whatIsSchizo").delay(350).fadeIn(300);
        $("#howManySchizo").delay(350).fadeIn(300);
        $("#cureForSchizo").delay(350).fadeIn(300);
        $("#symptomsSchizo").delay(350).fadeIn(300);
        $("#schizoAge").delay(350).fadeIn(300);
        $("#schizoMore").delay(350).fadeIn(300);
    });
    $("#aboutUs").on("click", function () {

        $("#directive").html("<a href='#'><span class='glyphicon glyphicon-chevron-left' style='font-size: .7em;'></span>Start Over</a>");


        //Out
        $("#informMe").fadeOut(300);
        $("#helpMe").fadeOut(300);
        $("#newUser").fadeOut(300);
        $("#speakFrench").fadeOut(300);
        $("#aboutUs").attr('class', 'form-control activeGuide');

        //In
        $("#aboutMission").delay(350).fadeIn(300);
        $("#aboutPrograms").delay(350).fadeIn(300);
        $("#aboutMSAA").delay(350).fadeIn(300);
        $("#contactUs").delay(350).fadeIn(300);
        $("#locateUs").delay(350).fadeIn(300);
    });
    $("#newUser").on("click", function () {

        $("#directive").html("<a href='#'><span class='glyphicon glyphicon-chevron-left' style='font-size: .7em;'></span>Start Over</a>");


        //Out
        $("#informMe").fadeOut(300);
        $("#aboutUs").fadeOut(300);
        $("#helpMe").fadeOut(300);
        $("#speakFrench").fadeOut(300);
        $("#newUser").attr('class', 'form-control activeGuide');

        //In
        $("#signIn").delay(350).fadeIn(300);
        $("#contactAdmin").delay(350).fadeIn(300);
        $("#feedback").delay(350).fadeIn(300);
    });
});
function setLoginCookie() {
    document.cookie = "login=Yes; path=/";
}