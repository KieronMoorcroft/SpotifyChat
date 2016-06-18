using System;
using System.Web.Mvc;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Models;
using System.Collections.Generic;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Enums;
using System.Threading.Tasks;
using SpotifyChat.Models;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Linq;

namespace SpotifyChat.Controllers
{
    public class HomeController : Controller
    {


        public ActionResult Index()
        {
            var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                var spotifyAccessToken = claimsIdentity.Claims.FirstOrDefault(x => x.Type == "urn:tokens:spotify:accesstoken");

                if (spotifyAccessToken != null)
                {
                    ViewBag.SpotifyAccessToken = spotifyAccessToken.Value;
                }
            }
            return View();
        }

        public ActionResult AuthResponse(string access_token, string token_type, string expires_in, string state)
        {

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


    }
}
