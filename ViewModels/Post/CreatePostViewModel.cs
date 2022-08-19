namespace Test_Task_for_GeeksForLess.ViewModels.Post
{
    public class CreatePostViewModel
    {
        public string Description { get; set; }
        public int TopicId { get; set; }
        public Models.Topic Topic { get; set; }
    }
}
