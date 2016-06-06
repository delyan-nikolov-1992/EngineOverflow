using EngineOverflow.Data.Common.Repository;
using EngineOverflow.Data.Models;
using EngineOverflow.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using EngineOverflow.Web.ViewModels.Questions;
using EngineOverflow.Web.ViewModels.PageableFeedbackList;

namespace EngineOverflow.Web.Controllers
{
    public class PageableFeedbackListController : Controller
    {
        private const int ItemsPerPage = 4;

        private readonly IDeletableEntityRepository<Feedback> feedbacks;

        public PageableFeedbackListController(IDeletableEntityRepository<Feedback> feedbacks)
        {
            this.feedbacks = feedbacks;
        }

        [HttpGet]
        public ActionResult Index(int postId, int id = 1)
        {
            var postFeedbacks = this.feedbacks.All().Where(x => x.PostId == postId);

            var page = id;
            var allItemsCount = postFeedbacks.Count();
            var totalPages = (int)Math.Ceiling(allItemsCount / (decimal)ItemsPerPage);
            var itemsToSkip = (page - 1) * ItemsPerPage;

            var feedbacks = postFeedbacks
                .OrderBy(x => x.CreatedOn)
                .ThenBy(x => x.Id)
                .Skip(itemsToSkip).Take(ItemsPerPage)
                .ProjectTo<FeedbackDisplayViewModel>().ToList();

            var viewModel = new FeedbackListViewModel
            {
                CurrentPage = page,
                TotalPages = totalPages,
                Feedbacks = feedbacks
            };

            return View(viewModel);
        }
    }
}
