using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

namespace YoutubeSearch.Application.Integrations.Youtube
{
    public class YoutubeSDKApiClient : IYoutubeApiClient
    {
        private readonly YouTubeService youtubeService;

        public YoutubeSDKApiClient(Configuration configuration)
        {
            youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = configuration.ApiKey,
                ApplicationName = this.GetType().ToString()
            });
        }
        public async Task<IEnumerable<Domain.Channel>> GetChannelsAsync(string searchTerm)
        {
            var searchResult = await SearchCoreAsync(searchTerm, "channel");
            return searchResult.Items.Select(v => new Domain.Channel
            {
                Id = v.Id.ChannelId,
                Title = v.Snippet.Title,
                Description = v.Snippet.Description
            });
        }

        public async Task<IEnumerable<Domain.Video>> GetVideosAsync(string searchTerm)
        {
            var searchResult = await SearchCoreAsync(searchTerm, "video");
            return searchResult.Items.Select(v => new Domain.Video
            {
                Id = v.Id.VideoId,
                Title = v.Snippet.Title,
                Description = v.Snippet.Description
            });
        }

        public async Task<Domain.Video> GetVideoDetailAsync(string videoId)
        {
            var searchListRequest = youtubeService.Videos.List("snippet,statistics");
            searchListRequest.Id = videoId;
            var response = await searchListRequest.ExecuteAsync();
            var v =  response.Items.FirstOrDefault();

            return new Domain.Video
            {
                Id = v.Id,
                Title = v.Snippet.Title,
                Description = v.Snippet.Description,
                CommentCount = v.Statistics.CommentCount,
                LikeCount = v.Statistics.LikeCount,
                DislikeCount = v.Statistics.DislikeCount,
                ViewCount = v.Statistics.ViewCount
            };
        }

        public async Task<Domain.Channel> GetChannelDetailAsync(string channelId)
        {
            var searchListRequest = youtubeService.Channels.List("snippet,statistics");
            searchListRequest.Id = channelId;
            var response = await searchListRequest.ExecuteAsync();
            var v =  response.Items.FirstOrDefault();

            return new Domain.Channel
            {
                Id = v.Id,
                Title = v.Snippet.Title,
                Description = v.Snippet.Description,
                CommentCount = v.Statistics.CommentCount,
                SubscriberCount = v.Statistics.SubscriberCount,
                VideoCount = v.Statistics.VideoCount
            };
        }

        private async Task<SearchListResponse> SearchCoreAsync(string searchTerm, string type)
        {
            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = searchTerm; 
            searchListRequest.MaxResults = 50;
            searchListRequest.Type = type;
            return  await searchListRequest.ExecuteAsync();
        }

     


        public class Configuration
        {
            public string ApiKey { get; set; }
        }
    }
}
