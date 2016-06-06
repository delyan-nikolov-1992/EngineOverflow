namespace EngineOverflow.Web.ViewModels.Questions
{
    using System.ComponentModel.DataAnnotations;

    using EngineOverflow.Data.Models;
    using EngineOverflow.Web.Infrastructure.Mapping;
    using EngineOverflow.Web.Infrastructure;

    public class QuestionDisplayViewModel : IMapFrom<Post>
    {
        private ISanitizer sanitizer;

        public QuestionDisplayViewModel()
        {
            this.sanitizer = new HtmlSanitizerAdapter();
        }

        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public int FeedbacksCount { get; set; }

        public FeedbackListViewModel FeedbackListViewModel { get; set; }

        public string SanitizedContent
        {
            get
            {
                return this.sanitizer.Sanitize(this.Content);
            }
        }
    }
}
