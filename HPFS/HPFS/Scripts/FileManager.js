$(function () {

    // Set the drop down to the correct folder for uploading files
    $("a[name='add']").click(function () {
        $('#Body_ddlUploadFileFolder').val($(this).attr('data-id'));
    });

    $("a[name='remove']").click(function () {
        if (confirm("This will delete all files within this folder. Are you sure you want to proceed?")) {
            PageMethods.DeleteFolder($(this).attr('data-id'));
        }
        else {
            return false
        }
    });

    $("body").on("click", "a[name='download']", function () {
        PageMethods.DownloadFile($(this).attr('data-id'));
    });

    $("a[name='delete']").click(function () {
        if (confirm("Are you sure?")) {
            PageMethods.DeleteFile($(this).attr('data-id'));
        }
        else {
            return false;
        }
    });

    $("#Body_btnCreateFolder").click(function () {
        // Create variables to hold error message and number of checkboxes checked
        var checkboxCount = 0,
            message = "",
            lblFolderName = $("#Body_lblFolderName"),
            lblFolderPermissions = $("#Body_lblFolderPermissions");

        // Count the number of checkboxes checked
        $('input[type=checkbox]').each(function () {
            if (this.checked) {
                checkboxCount++;
            }
        });

        // Check if checkboxes are checked
        // Have to do 2 because thats how it wants to do it, i dunno lol
        if (checkboxCount < 2) {
            lblFolderPermissions.html("Permissions: <span class='text-danger'>*</span>")
            message += "<br><br>* You must select at least two permission groups for this folder."
        }
        else {
            lblFolderPermissions.html("Permissions:");
        }

        // Check if name is filled out
        if ($("#Body_txtFolderName").val() == "") {
            lblFolderName.html("Folder Name: <span class='text-danger'>*</span>");
            message += "<br><br>* You must enter a folder name.";
        }
        else {
            lblFolderName.html("Folder Name:");
        }

        // Check if there are errors in the message
        if (message != "") {
            $("#Body_lblFolderErrors").html(message);
            return false;
        }
    });

    $("#Body_btnSearch").click(function () {
        var errors = "",
            lblStart = $("#Body_lblSearchStartDate"),
            start = $("#Body_txtSearchStartDate").val(),
            dateRegex = /(0[1-9]|1[012])[- \/.](0[1-9]|[12][0-9]|3[01])[- \/.](19|20)\d\d/,
            isChrome = /Chrome/.test(navigator.userAgent) && /Google Inc/.test(navigator.vendor),
            lblEnd = $("#Body_lblSearchEndDate"),
            end = $("#Body_txtSearchEndDate").val(),
            lblErrors = $("#Body_lblSearchFolderErrors");

        if (!dateRegex.test(start) && !isChrome && start != "") {
            errors += "The Start Date Entered Must be Valid. ex 12/21/2015";            
            lblStart.html("Upload Start Date: <span class='text-danger'>*</span>");
            lblEnd.html("Upload End Date:");
        }
        else if (start != "" && end == "") {
            errors += "<br />If you Entered a Start Date you Must also Enter an End Date.";
            lblEnd.html("Upload End Date: <span class='text-danger'>*</span>");
            lblStart.html("Upload Start Date:");
        }

        if (!dateRegex.test(end) && !isChrome && end != "") {
            errors += "<br />The End Date Entered Must be Valid. ex 12/21/2015";
            lblEnd.html("Upload End Date: <span class='text-danger'>*</span>");
            lblStart.html("Upload Start Date:");
        }
        else if (end != "" && start == "") {
            errors += "<br />If you Entered an End Date you Must also Enter a Start Date.";
            lblStart.html("Upload Start Date: <span class='text-danger'>*</span>");
            lblEnd.html("Upload End Date:");
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


//function success(result) {
//    // Display notification after 1 second
//    window.setTimeout(function () {
//        $.notify(result, "success");
//    }, 1000);
//    console.log(result);
//    alert(result);
//}

//function failure(result) {
//    // Display Notification after 1 second
//    window.setTimeout(function () {
//        $.notify(result, "danger");
//    }, 1000);
//    console.log(result);
//}