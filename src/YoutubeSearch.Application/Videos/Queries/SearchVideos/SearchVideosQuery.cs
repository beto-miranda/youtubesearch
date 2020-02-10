using System.Collections.Generic;
using MediatR;
using YoutubeSearch.Domain;

namespace YoutubeSearch.Application.Videos.Queries.SearchVideos
{
    public class SearchVideosQuery : IRequest<IEnumerable<Video>>
    {
        public SearchVideosQuery(string searchTerm, bool fromCache)
        {
            SearchTerm = searchTerm;
            FromCache = fromCache;
        }

        public string SearchTerm { get; set; }
        public bool FromCache { get; set; }
    }
}
