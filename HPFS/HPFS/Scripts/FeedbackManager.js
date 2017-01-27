// Required (firefox, and ie can't use calendar date picker)
function ValidateDates() {

    var startDate = $("#Body_txtFbStartDate").val(),
        endDate = $("#Body_txtFbEndDate").val(),
        errors = "",
        dateRegex = /(0[1-9]|1[012])[- \/.](0[1-9]|[12][0-9]|3[01])[- \/.](19|20)\d\d/,
        isChrome = /Chrome/.test(navigator.userAgent) && /Google Inc/.test(navigator.vendor);

    if (!isChrome && startDate != "" && !dateRegex.test(startDate)) {
        errors += "If you entered a start date it needs to be properly formatted. ex: 12/21/2016";
        $("#Body_lblFbStart").html("Start Date: <span class='text-danger'>*</span>");
    }
    else if (startDate != "" && endDate == "")
    {
        $("#Body_lblFbEnd").html("End Date: <span class='text-danger'>*</span>");
        $("#Body_lblFbStart").html("Start Date:");
        errors += "<br />If you Entered a Start Date you Must also Enter an End Date.";
    }

    if (endDate != "" && !dateRegex.test(endDate) && !isChrome) {
        errors += "<br />If you entered a end date it needs to be properly formatted. ex: 12/21/2016";
        $("#Body_lblFbEnd").html("End Date: <span class='text-danger'>*</span>");
    }
    else if (endDate != "" && startDate == "")
    {
        $("#Body_lblFbStart").html("Start Date: <span class='text-danger'>*</span>");
        $("#Body_lblFbEnd").html("End Date:");
        errors += "<br />If you Entered an End Date you Must also Enter a Start Date.";
    }

    if (errors != "") {
        $("#Body_lblFbErrors").html(errors);
        return false;
    }
    else {
        $("#Body_lblFbErrors").html("");
        $("#Body_lblFbEnd").html("End Date:");
        $("#Body_lblFbStart").html("Start Date:");
        return true;
    }
};

function filterFeedBackRows() {
    var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();
    var $rows = $('#Body_tblFeedback tbody tr');
    $rows.show().filter(function () {
        var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
        return !~text.indexOf(val);
    }).hide();
};

function BuildModal() {
    var feedbackId = $(this).attr("data-id");
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "/WebServices/Feedback.asmx/PopulateModal",
        data: JSON.stringify({ 'id': feedbackId }),
        dataType: "json",
        success: function (n) {
            console.log(n);
            $("#txtFeedbackFull").html(n.d.toString());
            $("#mdlFeedbackComment").modal('show');
        },
        error: function (xhr) {
            console.log(xhr);
        }
    });
}