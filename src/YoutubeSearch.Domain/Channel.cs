namespace YoutubeSearch.Domain
{
    public class Channel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ulong? CommentCount { get; set; }
        public ulong? SubscriberCount { get; set; }
        public ulong? VideoCount { get; set; }
    }
}
