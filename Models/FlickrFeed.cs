using System;
using System.Collections.Generic;
using System.Linq;

namespace flickr_app.Models
{

    public class FlickrFeed
    {
        public string title;
        public string link;
        public string modified;
        public IEnumerable<FlickrFeedItem> items;
    } 
        
    public class FlickrFeedItem
    {
        public string title;
        public string link;
        public FlickrMedia media;
        public string description;
        public string tags;
    }

    public class FlickrMedia
    {
        public string m;
    }
}