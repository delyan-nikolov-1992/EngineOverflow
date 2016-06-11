namespace EngineOverflow.Web.Controllers
{
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using EngineOverflow.Data.Common.Repository;
    using EngineOverflow.Data.Models;
    using EngineOverflow.Web.ViewModels.Home;

    public class HomeController : Controller
    {
        private readonly IDeletableEntityRepository<Post> posts;

        public HomeController(IDeletableEntityRepository<Post> posts)
        {
            this.posts = posts;
        }

        public ActionResult Index(string tags = null)
        {
            var posts = this.posts.All();

            if (!string.IsNullOrWhiteSpace(tags))
            {
                var separatedTags = Regex.Split(tags, @"\W+");
                posts = posts.Where(x => x.Tags.Any(y => separatedTags.Contains(y.Name)));
            }

            var model = posts
                .OrderByDescending(x => x.CreatedOn)
                .ThenBy(x => x.Id)
                .ProjectTo<IndexBlogPostViewModel>();

            return this.View(model);
        }
    }
}
