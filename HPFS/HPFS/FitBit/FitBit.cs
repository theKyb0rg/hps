using Fitbit.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fitbit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HPFS.FitBit
{
    public class FitBit
    {
        public static void Authorize()
        {

            //make sure you've set these up in Web.Config under <appSettings>:
            string ConsumerKey = ConfigurationManager.AppSettings["FitbitConsumerKey"];
            string ConsumerSecret = ConfigurationManager.AppSettings["FitbitConsumerSecret"];


            Fitbit.Api.Authenticator authenticator = new Fitbit.Api.Authenticator(ConsumerKey,
                                                                                    ConsumerSecret,
                                                                                    "http://api.fitbit.com/oauth/request_token",
                                                                                    "http://api.fitbit.com/oauth/access_token",
                                                                                    "http://api.fitbit.com/oauth/authorize");
            RequestToken token = authenticator.GetRequestToken();
            HttpContext.Current.Session.Add("FitbitRequestTokenSecret", token.Secret.ToString()); //store this somehow, like in Session as we'll need it after the Callback() action

            //note: at this point the RequestToken object only has the Token and Secret properties supplied. Verifier happens later.

            string authUrl = authenticator.GenerateAuthUrlFromRequestToken(token, true);

            // REDIRECT BRO
            HttpContext.Current.Response.Redirect(authUrl);
        }

        //Final step. Take this authorization information and use it in the app
        public static void Callback()
        {
            RequestToken token = new RequestToken();
            token.Token = HttpContext.Current.Request.Params["oauth_token"];
            token.Secret = HttpContext.Current.Session["FitbitRequestTokenSecret"].ToString();
            token.Verifier = HttpContext.Current.Request.Params["oauth_verifier"];

            string ConsumerKey = ConfigurationManager.AppSettings["FitbitConsumerKey"];
            string ConsumerSecret = ConfigurationManager.AppSettings["FitbitConsumerSecret"];

            //this is going to go back to Fitbit one last time (server to server) and get the user's permanent auth credentials

            //create the Authenticator object
            Fitbit.Api.Authenticator authenticator = new Fitbit.Api.Authenticator(ConsumerKey,
                                                                                    ConsumerSecret,
                                                                                    "http://api.fitbit.com/oauth/request_token",
                                                                                    "http://api.fitbit.com/oauth/access_token",
                                                                                    "http://api.fitbit.com/oauth/authorize");

            //execute the Authenticator request to Fitbit
            AuthCredential credential = authenticator.ProcessApprovedAuthCallback(token);

            //here, we now have everything we need for the future to go back to Fitbit's API (STORE THESE):
            //  credential.AuthToken;
            //  credential.AuthTokenSecret;
            //  credential.UserId;

            // For demo, put this in the session managed by ASP.NET
            HttpContext.Current.Session["FitbitAuthToken"] = credential.AuthToken;
            HttpContext.Current.Session["FitbitAuthTokenSecret"] = credential.AuthTokenSecret;
            HttpContext.Current.Session["FitbitUserId"] = credential.UserId;

            //HttpContext.Current.Response.Redirect("/Pages/FitBitManager.aspx");
            //return RedirectToAction("Index", "Home");

        }

        

    }
}
