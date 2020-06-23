using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace YoutubeWrapper.Models {
//    GET https://www.googleapis.com/youtube/v3/playlists?part=snippet&channelId=UCu-JXHcE9Pjds7DTLJYQ6Zw&maxResults=50&key=[YOUR_API_KEY] HTTP/1.1

//Accept: application/json


    public class Playlist {
        public string ID { get; set; }// "PLtpGlomW4baKGvZYXAvUmxYnLs30TqQHQ"

        public string Kind { get; set; }//"youtube#playlist"

        public string ETag { get; set; }//"p2mYXwJweITLt3YXifhiVuWVnsM"

        public DateTime PublishedAt { get; set; }

        public string ChannelID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ChannelTitle { get; set; }

        public ThumbnailList Thumbnails { get; set; }

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
            }
        }
    }
}
