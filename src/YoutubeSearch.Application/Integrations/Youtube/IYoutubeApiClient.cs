using System.Collections.Generic;
using System.Threading.Tasks;

namespace YoutubeSearch.Application.Integrations.Youtube
{
    public interface IYoutubeApiClient
    {
        Task<IEnumerable<Domain.Video>> GetVideosAsync(string searchTerm);
        Task<Domain.Video> GetVideoDetailAsync(string videoId);
        Task<IEnumerable<Domain.Channel>> GetChannelsAsync(string searchTerm);
        Task<Domain.Channel> GetChannelDetailAsync(string channelId);
    }
}
