namespace EngineOverflow.Web.ViewModels.Questions
{
    using System.ComponentModel.DataAnnotations;

    using EngineOverflow.Data.Models;
    using EngineOverflow.Web.Infrastructure.Mapping;
    using EngineOverflow.Web.ViewModels.PageableFeedbackList;

    public class QuestionDisplayViewModel : IMapFrom<Post>
    {
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public int FeedbacksCount { get; set; }

        public FeedbackListViewModel FeedbackListViewModel { get; set; }
    }
}
