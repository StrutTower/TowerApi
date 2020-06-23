using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace YoutubeWrapper.Models {
    //GET https://www.googleapis.com/youtube/v3/playlistItems?part=snippet&playlistId=PLF73EFA32EE7A10B4&key=[YOUR_API_KEY] HTTP/1.1
    ///Accept: application/json

    public class PlaylistItem {
        public string Kind { get; set; }

        public string ETag { get; set; }

        public string ID { get; set; }

        public DateTime PublishedAt { get; set; }

        public string ChannelID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public ThumbnailList Thumbnails { get; set; }

        public string ChannelTitle { get; set; }

        public string PlaylistID { get; set; }

        public int Position { get; set; }

        public string VideoID { get; set; }


        public override string ToString() {
            return Title;
        }

        [JsonExtensionData]
        private IDictionary<string, JToken> _additionalData;

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context) {
            if (_additionalData.ContainsKey("snippet")) {
                if (_additionalData["snippet"]["publishedAt"] != null) {
                    PublishedAt = _additionalData["snippet"]["publishedAt"].ToObject<DateTime>();
                }

                if (_additionalData["snippet"]["channelId"] != null) {
                    ChannelID = _additionalData["snippet"]["channelId"].ToString();
                }

                if (_additionalData["snippet"]["title"] != null) {
                    Title = _additionalData["snippet"]["title"].ToString();
                }

                if (_additionalData["snippet"]["description"] != null) {
                    Description = _additionalData["snippet"]["description"].ToString();
                }

                if (_additionalData["snippet"]["thumbnails"] != null) {
                    Thumbnails = _additionalData["snippet"]["thumbnails"].ToObject<ThumbnailList>();
                }

                if (_additionalData["snippet"]["channelTitle"] != null) {
                    ChannelTitle = _additionalData["snippet"]["channelTitle"].ToString();
                }

                if (_additionalData["snippet"]["playlistId"] != null) {
                    PlaylistID = _additionalData["snippet"]["playlistId"].ToString();
                }

                if (_additionalData["snippet"]["position"] != null) {
                    Position = _additionalData["snippet"]["position"].ToObject<int>();
                }

                if (_additionalData["snippet"]["resourceId"] != null && _additionalData["snippet"]["resourceId"]["videoId"] != null) {
                    VideoID = _additionalData["snippet"]["resourceId"]["videoId"].ToString();
                }
            }
        }
    }
}
