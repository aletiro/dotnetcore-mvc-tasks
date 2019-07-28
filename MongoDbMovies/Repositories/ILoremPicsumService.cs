using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDbMovies.Repositories
{
    public interface ILoremPicsumService
    {
        string GetRandomImage(int width, int height);
    }
}
