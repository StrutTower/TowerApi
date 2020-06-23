using System;
using System.Collections.Generic;
using System.Text;

namespace YoutubeWrapper.Models {
    public class ThumbnailList {
        public Thumbnail Default { get; set; }

        public Thumbnail Medium { get; set; }

        public Thumbnail High { get; set; }

        public Thumbnail Standard { get; set; }

        public Thumbnail MaxRes { get; set; }
    }
}
