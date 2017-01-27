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
    public partial class RehabilitationAndTreatment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ActivityTracker.Track("Rehab and Treatment", (int)UserActionEnum.Navigated);
        }
    }
}