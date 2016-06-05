namespace EngineOverflow.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using EngineOverflow.Data.Common.Repository;
    using EngineOverflow.Data.Models;
    using EngineOverflow.Web.Infrastructure;
    using EngineOverflow.Web.InputModels.Questions;
    using EngineOverflow.Web.ViewModels.Questions;

    using Microsoft.AspNet.Identity;
    using System.Collections.Generic;
    using System;

    public class QuestionsController : Controller
    {
        private readonly IDeletableEntityRepository<Post> posts;
        private readonly IDeletableEntityRepository<Tag> tags;

        private readonly ISanitizer sanitizer;

        public QuestionsController(IDeletableEntityRepository<Post> posts, IDeletableEntityRepository<Tag> tags, ISanitizer sanitizer)
        {
            this.posts = posts;
            this.tags = tags;
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

            return this.View(postViewModel);
        }

        // /questions/tagged/steam
        public ActionResult GetByTag(string tag)
        {
            return this.Content(tag);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Ask()
        {
            var model = new AskInputModel();

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Ask(AskInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.View(input);
            }

            var postTags = input.Tags.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            var uniquePostTags = new HashSet<string>(postTags);

            foreach (var uniquesPostTag in uniquePostTags)
            {
                if (!this.tags.All().Where(x => x.Name == uniquesPostTag).Any())
                {
                    var tag = new Tag { Name = uniquesPostTag };
                    this.tags.Add(tag);
                }
            }

            var userId = this.User.Identity.GetUserId();

            var post = new Post
            {
                Title = input.Title,
                Content = this.sanitizer.Sanitize(input.Content),
                AuthorId = userId
            };

            this.posts.Add(post);
            this.tags.SaveChanges();
            post.Tags = this.tags.All().Where(x => uniquePostTags.Contains(x.Name)).ToList();

            foreach (var dido in post.Tags)
            {
                dido.Posts.Add(post);
            }

            this.tags.SaveChanges();

            return this.RedirectToAction("Display", new { id = post.Id, url = "new" });
        }
    }
}
