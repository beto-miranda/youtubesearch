using System;

namespace YoutubeSearch.Domain
{
    public class Video
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ulong? CommentCount { get; set; }
        public ulong? LikeCount { get; set; }
        public ulong? DislikeCount { get; set; }
        public ulong? ViewCount { get; set; }
    }
}
