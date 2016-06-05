namespace EngineOverflow.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;

    using EngineOverflow.Data.Models;
    using EngineOverflow.Web.Infrastructure.Mapping;

    public class IndexBlogPostViewModel : IMapFrom<Post>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime CreatedOn { get; set; }

        public string AuthorUserName { get; set; }

        public IEnumerable<IndexBlogPostTagViewModel> Tags { get; set; }

        public string Url
        {
            get
            {
                return string.Format("/questions/{0}/{1}", this.Id, this.Title.ToLower().Replace(" ", "-"));
            }
        }
    }
}
