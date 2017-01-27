<%@ Page Title="" Language="C#" MasterPageFile="~/HPFSMaster.Master" AutoEventWireup="true" CodeBehind="ContactUs.aspx.cs" Inherits="HPFS.ContactUs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">HPS - Contact Us</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <div class="row">
        <div class="col-xs-12 col-sm-6">
            <h1 class="text-center">Map</h1>
            <hr />
            <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2054.7212040221943!2d-79.87005475006082!3d43.25530742889983!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x882c9b84adcd0927%3A0xba9009f17ca8411a!2s20+Hughson+St+S%2C+Hamilton%2C+ON+L8N+2A1!5e0!3m2!1sen!2sca!4v1453181116231" width="100%" height="450" frameborder="0" style="border: 1px solid black;" allowfullscreen></iframe>
        </div>
        <div class="col-xs-12 col-sm-6 text-center">
            <h1 class="text-center">Contact Information</h1>
            <hr />
            <div id="contact3" runat="server">
            </div>
            <hr />
            <h3 style="font-weight: 400;">Office Hours</h3>
            <h6 style="font-size:14px;"><b>* Please note we are closed during regular work hours between 12:00PM and 1:00PM.</b></h6>
            <div class="btn-group btn-group-md hidden-xs" role="group">
                <a id="tt1" runat="server" class="btn btn-default addBorder" data-container="body" data-toggle="tooltip" title="Closed">S</a>
                <a id="tt2" runat="server" class="btn btn-default addBorder" data-container="body" data-toggle="tooltip" title="9:00AM - 5:00PM">M</a>
                <a id="tt3" runat="server" class="btn btn-default addBorder" data-container="body" data-toggle="tooltip" title="9:00AM - 5:00PM">T</a>
                <a id="tt4" runat="server" class="btn btn-default addBorder" data-container="body" data-toggle="tooltip" title="9:00AM - 5:00PM">W</a>
                <a id="tt5" runat="server" class="btn btn-default addBorder" data-container="body" data-toggle="tooltip" title="9:00AM - 5:00PM">T</a>
                <a id="tt6" runat="server" class="btn btn-default addBorder" data-container="body" data-toggle="tooltip" title="9:00AM - 5:00PM">F</a>
                <a id="tt7" runat="server" class="btn btn-default addBorder" data-container="body" data-toggle="tooltip" title="Closed">S</a>
            </div>
            <div id="hours3" runat="server" class="hidden-md hidden-lg hidden-sm contact">
            </div>
            <br class="hidden-xs"/>
            <h6 class="hidden-xs"><b>Hover over the day to see the hours</b></h6>
            <br />
            <a href="#" data-toggle="modal" data-target="#mdlInformation">Send us a message.</a>
        </div>
    </div>
</asp:Content>
