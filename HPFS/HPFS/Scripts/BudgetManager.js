//Budget Manager Scripts

//Monthly Budget Scripts
function monthClick() {
    //Hide Warning/ Show Results
    document.getElementById("warning").style = "display:none";
    document.getElementById("results").style = "display:normal";

    //Set Active Heading
    x = document.getElementsByClassName("triPanel");
    for (i = 0; i < x.length; i++) {
        if (x[i].className == "triPanel active") {
            x[i].className = "triPanel";
        }
        if (i == 0) {
            x[i].className = "triPanel active";
        }

    }

    //Change Active Style
    var x = document.getElementsByClassName("months");
    for (i = 0; i < x.length; i++) {
        x[i].className = "btn btn-default months";
    }

    var clicked = event.target;
    clicked.className = "btn btn-info months";

    var month = clicked.text;
    var year = document.getElementById("txtYear").innerText;

    var result = PageMethods.GetMonth(month, year, monthSuccess, failure);
}
function monthSuccess(result) {

    var bodyGoal = document.getElementById("Body_goal"),
        bodyCurrent = document.getElementById("Body_expenses"),
        bodyDif = document.getElementById("Body_difference"),
        results = result.split("*"),
        goal = results[0];

    if (goal != 0) {
        goal = parseFloat(goal);
        bodyGoal.innerText = "$" + goal.toFixed(2);
    }
    else {
        bodyGoal.innerText = "No Data for this Month";
    }
    var current = results[1];
    if (current != 0) {
        current = parseFloat(current);
        bodyCurrent.innerText = "$" + current.toFixed(2);
    }
    else {
        bodyCurrent.innerText = "No Data for this Month";
    }
    var dif = results[2];
    dif = parseFloat(dif);
    if (dif < 0) {
        difSplit = dif.toString();
        difSplit = difSplit.split("-");
        difSplit[1] = parseFloat(difSplit[1]);
        bodyDif.style = "color:red;";
        bodyDif.innerText = "-$" + difSplit[1].toFixed(2);

    }
    else {
        bodyDif.style = "color:black;";
        bodyDif.innerText = "$" + dif.toFixed(2);
    }

    //Budget Manager Monthly Graph
    if (dif < 0) {
        var secondary = "#a50b0b";
        var altText = "Overage";
        alt = Math.abs(dif);
    }
    else {
        var secondary = "#222";
        var altText = "Spendable Income";
        alt = dif;
    }
    var chart = c3.generate({
        bindto: "#graph",
        data: {
            columns: [
                [altText, alt], ['Expenses', current]
            ],
            type: 'pie',
            onclick: function (d, i) { console.log("onclick", d, i); },
            onmouseover: function (d, i) { console.log("onmouseover", d, i); },
            onmouseout: function (d, i) { console.log("onmouseout", d, i); }
        },

        pie: {
            label: {
                format: function (value, ratio, id) {
                    return d3.format("$")(value);
                }
            },
            title: {
                text: 'My Title'
            },
        },
        color: {
            pattern: [secondary, '#4E3691']
        }

    })

}
//Daily Budget Scripts
function dayClick() {
    //change active style
    x = document.getElementsByClassName("triPanel");
    for (i = 0; i < x.length; i++) {
        if (x[i].className == "triPanel active") {
            x[i].className = "triPanel";
        }
    }
    var clicked = event.target;
    clicked.className = "triPanel active";

    //get the currently selected month
    var months = document.getElementsByClassName('btn btn-info months');
    for (i = 0; i < months.length; i++) {
        month = months[i].innerText;
    }
    //get the currently selected year
    var year = document.getElementById('txtYear').innerText;

    //pass the year and month to the C# method GetDay
    var result = PageMethods.GetDay(month, year, daySuccess, failure);
}
function daySuccess(results) {
    //Split the string into each day (delimited by &)
    results = results.split("&");

    //Then at each index split the index into another array (creating a multidimensional array of days and their value/day#/category)
    for (i = 0; i < results.length; i++) {
        results[i] = results[i].split("*");
    }

    //get the currently selected month
    var months = document.getElementsByClassName('btn btn-info months');
    for (i = 0; i < months.length; i++) {
        month = months[i].innerText;
    }

    //get the number of days in the month that is currently selected
    switch (month) {
        case "Jan": month = 1; days = 31; break;
        case "Feb": month = 2; days = 28; break;
        case "Mar": month = 3; days = 31; break;
        case "Apr": month = 4; days = 30; break;
        case "May": month = 5; days = 31; break;
        case "Jun": month = 6; days = 30; break;
        case "Jul": month = 7; days = 31; break;
        case "Aug": month = 8; days = 31; break;
        case "Sep": month = 9; days = 30; break;
        case "Oct": month = 10; days = 31; break;
        case "Nov": month = 11; days = 30; break;
        case "Dec": month = 12; days = 31; break;
        default: month = 1; days = 1; break;
    }
    //Fill in all of the days (up to the # of days in that month)
    var monthLengthArray = [];
    for (i = 0; i < days; i++) {
        monthLengthArray[i] = i + 1;
    }
    var daysArray = [];
    for (i = 0; i <= days; i++) {
        if (i == 0) {
            daysArray[i] = 'Expenses';
        }
        else {
            daysArray[i] = "0";
        }
    }

    //Assign daysArray the day number and it's value (or leave it 0 if there isn't one)
    for (i = 0; i < results.length; i++) {
        //if there is no value for this day (0)
        if (daysArray[Number(results[i][1])] == 0) {
            //then -> add the value to this day
            daysArray[Number(results[i][1])] = Number(results[i][0]);
        }
            //otherwise -> add it to the existing value
        else {
            daysArray[Number(results[i][1])] += Number(results[i][0]);
        }

    }
    //Get the 'Target Line' value
    var goal = document.getElementById("Body_goal").innerText;
    var dayTarget = Number(goal) / days;

    //Day Graph
    var chart = c3.generate({
        bindto: '#graph',
        data: {
            columns: [
                daysArray
            ],
            type: 'bar'
        },
        axis: {
            x: {
                tick: {
                    format: function (x) { return x + 1; }
                }
            }
        },
        bar: {
            width: {
                ratio: 1
            }
        }
        //grid: {
        //    y: {
        //        lines: [
        //            { value: dayTarget, text: 'Daily Target' },
        //        ]
        //    }
        //}
    });
}
//Click the Total Button
function totalClick() {

    //Change Active Style
    var x = document.getElementsByClassName("months");
    for (i = 0; i < x.length; i++) {
        if (x[i].className == "btn btn-info months") {
            month = x[i].innerText;
        }
    }

    x = document.getElementsByClassName("triPanel");
    for (i = 0; i < x.length; i++) {
        if (x[i].className == "triPanel active") {
            x[i].className = "triPanel";
        }
    }
    var clicked = event.target;
    clicked.className = "triPanel active";

    var year = document.getElementById("txtYear").innerText;

    var result = PageMethods.GetMonth(month, year, monthSuccess, failure);
}

function failure() {
    alert("oops");
}


function setYear(x) {
    document.getElementById("txtYear").innerText = x.toString();
}

function yearLeft() {
    //Hide Results/ Show Warning
    document.getElementById("warning").style = "display:normal";
    document.getElementById("results").style = "display:none";

    var months = document.getElementsByClassName("months");
    for (i = 0; i < months.length; i++) {
        months[i].className = "btn btn-default months";
    }
    var x = document.getElementById("txtYear").innerText;
    x = Number(x);
    if (x > 2000) {
        x--;
        setYear(x);
    }
    else {
        setYear(x);
    }
}
function yearRight() {
    //Hide Results/ Show Warning
    document.getElementById("warning").style = "display:normal";
    document.getElementById("results").style = "display:none";


    var months = document.getElementsByClassName("months");
    for (i = 0; i < months.length; i++) {
        months[i].className = "btn btn-default months";
    }

    var x = document.getElementById("txtYear").innerText;
    x = Number(x);
    if (x < 2020) {
        x++;
        setYear(x);
    }
    else {
        setYear(x);
    }
}