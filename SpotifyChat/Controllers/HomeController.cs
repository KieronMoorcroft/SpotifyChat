using System;
using System.Web.Mvc;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Models;
using System.Collections.Generic;
using SpotifyAPI.Web;
using System.Linq;
using SpotifyAPI.Web.Enums;

namespace SpotifyChat.Controllers
{
    public class HomeController : Controller
    {
        private SpotifyWebAPI _spotify;
        private ImplicitGrantAuth _auth;
        private PrivateProfile _profile;
        private List<FullTrack> _savedTracks;
        private List<SimplePlaylist> _playlist;

        public ActionResult Index()
        {
            ViewBag.AuthUri = GetAuthUri();
            
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


        public dynamic GetAuthUri()
        {
            _savedTracks = new List<FullTrack>();
            _auth = new ImplicitGrantAuth
            {
                RedirectUri = "http://localhost:58158/Home/AuthResponse",
                ClientId = "f2bd29ea842f4fde8b866fd15de6f3e7",
                Scope = Scope.UserReadPrivate | Scope.UserReadEmail | Scope.PlaylistReadPrivate | Scope.UserLibraryRead | Scope.UserReadPrivate | Scope.UserFollowRead | Scope.UserReadBirthdate | Scope.UserTopRead,
                State = "XSS"
            };

            _auth.OnResponseReceivedEvent += _auth_OnResponseReceivedEvent;

            return _auth;
        }

        private void _auth_OnResponseReceivedEvent(Token token, string state)
        {
            _auth.StopHttpServer();

            if (state != "XSS")
            {
                Console.WriteLine(@"Wrong state received.", @"SpotifyWeb API Error");
                return;
            }
            if (token.Error != null)
            {
                Console.WriteLine($"Error: {token.Error}", @"SpotifyWeb API Error");
                return;
            }

            _spotify = new SpotifyWebAPI
            {
                UseAuth = true,
                AccessToken = token.AccessToken,
                TokenType = token.TokenType
            };

        }


    }
}