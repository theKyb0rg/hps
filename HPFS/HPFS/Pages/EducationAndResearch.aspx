<%@ Page Title="" Language="C#" MasterPageFile="~/HPFSMaster.Master" AutoEventWireup="true" CodeBehind="EducationAndResearch.aspx.cs" Inherits="HPFS.EducationAndResearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">HPS - Education & Research</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="insideHead" runat="server">
    <link href="../Content/css/pages/EdAndResearch.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <h1 class="text-center">Education & Research</h1>
    <hr />
    <img src="../Content/images/McMasterLogo.png" class="pull-left" height="66" width="66" style="top:-10px;" />
    <p>
        As a McMaster University Clinical Teaching Unit HPFS offers learning experiences to nursing, 
        occupational therapy, psychiatry, psychology and community college students. 
        The Noon Hour Seminar Series at HPS provides learning opportunities for HPS staff and community mental health professionals. 
        HPS has collaborated in a number of research activities. Some of these include:
    </p>
    <hr />
    <%--NAV TABS--%>
    <ul class="nav nav-tabs nav-justified" id="RehabilitationPage_NavTabs">
        <li class="active"><a data-toggle="tab" href="#InProgress">In Progress</a></li>
        <li><a data-toggle="tab" href="#Current">Current</a></li>
        <li><a data-toggle="tab" href="#Recent">Recent</a></li>
        <li><a data-toggle="tab" href="#Links">Educational Resources</a></li>
    </ul>

    <div class="tab-content">
        <%--IN PROGRESS TAB--%>
        <div id="InProgress" class="tab-pane fade in active height-adjust-mobile" style="height: 271px;">
            <div class="row folder">
                <div class="col-xs-12">
                    <br />
                    <h2>In Progress</h2>
                    <br />
                    <ul>
                        <li>Studying the Connections between Schizophrenia, Boredom and other traits (in collaboration with York University)</li>
                        <li>Finding the Light: Exploring Engulfment in Individuals with Schizophrenia (in collaboration with McMaster University and Ryerson University) St. Joseph’s Healthcare</li>
                    </ul>
                </div>
            </div>
        </div>

        <%--CURRENT TAB--%>
        <div id="Current" class="tab-pane fade height-adjust-mobile" style="height: 271px;">
            <div class="row folder">
                <div class="col-xs-12">
                    <br />
                    <h2>Current</h2>
                    <br />
                    <ul>
                        <li>Improving the Definition of Schizophrenia with Memory Measures (in collaboration with York University)</li>
                        <li>Boredom Study (in collaboration with York University)</li>
                        <li>Spirituality and Schizophrenia (in collaboration with McMaster University)</li>
                        <li>Understanding the Reasons for Smoking in People with Psychotic Disorders (in collaboration with C.A.M.H.)</li>
                    </ul>
                </div>
            </div>
        </div>

        <%--RECENT TAB--%>
        <div id="Recent" class="tab-pane fade height-adjust-mobile" style="min-height: 271px;">
            <div class="row folder">
                <div class="col-xs-12"  style="padding-bottom: 40px;">
                    <h2>Recent</h2>
                    <br />
                    <ul>
                        <li>HPS Voices Questionnaire – continuation of research in a new test instrument to evaluate auditory hallucinations</li>
                        <li>Evaluation of the Benefits of Novel Anti-Psychotic Agents after switching from Typical Medications</li>
                        <li>Research Studies examining EEGs and Shyness (in collaboration with McMaster University)</li>
                        <li>Novel Anti-Psychotics and Exercise Research Project (in collaboration with MUMC-3G)</li>
                        <li>Hope and Schizophrenia Research Project (in collaboration with School of Nursing)</li>
                        <li>Qualitative Research Study on the Effects of Schizophrenia on Personal Identity (in collaboration with York University)</li>
                        <li>Evaluation of the Recovery Workbook Group as an intervention for facilitating recovery in persons with Schizophrenia (in collaboration with McMaster University)</li>
                    </ul>
                </div>
            </div>
        </div>

        <%-- asdf--%>
        <div id="Links" class="tab-pane fade height-adjust-mobile" style="height: 271px;">
            <div class="row folder">
                <div class="col-xs-12 col-md-6">
                    <br />
                    <h2>Useful Links</h2>
                    <br />
                    <p>
                        <a href="http://www.cmhahamilton.ca/">Canadian Mental Health Association – Hamilton</a> developed by the Hamilton Niagara Haldimand Brant (HNHB) 
                            Local Health Intergration Network (LHIN) can help you learn about many of the health care services available.
                    </p>
                </div>
                <div class="col-xs-12 col-md-6">
                    <br />
                    <h3>Other mental health resources:</h3>
                    <ul style="list-style: none;">
                        <li><a href="http://www.hnhblhin.on.ca/Page.aspx?id=13136&ekmensel=e2f22c9a_72_332_13136_1">
                            <br />
                            Understanding Your Health Care Options</a></li>
                        <li><a href="http://www.mentalhealthrights.ca/">Mental Health Rights Coalition</a></li>
                        <li><a href="http://www.schizophrenia.on.ca/">Schizophrenia Society of Ontario</a></li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <%--Education--%>
</asp:Content>
