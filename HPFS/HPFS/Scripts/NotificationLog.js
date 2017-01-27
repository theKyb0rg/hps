function ValidateDates() {

    var lblStart = $("#Body_lblNotificationStart"),
        start = $("#Body_txtNotificationStartDate").val(),
        lblEnd = $("#Body_lblNotificationEnd"),
        end = $("#Body_txtNotificationEndDate").val(),
        lblErrors = $("#Body_lblNotificationErrors"),
        dateRegex = /(0[1-9]|1[012])[- \/.](0[1-9]|[12][0-9]|3[01])[- \/.](19|20)\d\d/,
        isChrome = /Chrome/.test(navigator.userAgent) && /Google Inc/.test(navigator.vendor),
        errors = "";

    if (!dateRegex.test(start) && start != "" && !isChrome) {
        errors += "<br />The Start Date Entered Must be Valid. ex 12/21/2015";
        lblStart.html("Start Date: <span class='text-danger'>*</span>");
        lblEnd.html("End Date:");
    }
    else if (start != "" && end == "") {
        errors += "<br />If you Entered a Start Date you Must also Enter an End Date.";
        lblEnd.html("End Date: <span class='text-danger'>*</span>");
        lblStart.html("Start Date:");
    }

    if (!dateRegex.test(end) && end != "" && !isChrome) {
        errors += "The End Date Entered Must be Valid. ex 12/21/2015";
        lblEnd.html("End Date: <span class='text-danger'>*</span>");
        lblStart.html("Start Date:");
    }
    else if( end != "" && start == "")
    {
        errors += "<br />If you Entered an End Date you Must also Enter a Start Date.";
        lblStart.html("Start Date: <span class='text-danger'>*</span>");
        lblEnd.html("End Date:");
    }

    if (errors != "") {
        lblErrors.html(errors);
        return false;
    }
    else {
        lblEnd.html("End Date:");
        lblStart.html("Start Date:");
        lblErrors.html("");
        return true;
    }
};

function filterNotificationRows() {
    var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();
    var $rows = $('#Body_tblNotificationLog tbody tr');
    $rows.show().filter(function () {
        var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
        return !~text.indexOf(val);
    }).hide();
};