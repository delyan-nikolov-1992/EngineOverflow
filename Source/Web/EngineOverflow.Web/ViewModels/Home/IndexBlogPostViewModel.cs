namespace EngineOverflow.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EngineOverflow.Data.Models;
    using EngineOverflow.Web.Infrastructure.Mapping;
    using AutoMapper;
    using System.Linq;

    public class IndexBlogPostViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Title { get; set; }

        public int VotesCount { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        public string AuthorUserName { get; set; }

        public IEnumerable<IndexBlogPostTagViewModel> Tags { get; set; }

        public int FeedbacksCount { get; set; }

        public string Url
        {
            get
            {
                return string.Format("/questions/{0}/{1}", this.Id, this.Title.ToLower().Replace(" ", "-"));
            }
        }

        public void CreateMappings(IConfiguration configuration)
        {
            Mapper.CreateMap<Post, IndexBlogPostViewModel>()
                .ForMember(
                    x => x.VotesCount,
                    opt => opt.MapFrom(x => x.Votes.Any() ? x.Votes.Sum(y => (int)y.Type) : 0));
        }
    }
}
