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
        public async Task<IEnumerable<FlickrFeedItem>> Images(string query)
        {
            IEnumerable<FlickrFeedItem> result = await GetImagesFromFlickr(query);
            Console.WriteLine(result);
            return result;
        }

        private async Task<IEnumerable<FlickrFeedItem>> GetImagesFromFlickr(string query)
        {
            HttpClient client = _clientFactory.CreateClient();

            UriBuilder build = new UriBuilder("https://www.flickr.com/services/feeds/photos_public.gne?format=json");


            if (!String.IsNullOrEmpty(query))
            {
                string tags = Regex.Replace(query,"\\s+",",");
                build.Query = build.Query + "&tags=" + Uri.EscapeDataString(tags);
            }            

            var response = await client.GetStringAsync(build.ToString());
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
