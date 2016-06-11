namespace EngineOverflow.Web.Controllers
{
    using System.Linq;
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

        public ActionResult Index()
        {
            var model = this.posts.All()
                .OrderBy(x => x.Votes.Sum(y => (int)y.Type))
                .ThenBy(x => x.CreatedOn)
                .ThenBy(x => x.Id)
                .ProjectTo<IndexBlogPostViewModel>();

            return this.View(model);
        }
    }
}
