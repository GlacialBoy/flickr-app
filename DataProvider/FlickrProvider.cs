using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using flickr_app.Models;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("flick-app.TEST")]

namespace flickr_app.Provider
{
    public class FlickProvider
    {
        private readonly IHttpClientFactory _clientFactory;

        public FlickProvider(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IEnumerable<FlickrFeedItem>> GetImagesFromFlickr(string query)
        {
            HttpClient client = _clientFactory.CreateClient();

            string url = this.composeUrl(query);
            string response = await client.GetStringAsync(url);
            string cleanedResponse = cleanMalformedJson(response);

            FlickrFeed responseFeed = JsonConvert.DeserializeObject<FlickrFeed>(cleanedResponse);

            return responseFeed.items;
        } 

        private String composeUrl(String query){

            UriBuilder build = new UriBuilder("https://www.flickr.com/services/feeds/photos_public.gne?format=json");

            if (!String.IsNullOrEmpty(query))
            {
                string tags = Regex.Replace(query,"\\s+",",");
                build.Query = build.Query + "&tags=" + Uri.EscapeDataString(tags);
            }            
            return build.ToString();
        }
     
        private String cleanMalformedJson(String response)
        {
            return Regex.Replace(response, "jsonFlickrFeed\\((.*)\\)", "$1", RegexOptions.Singleline);
        }
    }
}
