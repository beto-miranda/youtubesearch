using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using YoutubeSearch.Application.Infrastructure.BackgroundTasks;
using YoutubeSearch.Application.Integrations.Youtube;
using YoutubeSearch.Domain;

namespace YoutubeSearch.Application.Channels.Queries.SearchChannels
{
    public class SearchChannelsQuery : IRequest<IEnumerable<Channel>>
    {
        public SearchChannelsQuery(string searchTerm, bool fromCache)
        {
            SearchTerm = searchTerm;
            FromCache = fromCache;
        }

        public string SearchTerm { get; set; }
        public bool FromCache { get; internal set; }
    }

     public class SearchChannelsQueryHandler : IRequestHandler<SearchChannelsQuery, IEnumerable<Channel>>
    {
        private readonly IYoutubeApiClient youtubeApiClient;
        private readonly IChannelRepository channelRepository;
        private readonly IBackgroundTaskQueue backgroundTaskQueue;
        private readonly IServiceScopeFactory serviceScopeFactory;

        public SearchChannelsQueryHandler(IYoutubeApiClient youtubeApiClient,
                                        IChannelRepository videoRepository,
                                        IBackgroundTaskQueue backgroundTaskQueue,
                                        IServiceScopeFactory serviceScopeFactory)
        {
            this.youtubeApiClient = youtubeApiClient;
            this.channelRepository = videoRepository;
            this.backgroundTaskQueue = backgroundTaskQueue;
            this.serviceScopeFactory = serviceScopeFactory;
        }
        public async Task<IEnumerable<Channel>> Handle(SearchChannelsQuery request, CancellationToken cancellationToken)
        {
            if(request.FromCache)
            {
                return channelRepository.SearchAsync(request.SearchTerm);
            }

            var channels = await youtubeApiClient.GetChannelsAsync(request.SearchTerm);
            await channelRepository.AddRangeAsync(channels);
            // backgroundTaskQueue.QueueBackgroundWorkItem(async token => 
            // {
            //     using (var scope = serviceScopeFactory.CreateScope())
            //     {
            //         var scopedServices = scope.ServiceProvider;
            //         var loggerFactory = scopedServices.GetRequiredService<ILoggerFactory>();
            //         var innerLogger = loggerFactory.CreateLogger("SaveChannelsWorker");
            //         var vr = scopedServices.GetRequiredService<IChannelRepository>();
            //         try
            //         {
            //             await vr.AddRangeAsync(channels);
            //         }
            //         catch (System.Exception ex)
            //         {
            //             innerLogger.LogError("Exception on save videos", ex);
            //         }
            //     }
                
            // });

            return channels;
        }
    }
}
