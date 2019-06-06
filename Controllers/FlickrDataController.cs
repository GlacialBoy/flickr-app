using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using flickr_app.Models;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace flickr_app.Controllers
{
    [Route("api/[controller]")]
    public class FlickrDataController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public FlickrDataController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<FlickrFeedItem>> Images()
        {
            IEnumerable<FlickrFeedItem> result = await GetImagesFromFlickr();
            return result;
        }

        private async Task<IEnumerable<FlickrFeedItem>> GetImagesFromFlickr()
        {
            HttpClient client = _clientFactory.CreateClient();
            const String flickrUrl = "https://www.flickr.com/services/feeds/photos_public.gne?format=json";
            
            var response = await client.GetStringAsync(flickrUrl);
            var cleanedResponse = cleanMalformedJson(response);
                    
            FlickrFeed responseFeed = JsonConvert.DeserializeObject<FlickrFeed>(cleanedResponse);

            return responseFeed.items;
        }

        private String cleanMalformedJson(String response)
        {
            return Regex.Replace(response, "jsonFlickrFeed\\((.*)\\)", "$1", RegexOptions.Singleline);
        }

    }
}
