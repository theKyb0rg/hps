<%@ Page Title="" Language="C#" MasterPageFile="~/HPFSMaster.Master" AutoEventWireup="true" CodeBehind="AboutUs.aspx.cs" Inherits="HPFS.AboutUs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">HPS - About Us</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="insideHead" runat="server">
    <link href="../Content/css/pages/AboutUs.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <%--</div>--%>
    <!-- This closes the master container... so I can use a fluid one-->
    <h1 class="text-center">About Us</h1>
    <div class="row">
        <div class="col-xs-12">
            <hr />
            <blockquote>
                The Hamilton Program for Schizophrenia is a comprehensive, community-based treatment and rehabilitation program for adults with schizophrenia, 
                        optimizing their recovery based on their goals.
            </blockquote>
            <hr />
            <p style="font-size:18px;">
                Fundamental shared values are at the heart of our mission. They unite us and provide direction and focus for the organization’s goals and objectives:
            </p>
            <br />
            <ol id="weBelieve" style="list-style: decimal; line-height:2em; text-indent:1em;">
                <li>We believe in care that ensures the dignity and rights of the individual.</li>
                <li>We believe in care that optimizes our clients’ recovery and provides them with opportunities to meet their individual rehabilitation goals.</li>
                <li>We believe that both direct care and community partnerships allow us to provide our clients with various levels and duration of support tailored to their individual needs.</li>
                <li>We believe in providing individual client care that is accessible and responsive to individual needs and goals in the community and, if needed, in a hospital setting.</li>
                <li>We place high value on establishing and maintaining therapeutic alliances with our clients and families.</li>
                <li>We believe in actively involving our clients in decisions that improve both their recovery and the rehabilitation services we offer.</li>
                <li>We believe that each individual that works at HPS makes an important contribution and we value the pursuit of excellence, skill development and team work.</li>
                <li>We believe in being accountable to our clients and we will endeavor to provide services in a responsible, effective and efficient manner.</li>
                <li>We believe that education and research enhance our expertise and strengthen our partnerships in the community.</li>
                <li>We believe in fostering community understanding about schizophrenia.</li>
            </ol>
        </div>
    </div>
</asp:Content>
