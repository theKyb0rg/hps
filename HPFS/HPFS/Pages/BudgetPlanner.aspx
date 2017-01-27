<%@ Page Title="" Language="C#" MasterPageFile="~/HPFSMaster.Master" AutoEventWireup="true" CodeBehind="BudgetPlanner.aspx.cs" Inherits="HPFS.Pages.BudgetPlanner" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">Budget Planner</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="insideHead" runat="server">
    <script src="../Scripts/BudgetPlanner.js"></script>
    <script src="../Scripts/printElement.min.js"></script>
    <link href="../Content/css/pages/BudgetPlanner.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="server">
    <div class="row print">
        <div class="col-xs-12 text-center">
            <h1>Budget Planner</h1>
            <hr />
        </div>
    </div>
    <div class="row print">
        <div class="col-xs-12 col-sm-2">
            <label for="ddlType">Choose a Timeframe</label>
            <select id="ddlType" class="form-control" onchange="onChange(this)">
                <option id="none" disabled="disabled" selected="selected">Please Select</option>
                <option id="weekly">Weekly</option>
                <option id="montly">Monthly</option>
                <option id="yearly">Yearly</option>
            </select>
        </div>
        <div class="col-xs-12 col-sm-4">
            <label id="lblIncome" for="txtIncome">Enter your income</label>
            <div class="input-group">
                <div class="input-group-addon">$</div>
                <input type="number" class="form-control" id="txtIncome" readonly="true" placeholder="Please select a timeframe" />
            </div>
        </div>
        <div class="col-xs-12 col-sm-4">
            <label for="txtGoal">Enter a savings goal</label>
            <div class="input-group">
                <div class="input-group-addon">$</div>
                <input type="number" class="form-control" id="txtGoal" readonly="true" placeholder="Please select a timeframe" />
            </div>
        </div>
        <div class="col-xs-12 col-sm-2">
            <label id="lblRange" for="txtNumRange">Choose range</label>
            <input type="text" class="form-control" id="txtNumRange" readonly="true" />
        </div>
    </div>
    <div class="row print">
        <div class="col-xs-12 text-center">
            <input type="button" class="btn btn-default" onclick="submitIncome()" value="Submit" id="btnSubmitIncome" />
            <hr />
        </div>
    </div>
    <div id="response" style="display: none;">
        <div class="row">
            <div class="col-xs-12 text-center">
                <h1 id="headingResult"></h1>
                <br />
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12 text-center" id="printFix" >
                <div id="graph" class="text-center"></div>
            </div>
            <div class="col-xs-5" id="buffer" style="display:none;"></div>
            
        </div>
        <div class="row">
            <div class="col-xs-6 col-sm-6">
                <label for="txtHousing">Housing: </label>
                <div class="input-group">
                    <div class="input-group-addon">$</div>
                    <input type="text" id="txtHousing" class="form-control" />
                </div>
            </div>
            <div class="col-xs-6 col-sm-6">
                <label for="txtUtilities">Utilities: </label>
                <div class="input-group">
                    <div class="input-group-addon">$</div>
                    <input type="text" id="txtUtilities" class="form-control" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-6 col-sm-6">
                <label for="txtTransportation">Transportation: </label>
                <div class="input-group">
                    <div class="input-group-addon">$</div>
                    <input type="text" id="txtTransportation" class="form-control" />
                </div>
            </div>
            <div class="col-xs-6 col-sm-6">
                <label for="txtHealthcare">Healthcare: </label>
                <div class="input-group">
                    <div class="input-group-addon">$</div>
                    <input type="text" id="txtHealthcare" class="form-control" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-6 col-sm-6">
                <label for="txtFood">Food: </label>
                <div class="input-group">
                    <div class="input-group-addon">$</div>
                    <input type="text" id="txtFood" class="form-control" />
                </div>
            </div>
            <div class="col-xs-6 col-sm-6">
                <label for="txtSavings">Savings: </label>
                <div class="input-group">
                    <div class="input-group-addon">$</div>
                    <input type="text" id="txtSavings" class="form-control" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-6 col-sm-6">
                <label for="txtDebtPayments">Debt Payments: </label>
                <div class="input-group">
                    <div class="input-group-addon">$</div>
                    <input type="text" id="txtDebtPayments" class="form-control" />
                </div>
            </div>
            <div class="col-xs-6 col-sm-6">
                <label for="txtRecreation">Recreation: </label>
                <div class="input-group">
                    <div class="input-group-addon">$</div>
                    <input type="text" id="txtRecreation" class="form-control" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12">
                <label for="txtMisc">Miscellaneous: </label>
                <div class="input-group">
                    <div class="input-group-addon">$</div>
                    <input type="text" id="txtMisc" class="form-control" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12">
                <p id="result"></p>
            </div>
        </div>
        <div class="row print">
            <div class="col-xs-12 text-center">
                <a class="btn btn-default" id="printPage" onclick="preparePrint()">
                    <span class="glyphicon glyphicon-print"></span>
                </a>
            </div>
        </div>
    </div>
</asp:Content>
