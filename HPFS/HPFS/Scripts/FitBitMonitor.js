$(function () {
    $("a[name='bar']").click(function (e) {
        e.preventDefault();
        var selector = "#" + $(this).attr("data-id");
        SwapClass($(this));
        ChangeChartType(selector, "bar");
    });

    $("a[name='line']").click(function (e) {
        e.preventDefault();
        var selector = "#" + $(this).attr("data-id");
        SwapClass($(this));
        ChangeChartType(selector, "area");
    });

    $("a[name='spline']").click(function (e) {
        e.preventDefault();
        var selector = "#" + $(this).attr("data-id");
        SwapClass($(this));
        ChangeChartType(selector, "area-spline");
    });

    $("a[name='scatter']").click(function (e) {
        e.preventDefault();
        var selector = "#" + $(this).attr("data-id");
        SwapClass($(this));
        ChangeChartType(selector, "scatter");
    });


    // LEFT IMAGE ANIMATIONS
    $("#distancesImage").on("click", ShowDistanceInfo);
    $("#distancesBackButton").on("click", HideDistanceInfo);

    // MIDDLE IMAGE ANIMATIONS
    $("#stepsImage").on("click", ShowStepsInfo);
    $("#stepsBackButton").on("click", HideStepsInfo);

    // RIGHT IMAGE ANIMATIONS
    $("#minutesImage").on("click", ShowMinutesInfo);
    $("#minutesBackButton").on("click", HideMinutesInfo);

});

function ShowDistanceInfo() {
    // Unbind click on all events
    $("#stepsImage").off("click", ShowStepsInfo);
    $("#minutesImage").off("click", ShowMinutesInfo);
    $("#distancesImage").off("click", ShowDistanceInfo);

    // Animate steps first
    $("#stepsImage").stop().animate({
        right: $("#rowWidth").width() / 3,
        opacity: '0',
    }, 300, function () {
        // Animate minutes after steps
        $("#minutesImage").stop().animate({
            right: $("#rowWidth").width() / 3 * 2,
            opacity: '0',
        }, 300, function () {
            // fade in back button control if true
            $("#distancesBackButton").stop().fadeIn(300);

            // Change the cursor back from pointer to auto
            $("#distancesImage").css('cursor', 'auto');
            $("#minutesImage").css('cursor', 'auto').hide();
            $("#stepsImage").css('cursor', 'auto').hide();

            // Show information section
            $("#distancesInfo").fadeIn(300);
        });
    });
}

function HideDistanceInfo() {
    // Hide the back button
    $("#distancesBackButton").stop().fadeOut(300);
    $("#distancesInfo").fadeOut(300);


    setTimeout(function () {
        $("#stepsImage").show();
        $("#minutesImage").show();


        // Animate steps first
        $("#stepsImage").stop().animate({
            right: 0,
            opacity: '100',
        }, 300, function () {
            // Animate minutes after steps
            $("#minutesImage").stop().animate({
                right: 0,
                opacity: '100',
            }, 300, function () {
                // Reenable the click functions
                $("#stepsImage").on("click", ShowStepsInfo);
                $("#minutesImage").on("click", ShowMinutesInfo);
                $("#distancesImage").on("click", ShowDistanceInfo);

                // Set the middle one to auto because it contains both left and right properties, done messes with the animation
                $('#stepsImage').css('left', 'auto');

                // Change the cursor back from auto to pointer
                $("#stepsImage").css('cursor', 'pointer');
                $("#minutesImage").css('cursor', 'pointer');
                $("#distancesImage").css('cursor', 'pointer');
            });
        });
    }, 300)

}

function ShowStepsInfo() {
    // Unbind click on other events
    $("#distancesImage").off("click", ShowDistanceInfo);
    $("#minutesImage").off("click", ShowMinutesInfo);
    $("#stepsImage").off("click", ShowStepsInfo);

    // Animate steps first
    $("#distancesImage").stop().animate({
        left: $("#rowWidth").width() / 3,
        opacity: '0',
    }, 300, function () {
        // Animate minutes after steps
        $("#minutesImage").stop().animate({
            right: $("#rowWidth").width() / 3,
            opacity: '0',
        }, 300, function () {
            // fade in back button control if true
            $("#stepsImage").stop().animate({
                right: $("#rowWidth").width() / 3,
            }, 300, function () {
                // fade in back button control if true
                $("#stepsBackButton").stop().fadeIn(300);

                // Change the cursor back from pointer to auto
                $("#stepsImage").css('cursor', 'auto').css('right', 'auto');
                $("#minutesImage").css('cursor', 'auto').hide();
                $("#distancesImage").css('cursor', 'auto').hide();

                // Show information section
                $("#stepsInfo").fadeIn(300);
            });
        });
    });
}

