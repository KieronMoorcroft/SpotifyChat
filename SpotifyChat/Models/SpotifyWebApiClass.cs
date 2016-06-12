using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpotifyChat.Models
{
    internal class SpotifyWebApiClass
    {
        public SpotifyWebApiClass()
        {
        }

        public string AccessToken { get; set; }
        public string TokenType { get; set; }
    }
}