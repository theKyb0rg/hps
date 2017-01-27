$(function () {
    $("#Body_btnCreateEvent").click(function () {

        var timeRegex = /(?:[01]|2(?![4-9])){1}\d{1}:[0-5]{1}\d{1}/,
            dateRegex = /(0[1-9]|1[012])[- \/.](0[1-9]|[12][0-9]|3[01])[- \/.](19|20)\d\d/,
            isChrome = /Chrome/.test(navigator.userAgent) && /Google Inc/.test(navigator.vendor),
            date = $("#Body_txtEventDate").val(),
            time = $("#Body_txtEventTime").val(),
            visibility = $("#Body_ddlVisibilty").val(),
            name = $("#Body_txtEventName").val(),
            errors = "",
            lblErrors = $("#Body_lblEventErrors"),
            lblDate = $("#Body_lblEventDate"),
            lblVisibility = $("#Body_lblVisibilty"),
            lblName = $("#Body_lblEventName"),
            lblTime = $("#Body_lblEventTime");


        if (time == "" || !timeRegex.test(time)) {
            lblTime.html("Time: <span class='text-danger'>*</span>");
            errors += "A Valid Time is Required. ex 23:59";
        }
        else {
            lblTime.html("Time:");
        }

        if (date == "" || (!dateRegex.test(date) && !isChrome)) {
            lblDate.html("Date: <span class='text-danger'>*</span>");
            errors += "<br />A Valid Date is Required. ex 12/21/2015";
        }
        else {
            lblDate.html("Date:");
        }

        if (visibility == -1) {
            lblVisibility.html("Visibility: <span class='text-danger'>*</span>");
            errors += "<br />Please Select a Visibility Setting.";
        }
        else {
            lblVisibility.html("Visibility:");
        }

        if (name == "") {
            lblName.html("Event Name: <span class='text-danger'>*</span>");
            errors += "<br />You Must Enter an Event Name.";
        }
        else {
            lblName.html("Event Name:");
        }

        if (errors != "") {
            lblErrors.html(errors);
            return false;
        }
        else {
            return true;
        }
    });

    $("#Body_btnSaveEvent").click(function () {

        var timeRegex = /(?:[01]|2(?![4-9])){1}\d{1}:[0-5]{1}\d{1}/,
            dateRegex = /^[0-9]{4}-(0[1-9]|1[0-2])-(0[1-9]|[1-2][0-9]|3[0-1])$/,
            date = $("#Body_txtEventDate").val(),
            time = $("#Body_txtEventTime").val(),
            visibility = $("#Body_ddlVisibilty").val(),
            name = $("#Body_txtEventName").val(),
            errors = "",
            lblErrors = $("#Body_lblEventErrors"),
            lblDate = $("#Body_lblEventDate"),
            lblVisibility = $("#Body_lblVisibilty"),
            lblName = $("#Body_lblEventName"),
            lblTime = $("#Body_lblEventTime");


        if (time == "" || !timeRegex.test(time)) {
            lblTime.html("Time: <span class='text-danger'>*</span>");
            errors += "A Valid Time is Required. ex 23:59";
        }
        else {
            lblTime.html("Time:");
        }

        if (date == "" || !dateRegex.test(date)) {
            lblDate.html("Date: <span class='text-danger'>*</span>");
            errors += "<br />A Valid Date is Required. ex 2016-04-12";
        }
        else {
            lblDate.html("Date:");
        }

        if (visibility == -1) {
            lblVisibility.html("Visibility: <span class='text-danger'>*</span>");
            errors += "<br />Please Select a Visibility Setting.";
        }
        else {
            lblVisibility.html("Visibility:");
        }

        if (name == "") {
            lblName.html("Event Name: <span class='text-danger'>*</span>");
            errors += "<br />You Must Enter an Event Name.";
        }
        else {
            lblName.html("Event Name:");
        }

        if (errors != "") {
            lblErrors.html(errors);
            return false;
        }
        else {
            return true;
        }
    });
})