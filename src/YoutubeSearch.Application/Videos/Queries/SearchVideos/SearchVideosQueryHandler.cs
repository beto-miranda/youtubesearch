using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using YoutubeSearch.Application.Infrastructure.BackgroundTasks;
using YoutubeSearch.Application.Integrations.Youtube;
using YoutubeSearch.Domain;

namespace YoutubeSearch.Application.Videos.Queries.SearchVideos
{
    public class SearchVideosQueryHandler : IRequestHandler<SearchVideosQuery, IEnumerable<Video>>
    {
        private readonly IYoutubeApiClient youtubeApiClient;
        private readonly IVideoRepository videoRepository;
        private readonly IBackgroundTaskQueue backgroundTaskQueue;
        private readonly IServiceScopeFactory serviceScopeFactory;

        public SearchVideosQueryHandler(IYoutubeApiClient youtubeApiClient,
                                        IVideoRepository videoRepository,
                                        IBackgroundTaskQueue backgroundTaskQueue,
                                        IServiceScopeFactory serviceScopeFactory)
        {
            this.youtubeApiClient = youtubeApiClient;
            this.videoRepository = videoRepository;
            this.backgroundTaskQueue = backgroundTaskQueue;
            this.serviceScopeFactory = serviceScopeFactory;
        }
        public async Task<IEnumerable<Video>> Handle(SearchVideosQuery request, CancellationToken cancellationToken)
        {
            if(request.FromCache)
            {
                return videoRepository.SearchAsync(request.SearchTerm);
            }

            var videos = await youtubeApiClient.GetVideosAsync(request.SearchTerm);
            await videoRepository.AddRangeAsync(videos);
            // backgroundTaskQueue.QueueBackgroundWorkItem(async token => 
            // {
            //     using (var scope = serviceScopeFactory.CreateScope())
            //     {
            //         var scopedServices = scope.ServiceProvider;
            //         var loggerFactory = scopedServices.GetRequiredService<ILoggerFactory>();
            //         var innerLogger = loggerFactory.CreateLogger("SaveVideosWorker");
            //         var vr = scopedServices.GetRequiredService<IVideoRepository>();
            //         try
            //         {
            //             await vr.AddRangeAsync(videos);
            //         }
            //         catch (System.Exception ex)
            //         {
            //             innerLogger.LogError("Exception on save videos", ex);
            //         }
            //     }
                
            // });

            return videos;
        }
    }
}
