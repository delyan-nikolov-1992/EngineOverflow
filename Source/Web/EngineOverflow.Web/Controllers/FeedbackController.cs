﻿namespace EngineOverflow.Web.Controllers
{
    using System;
    using System.Web.Mvc;

    using EngineOverflow.Data.Common.Repository;
    using EngineOverflow.Data.Models;
    using EngineOverflow.Web.Infrastructure;
    using EngineOverflow.Web.InputModels.Feedback;

    using Microsoft.AspNet.Identity;

    public class FeedbackController : Controller
    {
        private readonly IDeletableEntityRepository<Feedback> feedbacks;

        private readonly ISanitizer sanitizer;

        public FeedbackController(IDeletableEntityRepository<Feedback> feedbacks, ISanitizer sanitizer)
        {
            this.feedbacks = feedbacks;
            this.sanitizer = sanitizer;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var feedback = new Feedback()
            {
                Content = this.sanitizer.Sanitize(input.Content),
                PostId = Convert.ToInt32(TempData["PostId"])
            };

            if (this.User.Identity.IsAuthenticated)
            {
                feedback.AuthorId = this.User.Identity.GetUserId();
            }

            this.feedbacks.Add(feedback);
            this.feedbacks.SaveChanges();

            this.TempData["Notification"] = "Thank you for your feedback!";

            return this.Redirect(Request.UrlReferrer.ToString());
        }
    }
}
