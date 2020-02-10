using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using YoutubeSearch.Application.Videos.Queries.SearchVideos;

namespace YoutubeSearch.Application.Tests
{
    public class VideosTests : IClassFixture<MediatorFixture>
    {
        private MediatorFixture fixture;

        public VideosTests(MediatorFixture fixture)
        {
            this.fixture = fixture;
            fixture.VideoRepositoryMock.Invocations.Clear();
            fixture.YoutubeApiClientMock.Invocations.Clear();
        }

        [Fact]
        public async Task SearchVideos_FromCacheEqualsFalse_DataFromGoogleApi()
        {
            // Arrange
            string searchTerm = "oscar 2020";
            var query = new SearchVideosQuery(searchTerm, false);

            // Act
            await fixture.Instance.Send(query);

            // Assert
            fixture.VideoRepositoryMock.Verify(m => m.SearchAsync(It.IsAny<string>()), Times.Never());
            fixture.YoutubeApiClientMock.Verify(m => m.GetVideosAsync(searchTerm), Times.Once());
            fixture.VideoRepositoryMock.Verify(m => m.AddRangeAsync(It.IsAny<IEnumerable<Domain.Video>>()), Times.Once());
        }

        [Fact]
        public async Task SearchVideos_FromCacheEqualsTrue_DataFromApplicationDatabase()
        {
            // Arrange
            string searchTerm = "oscar 2021";
            var query = new SearchVideosQuery(searchTerm, true);

            // Act
            await fixture.Instance.Send(query);

            // Assert
            fixture.VideoRepositoryMock.Verify(m => m.SearchAsync(It.IsAny<string>()), Times.Once());
            fixture.YoutubeApiClientMock.Verify(m => m.GetVideosAsync(searchTerm), Times.Never());
        }
        
    }
}
