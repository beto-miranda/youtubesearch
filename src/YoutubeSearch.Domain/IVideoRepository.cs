using System.Collections.Generic;
using System.Threading.Tasks;

namespace YoutubeSearch.Domain
{
    public interface IVideoRepository
    {
        Task AddRangeAsync(IEnumerable<Video> videos);
        IEnumerable<Video> SearchAsync(string searchTerm);
    }
}
