using System;
using System.Web.Mvc;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Models;
using System.Collections.Generic;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Enums;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Linq;
using SpotifyChat.Models;
namespace SpotifyChat.Controllers
{
    public class HomeController : Controller
    {
        private SpotifyWebAPI _spotify;
        private PrivateProfile _profile;
        
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
            var savedTracks = GetSavedTracks();


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

        public List<FullTrack> GetSavedTracks()
        {
            Paging<SavedTrack> savedTracks = _spotify.GetSavedTracks();
            List<FullTrack> list = savedTracks.Items.Select(track => track.Track).ToList();

            while (savedTracks.Next != null)
            {
                savedTracks = _spotify.GetSavedTracks(20, savedTracks.Offset + savedTracks.Limit);
                list.AddRange(savedTracks.Items.Select(track => track.Track));
            }

            return list;
        }

        public List<SimplePlaylist> GetPlaylists()
        {
            Paging<SimplePlaylist> playlists = _spotify.GetUserPlaylists(_profile.Id);
            List<SimplePlaylist> list = playlists.Items.ToList();

            while (playlists.Next != null)
            {
                playlists = _spotify.GetUserPlaylists(_profile.Id, 20, playlists.Offset + playlists.Limit);
                list.AddRange(playlists.Items);
            }

            return list;
        }

    }
}
