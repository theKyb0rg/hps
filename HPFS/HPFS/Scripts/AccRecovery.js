$(function () {
    $("#btnNext").click(function () {

        // check which radio button was selected
        var value = $("input[name=radWhy]:checked").val();

        // display the appropriate controls
        if (value == "pass") {

            $("#why").animate({
                left: '250px',
                opacity: '0',
                width: '384px'
            }, 300, function () {
                // make it display:none
                $(this).hide();
                // fade in new control
                $("#recPass").fadeIn(300);
            });
        }
        else {

            $("#why").animate({
                left: '250px',
                opacity: '0',
                width: '384px'
            }, 300, function () {
                $(this).hide();
                $("#recUser").fadeIn(300);
            });
        }
    });

    $("#Body_btnSendCode").click(function () {

        // declare variables to use when validating
        var info = $("#Body_txtInfo").val(),
            emailRegex = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/,
            phoneRegex = /^\d{10}$/;

        if (info != "") {
            if (emailRegex.test(info)) {
                return true;
            }
            else if (phoneRegex.test(info)) {
                return true;
            }
            else {
                $("#Body_lblErrors").html("You have not entered a valid email or phone number."
                                        + "<br/>A phone number can only contain numbers.");
                return false;
            }
        }
        else {
            $("#Body_lblErrors").html("In order to send you a code you must enter your phone number or email address.");
            return false;
        }
    });

    // More password validation
    $("#Body_btnSavePass").click(function () {

        // declare variables
        var message = "",
            newpass = $("#Body_txtPass").val(),
            repass = $("#Body_txtRePass").val(),
            lblpass = $("#Body_lblPass"),
            lblrepass = $("#Body_lblRePass"),
            lblerrors = $("#Body_lblPassErrors");

        if (newpass.length <= 5) {

            lblpass.html("Enter your new password: <span class='text-danger'>*</span>");
            message += "Your new password must be at least 6 characters.<br />";
        }
        else if (repass.length <= 5) {

            lblrepass.html("Confirm your password: <span class='text-danger'>*</span>");
            message += "Both password fields are required and must be at least 6 characters.<br />";
        }
        else { // both fields are 6 characters

            if (newpass == repass) { //success

                lblrepass.html("Confirm your password:");
                lblpass.html("Enter your new password:");
            }
            else { //passwords don't match

                lblpass.html("Enter your new password: <span class='text-danger'>*</span>");
                lblrepass.html("Confirm your password: <span class='text-danger'>*</span>");
                message += "The two passwords don't match.<br />"
            }
        }

        if (message == "") {
            return true;
        }
        else // errors
        {
            lblerrors.html(message);
            return false;
        }

    });

    // Username validation
    $("#Body_btnUsernameNext").click(function () {

        //declare variables
        var first = $("#Body_txtFirst").val(),
            last = $("#Body_txtLast").val(),
            message = "",
            lblerrors = $("#Body_lblUsernameErrors"),
            lblfirst = $("#Body_lblFirst"),
            lbllast = $("#Body_lblLast");

        // see if they entered something
        if (first == "" && last == "") {
            message += "You must enter both your first and last name.";
            lblfirst.html("Enter your first name: <span class='text-danger'>*</span>");
            lbllast.html("Enter your last name: <span class='text-danger'>*</span>");
        }
        else if (first == "") {
            message += "You must enter your first name.";
            lblfirst.html("Enter your first name: <span class='text-danger'>*</span>");
            lbllast.html("Enter your last name:");
        }
        else if (last == "") {
            message += "You must enter your last name."
            lbllast.html("Enter your last name: <span class='text-danger'>*</span>");
            lblfirst.html("Enter your first name:");
        }

        if (message == "") {
            lblfirst.html("Enter your first name:");
            lbllast.html("Enter your last name:");
            return true;
        }
        else {
            lblerrors.html(message);
            return false;
        }
    });
});