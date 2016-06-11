namespace EngineOverflow.Web.ViewModels.Questions
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using AutoMapper;

    using EngineOverflow.Data.Models;
    using EngineOverflow.Web.Infrastructure;
    using EngineOverflow.Web.Infrastructure.Mapping;

    public class FeedbackDisplayViewModel : IMapFrom<Feedback>, IHaveCustomMappings
    {
        private ISanitizer sanitizer;

        public FeedbackDisplayViewModel()
        {
            this.sanitizer = new HtmlSanitizerAdapter();
        }

        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public string AuthorUserName { get; set; }

        public DateTime CreatedOn { get; set; }

        public int VotesCount { get; set; }

        public string SanitizedContent
        {
            get
            {
                return this.sanitizer.Sanitize(this.Content);
            }
        }

        public string AuthorUserNameDisplay
        {
            get
            {
                return string.IsNullOrWhiteSpace(this.AuthorUserName) ? "Anonymous user" : this.AuthorUserName;
            }
        }

        public void CreateMappings(IConfiguration configuration)
        {
            Mapper.CreateMap<Feedback, FeedbackDisplayViewModel>()
                .ForMember(
                    x => x.VotesCount,
                    opt => opt.MapFrom(x => x.Votes.Any() ? x.Votes.Sum(y => (int)y.Type) : 0));
        }
    }
}
