using HPFS.HelperMethods;
using HPFS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HPFS
{
    public partial class Programs : System.Web.UI.Page
    {
        public static HPSDB db = new HPSDB();

        protected void Page_Load(object sender, EventArgs e)
        {
            ActivityTracker.Track("Programs", (int)UserActionEnum.Navigated);

            // Build the first visible carousel
            if (!IsPostBack)
            {
                CarouselBuilder.BuildCarousel(plCollectiveKitchen, (int)SlideShowEnums.CollectiveKitchen, "CollectiveKitchenCarousel");

                FillCollectiveKitchenTags();
                FillComputerTutoringTags();
                FillCottageStudioTags();
                FillWednesdayLeisureGroupTags();
                FillWalkingGroupTags();
                FillSummerSportsTags();
                FillTravellingCupTags();
                FillSweetDonationsGroupTags();
                FillMovieGroupTags();
                FillGamingGroupTags();
                FillFridaySocialGroupTags();
                FillOverview();
            }            
        }

        protected void LoadCarousel(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            if (btn.ID == "btnCollectiveKitchenTab")
            {
                CarouselBuilder.BuildCarousel(plCollectiveKitchen, (int)SlideShowEnums.CollectiveKitchen, "CollectiveKitchenCarousel");                
                ShowBootstrapTab("#CollectiveKitchen");
            }
            else if (btn.ID == "btnComputerTutoringTab")
            {
                CarouselBuilder.BuildCarousel(plComputerTutoring, (int)SlideShowEnums.ComputerTutoring, "ComputerTutoringCarousel");                
                ShowBootstrapTab("#ComputerTutoring");
            }
            else if (btn.ID == "btnCottageStudioTab")
            {
                CarouselBuilder.BuildCarousel(plCottageStudio, (int)SlideShowEnums.CottageStudio, "CottageStudioCarousel");                
                ShowBootstrapTab("#CottageStudio");
            }
            else if (btn.ID == "btnFridaySocialGroupTab")
            {
                CarouselBuilder.BuildCarousel(plFridaySocialGroup, (int)SlideShowEnums.FridaySocialGroup, "FridaySocialGroupCarousel");
                ShowBootstrapTab("#FridaySocialGroup");
            }
            else if (btn.ID == "btnGamingGroupTab")
            {
                CarouselBuilder.BuildCarousel(plGamingGroup, (int)SlideShowEnums.GamingGroup, "GamingGroupCarousel");
                ShowBootstrapTab("#GamingGroup");
            }
            else if (btn.ID == "btnMovieGroupTab")
            {
                CarouselBuilder.BuildCarousel(plMovieGroup, (int)SlideShowEnums.MovieGroup, "MovieGroupCarousel");
                ShowBootstrapTab("#MovieGroup");
            }
            else if (btn.ID == "btnSweetDonationsGroupTab")
            {
                CarouselBuilder.BuildCarousel(plSweetDonationsGroup, (int)SlideShowEnums.SweetDonationsGroup, "SweetDonationsGroupCarousel");
                ShowBootstrapTab("#SweetDonationsGroup");
            }
            else if (btn.ID == "btnTravellingCupTab")
            {
                CarouselBuilder.BuildCarousel(plTravellingCup, (int)SlideShowEnums.TravellingCup, "TravellingCupGroupCarousel");
                ShowBootstrapTab("#TravellingCup");
            }
            else if (btn.ID == "btnVolleyballSummerSportsTab")
            {
                CarouselBuilder.BuildCarousel(plVolleyballSummerSports, (int)SlideShowEnums.VolleyBallSummerSports, "VolleyballSummerSportsCarousel");
                ShowBootstrapTab("#VolleyballSummerSports");
            }
            else if (btn.ID == "btnWalkingGroupTab")
            {
                CarouselBuilder.BuildCarousel(plWalkingGroup, (int)SlideShowEnums.WalkingGroup, "WalkingGroupCarousel");
                ShowBootstrapTab("#WalkingGroup");
            }
            else if (btn.ID == "btnWednesdayLeisureGroupTab")
            {
                CarouselBuilder.BuildCarousel(plWednesdayLeisureGroup, (int)SlideShowEnums.WednesdayLeisureGroup, "WednesdayLeisureGroupCarousel");
                ShowBootstrapTab("#WednesdayLeisureGroup");
            }
            else if (btn.ID == "btnOverviewTab")
            {
                ShowBootstrapTab("#Overview");
            }
        }

        protected void ShowBootstrapTab(string tabId)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showTab", "$('.panel-title a[href=\"" + tabId + "\"]').tab('show');", true);
        }

        protected void FillCollectiveKitchenTags()
        {
            // Locate record
            Program pg = db.Programs.Find(1);

            // Fill
            colKitGoal.InnerHtml = pg.ProgramGoals;
            colKitCoord.InnerHtml = pg.ProgramCoordinator;
            colKitDesc.InnerHtml = pg.ProgramDescription;
            colKitEmail.InnerHtml = pg.ProgramEmail;
            colKitLocale.InnerHtml = pg.ProgramLocation;
            colKitPhone.InnerHtml = pg.ProgramPhone;
            colKitSched.InnerHtml = pg.ProgramSchedule;
            colKitMap.Src = pg.ProgramMap;
        }

        protected void FillComputerTutoringTags()
        {
            // Locate record
            Program pg = db.Programs.Find(2);

            // Fill
            compTutGoal.InnerHtml = pg.ProgramGoals;
            compTutCoord.InnerHtml = pg.ProgramCoordinator;
            compTutDesc.InnerHtml = pg.ProgramDescription;
            compTutEmail.InnerHtml = pg.ProgramEmail;
            compTutLocale.InnerHtml = pg.ProgramLocation;
            compTutPhone.InnerHtml = pg.ProgramPhone;
            compTutSched.InnerHtml = pg.ProgramSchedule;
            compTutMap.Src = pg.ProgramMap;
        }

        protected void FillCottageStudioTags()
        {
            // Locate record
            Program pg = db.Programs.Find(3);

            // Fill
            cotStudioGoal.InnerHtml = pg.ProgramGoals;
            cotStudioCoord.InnerHtml = pg.ProgramCoordinator;
            cotStudioDesc.InnerHtml = pg.ProgramDescription;
            cotStudioEmail.InnerHtml = pg.ProgramEmail;
            cotStudioLocale.InnerHtml = pg.ProgramLocation;
            cotStudioPhone.InnerHtml = pg.ProgramPhone;
            cotStudioSched.InnerHtml = pg.ProgramSchedule;
            cotStudioMap.Src = pg.ProgramMap;
        }

        protected void FillFridaySocialGroupTags()
        {
            // Locate record
            Program pg = db.Programs.Find(4);

            // Fill
            friSocGoal.InnerHtml = pg.ProgramGoals;
            friSocCoord.InnerHtml = pg.ProgramCoordinator;
            friSocDesc.InnerHtml = pg.ProgramDescription;
            friSocEmail.InnerHtml = pg.ProgramEmail;
            friSocLocale.InnerHtml = pg.ProgramLocation;
            friSocPhone.InnerHtml = pg.ProgramPhone;
            friSocSched.InnerHtml = pg.ProgramSchedule;
            friSocMap.Src = pg.ProgramMap;
        }

        protected void FillGamingGroupTags()
        {
            // Locate record
            Program pg = db.Programs.Find(5);

            // Fill
            gameGrpGoal.InnerHtml = pg.ProgramGoals;
            gameGrpCoord.InnerHtml = pg.ProgramCoordinator;
            gameGrpDesc.InnerHtml = pg.ProgramDescription;
            gameGrpEmail.InnerHtml = pg.ProgramEmail;
            gameGrpLocale.InnerHtml = pg.ProgramLocation;
            gameGrpPhone.InnerHtml = pg.ProgramPhone;
            gameGrpSched.InnerHtml = pg.ProgramSchedule;
            gameGrpMap.Src = pg.ProgramMap;
        }

        protected void FillMovieGroupTags()
        {
            // Locate record
            Program pg = db.Programs.Find(6);

            // Fill
            movieGrpGoal.InnerHtml = pg.ProgramGoals;
            movieGrpCoord.InnerHtml = pg.ProgramCoordinator;
            movieGrpDesc.InnerHtml = pg.ProgramDescription;
            movieGrpEmail.InnerHtml = pg.ProgramEmail;
            movieGrpLocale.InnerHtml = pg.ProgramLocation;
            movieGrpPhone.InnerHtml = pg.ProgramPhone;
            movieGrpSched.InnerHtml = pg.ProgramSchedule;
            movieGrpMap.Src = pg.ProgramMap;
        }

        protected void FillSweetDonationsGroupTags()
        {
            // Locate record
            Program pg = db.Programs.Find(7);

            // Fill
            sweetGrpGoal.InnerHtml = pg.ProgramGoals;
            sweetGrpCoord.InnerHtml = pg.ProgramCoordinator;
            sweetGrpDesc.InnerHtml = pg.ProgramDescription;
            sweetGrpEmail.InnerHtml = pg.ProgramEmail;
            sweetGrpLocale.InnerHtml = pg.ProgramLocation;
            sweetGrpPhone.InnerHtml = pg.ProgramPhone;
            sweetGrpSched.InnerHtml = pg.ProgramSchedule;
            sweetGrpMap.Src = pg.ProgramMap;
        }

        protected void FillTravellingCupTags()
        {
            // Locate record
            Program pg = db.Programs.Find(8);

            // Fill
            travCupGoal.InnerHtml = pg.ProgramGoals;
            travCupCoord.InnerHtml = pg.ProgramCoordinator;
            travCupDesc.InnerHtml = pg.ProgramDescription;
            travCupEmail.InnerHtml = pg.ProgramEmail;
            travCupLocale.InnerHtml = pg.ProgramLocation;
            travCupPhone.InnerHtml = pg.ProgramPhone;
            travCupSched.InnerHtml = pg.ProgramSchedule;
            travCupMap.Src = pg.ProgramMap;
        }

        protected void FillSummerSportsTags()
        {
            // Locate record
            Program pg = db.Programs.Find(9);

            // Fill
            sumSportGoal.InnerHtml = pg.ProgramGoals;
            sumSportCoord.InnerHtml = pg.ProgramCoordinator;
            sumSportDesc.InnerHtml = pg.ProgramDescription;
            sumSportEmail.InnerHtml = pg.ProgramEmail;
            sumSportLocale.InnerHtml = pg.ProgramLocation;
            sumSportPhone.InnerHtml = pg.ProgramPhone;
            sumSportSched.InnerHtml = pg.ProgramSchedule;
            sumSportMap.Src = pg.ProgramMap;
        }

        protected void FillWalkingGroupTags()
        {
            // Locate record
            Program pg = db.Programs.Find(10);

            // Fill
            walkGrpGoal.InnerHtml = pg.ProgramGoals;
            walkGrpCoord.InnerHtml = pg.ProgramCoordinator;
            walkGrpDesc.InnerHtml = pg.ProgramDescription;
            walkGrpEmail.InnerHtml = pg.ProgramEmail;
            walkGrpLocale.InnerHtml = pg.ProgramLocation;
            walkGrpPhone.InnerHtml = pg.ProgramPhone;
            walkGrpSched.InnerHtml = pg.ProgramSchedule;
            walkGrpMap.Src = pg.ProgramMap;
        }

        protected void FillWednesdayLeisureGroupTags()
        {
            // Locate record
            Program pg = db.Programs.Find(11);

            // Fill
            wedsGrpGoal.InnerHtml = pg.ProgramGoals;
            wedsGrpCoord.InnerHtml = pg.ProgramCoordinator;
            wedsGrpDesc.InnerHtml = pg.ProgramDescription;
            wedsGrpEmail.InnerHtml = pg.ProgramEmail;
            wedsGrpLocale.InnerHtml = pg.ProgramLocation;
            wedsGrpPhone.InnerHtml = pg.ProgramPhone;
            wedsGrpSched.InnerHtml = pg.ProgramSchedule;
            wedsGrpMap.Src = pg.ProgramMap;
        }

        protected void FillOverview()
        {
            var progQuery = from prog in db.Programs
                            select prog;

            decimal count = 1;
            foreach (var p in progQuery)
            {
                if (count % 2 == 0)
                {
                    overviewDiv.InnerHtml += "<div class='col-xs-12 col-sm-6 text-center'>";

                    if (p.ProgramSchedule != "")
                    {
                        overviewDiv.InnerHtml += "<p><b><u>" + p.ProgramName + "</u></b><br /><br />" + p.ProgramSchedule + "</p>";
                    }
                    else
                    {
                        overviewDiv.InnerHtml += "<p><b><u>" + p.ProgramName + "</u></b><br /><br />No Schedule.</p>";
                    }

                    overviewDiv.InnerHtml += "</div></div><hr />";
                }
                else 
                {
                    overviewDiv.InnerHtml += "<div class='row' style='padding-bottom:10px;'>"
                                           + "<div class='col-xs-12 col-sm-6 text-center' style='border-right:1px solid #c0c0c0;'>";

                    if (p.ProgramSchedule != "")
                    {
                        overviewDiv.InnerHtml += "<p><b><u>" + p.ProgramName + "</u></b><br /><br />" + p.ProgramSchedule + "</p><br />";
                    }
                    else
                    {
                        overviewDiv.InnerHtml += "<p><b><u>" + p.ProgramName + "</u></b><br /><br />No Schedule.</p><br />";
                    }

                    if (count == 11)
                    {
                        overviewDiv.InnerHtml += "</div></div>";
                    }
                    else
                    {
                        overviewDiv.InnerHtml += "</div>";
                    }
                }

                count++;                                                  
            }
        }
    }
}