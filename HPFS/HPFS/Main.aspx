<%@ Page Title="" Language="C#" MasterPageFile="~/HPFSMaster.Master" AutoEventWireup="true" CodeBehind="/Main.aspx.cs" Inherits="HPFS.Main" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">HPS</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="insideHead" runat="server">
    <script src="Scripts/Main.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <div class="container">
        <h1 class="text-center">Hamilton Program for Schizophrenia</h1>
        <hr />
        <div class="row folder">
            <div class="col-xs-12 col-sm-6 add-20-pix-padding-bottom fix-border-right">
                <h2 class="text-center">Our Goal</h2>
                <br />
                <p class="bodyText">Our goal is to see each client actively pursuing a healthy and fulfilling life in the community and to establish a supportive environment within which individuals with schizophrenia can pursue their desired goals.</p>
                <p class="bodyText">To this end, we provide support to individuals at home or work and in leisure activities. Situated in downtown Hamilton, we are easily accessible by public transportation.</p>
                <p class="bodyText">In the event that hospitalization is required, the HPS team continues in the treatment delivery through formal links with <a class="purp" href="https://www.stjoes.ca/hospital-services/mental-health-addiction-services">St. Joseph's Healthcare, Centre for Mental Health Services</a>.</p>
            </div>
            <div class="col-xs-12 col-sm-6 add-20-pix-padding-bottom">
                <h2 class="text-center">Our Commitment</h2>
                <br />
                <p class="bodyText">We are committed to ongoing schizophrenia research and program development and evaluation while striving to ensure that our activities are in step with current, evidence-based best practices.</p>
                <p class="bodyText">This website provides a comprehensive listing of HPS programs and services including the many community partners who play a vital part in assisting our clients to achieve their individual goals.</p>
                <p class="bodyText">Pour obtenir des services en fran&ccedil;ais, <a class="purp" href="#" data-toggle="modal" data-target="#mdlFrench">veuillez cliquer ici</a>.</p>
            </div>
        </div>
        <hr />
    </div>
    <div class="container">
        <h2 class="text-center">What We Do</h2>
        <br />
        <div class="row">
            <div class="col-xs-12 col-sm-4">
                <div class="panel panel-default imagePanel">
                    <div class="panel-body">
                        <img src="/Content/Images/EandRBanner.jpg" alt="" class="img-responsive center-block" style="max-height: 358px;" />
                    </div>
                    <div class="panel-footer">
                        <p>
                            The Hamilton Program for Schizophrenia is a McMaster University Clinical Teaching Unit. As such we have developed, and maintain, a close relationship with the University.
                            This has allowed us the opportunity to participate in both past and present research activites. 
                            
                            <%--HPS offers learning experiences to nursing, occupational therapy, psychiatry, psychology, and community college students.
                            Educational programs such as our 'Noon Hour Seminar Series' provide learning opportunities for community mental health professionals and our own staff.--%>
                        </p>
                    </div>
                    <div class="panel-footer">
                        <a href="/Pages/EducationAndResearch.aspx" class="btn btn-default">Read More</a>
                    </div>
                </div>
            </div>
            <div class="col-xs-12 col-sm-4">
                <div class="panel panel-default imagePanel">
                    <div class="panel-body">
                        <img src="/Content/Images/RehabBanner.jpg" alt="" class="img-responsive center-block" />
                    </div>
                    <div class="panel-footer">
                        <p>
                            Psychosocial Rehabilitation is an important part of recovery and is an essential feature of the Hamilton Program for Schizophrenia. 
                            We offer rehabilitation to clients through programs, as well as through living, learning, working, social, and recreational activities. 
                        </p>
                    </div>
                    <div class="panel-footer">
                        <a href="/Pages/RehabilitationAndTreatment.aspx" class="btn btn-default">Read More</a>
                    </div>
                </div>
            </div>
            <div class="col-xs-12 col-sm-4">
                <div class="panel panel-default imagePanel">
                    <div class="panel-body">
                        <img src="/Content/Images/HPSFamilyBanner.jpg" alt="" class="img-responsive center-block" />
                    </div>
                    <div class="panel-footer">
                        <p>
                            The HPS Family Association's overall purpose is to enhance the quality of life for the HPS clients and their families. This is achieved by 
                            organizing a number of activities that are both educational and/or social in nature while also acting as a support for the families. 
                            <%--                            The Family Association, through the generous donations of family members, is responsible for funding, organizing and implementing two major 
                            activities for the clients each year: a family/client Christmas Party and Summer Picnic/BBQ. This is an opportunity to meet clients, other families and staff in a relaxed and informal 
                            social setting.--%>
                        </p>
                    </div>
                    <div class="panel-footer">
                        <a href="/Pages/Programs.aspx" class="btn btn-default">Read More</a>
                    </div>
                </div>
            </div>
        </div>
        <hr />
        <br />
        <h2 class="text-center">What Are You Looking For?</h2>
        <br />
        <div class="row">
            <div class="col-sm-4">
                <div class="well text-center">
                    <a href="Pages/EducationAndResearch.aspx">Education & Research</a>
                </div>
                <div class="well text-center">
                    <a href="#" data-toggle="modal" data-target="#mdlEvents">Events</a>
                </div>
            </div>
            <div class="col-sm-4">
                <div class="well text-center">
                    <a href="Pages/RehabilitationAndTreatment.aspx">Rehabilitation & Treatment</a>
                </div>
                <div class="well text-center">
                    <a href="Pages/ContactUs.aspx">Contact Information</a>
                </div>
            </div>
            <div class="col-sm-4">
                <div class="well text-center">
                    <a href="Pages/Programs.aspx">Programs</a>
                </div>

                <div class="well text-center">
                    <a href="Pages/AboutUs.aspx">About Us</a>
                </div>
            </div>
        </div>
        <hr />
    </div>
    <div class="container text-center">
        <div class="row">
            <h2>Our Partners</h2>
            <br />
            <div class="col-xs-6 col-sm-2">
                <a href="http://stjoes.ca/" class="partner">
                    <img src="/Content/Images/StJoesLogo.png" class="img-responsive partner" style="width: 100%" alt="Link to St Joseph's Hospital Hamilton Website" />
                </a>
                <h5>St. Josephs Hospital</h5>
            </div>
            <div class="col-xs-6 col-sm-2">
                <a href="http://www.hnhblhin.on.ca/" class="partner">
                    <img src="/Content/Images/LHIN.png" class="img-responsive partner" style="width: 100%" alt="Link to Local Health Integration Network Website" />
                </a>
                <h5>Local Health Integration Network</h5>
            </div>
            <div class="col-xs-6 col-sm-2">
                <a href="http://www.mohawkcollege.ca/" class="partner">
                    <img src="/Content/Images/MohawkLogo.png" class="img-responsive partner" style="width: 100%" alt="Link to Mohawk College's Website" />
                </a>
                <h5>Mohawk College</h5>
            </div>
            <div class="col-xs-6 col-sm-2">
                <a href="http://future.mcmaster.ca/" class="partner">
                    <img src="/Content/Images/McMasterLogo.png" class="img-responsive partner" style="width: 100%" alt="Link to McMaster University Website" />
                </a>
                <h5>McMaster University</h5>
            </div>
            <div class="col-xs-6 col-sm-2">
                <a href="http://www.niagaracollege.ca/" class="partner">
                    <img src="/Content/Images/NiagaraCollegeLogo.png" class="img-responsive partner" style="width: 100%" alt="Link to the Niagara College Website" />
                </a>
                <h5>Niagara College</h5>
            </div>
            <div class="col-xs-6 col-sm-2">
                <a href="#" class="partner">
                    <img src="/Content/Images/bliss.png" class="img-responsive partner" style="width: 100%" alt="bliss logo" />
                </a>
                <h5>Bliss</h5>
            </div>
        </div>
    </div>
    <div id="ribbon" class="text-center hidden-xs">
        <a href="Pages/ContactUs.aspx" style="color: white;">Contact Us!</a>
    </div>
</asp:Content>
