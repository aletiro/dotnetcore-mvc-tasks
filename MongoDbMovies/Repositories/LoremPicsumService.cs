using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MongoDbMovies.Repositories
{
    public class LoremPicsumService : ILoremPicsumService
    {
        public string GetRandomImage(int width, int height)
        {
            string imageUrl = "";
            HttpWebRequest request = HttpWebRequest.Create($"https://picsum.photos/{width}/{height}") as HttpWebRequest;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            if (response.StatusCode == HttpStatusCode.OK)
                imageUrl = response.ResponseUri.ToString();
            else
                throw new Exception($"Could not connect to Picsum service. Error: {response.StatusCode} - {response.StatusDescription}");

            return imageUrl;
        }
    }
}
