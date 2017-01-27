$(function () {

    function swapControls() {
        console.log($('#Body_current').children().length);
        if ($('#Body_current').children().length - 3 > 0) {
            // Hide the text and show the save button
            $('#currentText').hide();
            $('.save').show();
        }
        else {
            // Hide the button and show the text
            $('#currentText').show();
            $('.save').hide();
        }
    }

    var dropped;
    var imageId;

    $(".currentSortable").sortable({
        items: ".sort",
        tolerance: "pointer",
        stop: function (event, ui) {
            //console.log(".currentSortable");
            console.log(dropped);
            console.log(event);
            //// Append the thumbnail to selected div
            //if (event.target.firstElementChild.id == "Body_current") {
                dropped.append(ui.item);
            //}
            //else {
            //    $(ui.draggable).detach().css({ top: 0, left: 0 }).appendTo(this);
            //}

            // Hide the button
            $('.all').find("#" + ui.item.context.childNodes[2].id).css("display", "none");

            // Swap the controls
            swapControls();

        }
    });

    $(".allSortable").sortable({
        items: ".sort",
        tolerance: "pointer",
        stop: function (event, ui) {
            //console.log(".allSortable");
            console.log(dropped);
            //console.log();

            //if (event.target.firstElementChild.id == "Body_current") {
                dropped.append(ui.item);
            //}
            //else {
            //    $(ui.draggable).detach().css({ top: 0, left: 0 }).appendTo(this);
            //}
            // Append the thumbnail to selected div

            // Show the edit button
            var button = $('.current').find("#" + ui.item.context.childNodes[2].id).css("display", "block");

            // Swap controls
            swapControls();
        }
    });

    $(".current").droppable({
        accept: ".sort",
        drop: function (event, ui) {
            //console.log(".currentDroppable");
            //$(ui.draggable).detach().css({ top: 0, left: 0 }).appendTo(this);
            dropped = $(this);
        }
    });

    $(".all").droppable({
        accept: ".sort",
        drop: function (event, ui) {
            //$(ui.draggable).detach().css({ top: 0, left: 0 }).appendTo(this);
            dropped = $(this);
        }
    });

    $("#btnSaveSlideShow").click(function () {
        var errorCounter = 0,
            lblPage = $("#Body_lblPage"),
            lblSlideShow = $("#Body_lblSlideShow");

        // Check if drop downs are selected
        if ($("#Body_ddlPage").val() == -1) {
            errorCounter++;
            lblPage.html("Slide Show: <span class='text-danger'>*</span>");
            $("#lblPageError").html("<br><label class='text-danger'>* You must select a Web Page.</label>");
        }
        else {
            lblPage.html("Slide Show:");
            $("#lblPageError").html("");
        }

        // check if slideshow dropdowns selected
        if ($("#Body_ddlSlideShow").val() == -1) {
            errorCounter++;
            lblSlideShow.html("Slide Show: <span class='text-danger'>*</span>");
            $("#lblSlideShowError").html("<br><label class='text-danger'>* You must select a Slide Show.</label>");
        }
        else {
            lblSlideShow.html("Slide Show:");
            $("#lblSlideShowError").html("");
        }

        if (errorCounter == 0) {
            // Declare array to hold image ids to be added to slideshow
            var imageArray = [];
            captionHeadingArray = [],
            captionTextArray = [];

            // get the id of the slideshow
            var slideShowId = $('#Body_ddlSlideShow').val();

            // Loop through each image div and get the id and add to the array
            $("#Body_current .sort").each(function () {
                // Get the id from the image
                var id = $(this).attr("data-id");

                // Add iamge ids to imageArray
                imageArray.push(id);
            });

            // Loop through the controls to get the caption heading text and caption text for each image to be set for a certain slideshow
            for (var i = 0; i < imageArray.length; i++) {
                // Get the text from the textboxes of all the images in the current div
                var captionHeading = $("#Body_mdl" + imageArray[i]).find("input")[0].value;
                var captionText = $("#Body_mdl" + imageArray[i]).find("input")[1].value;

                // Add the headings and text ot their respective arrays
                captionHeadingArray.push(captionHeading);
                captionTextArray.push(captionText);
            }

            var slideShowName = $("#Body_ddlSlideShow option:selected").text();
            // Call the server side method to update the database with new images for the slideshow
            PageMethods.SaveSlideShow(imageArray, slideShowId, captionHeadingArray, captionTextArray, slideShowName);
        }
        else {
            return false;
        }
    });

    // Used to clear textboxes in modals with reset button
    $("a[name='reset']").click(function () {
        var id = $(this).attr('data-id');
        $("#Body_txtCaptionHeading" + id).val("");
        $("#Body_txtCaptionText" + id).val("");
    });
});

