namespace EngineOverflow.Web.ViewModels.Home
{
    using EngineOverflow.Data.Models;
    using EngineOverflow.Web.Infrastructure.Mapping;

    public class IndexBlogPostTagViewModel : IMapFrom<Tag>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
