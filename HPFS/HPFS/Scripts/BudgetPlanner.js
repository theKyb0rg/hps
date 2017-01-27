function submitIncome() {
    document.getElementById('response').style.display = "";
    document.getElementById('graph').style.display = "";

    var range = document.getElementById('ddlType');
    var income = document.getElementById('txtIncome').value;
    var goalTotal = document.getElementById('txtGoal').value;
    var dividend = document.getElementById('txtNumRange').value;

    var goal = goalTotal / dividend;

    var budget = income - goal;

    var housing = budget * 0.30;

    var utilities = budget * 0.07;

    var transportation = budget * 0.13;

    var healthcare = budget * 0.07;

    var food = budget * 0.14;

    var debt = budget * 0.10;

    var recreation = budget * 0.09;

    var misc = budget * 0.10;

    document.getElementById('txtHousing').value = housing.toFixed(2);

    document.getElementById('txtUtilities').value = utilities.toFixed(2);

    document.getElementById('txtTransportation').value = transportation.toFixed(2);

    document.getElementById('txtHealthcare').value = healthcare.toFixed(2);

    document.getElementById('txtFood').value = food.toFixed(2);

    document.getElementById('txtSavings').value = Number(goal).toFixed(2);

    document.getElementById('txtDebtPayments').value = debt.toFixed(2);

    document.getElementById('txtRecreation').value = recreation.toFixed(2);

    document.getElementById('txtMisc').value = misc.toFixed(2);

    document.getElementById('headingResult').innerText = "Your " + range.options[range.selectedIndex].text + " Budget";

    //generate a graph... 
    var chart = c3.generate({
        bindto: '#graph',
        data: {
            // iris data from R
            columns: [
                ['Housing', housing],
                ['Utilities', utilities],
                ['Transportation', transportation],
                ['Healthcare', healthcare],
                ['Food', food],
                ['Savings', goal],
                ['Debt', debt],
                ['Recreation', recreation],
                ['Misc', misc]
            ],
            type: 'pie',
            onclick: function (d, i) { console.log("onclick", d, i); },
            onmouseover: function (d, i) { console.log("onmouseover", d, i); },
            onmouseout: function (d, i) { console.log("onmouseout", d, i); }
        }
    });

    //set the number of days to multiply the range by
    //(ie. if the user chose weekly, multiply the number of weeks by 7)
    if (range.options[range.selectedIndex].text == "Weekly") {
        var days = 7;
    }
        //(or if the user chose monthly, multiply the number of months by 28 (4 weeks))
    else if (range.options[range.selectedIndex].text == "Monthly") {
        var days = 28;
    }
        //(or if the user chose yearly, multiply the number of years by 365)
    else if (range.options[range.selectedIndex].text == "Yearly") {
        var days = 365;
    }

    var curDate = new Date();
    var goalDate = new Date();

    //set the goal date as x number of days from today
    goalDate.setDate(curDate.getDate() + (days * dividend));
    //format the date string
    goalDate = (goalDate.getMonth() + 1) + "/" + goalDate.getDate() + "/" + goalDate.getFullYear()

    //output the results
    var output = "In order to save $" + Number(goalTotal).toFixed(2) + " by " + goalDate + " you will need to save $"

    //if the user chose weekly, output just the weekly amount
    if (days == 7) {
        output += goal.toFixed(2) + " each week.";
    }
        //if the user chose monthly, output the monthly AND weekly amounts.
    else if (days == 28) {
        output += (goal / 4).toFixed(2) + " each week, or $";
        output += goal.toFixed(2) + " each month.";
    }
        //if the user chose yearly, output the yearly, monthly, AND weekly amounts.
    else if (days == 365) {
        output += (goal / 52).toFixed(2) + " each week, or $";
        output += (goal / 12).toFixed(2) + " each month, or $";
        output += goal.toFixed(2) + " each year.";
    }
    //output the final string
    document.getElementById('result').innerText = output;


}
function onChange(sel) {
    //Selected a type of budget
    var e = sel.options[sel.selectedIndex].text;

    //Set the labels accordingly
    if (e == "Weekly") {
        document.getElementById('lblIncome').innerText = "Enter your weekly income";
        document.getElementById('lblRange').innerText = "Enter the # of weeks";
    }
    else if (e == "Monthly") {
        document.getElementById('lblIncome').innerText = "Enter your monthly income";
        document.getElementById('lblRange').innerText = "Enter the # of months";
    }
    else {
        document.getElementById('lblIncome').innerText = "Enter your annual income";
        document.getElementById('lblRange').innerText = "Enter the # of years";
    }

    //remove the readonly & placeholders
    document.getElementById('txtIncome').removeAttribute("readonly");
    document.getElementById('txtIncome').removeAttribute("placeholder");
    document.getElementById('txtGoal').removeAttribute("readonly");
    document.getElementById('txtGoal').removeAttribute("placeholder");
    document.getElementById('txtNumRange').removeAttribute("readonly");
}
function preparePrint() {
    $(".print").addClass("printHide");
    $(".footer").addClass("printHide");
    $("body").attr("style", "padding-top:0px;");
    $("#content").attr("style", "padding-bottom:0px;")

    $("#printFix").attr("class", "col-xs-7 text-center");
    $("#buffer").removeAttr("style");
    $("#printFix").attr("style", "margin-left:25px;");
    submitIncome();

    setTimeout(function () {

        window.print();
        $("body").removeAttr("style");
        $("#printFix").attr("class", "col-xs-12 text-center");
        $("#buffer").attr("style", "display:none;");
        $("#content").removeAttr("style");
        $("#graph").removeAttr("style");
        $(".print").removeClass("printHide");
        $(".footer").removeClass("printHide");
    }, 200);
}