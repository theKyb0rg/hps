$(function () {

    $("#Body_btnSetNewGoal").click(function () {
        // Get all controls
        var message = "",
            lblGoal = $("#Body_lblGoal"),
            lblGoalType = $("#Body_lblGoalType"),
            lblGoalStartDate = $("#Body_lblGoalStartDate"),
            lblGoalEndDate = $("#Body_lblGoalEndDate"),
            txtGoal = $("#Body_txtGoal"),
            ddlGoalType = $("#Body_ddlGoalType"),
            txtGoalStartDate = $("#Body_txtGoalStartDate"),
            txtGoalEndDate = $("#Body_txtGoalEndDate"),
            lblGoalErrorMessages = $("#lblGoalErrorMessages");

        // Check values of controls
        if (txtGoal.val() == "") {
            lblGoal.html("Goal: <span class='text-danger'>*</span>")
            message += "<br><br>* You must enter in a Monthly Goal."
        }
        else {
            lblGoal.html("Goal:");
        }
        if (ddlGoalType.val() == "-1") {
            lblGoalType.html("Type: <span class='text-danger'>*</span>")
            message += "<br><br>* You must select a Type of goal."
        }
        else {
            lblGoalType.html("Type:");
        }
        if (txtGoalStartDate.val() == "") {
            lblGoalStartDate.html("Start Date: <span class='text-danger'>*</span>")
            message += "<br><br>* You must enter a Start Date for your goal."
        }
        else {
            lblGoalStartDate.html("Start Date:");
        }
        if (txtGoalEndDate.val() == "") {
            lblGoalEndDate.html("End Date: <span class='text-danger'>*</span>")
            message += "<br><br>* You must enter an End Date for your goal."
        }
        else {
            lblGoalEndDate.html("End Date:");
        }

        // If there are errors in the message variable, stop execution of server side code
        if (message != "") {
            lblGoalErrorMessages.html(message);
            return false;
        }
        else {
            lblGoalErrorMessages.html("");
        }
    });

    $("a[name='delete-distance-goal']").click(function () {
        if (confirm("Are you sure you want to delete this Distance Goal?")) {
            PageMethods.DeleteDistanceGoal($(this).attr('data-id'));
        }
        else {
            return false
        }
    });

    $("a[name='delete-step-goal']").click(function () {
        if (confirm("Are you sure you want to delete this Step Goal?")) {
            PageMethods.DeleteStepGoal($(this).attr('data-id'));
        }
        else {
            return false
        }
    });

    $("a[name='delete-minute-goal']").click(function () {
        if (confirm("Are you sure you want to delete this Minute Goal?")) {
            PageMethods.DeleteMinuteGoal($(this).attr('data-id'));
        }
        else {
            return false
        }
    });

});