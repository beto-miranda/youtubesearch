using System.Reflection;
using AutoMapper;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using YoutubeSearch.Application.Infrastructure.BackgroundTasks;
using YoutubeSearch.Application.Integrations.Youtube;
using YoutubeSearch.Application.Videos.Queries.SearchVideos;
using YoutubeSearch.Domain;

namespace YoutubeSearch.Application.Tests
{
    public class MediatorFixture
    {
        public ServiceProvider ServiceProvider { get; private set; }
        public IMediator Instance { get; private set; }
        public Mock<IVideoRepository> VideoRepositoryMock { get; private set; }
        public Mock<IChannelRepository> ChannelRepositoryMock { get; private set; }
        public Mock<IYoutubeApiClient> YoutubeApiClientMock { get; private set; }
        public Mock<IBackgroundTaskQueue> BackgroundTaskQueueMock { get; private set; }

        public MediatorFixture()
        {
            BuildMediator();
        }

        private void BuildMediator()
        {
            var services = new ServiceCollection();
            services.AddLogging(configure => configure.AddConsole());
            services.AddMediatR(typeof(SearchVideosQuery));
            
            VideoRepositoryMock = new Mock<IVideoRepository>();
            services.AddSingleton(VideoRepositoryMock.Object);

            ChannelRepositoryMock = new Mock<IChannelRepository>();
            services.AddSingleton(ChannelRepositoryMock.Object);

            YoutubeApiClientMock = new Mock<IYoutubeApiClient>();
            services.AddSingleton(YoutubeApiClientMock.Object);

            BackgroundTaskQueueMock = new Mock<IBackgroundTaskQueue>();
            services.AddSingleton(BackgroundTaskQueueMock.Object);

            ServiceProvider = services.BuildServiceProvider();
            Instance = ServiceProvider.GetRequiredService<IMediator>();
        }
    }
}
