<%@ Page Title="" Language="C#" MasterPageFile="~/HPFSMaster.Master" AutoEventWireup="true" CodeBehind="RehabilitationAndTreatment.aspx.cs" Inherits="HPFS.RehabilitationAndTreatment" %>

<asp:Content ID="Content3" ContentPlaceHolderID="insideHead" runat="server">
    <link href="../Content/css/pages/Rehabilitation.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">HPS - Rehabilitation and Treatment</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <h1 class="text-center">Rehabilitation & Treatment</h1>
    <hr />

    <%--NAV TABS--%>
    <ul class="nav nav-tabs nav-justified" id="RehabilitationPage_NavTabs">
        <li class="active"><a data-toggle="tab" href="#Tutorial">General Information</a></li>
        <li><a data-toggle="tab" href="#CaseManagement">Case Management</a></li>
        <li><a data-toggle="tab" href="#PsychiatricCare">Psychiatric Care</a></li>
        <li><a data-toggle="tab" href="#Rehabilitation">Rehabilitation</a></li>
    </ul>

    <div class="tab-content">
        <%--MAIN CONTENT TAB--%>
        <div id="Tutorial" class="tab-pane fade in active">
            <div class="row folder" style="padding-bottom: 20px;">
                <div class="col-xs-12 remove-padding-mobile">
                    <%--<div id="TutorialCarousel" class="carousel slide" data-ride="carousel">
                        <!-- Indicators -->
                        <ol class="carousel-indicators">
                            <li data-target="#TutorialCarousel" data-slide-to="0" class="active"></li>
                            <li data-target="#TutorialCarousel" data-slide-to="1"></li>
                            <li data-target="#TutorialCarousel" data-slide-to="2"></li>
                            <li data-target="#TutorialCarousel" data-slide-to="3"></li>
                        </ol>
                        <div class="carousel-inner" role="listbox" style="background-color: lightgrey;">
                            <div class="item active">
                                <img src="http://www.placehold.it/500x250" alt="" class="img-responsive center-block" />
                            </div>
                            <div class="item">
                                <img src="http://www.placehold.it/500x250" alt="" class="img-responsive center-block" />
                            </div>
                            <div class="item">
                                <img src="http://www.placehold.it/500x250" alt="" class="img-responsive center-block" />
                            </div>
                            <div class="item">
                                <img src="http://www.placehold.it/500x250" alt="" class="img-responsive center-block" />
                            </div>
                        </div>
                        <a class="left carousel-control" href="#TutorialCarousel" role="button" data-slide="prev">
                            <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
                            <span class="sr-only">Previous</span>
                        </a>
                        <a class="right carousel-control" href="#TutorialCarousel" role="button" data-slide="next">
                            <span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
                            <span class="sr-only">Next</span>
                        </a>
                    </div>--%>
                </div>
                <div class="col-xs-12">
                    <br />
                    <h2>General Information</h2>
                    <br />
                    <p>
                        Psychosocial Rehabilitation is an important part of recovery and is an essential feature of the Hamilton Program for Schizophrenia. 
        We offer rehabilitation to clients through programs, as well as through living, learning, working, social and recreational activities. 
        Our rehabilitation programs follow evidence-based practices; that is, we offer skill training that has been shown to be effective in increasing success in living in the community. 
        Our groups have peer support workers who contribute as a part of the client-centred team. 
        A wide range of interventions and social and recreational opportunities are offered and we encourage clients to participate in those areas which fit specific goals and learning needs.
                    </p>
                </div>
            </div>
        </div>

        <%--CASE MANAGEMENT TAB--%>
        <div id="CaseManagement" class="tab-pane fade">
            <div class="row folder" style="padding-bottom: 20px;">
                <div class="col-xs-12 remove-padding-mobile">
                    <%--<div id="CaseManagementCarousel" class="carousel slide" data-ride="carousel">
                        <!-- Indicators -->
                        <ol class="carousel-indicators">
                            <li data-target="#CaseManagementCarousel" data-slide-to="0" class="active"></li>
                            <li data-target="#CaseManagementCarousel" data-slide-to="1"></li>
                            <li data-target="#CaseManagementCarousel" data-slide-to="2"></li>
                            <li data-target="#CaseManagementCarousel" data-slide-to="3"></li>
                        </ol>
                        <div class="carousel-inner" role="listbox" style="background-color: lightgrey;">
                            <div class="item active">
                                <img src="http://www.placehold.it/500x250" alt="" class="img-responsive center-block" />
                            </div>
                            <div class="item">
                                <img src="http://www.placehold.it/500x250" alt="" class="img-responsive center-block" />
                            </div>
                            <div class="item">
                                <img src="http://www.placehold.it/500x250" alt="" class="img-responsive center-block" />
                            </div>
                            <div class="item">
                                <img src="http://www.placehold.it/500x250" alt="" class="img-responsive center-block" />
                            </div>
                        </div>
                        <a class="left carousel-control" id="chevLeft" href="#CaseManagementCarousel" role="button" data-slide="prev">
                            <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
                            <span class="sr-only">Previous</span>
                        </a>
                        <a class="right carousel-control" id="chevRight" href="#CaseManagementCarousel" role="button" data-slide="next">
                            <span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
                            <span class="sr-only">Next</span>
                        </a>
                    </div>--%>
                </div>
                <div class="col-xs-12">
                    <br />
                    <h2>Case Management</h2>
                    <br />
                    <p>
                        Every client develops an individual service plan with their case manager. 
                        The goal is to increase the client’s satisfaction and success in the areas of living, learning, work and social life. 
                        Each client is assigned to a specific case manager; however, the treatment and rehabilitation work is shared with the client’s psychiatrist and the rest of the HPS team. 
                        HPS case managers are experienced clinicians with professional training in the disciplines of nursing, occupational therapy, psychology and social work. 
                        The HPS team forms a therapeutic alliance with the client which becomes the basis for their personal growth, change and sustained hope in managing the challenges arising from schizophrenia.
                    </p>
                    <br />
                    <p>
                        In addition to the individual treatment and service provided by specific case managers, we have developed a variety of group programs. 
                        Here clients work together to achieve common goals such as quitting smoking, learning to eat nutritiously and inexpensively, succeeding in employment situations, or seeking an artistic outlet. 
                        These groups are beneficial in providing opportunities for socialization, normalization of the individual’s experience, peer encouragement and the enhancement of self-esteem.
                    </p>
                </div>
            </div>
        </div>

        <%--PSYCHIATRIC CARE TAB--%>
        <div id="PsychiatricCare" class="tab-pane fade">
            <div class="row folder" style="padding-bottom: 20px;">
                <div class="col-xs-12 remove-padding-mobile">
                    <%--<div id="PsychiatricCareCarousel" class="carousel slide" data-ride="carousel">
                        <!-- Indicators -->
                        <ol class="carousel-indicators">
                            <li data-target="#PsychiatricCareCarousel" data-slide-to="0" class="active"></li>
                            <li data-target="#PsychiatricCareCarousel" data-slide-to="1"></li>
                            <li data-target="#PsychiatricCareCarousel" data-slide-to="2"></li>
                            <li data-target="#PsychiatricCareCarousel" data-slide-to="3"></li>
                        </ol>
                        <div class="carousel-inner" role="listbox" style="background-color: lightgrey;">
                            <div class="item active">
                                <img src="http://www.placehold.it/500x250" alt="" class="img-responsive center-block" />
                            </div>
                            <div class="item">
                                <img src="http://www.placehold.it/500x250" alt="" class="img-responsive center-block" />
                            </div>
                            <div class="item">
                                <img src="http://www.placehold.it/500x250" alt="" class="img-responsive center-block" />
                            </div>
                            <div class="item">
                                <img src="http://www.placehold.it/500x250" alt="" class="img-responsive center-block" />
                            </div>
                        </div>
                        <a class="left carousel-control" href="#PsychiatricCare" role="button" data-slide="prev">
                            <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
                            <span class="sr-only">Previous</span>
                        </a>
                        <a class="right carousel-control" href="#PsychiatricCare" role="button" data-slide="next">
                            <span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
                            <span class="sr-only">Next</span>
                        </a>
                    </div>--%>
                </div>
                <div class="col-xs-12">
                    <br />
                    <h2>Psychiatric Care</h2>
                    <br />
                    <p>
                        Schizophrenia is a serious medical disorder that affects 1% of the population. HPS psychiatrists prescribe medication treatments for the psychiatric symptoms associated with schizophrenia. 
                        In addition, we have developed support linkages with St. Joseph’s Healthcare, Centre for Mountain Health Services, laboratory services, pharmacies and family doctors. 
                        Working with the HPS staff, these community partners are an important part of providing our clients with essential psychiatric and medical care in the community. 
                        We work collaboratively with over 80 family physicians who assist with clients’ physical needs. HPS clients utilize community and hospital pharmacies to dispense their medications.
                    </p>
                </div>
            </div>
        </div>

        <%--REHABILITATION TAB--%>
        <div id="Rehabilitation" class="tab-pane fade">
            <div class="row folder" style="padding-bottom: 20px;">
                <div class="col-xs-12 remove-padding-mobile" >
                    <%--<div id="RehabCarousel" class="carousel slide" data-ride="carousel">
                        <!-- Indicators -->
                        <ol class="carousel-indicators">
                            <li data-target="#RehabCarousel" data-slide-to="0" class="active"></li>
                            <li data-target="#RehabCarousel" data-slide-to="1"></li>
                            <li data-target="#RehabCarousel" data-slide-to="2"></li>
                            <li data-target="#RehabCarousel" data-slide-to="3"></li>
                        </ol>
                        <div class="carousel-inner" role="listbox" style="background-color: lightgrey;">
                            <div class="item active">
                                <img src="http://www.placehold.it/500x250" alt="" class="img-responsive center-block" />
                            </div>
                            <div class="item">
                                <img src="http://www.placehold.it/500x250" alt="" class="img-responsive center-block" />
                            </div>
                            <div class="item">
                                <img src="http://www.placehold.it/500x250" alt="" class="img-responsive center-block" />
                            </div>
                            <div class="item">
                                <img src="http://www.placehold.it/500x250" alt="" class="img-responsive center-block" />
                            </div>
                        </div>
                        <a class="left carousel-control" href="#RehabCarousel" role="button" data-slide="prev">
                            <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
                            <span class="sr-only">Previous</span>
                        </a>
                        <a class="right carousel-control" href="#RehabCarousel" role="button" data-slide="next">
                            <span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
                            <span class="sr-only">Next</span>
                        </a>
                    </div>--%>
                </div>
                <div class="col-xs-12">
                    <br />
                    <h2>Rehabilitation</h2>
                    <br />
                    <p>
                        Schizophrenia rehabilitation attempts to increase an individual’s level of functioning. 
                        The aim is to build upon strengths and assets while reducing deficits. 
                        “The goal of rehabilitation is to nurture the strengths and life skills that the patient with schizophrenia requires to live as independently as possible in the community” (Lalonde, 1995, p. 71). 
                        The ability to enjoy a quality of life comparable to that of others is also foremost in rehabilitation. 
                        This paper will focus on the wide range of interventions that are implemented in rehabilitation. 
                        In order for these interventions to be effective, they must be comprehensive, continuous, coordinated and all encompassing.
                    </p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
