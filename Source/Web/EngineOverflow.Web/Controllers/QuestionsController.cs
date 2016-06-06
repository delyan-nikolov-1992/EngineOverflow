namespace EngineOverflow.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using EngineOverflow.Data.Common.Repository;
    using EngineOverflow.Data.Models;
    using EngineOverflow.Web.Infrastructure;
    using EngineOverflow.Web.InputModels.Questions;
    using EngineOverflow.Web.ViewModels.Questions;

    using Microsoft.AspNet.Identity;

    public class QuestionsController : Controller
    {
        private const int ItemsPerPage = 4;

        private readonly IDeletableEntityRepository<Post> posts;
        private readonly IDeletableEntityRepository<Tag> tags;
        private readonly IDeletableEntityRepository<Feedback> feedbacks;

        private readonly ISanitizer sanitizer;

        public QuestionsController(
            IDeletableEntityRepository<Post> posts,
            IDeletableEntityRepository<Tag> tags,
            IDeletableEntityRepository<Feedback> feedbacks,
            ISanitizer sanitizer)
        {
            this.posts = posts;
            this.tags = tags;
            this.feedbacks = feedbacks;
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

            var postFeedbacks = this.feedbacks.All().Where(x => x.PostId == id);
            var allItemsCount = postFeedbacks.Count();
            var totalPages = (int)Math.Ceiling(allItemsCount / (decimal)ItemsPerPage);
            var itemsToSkip = (page - 1) * ItemsPerPage;

            var feedbacks = postFeedbacks
                .OrderBy(x => x.CreatedOn)
                .ThenBy(x => x.Id)
                .Skip(itemsToSkip).Take(ItemsPerPage)
                .ProjectTo<FeedbackDisplayViewModel>().ToList();

            var feedbackViewModel = new FeedbackListViewModel
            {
                PostId = id,
                PostTitle = postViewModel.Title,
                CurrentPage = page,
                TotalPages = totalPages,
                Feedbacks = feedbacks
            };

            postViewModel.FeedbackListViewModel = feedbackViewModel;

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

            var tagNames = input.Tags.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            var uniqueTagNames = new HashSet<string>(tagNames);
            var postTags = new List<Tag>();

            foreach (var uniquesTagName in uniqueTagNames)
            {
                var tag = this.tags.All().Where(x => x.Name == uniquesTagName).FirstOrDefault();

                if (tag == null)
                {
                    tag = new Tag { Name = uniquesTagName };
                    this.tags.Add(tag);
                }

                postTags.Add(tag);
            }

            var userId = this.User.Identity.GetUserId();

            var post = new Post
            {
                Title = input.Title,
                Content = this.sanitizer.Sanitize(input.Content),
                AuthorId = userId
            };

            foreach (var postTag in postTags)
            {
                postTag.Posts.Add(post);
            }

            this.tags.SaveChanges();

            return this.RedirectToAction("Display", new { id = post.Id, url = post.Title.ToLower().Replace(" ", "-") });
        }
    }
}
