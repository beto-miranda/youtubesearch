using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using YoutubeSearch.Application.Channels.Queries.SearchChannels;

namespace YoutubeSearch.Application.Tests
{
    public class ChannelsTests : IClassFixture<MediatorFixture>
    {
        private MediatorFixture fixture;

        public ChannelsTests(MediatorFixture fixture)
        {
            this.fixture = fixture;
            fixture.ChannelRepositoryMock.Invocations.Clear();
            fixture.YoutubeApiClientMock.Invocations.Clear();
        }

        [Fact]
        public async Task SearchChannels_FromCacheEqualsFalse_DataFromGoogleApi()
        {
            // Arrange
            string searchTerm = "pipocando";
            var query = new SearchChannelsQuery(searchTerm, false);

            // Act
            await fixture.Instance.Send(query);

            // Assert
            fixture.ChannelRepositoryMock.Verify(m => m.SearchAsync(It.IsAny<string>()), Times.Never());
            fixture.YoutubeApiClientMock.Verify(m => m.GetChannelsAsync(searchTerm), Times.Once());
            fixture.ChannelRepositoryMock.Verify(m => m.AddRangeAsync(It.IsAny<IEnumerable<Domain.Channel>>()), Times.Once());
        }

        [Fact]
        public async Task SearchChannels_FromCacheEqualsTrue_DataFromApplicationDatabase()
        {
            // Arrange
            string searchTerm = "pipocando";
            var query = new SearchChannelsQuery(searchTerm, true);

            // Act
            await fixture.Instance.Send(query);

            // Assert
            fixture.ChannelRepositoryMock.Verify(m => m.SearchAsync(It.IsAny<string>()), Times.Once());
            fixture.YoutubeApiClientMock.Verify(m => m.GetChannelsAsync(searchTerm), Times.Never());
        }
        
    }
}
