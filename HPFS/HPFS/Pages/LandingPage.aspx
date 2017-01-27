<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LandingPage.aspx.cs" Inherits="HPFS.Pages.LandingPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Hamilton Program for Schizophrenia</title>

    <!-- Favicon -->
    <link runat="server" rel="icon" href="~/faviconHPFS.ico" type="image/ico" />
    <link runat="server" rel="shortcut icon" href="~/faviconHPFS.ico" type="image/x-icon" />

    <!-- Scripts -->
    <script src="../Scripts/jquery-1.9.1.min.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Scripts/LandingPage.js"></script>

    <!-- Stylesheets -->
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/jquery-ui.css" rel="stylesheet" />
    <link href="../Content/css/pages/LandingPage.min.css" rel="stylesheet" />

    <script>
        $(function () {
            f = navigator.userAgent.search("Firefox");
            if (f > -1) {
                $("#guide").attr("class", "");
                $("#guide2").attr("class", "");
            }
        });
    </script>
</head>
<body>
    <a href="../Main.aspx" id="skipLink" class="text-center">Take Me to the Site <span class="glyphicon glyphicon-chevron-right" style="font-size: .7em;"></span></a>
    <h1 id="mainHeading" style="display: none;" class="text-center">
        <span class="p">H</span>amilton <span class="p">P</span>rogram for <span class="p">S</span>chizophrenia
    </h1>
    <div class="container-fluid">
        <div class="col-xs-12" id="outerBox">
            <div class="col-xs-12 text-center" id="contentBox">
                <div class="content" id="content1">
                    <div class="hidden-xs col-md-3"></div>
                    <div class="col-xs-4 col-md-2">
                        <img id="guide" class="shadowed" src="http://i.imgur.com/y7C3y4l.png" height="200" width="200" />
                    </div>
                    <div class="col-xs-8 col-md-5">
                        <h1>Welcome!</h1>
                        <h2>Hamilton Program for Schizophrenia</h2>
                    </div>
                    <div class="hidden-xs col-md-3"></div>
                </div>
                <div class="content" id="content2" style="display: none;">
                    <div class="col-xs-12">
                        <h2 id="directive">What are you looking for?</h2>
                    </div>
                    <div class="col-xs-12">
                        <img id="guide2" class="shadowed" src="http://i.imgur.com/y7C3y4l.png" height="150" width="150" />
                    </div>
                    <div class="col-xs-12" style="min-height: 341px;">
                        <div class="col-xs-12 col-md-4 col-md-offset-4">
                            <a href="#" class="form-control guideButton" id="aboutUs">What is HPS?</a>
                            <a href="#" class="form-control guideButton" id="helpMe">I need help...</a>
                            <a href="#" class="form-control guideButton" id="informMe">I need schizophrenia info...</a>
                            <a href="#" class="form-control guideButton" id="newUser">Website Help</a>
                            <a href="#" data-toggle="modal" data-target="#mdlFrench" class="form-control guideButton" id="speakFrench">En français?</a>

                            <!-- Help Me Section -->
                            <a href="#" data-toggle="modal" data-target="#mdlEmergency" class="form-control guideButton" id="emergencyBtn" style="display: none;">Emergency!</a>
                            <a href="#" data-toggle="modal" data-target="#mdlNonEmergency" class="form-control guideButton" id="nonEmergency" style="display: none;">Not an emergency</a>

                            <!-- Inform Me Section -->
                            <a href="#" data-toggle="modal" data-target="#mdlWhatIsSchizo" class="form-control guideButton" id="whatIsSchizo" style="display: none;">What is Schizophrenia?</a>
                            <a href="#" data-toggle="modal" data-target="#mdlHowManySchizo" class="form-control guideButton" id="howManySchizo" style="display: none;">How many people are affected?</a>
                            <a href="#" data-toggle="modal" data-target="#mdlCureForSchizo" class="form-control guideButton" id="cureForSchizo" style="display: none;">Is there a cure for Schizophrenia?</a>
                            <a href="#" data-toggle="modal" data-target="#mdlSymptomsSchizo" class="form-control guideButton" id="symptomsSchizo" style="display: none;">What are the symptoms?</a>
                            <a href="#" data-toggle="modal" data-target="#mdlSchizoAge" class="form-control guideButton" id="schizoAge" style="display: none;">When do people get Schizophrenia?</a>
                            <a href="#" data-toggle="modal" data-target="#mdlMoreInfo" class="form-control guideButton" id="schizoMore" style="display: none;">Learn more about Schizophrenia</a>

                            <!-- About Us Section -->
                            <a href="#" data-toggle="modal" data-target="#mdlMission" class="form-control guideButton" id="aboutMission" style="display: none;">Our Mission</a>
                            <a href="/Pages/Programs.aspx" class="form-control guideButton" id="aboutPrograms" style="display: none;">Our Programs</a>
                            <a href="/Content/files/Hamiton-Program-for-Schizophrenia-Inc.-M-SAA-CMHA-final-Feb171.pdf" target="_blank" class="form-control guideButton" id="aboutMSAA" style="display: none;">MSAA</a>
                            <a href="/Pages/ContactUs.aspx" class="form-control guideButton" id="contactUs" style="display: none;">Contact Us</a>
                            <a href="/Pages/ContactUs.aspx" class="form-control guideButton" id="locateUs" style="display: none;">Locate Us</a>

                            <!-- New User Section -->
                            <form id="LandingForm" runat="server">
                                <asp:LinkButton ID="signIn" CssClass="form-control guideButton" OnClick="signIn_Click" Style="display: none;" runat="server">Sign In</asp:LinkButton>
                            </form>
                            <%--<a href="/Main.aspx" onclick="setLoginCookie()" class="form-control guideButton" id="signIn" style="display: none;">Sign In</a>--%>
                            <a href="#" class="form-control guideButton" id="contactAdmin" style="display: none;">Contact an Administrator</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <%--I Need Help Modals--%>
        <!-- Emergency Modal -->
        <div class="modal fade" id="mdlEmergency" tabindex="-1" role="dialog" aria-labelledby="mdlEmergency-label" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h2 class="modal-title" id="mdlEmergency-label">I'm Having a Mental Health Emergency!</h2>
                    </div>
                    <div class="modal-body">
                        <h3>If someone's life is in danger call <span style="color: red;">911</span> (your local emergency services) immediately!</h3>
                        <hr />
                        <h3>For less life threatening emergencies please call the Hamilton 24 Hour Crisis Line: <span style="color: red;">905-972-8338</span></h3>
                        <hr />
                        <h3>During our office hours you can also call us: <span style="color: red;">905 525-2832</span></h3>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
        <!-- Non Emergency Modal -->
        <div class="modal fade" id="mdlNonEmergency" tabindex="-1" role="dialog" aria-labelledby="mdlNonEmergency-label" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h2 class="modal-title" id="mdlNonEmergency-label">I'm Having a Mental Health Issue!</h2>
                    </div>
                    <div class="modal-body">
                        <h3>During our office hours you can call us: <span style="color: #4e3691;">905 525-2832</span></h3>
                        <hr />
                        <h3>To be referred as a client to HPS contact IntAc: </h3>
                        <p>
                            Intensive Case Management Access Coordination<br />
                            21 King Street West, Suite 100<br />
                            Hamilton, Ontario L8P 4W7<br />
                            Telephone: (905) 528-0683<br />
                            Fax: (905) 546-0055<br />
                            Referral Hours: Monday to Friday 9:00am to 4:30pm
                       
                        </p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
        <%--I Need Help Modals End--%>

        <!-- Information Modals -->
        <div class="modal fade" id="mdlWhatIsSchizo" tabindex="-1" role="dialog" aria-labelledby="mdlWhatIsSchizo-label" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h2 class="modal-title" id="mdlWhatIsSchizo-label">What is Schizophrenia?</h2>
                    </div>
                    <div class="modal-body">
                        <p>Schizophrenia is a mental illness that affects the way you understand and interact with the world around you.</p>
                        <p>At the beginning of an episode, people may feel that things around them seem different or strange. They may start to experience problems concentrating, thinking or communicating clearly, or taking part in their usual activities. At the height of the episode, people may experience breaks from reality called psychosis. These could be hallucinations (sensations, like voices, that aren’t real) and delusions (strong beliefs that aren’t true, like the belief that they have superpowers). Some people feel ‘flat’ or numb. They may also experience changes in mood, motivation, and the ability to complete tasks. After an episode, signs can continue for some time. People may feel restless, withdraw from others, or have a hard time concentrating.</p>
                        <p>The exact course and impact of schizophrenia is unique for each person. Some people only experience one episode in their lifetime while others experience many episodes. Some people experience periods of wellness between episodes while others may experience episodes that last a long time. Some people experience a psychotic episode without warning while others experience many early warning signs. No matter how someone experiences schizophrenia, researchers agree that early treatment can help reduce the impact of episodes in the future.</p>
                    </div>
                    <div class="modal-footer">
                        <a href="http://www.cmha.ca/mental_health/facts-about-schizophrenia/#.VvVta-IrKUk" class="pull-left citation">Information Via: The Canadian Mental Health Association</a>
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="mdlHowManySchizo" tabindex="-1" role="dialog" aria-labelledby="mdlHowManySchizo-label" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h2 class="modal-title" id="mdlHowManySchizo-label">How Many People are Affected by Schizophrenia?</h2>
                    </div>
                    <div class="modal-body">
                        <p>Striking most often in the 16 to 30 year age group, affecting an estimated one person in a hundred, it is youth’s greatest disabler.</p>
                    </div>
                    <div class="modal-footer">
                        <a href="http://ontario.cmha.ca/mental-health/mental-health-conditions/schizophrenia/" class="pull-left citation">Information Via: The Canadian Mental Health Association</a>
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="mdlCureForSchizo" tabindex="-1" role="dialog" aria-labelledby="mdlCureForSchizo-label" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h2 class="modal-title" id="mdlCureForSchizo-label">Is There a Cure for Schizophrenia?</h2>
                    </div>
                    <div class="modal-body">
                        <p>While there is no cure for schizophrenia, people can and do recover. Recovery may mean learning to reduce the impact of problems, work around challenges, or maintain wellness. Most people use some combination of the following treatments and supports.</p>
                        <p>Some people need to spend time in hospital if they experience a severe episode of psychosis. This is a time to figure out the best treatment for you and begin your journey to health. Before you leave the hospital, care providers should help you map out the service providers (like doctors, counsellors, and social workers) who will be involved in your care and support your recovery.</p>
                        <hr />
                        <ul class="nav nav-tabs nav-justified" id="Cure_NavTabs">
                            <li class="active"><a data-toggle="tab" href="#Medication">Medication</a></li>
                            <li><a data-toggle="tab" href="#Counseling">Counseling</a></li>
                        </ul>
                        <div class="tab-content">
                            <%--MEDICATION TAB--%>
                            <div id="Medication" class="tab-pane fade in active">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <br />
                                        <p>Medication called antipsychotics may help reduce the severity of symptoms like hallucinations and delusions, and may eliminate these symptoms all together for many people. Continuing medication after you feel well again may help reduce the risk of relapse (when symptoms come back). There are many different kinds of antipsychotics, so it may take time and patience to find the best one for you.</p>
                                        <p>All medications can cause side effects—some of which can be uncomfortable or difficult. It’s best to have ongoing, open conversations about medication with a doctor so that everyone understands how a medication is affecting you, what can be done, and what other options you may have.</p>
                                    </div>
                                </div>
                            </div>
                            <%--COUNSELING TAB--%>
                            <div id="Counseling" class="tab-pane fade">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <br />
                                        <p>Counselling can help with many problems like low mood, anxiety, and relationships. You can learn helpful skills like problem-solving and setting goals. There are also therapies to help reduce the impact of delusions and hallucinations. Schizophrenia can affect people’s goals around education, work, and independent living. Professionals like occupational therapists and social workers can help with daily living, social skills, employment or volunteer training, and community activities. They can also connect you with community supports like home care, housing, and income assistance. <span class="p">HPS</span> offers many possible support opportunities.</p>
                                        <p>A big part of managing schizophrenia is relapse prevention. You can learn what might trigger an episode and learn to recognize early warning signs of an episode. The goal is to learn when to seek extra supports, which may help reduce the impact or length of the episode.</p>
                                        <p>Self-care is important for everyone. Small steps like eating well, getting regular exercise, building healthy sleep habits, spending time on activities you enjoy, spirituality, and connecting with loved ones can make a big difference.</p>
                                        <p>Schizophrenia can leave people feeling very isolated and alone. At times, many people who experience schizophrenia feel uncomfortable around others. But many also worry about what others will think of them. The right relationships can be supportive and healing. Your support team can help you connect with support groups.</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <a href="http://www.cmha.ca/mental_health/facts-about-schizophrenia/#.VvVta-IrKUk" class="pull-left citation">Information Via: The Canadian Mental Health Association</a>
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="mdlSymptomsSchizo" tabindex="-1" role="dialog" aria-labelledby="mdlSymptomsSchizo-label" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h2 class="modal-title" id="mdlSymptomsSchizo-label">What Are the Symptoms of Schizophrenia?</h2>
                    </div>
                    <div class="modal-body">
                        <p>Schizophrenia often starts slowly. When the symptoms first appear, usually in adolescence or early adulthood, they may seem more bewildering than serious.</p>
                        <p>In the early stages, people with schizophrenia may find themselves losing the ability to relax, concentrate or sleep. They may start to shut long-time friends out of their lives. Work or school begins to suffer; so does their personal appearance. During this time, there may be one or more episodes where they talk in ways that may be difficult to understand and/or start having unusual perceptions.</p>
                        <p>Once it has taken hold, schizophrenia tends to appear in cycles of remission and relapse.</p>
                        <p>When in remission, a person with schizophrenia may seem relatively unaffected and can more or less function in society. During relapse, however, it is a different story. People with schizophrenia may experience one or all of these main conditions:</p>
                        <ul>
                            <li>Delisuions and/or Hallucinations</li>
                            <li>Lack of Motivation</li>
                            <li>Social Withdraw</li>
                            <li>Thought Disorders</li>
                        </ul>
                        <p>Delusions are false beliefs that have no basis in reality. People with schizophrenia may think, for example, that someone is spying on them, listening to their thoughts, or placing thoughts in their minds.</p>
                        <p>Hallucinations most often consist of hearing voices that comment on behaviour, are insulting or give commands. Less often, people with schizophrenia may see or feel things that aren’t there.</p>
                        <p>Disorganized thinking makes some people with schizophrenia feel mixed up. In conversation, they may jump randomly from one unrelated topic to another. Depression and anxiety frequently accompany these feelings.</p>
                        <p>The symptoms of schizophrenia vary greatly from person to person, from mild to severe. A specialist is needed to make the diagnosis, especially because there are no diagnostic tests.</p>
                    </div>
                    <div class="modal-footer">
                        <a href="http://ontario.cmha.ca/mental_health/schizophrenia/#.VvV0d-IrKUk" class="pull-left citation">Information Via: The Canadian Mental Health Association</a>
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="mdlSchizoAge" tabindex="-1" role="dialog" aria-labelledby="mdlSchizoAge-label" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h2 class="modal-title" id="mdlSchizoAge-label">When Do People Get Schizophrenia?</h2>
                    </div>
                    <div class="modal-body">
                        <p>Schizophrenia can affect anyone. It usually starts to affect people in the teen years, though females often start to experience the illness a little later than males. No one knows exactly what causes schizophrenia or why it can affect people so differently. Genes, the way a person’s brain develops, and life events may all play a part.</p>
                    </div>
                    <div class="modal-footer">
                        <a href="http://www.cmha.ca/mental_health/facts-about-schizophrenia/#.VvVta-IrKUk" class="pull-left citation">Information Via: The Canadian Mental Health Association</a>
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="mdlMoreInfo" tabindex="-1" role="dialog" aria-labelledby="mdlMoreInfo-label" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h2 class="modal-title" id="mdlMoreInfo-label">Need More Information About Schizophrenia?</h2>
                    </div>
                    <div class="modal-body">
                        <ul>
                            <li><a href="http://www.cmha.ca/mental_health/facts-about-schizophrenia/#.VvV2p-IrKUl">Canadian Mental Health Association</a></li>
                            <li><a href="http://ontario.cmha.ca/mental_health/schizophrenia/#.VvV0d-IrKUk">Ontario Branch of the CMHA</a></li>
                            <li><a href="http://www.schizophrenia.on.ca/">The Schizophrenia Society of Ontario</a></li>
                            <li><a href="http://www.schizophrenia.ca/">Schizophrenia Society of Canada</a></li>
                            <li><a href="~/Pages/ContactUs.aspx">Contact Us</a></li>
                        </ul>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
        <!-- End Information Modals -->

        <!-- What is HPFS Modals -->
        <div class="modal fade" id="mdlMission" tabindex="-1" role="dialog" aria-labelledby="mdlMission-label" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h2 class="modal-title" id="mdlMission-label">Our Mission</h2>
                    </div>
                    <div class="modal-body">
                        <h2>Fundamental shared values are at the <span class="p">heart</span> of our mission. They unite us and provide direction for our organization.</h2>
                        <hr />
                        <p>The Hamilton Program for Schizophrenia is a comprehensive, community-based treatment and rehabilitation program for adults with schizophrenia, optimizing their recovery based on their goals.</p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
        <!-- End What is HPFS Modals -->

        <!-- I'm a new user Modals -->
        <!-- End I'm a new user Modals -->

        <!-- French Modal -->
        <div class="modal fade" id="mdlFrench" tabindex="-1" role="dialog" aria-labelledby="mdlFrench-label" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" id="mdlFrench-label">En Fran&ccedil;ais</h4>
                    </div>
                    <div class="modal-body">
                        <p>Le Programme de schizophrénie de Hamilton (HPS) est un programme global de traitement et de réadaptation en milieu communautaire. Nous aidons les personnes atteintes de schizophrénie par le biais de services de gestion de cas, de programmes de réadaptation et de soins psychiatriques. Nous offrons à nos clients et à leur famille un engagement à long terme et nous les appuyons aussi longtemps qu’ils ont besoin d’aide pour mener une vie enrichissante au sein de la communauté.</p>
                        <p>En situation de crise, nos psychiatres et nos gestionnaires de cas assurent la prestation de soins directs continus. En cas d’hospitalisation, notre équipe participe au traitement grâce à une collaboration formelle avec le Centre for Mountain Health Services de St. Joseph’s Healthcare.</p>
                        <p>Notre but est d’aider chaque client à mener une vie saine et enrichissante au sein de la communauté. Pour ce faire, nous offrons un soutien à domicile, au travail et dans les loisirs. Situés au centre-ville de Hamilton, nos locaux sont facilement accessibles par transport en commun.</p>
                        <p>Nos programmes et services sont offerts en anglais. Toutefois, les services de gestion de cas, de soutien psychiatrique et de réadaptation peuvent être offerts en français avec l’assistance d’un interprète professionnel.</p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Ferme</button>
                    </div>
                </div>
            </div>
        </div>
        <!-- End French Modal -->
    </div>
    <!-- this SVG is for drop shadow on PNGs -->
    <svg height="0" xmlns="http://www.w3.org/2000/svg">
        <filter id="drop-shadow">
            <feGaussianBlur in="SourceAlpha" stdDeviation="4" />
            <feOffset dx="12" dy="12" result="offsetblur" />
            <feFlood flood-color="rgba(0,0,0,0.5)" />
            <feComposite in2="offsetblur" operator="in" />
            <feMerge>
                <feMergeNode />
                <feMergeNode in="SourceGraphic" />
            </feMerge>
        </filter>
    </svg>
</body>
</html>
