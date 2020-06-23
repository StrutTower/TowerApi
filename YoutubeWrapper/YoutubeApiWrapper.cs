using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using YoutubeWrapper.Models;
using YoutubeWrapper.Utilities;

namespace YoutubeWrapper {
    public class YoutubeApiWrapper {

        private string ApiKey { get; set; }

        public YoutubeApiWrapper(string apiKey = null) {
            ApiKey = apiKey;
        }

        public void SetApiKey(string apiKey) {
            ApiKey = apiKey;
        }

        public List<Playlist> GetPlaylists(string userID) {
            string url = "https://www.googleapis.com/youtube/v3/playlists";
            Dictionary<string, string> parameters = new Dictionary<string, string> {
                    { "part", "snippet" },
                    { "channelId", userID },
                    { "maxResults", "50" },
                    { "key", ApiKey }
                };
            return ApiRequestor.GetResponse<List<Playlist>>(url, parameters);
        }

        public List<PlaylistItem> GetPlaylistItems(string playlistID) {
            string url = "https://www.googleapis.com/youtube/v3/playlistItems";
            Dictionary<string, string> parameters = new Dictionary<string, string> {
                    { "part", "snippet" },
                    { "playlistId", playlistID },
                    { "maxResults", "50" },
                    { "key", ApiKey }
                };
            return ApiRequestor.GetResponse<List<PlaylistItem>>(url, parameters);
        }
    }
}
