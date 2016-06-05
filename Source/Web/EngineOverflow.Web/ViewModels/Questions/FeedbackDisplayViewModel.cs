namespace EngineOverflow.Web.ViewModels.Questions
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using EngineOverflow.Data.Models;
    using EngineOverflow.Web.Infrastructure.Mapping;

    public class FeedbackDisplayViewModel : IMapFrom<Feedback>
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public string AuthorUserName { get; set; }

        public DateTime CreatedOn { get; set; }

        public string AuthorUserNameDisplay
        {
            get
            {
                return string.IsNullOrWhiteSpace(this.AuthorUserName) ? "Anonymous user" : this.AuthorUserName;
            }
        }
    }
}
