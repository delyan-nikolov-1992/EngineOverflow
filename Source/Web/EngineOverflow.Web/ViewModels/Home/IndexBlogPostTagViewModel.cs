namespace EngineOverflow.Web.ViewModels.Home
{
    using System.ComponentModel.DataAnnotations;

    using EngineOverflow.Data.Models;
    using EngineOverflow.Web.Infrastructure.Mapping;

    public class IndexBlogPostTagViewModel : IMapFrom<Tag>
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
