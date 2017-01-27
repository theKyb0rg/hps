function openLoginModal() {
    $("#mdlLogin").modal("show");
}

function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + "; " + expires;
}

function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1);
        if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
    }
    return "";
}

function checkCookie() {
    var user = getCookie("username");
    if (user != "") {
        alert("Welcome again " + user);
    } else {
        user = prompt("Please enter your name:", "");
        if (user != "" && user != null) {
            setCookie("username", user, 365);
        }
    }
}

function deleteCookie(name) {
    document.cookie = name + '=; expires=Thu, 01 Jan 1970 00:00:01 GMT;';
}

// Login when user presses enter on login modal (doesn't work on chrome)
function EnterToClick() {

    if (event.keyCode == 13) {
        var strId = event.srcElement.id;
        event.returnValue = false;
        event.cancel = true;

        if (strId == '#txtUsername') {
            document.getElementById('#btnLogin').click();
        }
        else if (strId == '#txtPassword') {
            document.getElementById('#btnLogin').click();
        }
    }
}

// Build Mobile Calendar
function MobilePublicEvents() {

    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "/WebServices/Calendar.asmx/BuildMobileEvents",
        //data: JSON.stringify({ 'id': eventId }),
        //dataType: "json",
        success: function (n) {
            console.log(n);
            $("#mobilePublicEvents").html(n.d.toString());
        },
        error: function (xhr) {
            console.log(xhr);
        }
    });
}

