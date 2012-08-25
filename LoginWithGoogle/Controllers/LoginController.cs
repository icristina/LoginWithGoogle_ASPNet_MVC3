using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;

namespace LoginWithGoogle.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }

        # region googlelogin

        public ActionResult LoginWithGoogle()
        {
            string authorizationUrl = string.Empty;
            authorizationUrl = "https://accounts.google.com/o/oauth2/auth?client_id=" + System.Web.Configuration.WebConfigurationManager.AppSettings["GoogleClientId"] + "&redirect_uri=" + System.Web.Configuration.WebConfigurationManager.AppSettings["GoogleRedirectURL"] + "&scope=https://www.googleapis.com/auth/userinfo.profile https://www.googleapis.com/auth/userinfo.email &response_type=token";
            return Redirect(authorizationUrl);
        }

        public ActionResult ReturnFromGoogle()
        {
            return View();
        }

        public ActionResult ReturnFromGoogle2()
        {
            var token = Request.QueryString["access_token"];
            JObject je = new JObject();
            HttpWebRequest req3 = (HttpWebRequest)WebRequest.Create("https://www.googleapis.com/oauth2/v1/userinfo?alt=json&access_token=" + token);
            HttpWebResponse res3 = (HttpWebResponse)req3.GetResponse();
            StreamReader streamReader3 = new StreamReader(res3.GetResponseStream());
            string returnStr3 = streamReader3.ReadToEnd();
            je = JObject.Parse(returnStr3);

            try
            {
                ViewBag.SocialNetworkId = je["id"].ToString().Replace("\"", "");
                ViewBag.FirstName = je["given_name"].ToString().Replace("\"", "");
                ViewBag.LastName = je["family_name"].ToString().Replace("\"", "");
                ViewBag.Email = je["email"].ToString().Replace("\"", "");
                ViewBag.ProfilePicURL = je["picture"].ToString().Replace("\"", "");
            }
            catch { }
            return View();
        }

        # endregion googlelogin

    }
}
