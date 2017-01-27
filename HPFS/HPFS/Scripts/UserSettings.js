$(function () {

    // Contact Information Validation
    $("#Body_btnSaveContact").click(function () {

        // Declare variables
        var message = "",
            txtFN = $("#Body_txtFN").val(),
            txtLN = $("#Body_txtLN").val(),
            txtEmail = $("#Body_txtEmail").val(),
            txtPhone = $("#Body_txtPhone").val(),            
            numberRegex = /\d/g;

        // Check if any are empty or Invalid
        if (txtFN == "" || numberRegex.test(txtFN)) {
            message += "<br><br><label class='text-danger'>* First name can't be empty or contain numbers.</label>";
            $("#Body_lblFN").html("First Name: <span class='text-danger'>*</span>");
        }
        else {
            $("#Body_lblFN").html("First Name:");
        }

        // If empty or containing numbers
        if (txtLN == "" || numberRegex.test(txtLN)) {
            message += "<br><br><label class='text-danger'>* Last name can't be empty or contain numbers.</label>";
            $("#Body_lblLN").html("Last Name: <span class='text-danger'>*</span>");
        }
        else {
            $("#Body_lblLN").html("Last Name:");
        }

        // if empty or not a valid email address
        if (txtEmail != "" && !isValidEmailAddress(txtEmail)) {
            message += "<br><br><label class='text-danger'>* A proper email address is required. Example. johnsmith@domain.com</label>";
            $("#Body_lblEmail").html("Email Address: <span class='text-danger'>*</span>");
        }
        else {
            $("#Body_lblEmail").html("Email Address:");
        }

        // if empty or not a valid phone number
        if (txtPhone != "" && !isValidPhoneNumber(txtPhone)) {
            message += "<br><br><label class='text-danger'>* A valid phone number is required. Only 10 digits will be accepted.</label>";
            $("#Body_lblPhone").html("Phone #: <span class='text-danger'>*</span>");
        }
        else {
            $("#Body_lblPhone").html("Phone #:");
        }

        // if there are errors display message
        if (message != "") {
            $("#Body_lblContactErrors").html(message);
            $("#Body_lblContactSuccess").html("");
            return false;
        }
    });

    // Password Validation
    $("#Body_btnSavePass").click(function () {

        // Declare variables
        var message = "",
            txtNewPass = $("#Body_txtNewPass").val(),
            txtRePass = $("#Body_txtRePass").val(),
            txtOldPass = $("#Body_txtOldPass").val();

        // Check if current password is empty first or less than 6
        if (txtOldPass.length < 6) {
            message += "<br><br><label class='text-danger'>* You must enter your current password before changing it.</label>";
            $("#Body_lblOldPass").html("Current Password: <span class='text-danger'>*</span>");
            $("#Body_lblNewPass").html("New Password:");
            $("#Body_lblRePass").html("Confirm Password:");
            $("#Body_lblPassErrors").html(message);
            return false;
        }
        else {
            $("#Body_lblOldPass").html("Current Password:");
        }

        if (txtRePass.length < 6 && txtNewPass.length < 6) {
            message += "<br><br><label class='text-danger'>* Both Passwords must be at least 6 characters.\n</label>";
            $("#Body_lblRePass").html("Confirm Password: <span class='text-danger'>*</span>");
            $("#Body_lblNewPass").html("New Password: <span class='text-danger'>*</span>");
            $("#Body_lblPassErrors").html(message);
            return false;
        }
        else {
            $("#Body_lblRePass").html("Re-enter Password:");
            $("#Body_lblNewPass").html("New Password:");
        }

        // Check if any are empty or Invalid
        if (txtNewPass.length < 6) {
            message += "<br><br><label class='text-danger'>* Password must be at least 6 characters.</label>";
            $("#Body_lblNewPass").html("New Password: <span class='text-danger'>*</span>");
            $("#Body_lblRePass").html("Confirm Password:");
            $("#Body_lblPassErrors").html(message);
            return false;
        }
        else {
            $("#Body_lblNewPass").html("New Password:");
        }

        // if the re-entered new password is empty or less than 6 characters
        if (txtRePass.length < 6) {
            message += "<br><br><label class='text-danger'>* Password must be at least 6 characters.\n</label>";
            $("#Body_lblRePass").html("Confirm Password: <span class='text-danger'>*</span>");
            $("#Body_lblPassErrors").html(message);
            return false;
        }
        else {
            $("#Body_lblRePass").html("Confirm Password:");
        }

        // if the old password entered is equal to the new
        if (txtOldPass == txtNewPass || txtOldPass == txtRePass) {
            message += "<br><br><label class='text-danger'>* Your New Password can't be the same as your current.</label>";
            $("#Body_lblNewPass").html("New Password: <span class='text-danger'>*</span>");
            $("#Body_lblRePass").html("Confirm Password: <span class='text-danger'>*</span>");
            $("#Body_lblPassErrors").html(message);
            return false;
        }
        else {
            $("#Body_lblNewPass").html("New Password:");
            $("#Body_lblRePass").html("Confirm Password:");
        }

        // if the error message is empty on to the next validation layer
        if (message == "") {
            if (txtNewPass != txtRePass) {
                message += "<br><br><label class='text-danger'>* The passwords entered don't match.\n</label>";
                $("#Body_lblNewPass").html("New Password: <span class='text-danger'>*</span>");
                $("#Body_lblRePass").html("Confirm Password: <span class='text-danger'>*</span>");
                $("#Body_lblPassErrors").html(message);
                return false;
            }
            else {
                $("#Body_lblNewPass").html("New Password:");
                $("#Body_lblRePass").html("Confirm Password:");
            }
        }
        else { // (message != "")
            $("#Body_lblPassErrors").html(message);
            $("#Body_lblPassSuccess").html("");
            return false;
        }
    });

    $("#Body_btnSendEmailCode").click(function () {

        var lblEmailErrors = $("#Body_lblVerifyEmail"),
            email = $("#Body_txtVerifyEmail").val();

        $("#Body_lblVerifyEmailSuccess").html("");
        $("#Body_lblVerifyEmailConflict").html("");

        if (email == "") {
            lblEmailErrors.html("You Must First Enter an Email Address to Have it Verified.");
            return false;
        }
        else if (!isValidEmailAddress(email)) {
            lblEmailErrors.html("You Must Enter a Valid Email Address. ex johnsmith@domain.com");
            return false;
        }
        else {
            return true;
        }
    });

    $("#Body_btnSendCellCode").click(function () {

        var cell = $("#Body_txtVerifyCell").val(),
            lblCellErrors = $("#Body_lblVerifyCell");

        $("#Body_lblVerifyCellSuccess").html("");
        $("#Body_lblVerifyCellConflict").html("");

        if (cell == "") {
            lblCellErrors.html("You Must First Enter a Mobile Number to Have it Verified.");
            return false;
        }
        else if (!isValidPhoneNumber(cell)) {
            lblCellErrors.html("You Must Enter a Valid Mobile Number. 10 Digits nothing else.");
            return false;
        }
        else {
            return true;
        }
    });

    $("#Body_btnVerifyEmailCode").click(function () {

        var emailCode = $("#Body_txtVerifyEmailCode").val(),
            lblEmailErrors = $("#Body_lblVerifyEmail");

        if (emailCode == "") {
            lblEmailErrors.html("You Must Enter The Code You Received to Verify it.");
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

function isValidPhoneNumber(phoneNumber) {
    var pattern = /^\d{10}$/;
    return pattern.test(phoneNumber);
};