using EngineOverflow.Data.Common.Repository;
using EngineOverflow.Data.Models;
using EngineOverflow.Web.InputModels.Questions;
using EngineOverflow.Web.ViewModels.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using EngineOverflow.Web.Infrastructure;

namespace EngineOverflow.Web.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly IDeletableEntityRepository<Post> posts;

        private readonly ISanitizer sanitizer;

        public QuestionsController(IDeletableEntityRepository<Post> posts,
            ISanitizer sanitizer)
        {
            this.posts = posts;
            this.sanitizer = sanitizer;
        }

        // /questions/5/difference-between-gasoline-engine-and-diesel-engine
        public ActionResult Display(int id, string url, int page = 1)
        {
            var postViewModel = this.posts.All().Where(x => x.Id == id)
                .ProjectTo<QuestionDisplayViewModel>().FirstOrDefault();

            if (postViewModel == null)
            {
                return this.HttpNotFound("No such post!");
            }

            return View(postViewModel);
        }

        // /questions/tagged/steam
        public ActionResult GetByTag(string tag)
        {
            return Content(tag);
        }

        [HttpGet]
        public ActionResult Ask()
        {
            var model = new AskInputModel();

            return this.View(model);
        }

        [HttpPost]
        public ActionResult Ask(AskInputModel input)
        {
            if (ModelState.IsValid)
            {
                var post = new Post
                {
                    Title = input.Title,
                    Content = sanitizer.Sanitize(input.Content)
                    // TODO: Tags
                    // TODO: Author
                };

                this.posts.Add(post);
                this.posts.SaveChanges();
                return this.RedirectToAction("Display", new { id = post.Id, url = "new" });
            }

            return this.View(input);
        }
    }
}
