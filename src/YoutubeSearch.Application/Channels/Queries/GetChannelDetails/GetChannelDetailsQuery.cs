using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using YoutubeSearch.Application.Infrastructure.BackgroundTasks;
using YoutubeSearch.Application.Integrations.Youtube;
using YoutubeSearch.Domain;

namespace YoutubeSearch.Application.Channels.Queries.GetChannelDetails
{
    public class GetChannelDetailsQuery : IRequest<Channel>
    {
        public GetChannelDetailsQuery(string channelId)
        {
            ChannelId = channelId;
        }

        public string ChannelId { get; }
    }

    public class GetChannelDetailsQueryHandler : IRequestHandler<GetChannelDetailsQuery, Channel>
    {
        private readonly IYoutubeApiClient youtubeApiClient;
        private readonly IVideoRepository videoRepository;
        private readonly IBackgroundTaskQueue backgroundTaskQueue;
        private readonly IServiceScopeFactory serviceScopeFactory;

        public GetChannelDetailsQueryHandler(IYoutubeApiClient youtubeApiClient,
                                        IVideoRepository videoRepository,
                                        IBackgroundTaskQueue backgroundTaskQueue,
                                        IServiceScopeFactory serviceScopeFactory)
        {
            this.youtubeApiClient = youtubeApiClient;
            this.videoRepository = videoRepository;
            this.backgroundTaskQueue = backgroundTaskQueue;
            this.serviceScopeFactory = serviceScopeFactory;
        }
        public async Task<Channel> Handle(GetChannelDetailsQuery request, CancellationToken cancellationToken)
        {
            return await youtubeApiClient.GetChannelDetailAsync(request.ChannelId);
        }
    }
}
