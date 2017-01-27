﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HPFS
{
    public partial class ImageManager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.User.Identity.IsAuthenticated || Session["UserId"] == null || !Page.User.IsInRole("Administrator"))
            {
                Response.Redirect("/Main.aspx");
            }
        }
    }
}