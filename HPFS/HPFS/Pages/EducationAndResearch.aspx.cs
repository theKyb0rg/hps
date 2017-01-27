using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HPFS.HelperMethods;
using HPFS.Models;

namespace HPFS
{
    public partial class EducationAndResearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ActivityTracker.Track("Education And Research", (int)UserActionEnum.Navigated);
        }
    }
}