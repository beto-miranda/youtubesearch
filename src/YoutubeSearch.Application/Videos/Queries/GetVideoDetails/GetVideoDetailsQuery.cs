using System;
using MediatR;
using YoutubeSearch.Domain;

namespace YoutubeSearch.Application.Videos.Queries.GetVideoDetails
{
    public class GetVideoDetailsQuery : IRequest<Video>
    {
        public GetVideoDetailsQuery(string videoId)
        {
            VideoId = videoId;
        }

        public string VideoId { get; set; }
    }
}
