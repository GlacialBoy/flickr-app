using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using flickr_app.Models;
using flickr_app.Provider;

namespace flickr_app.Controllers
{
    [Route("api/[controller]")]
    public class FlickrDataController : Controller
    {
        private readonly FlickProvider flickrProvider;

        public FlickrDataController(IHttpClientFactory clientFactory)
        {
            this.flickrProvider = new FlickProvider(clientFactory);
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<FlickrFeedItem>> Images(string query)
        {
            IEnumerable<FlickrFeedItem> result = await this.flickrProvider.GetImagesFromFlickr(query);
            return result;
        }

    }
}
