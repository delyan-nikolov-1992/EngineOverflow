namespace EngineOverflow.Web.Controllers
{
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
            this.posts.ActualDelete(this.posts.GetById(1));
            this.posts.SaveChanges();

            var model = this.posts.All().ProjectTo<IndexBlogPostViewModel>();

            return this.View(model);
        }
    }
}
