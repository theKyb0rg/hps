$(function () {
    $("a[name='delete']").click(function () {
        if (confirm("Are you sure?")) {
            var deletedUserId = $(this).attr("data-id");
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "/WebServices/UserManager.asmx/DeleteUser",
                data: JSON.stringify({ 'id': deletedUserId }),
                dataType: "json",
                success: function (n) {
                    console.log(n.d);
                    $("#Body_lblMessage").addClass("text-success").html(n.d);
                },
                error: function (xhr) {
                    console.log(xhr.d);
                }
            });
        }
        else {
            return false;
        }
    });

    $("#Body_btnCreateUser").click(function () {
        // Create variables to hold error message and number of checkboxes checked
        var message = "",
            lblUsername = $("#Body_lblUsername"),
            lblEmail = $("#Body_lblEmail"),
            lblUserFirstName = $("#Body_lblUserFirstName"),
            lblUserLastName = $("#Body_lblUserLastName"),
            lblPassword = $("#Body_lblPassword"),
            lblPasswordConfirm = $("#Body_lblPasswordConfirm"),
            lblRole = $("#Body_lblRole");

        // Check if username is filled out
        if ($("#Body_txtUsername").val() == "") {
            lblUsername.html("Username: <span class='text-danger'>*</span>");
            message += "<br><br><label class='text-danger'>* You must enter a Username.</label>";
        }
        else if ($("#Body_txtUsername").val().length < 6) {
            lblUsername.html("Username: <span class='text-danger'>*</span>");
            message += "<br><br><label class='text-danger'>* You must enter a Username with at least <b>6</b> characters.</label>";
        }
        else {
            lblUsername.html("Username:");
        }

        // Check if email is filled out and is valid
        if ($("#Body_txtEmail").val() == "") {
            lblEmail.html("Email: <span class='text-danger'>*</span>");
            message += "<br><br><label class='text-danger'>* You must enter an Email Address.</label>";
        }
        else if (!isValidEmailAddress($("#Body_txtEmail").val())) {
            lblEmail.html("Email: <span class='text-danger'>*</span>");
            message += "<br><br><label class='text-danger'>* You must enter a valid Email Address. Example: username@email.com</label>";
        }
        else {
            lblEmail.html("Email:");
        }

        // Check for first name
        if ($("#Body_txtUserFirstName").val() == "") {
            lblUserFirstName.html("First Name: <span class='text-danger'>*</span>")
            message += "<br><br><label class='text-danger'>* You must enter a First Name.</label>"
        }
        else {
            lblUserFirstName.html("First Name:");
        }

        // Check for last name
        if ($("#Body_txtUserLastName").val() == "") {
            lblUserLastName.html("Last Name: <span class='text-danger'>*</span>")
            message += "<br><br><label class='text-danger'>* You must enter a Last Name.</label>"
        }
        else {
            lblUserLastName.html("Last Name:");
        }

        // Check for password
        if ($("#Body_txtPassword").val() == "") {
            lblPassword.html("Password: <span class='text-danger'>*</span>")
            message += "<br><br><label class='text-danger'>* You must enter a Password.</label>"
        }
        else if ($("#Body_txtPassword").val().length < 6) {
            lblPassword.html("Password: <span class='text-danger'>*</span>");
            message += "<br><br><label class='text-danger'>* You must enter a Password with at least <b>6</b> characters.</label>";
        }
        else {
            lblPassword.html("Password:");
        }

        // Check for blank confirmed password
        if ($("#Body_txtPasswordConfirm").val() == "") {
            lblPasswordConfirm.html("Confirm Password: <span class='text-danger'>*</span>");
            message += "<br><br><label class='text-danger'>* You must confirm your Password.</label>"
        }
        else {
            lblPasswordConfirm.html("Confirm Password:");
        }

        // Check for matching password
        if ($("#Body_txtPassword").val() != $("#Body_txtPasswordConfirm").val()) {
            lblPassword.html("Password: <span class='text-danger'>*</span>");
            lblPasswordConfirm.html("Confirm Password: <span class='text-danger'>*</span>");
            message += "<br><br><label class='text-danger'>* Your Passwords do not match.</label>";
        }

        // Check for role
        if ($("#Body_ddlRole").val() == -1) {
            lblRole.html("Role: <span class='text-danger'>*</span>");
            message += "<br><br><label class='text-danger'>* You must select a Role.</label>";
        }
        else {
            lblRole.html("Role:");
        }

        // Check if there are errors in the message
        if (message != "") {
            $("#Body_lblMessage").html(message);
            return false;
        }
    });

    $("#Body_btnSearch").click(function () {
        var errors = "",
            dateRegex = /(0[1-9]|1[012])[- \/.](0[1-9]|[12][0-9]|3[01])[- \/.](19|20)\d\d/,            
            isChrome = /Chrome/.test(navigator.userAgent) && /Google Inc/.test(navigator.vendor),
            lblEnd = $("#Body_lblSearchEndDate"),
            end = $("#Body_txtSearchEndDate").val(),
            lblStart = $("#Body_lblSearchStartDate"),
            start = $("#Body_txtSearchStartDate").val(),
            lblEmailSearch = $("#Body_lblSearchEmail"),
            email = $("#Body_txtSearchEmail").val(),
            lblErrors = $("#Body_lblUserSearchErrors");

        if (!dateRegex.test(start) && !isChrome && start != "") {
            errors += "The Start Date Entered Must be Valid. ex 12/21/2015";
            lblStart.html("Creation Start Date: <span class='text-danger'>*</span>");
            lblEnd.html("Creation End Date:");
        }
        else if (start != "" && end == "") {
            errors += "<br />If you Entered a Start Date you Must also Enter an End Date.";
            lblEnd.html("Creation End Date: <span class='text-danger'>*</span>");
            lblStart.html("Creation Start Date:");
        }

        if (!dateRegex.test(end) && !isChrome && end != "") {
            errors += "<br />The End Date Entered Must be Valid. ex 12/21/2015";
            lblEnd.html("Creation End Date: <span class='text-danger'>*</span>");
            lblStart.html("Creation Start Date:");
        }
        else if (end != "" && start == "") {
            errors += "<br />If you Entered an End Date you Must also Enter a Start Date.";
            lblStart.html("Creation Start Date: <span class='text-danger'>*</span>");
            lblEnd.html("Creation End Date:");
        }

        if (!isValidEmailAddress(email) && email != "")
        {
            errors += "<br />Email Address Must be a Valid Format. ex johnsmith@domain.com";
            lblEmailSearch.html("Email: <span class='text-danger'>*</span>");
        }
        else {
            lblEmailSearch.html("Email:");
        }

        if (errors != "") {
            lblErrors.html(errors);
            return false;
        }
        else {
            return true;
        }
    });
});

function isValidEmailAddress(emailAddress) {
    var pattern = /^([a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+(\.[a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+)*|"((([ \t]*\r\n)?[ \t]+)?([\x01-\x08\x0b\x0c\x0e-\x1f\x7f\x21\x23-\x5b\x5d-\x7e\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|\\[\x01-\x09\x0b\x0c\x0d-\x7f\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))*(([ \t]*\r\n)?[ \t]+)?")@(([a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.)+([a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.?$/i;
    return pattern.test(emailAddress);
};