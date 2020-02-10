using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using YoutubeSearch.Application.Infrastructure.BackgroundTasks;
using YoutubeSearch.Application.Integrations.Youtube;
using YoutubeSearch.Domain;

namespace YoutubeSearch.Application.Videos.Queries.GetVideoDetails
{
    public class GetVideoDetailsQueryHandler : IRequestHandler<GetVideoDetailsQuery, Video>
    {
        private readonly IYoutubeApiClient youtubeApiClient;
        private readonly IVideoRepository videoRepository;
        private readonly IBackgroundTaskQueue backgroundTaskQueue;
        private readonly IServiceScopeFactory serviceScopeFactory;

        public GetVideoDetailsQueryHandler(IYoutubeApiClient youtubeApiClient,
                                        IVideoRepository videoRepository,
                                        IBackgroundTaskQueue backgroundTaskQueue,
                                        IServiceScopeFactory serviceScopeFactory)
        {
            this.youtubeApiClient = youtubeApiClient;
            this.videoRepository = videoRepository;
            this.backgroundTaskQueue = backgroundTaskQueue;
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public async  Task<Video> Handle(GetVideoDetailsQuery request, CancellationToken cancellationToken)
        {
            return await youtubeApiClient.GetVideoDetailAsync(request.VideoId);
        }

    }
}
