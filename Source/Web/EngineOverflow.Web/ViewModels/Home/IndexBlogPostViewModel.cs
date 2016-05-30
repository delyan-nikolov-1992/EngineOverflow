namespace EngineOverflow.Web.ViewModels.Home
{
    using EngineOverflow.Data.Models;
    using EngineOverflow.Web.Infrastructure.Mapping;

    public class IndexBlogPostViewModel : IMapFrom<Post>
    {
        public string Title { get; set; }
    }
}
