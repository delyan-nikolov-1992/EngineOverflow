namespace EngineOverflow.Web.ViewModels.Questions
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EngineOverflow.Data.Models;
    using EngineOverflow.Web.Infrastructure.Mapping;

    public class QuestionDisplayViewModel : IMapFrom<Post>
    {
        [MaxLength(100)]
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public IEnumerable<FeedbackDisplayViewModel> Feedbacks { get; set; }
    }
}
