namespace Simple_forum.ViewModels.Post
{
    public class CreatePostViewModel
    {
        public string Description { get; set; }
        public int TopicId { get; set; }
        public Models.Topic Topic { get; set; }
    }
}