// Document ready
$(function () {

    var isMobile = window.matchMedia("only screen and (max-width: 768px)");
    //cbRem = $("#cbRemember");

    $(".call").hide(); // needed or else link can be seen on contact page load.

    // Go to top button on mobile footer
    $("#top").click(function () {
        $("html, body").animate({ scrollTop: 0 }, "slow");
        return false;
    });

    // Set username textbox to be username not working cause it's in an update panel
    //$("#mdlLogin .modal-body").click(function () {
    //    if (getCookie("username") != "") {
    //        $("#txtUsername").val() = getCookie("username");
    //        cbRem.attr("checked");
    //    }
    //});

    // Setting / Updating / Deleting username cookie
    //$("#btnLogin").click(function () {

    //    if (getCookie("username") == "" && cbRem.prop("checked") && $("#txtUsername").val() != "") {
    //        setCookie("username", $("#txtUsername").val(), 365);
    //    }
    //    else if (getCookie("username") != "" && cbRem.prop("checked") && $("#txtUsername").val() != "" && getCookie("username") != $("#txtUsername").val()) {
    //        setCookie("username", $("#txtUsername").val(), 365);
    //    }
    //    else {
    //        deleteCookie("username");
    //    }
    //});

    //// If checkbox is checked on reset btn click re populate with cookie data
    //$("#btnReset").click(function () {

    //    if (cbRem.prop("checked") && getCookie("username") != "")
    //    {
    //        alert("in if");
    //        $("#txtUsername").val() = getCookie("username");
    //        return false;
    //    }
    //    else {
    //        return true;
    //    }
    //});

    // Feedback submission 
    $("#btnSubmit").click(function () {
        alert("\tYour Feedback has been submitted.\t \n\n Thank you for your time and consideration.");
    });

    // Saving Admin Settings 
    $("#btnSaveAdminSettings").click(function () {
        alert("Your changes have been saved.");
    });

    $(window).load(function () {

        // allow call link to work on contact page and footer for mobile devices and show/hide stripes
        if (isMobile.matches && $(window).availHeight <= 768 && $(window).availWidth <= 1024) {
            $(".call").show();
            $(".callOff").hide();
            $("#navStripes").hide();

        } else {
            $(".call").hide();
            $(".callOff").show();
            $("#navStripes").show();
        }

        if (isMobile.matches) {
            MobilePublicEvents();
        }
    });

    $(window).resize(function () {

        // show / hide stripes 
        if (isMobile.matches) {
            $("#navStripes").hide();
            MobilePublicEvents();
        } else {
            $("#navStripes").show();
        }
    });

    // adding events for the hps Logo to change on hover and click
    //$("#hpsLogo").on({
    //    mouseenter: function () {
    //        $(this).attr("src", "/Content/Images/hpsLogoHover.png");
    //    },
    //    mouseleave: function () {
    //        $(this).attr("src", "/Content/Images/hpsLogo.png");
    //    },
    //    mousedown: function () {
    //        $(this).attr("src", "/Content/Images/hpsLogoClick.png");
    //    }
    //});

    /* Validating Contact Form on master page */
    $("#btnContactSend").click(function () {

        // Variables
        var errors = $("#lblContactFormErrors"),
            message = "",
            txtarea = $("#tarContactMessage").val(),
            email = $("#txtContactEmail").val(),
            lastname = $("#txtContactLast").val(),
            firstname = $("#txtContactFirst").val(),
            subject = $("#txtContactSubject").val(),
            emailRegex = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

        if (firstname == "") {
            message += "<br />A first name is required.";
            $("#lblContactFirstName").html("First Name: <span class='text-danger'>*</span>");
        }
        else {
            $("#lblContactFirstName").html("First Name:");
        }

        if (lastname == "") {
            message += "<br />A last name is required.";
            $("#lblContactLastName").html("Last Name: <span class='text-danger'>*</span>");
        }
        else {
            $("#lblContactLastName").html("Last Name:");
        }

        if (email == "" || !emailRegex.test(email)) {
            message += "<br />A valid email is required. ex johnsmith@domain.com";
            $("#lblContactEmail").html("Email: <span class='text-danger'>*</span>");
        }
        else {
            $("#lblContactEmail").html("Email:");
        }

        if (subject == "") {
            message += "<br />A subject is required.";
            $("#lblContactSubject").html("Subject: <span class='text-danger'>*</span>");
        }
        else {
            $("#lblContactSubject").html("Subject:");
        }

        if (txtarea == "") {
            message += "<br />A message is required.";
            $("#lblContactMessage").html("Message: <span class='text-danger'>*</span>");
        }
        else {
            $("#lblContactMessage").html("Message:");
        }

        if (message == "") {
            return true;
        }
        else {
            errors.html(message);
            return false;
        }
    });

    // Clear All Notifications
    $("#btnReadAll").click(function () {
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: "/WebServices/Notifications.asmx/ReadAllNotifications",
            //data: JSON.stringify({ 'id': notificationId }),
            //dataType: "json",
            success: function (n) {
                console.log(n);
                // Start the table with the headers
                var table = "";

                // Check for empty result set
                if (n.d.length == 0) {

                    // Display notification if there are no records to show
                    table = "<label><b>No notifications to show.</b></label>";
                }
                else {
                    // Set up the table headers
                    table = "<table id='tblNotifications' class='table table-condensed sortable'>"
                    + "<thead>"
                        + "<tr>"
                            + "<td style='font-weight:bold;'>Priority</td>"
                            + "<td style='font-weight:bold;'>Title</td>"
                            + "<td style='font-weight:bold;'>Description</td>"
                            + "<td style='font-weight:bold;'>Date</td>"
                            + "<td style='font-weight:bold;'>Action</td>"
                        + "</tr>"
                    + "</thead>"
                    + "<tbody>";

                    // Loop through all notifications and add the data back to the table
                    for (var i = 0; i < n.d.length; i++) {

                        // Parse the date into a string MM/DD/YYYY
                        var date = new Date(parseInt(n.d[i].NotificationDate.substr(6))),
                            currentTime = new Date(date),
                            month = currentTime.getMonth() + 1,
                            day = currentTime.getDate(),
                            year = currentTime.getFullYear(),
                            date = month + "/" + day + "/" + year;

                        // Check priority level and apply appropriate class
                        var className = "";
                        switch (n.d[i].Priority) {
                            case "High":
                                className = "text-danger";
                                break;
                            case "Low":
                                className = "text-success";
                                break;
                            case "Normal":
                                className = "text-warning";
                                break;
                            default:
                                className = "text-primary";
                                break;
                        }

                        // Add the rows to the table
                        table += "<tr>"
                            + "<td class='" + className + "'>" + n.d[i].Priority + "</td>"
                            + "<td style='width: 150px;'>" + n.d[i].Title + "</td>"
                            + "<td style='width: 300px;'>" + n.d[i].Description + "</td>"
                            + "<td>" + date + "</td>"

                            + "<td class='text-center'><a class='btn btn-default btn-xs' data-toggle='tooltip' data-id='" + n.d[i].Id + "' title='Mark As Read' name='read'><span class='glyphicon glyphicon-pushpin'></span></a></td>"
                        + "</tr>"
                    }
                    table += "</tbody></table>";
                }

                // Replace modal body with new updated table
                $("#mdlNotifications .modal-body").html(table);

                // Update the notification count after message is marked as read
                $("#notificationCount").text(n.d.length);
            },
            error: function (xhr) {
                console.log(xhr);
            }
        });
    });

    // Read Notification
    $("#mdlNotifications .modal-body").on("click", "a[name='read']", function () {
        var notificationId = $(this).attr("data-id");
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: "/WebServices/Notifications.asmx/UpdateNotification",
            data: JSON.stringify({ 'id': notificationId }),
            dataType: "json",
            success: function (n) {
                console.log(n);
                // Start the table with the headers
                var table = "";

                // Check for empty result set
                if (n.d.length == 0) {

                    // Display notification if there are no records to show
                    table = "<label><b>No notifications to show.</b></label>";
                }
                else {
                    // Set up the table headers
                    table = "<table id='tblNotifications' class='table table-condensed sortable'>"
                    + "<thead>"
                        + "<tr>"
                            + "<td style='font-weight:bold;'>Priority</td>"
                            + "<td style='font-weight:bold;'>Title</td>"
                            + "<td style='font-weight:bold;'>Description</td>"
                            + "<td style='font-weight:bold;'>Date</td>"
                            + "<td style='font-weight:bold;'>Action</td>"
                        + "</tr>"
                    + "</thead>"
                    + "<tbody>";

                    // Loop through all notifications and add the data back to the table
                    for (var i = 0; i < n.d.length; i++) {

                        // Parse the date into a string MM/DD/YYYY
                        var date = new Date(parseInt(n.d[i].NotificationDate.substr(6))),
                            currentTime = new Date(date),
                            month = currentTime.getMonth() + 1,
                            day = currentTime.getDate(),
                            year = currentTime.getFullYear(),
                            date = month + "/" + day + "/" + year;

                        // Check priority level and apply appropriate class
                        var className = "";
                        switch (n.d[i].Priority) {
                            case "High":
                                className = "text-danger";
                                break;
                            case "Low":
                                className = "text-success";
                                break;
                            case "Normal":
                                className = "text-warning";
                                break;
                            default:
                                className = "text-primary";
                                break;
                        }

                        // Add the rows to the table
                        table += "<tr>"
                            + "<td class='" + className + "'>" + n.d[i].Priority + "</td>"
                            + "<td style='width: 150px;'>" + n.d[i].Title + "</td>"
                            + "<td style='width: 300px;'>" + n.d[i].Description + "</td>"
                            + "<td>" + date + "</td>"

                            + "<td class='text-center'><a class='btn btn-default btn-xs' data-toggle='tooltip' data-id='" + n.d[i].Id + "' title='Mark As Read' name='read'><span class='glyphicon glyphicon-pushpin'></span></a></td>"
                        + "</tr>"
                    }
                    table += "</tbody></table>";
                }

                // Replace modal body with new updated table
                $("#mdlNotifications .modal-body").html(table);

                // Update the notification count after message is marked as read
                $("#notificationCount").text(n.d.length);
            },
            error: function (xhr) {
                console.log(xhr);
            }
        });
    });

    // Mobile Public Calendar Event Modal
    $("#mdlEvents .modal-body").on("click", "a[name='mobCalEvnt']", function () {
        var evntId = $(this).attr("data-id");

        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: "/WebServices/Calendar.asmx/GetCalendarEventInformation",
            data: JSON.stringify({ 'id': evntId }),
            dataType: "json",
            success: function (n) {
                console.log(n);

                // Parse the date into a string MM/DD/YYYY
                var date = new Date(parseInt(n.d[0].CalendarEventDate.substr(6))),
                    currentTime = new Date(date),
                    month = currentTime.getMonth() + 1,
                    day = currentTime.getDate(),
                    year = currentTime.getFullYear(),
                    date = month + "/" + day + "/" + year;

                // Parse the time
                var hours = parseInt(n.d[1].substr(0, 2));
                var suffix = (hours >= 12) ? " PM" : " AM";
                hours = ((hours + 11) % 12 + 1);
                var minutes = n.d[1].substr(3);
                var time = hours + ":" + minutes + suffix;

                // Fill the modal information
                $("#hdrMasterEventModalTitle").html("<b>" + n.d[0].CalendarEventName + "</b> - " + currentTime.toDateString());
                $("#txtMasterEventName").val(n.d[0].CalendarEventName);
                $("#txtMasterEventDate").val(date);
                $("#txtMasterEventTime").val(time);
                $("#txtMasterEventDescription").val(n.d[0].CalendarEventDescription);
                $('#mdlMasterEventsCalendar').modal('show');
            },
            error: function (xhr) {
                console.log(xhr);
            }
        });
    });

    // Public Calendar Event Modal
    $("#mdlEvents .modal-body").on("click", "a[name='calendar-event']", function () {
        var eventId = $(this).attr("data-id");

        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: "/WebServices/Calendar.asmx/GetCalendarEventInformation",
            data: JSON.stringify({ 'id': eventId }),
            dataType: "json",
            success: function (n) {
                console.log(n);

                // Parse the date into a string MM/DD/YYYY
                var date = new Date(parseInt(n.d[0].CalendarEventDate.substr(6))),
                    currentTime = new Date(date),
                    month = currentTime.getMonth() + 1,
                    day = currentTime.getDate(),
                    year = currentTime.getFullYear(),
                    date = month + "/" + day + "/" + year;

                // Parse the time
                var hours = parseInt(n.d[1].substr(0, 2));
                var suffix = (hours >= 12) ? " PM" : " AM";
                hours = ((hours + 11) % 12 + 1);
                var minutes = n.d[1].substr(3);
                var time = hours + ":" + minutes + suffix;

                // Fill the modal information
                $("#hdrMasterEventModalTitle").html("<b>" + n.d[0].CalendarEventName + "</b> - " + currentTime.toDateString());
                $("#txtMasterEventName").val(n.d[0].CalendarEventName);
                $("#txtMasterEventDate").val(date);
                $("#txtMasterEventTime").val(time);
                $("#txtMasterEventDescription").val(n.d[0].CalendarEventDescription);
                $('#mdlMasterEventsCalendar').modal('show');
            },
            error: function (xhr) {
                console.log(xhr);
            }
        });
    });
});

$(function () {
    $(window).scroll(function () {
        if ($(this).scrollTop() >= 150) {        // If page is scrolled more than 50px
            $('#ribbon').css('opacity', 1);    // Fade in the arrow
        } else {
            $('#ribbon').css('opacity', 0);   // Else fade out the arrow
        }
    });
    $(".panelTabs").on("click", function () {
        var activePanel = $(this).attr("id");
        console.log("Clicked ID", $(this).attr("id"));

        $(".panelTabs").each(function () {
            if ($(this).attr("id") == activePanel) {
                console.log("This ID", $(this).attr("id"));
                $(this).addClass("activePanel");
            }

            else {
                $(this).removeClass("activePanel");
            }
        });
    });

    //This enables the text filtering on the activity tracking page
    $('#search').on("keyup", filterRows);
});

function filterRows() {
    var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();
    var $rows = $('#Body_tblActivityData tbody tr');
    $rows.show().filter(function () {
        var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
        return !~text.indexOf(val);
    }).hide();
};