function HideStepsInfo() {
    // Fade out back button
    $("#stepsBackButton").stop().fadeOut(300);
    $("#stepsInfo").fadeOut(300);

    // Set time out for smooth animation
    setTimeout(function () {
        // Show these divs
        $("#minutesImage").show();
        $("#distancesImage").show();

        // HJave to reset after showing the divs so it returns to its original position or the animation gets jumpy
        $("#stepsImage").css('right', $("#rowWidth").width() / 3);

        $("#stepsImage").stop().animate({
            right: 0,
        }, 300, function () {
            // Animate steps first
            $("#distancesImage").stop().animate({
                left: 0,
                opacity: '100',
            }, 300, function () {
                // Animate minutes after steps
                $("#minutesImage").stop().animate({
                    right: 0,
                    opacity: '100',
                }, 300, function () {
                    // Reenable the click functions
                    $("#distancesImage").on("click", ShowDistanceInfo);
                    $("#minutesImage").on("click", ShowMinutesInfo);
                    $("#stepsImage").on("click", ShowStepsInfo);

                    // Set the middle one to auto because it contains both left and right properties, done messes with the animation
                    $('#stepsImage').css('left', 'auto');

                    // Change the cursor back from auto to pointer
                    $("#stepsImage").css('cursor', 'pointer');
                    $("#minutesImage").css('cursor', 'pointer')
                    $("#distancesImage").css('cursor', 'pointer')
                });
            });
        });
    }, 300);
}

function ShowMinutesInfo() {
    // Unbind click on all events
    $("#stepsImage").off("click", ShowStepsInfo);
    $("#distancesImage").off("click", ShowDistanceInfo);
    $("#minutesImage").off("click", ShowMinutesInfo);

    // Animate steps first
    $("#distancesImage").stop().animate({
        left: $("#rowWidth").width() / 3,
        opacity: '0',
    }, 300, function () {
        // Animate minutes after steps
        $("#stepsImage").stop().animate({
            left: $("#rowWidth").width() / 3,
            opacity: '0',
        }, 300, function () {
            // fade in back button control if true
            $("#minutesImage").stop().animate({
                right: $("#rowWidth").width() / 3 * 2,
            }, 300, function () {
                // fade in back button control if true
                $("#minutesBackButton").stop().fadeIn(300);

                // Change the cursor back from pointer to auto
                $("#minutesImage").css('cursor', 'auto').css('right', 'auto');
                $("#stepsImage").css('cursor', 'auto').hide();
                $("#distancesImage").css('cursor', 'auto').hide();

                // Show information section
                $("#minutesInfo").fadeIn(300);
            });
        });
    });
}

function HideMinutesInfo() {
    $("#minutesBackButton").stop().fadeOut(300);
    $("#minutesInfo").fadeOut(300);

    setTimeout(function () {

        // Show these divs again
        $("#stepsImage").show();
        $("#distancesImage").show();

        // Reset it back to the row width division or the animation gets jumpy
        $("#minutesImage").css('right', $("#rowWidth").width() / 3 * 2);

        // fade in back button control if true
        $("#minutesImage").stop().animate({
            right: 0,
        }, 300, function () {
            // Animate minutes after steps
            $("#stepsImage").stop().animate({
                left: 0,
                opacity: '100',
            }, 300, function () {
                // Animate steps first
                $("#distancesImage").stop().animate({
                    left: 0,
                    opacity: '100',
                }, 300, function () {
                    // Reenable the click functions
                    $("#stepsImage").on("click", ShowStepsInfo);
                    $("#distancesImage").on("click", ShowDistanceInfo);
                    $("#minutesImage").on("click", ShowMinutesInfo);

                    // Set the middle one to auto because it contains both left and right properties, done messes with the animation
                    $('#stepsImage').css('left', 'auto');

                    // Change the cursor back from auto to pointer
                    $("#stepsImage").css('cursor', 'pointer');
                    $("#minutesImage").css('cursor', 'pointer');
                    $("#distancesImage").css('cursor', 'pointer');
                });
            });
        });
    }, 300);

}

function SwapClass(control) {
    // Add the blue coloring to the button that was clicked
    control.addClass("btn-primary");

    // Remove any blue coloring from other buttons and add the black coloring
    control.parent()
        .siblings()
        .children()
        .removeClass("btn-primary")
        .addClass("btn-default");
}

function ChangeChartType(selector, type) {
    // Check if the selectors data-id matches the graph id to determine which graph to transform
    if (selector == "#userDistancesChart") {
        distanceChart.transform(type);
    }
    else if (selector == "#userStepsChart") {
        stepChart.transform(type);
    }
    else {
        minuteChart.transform(type)
    }
}