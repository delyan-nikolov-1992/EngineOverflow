using EngineOverflow.Web.ViewModels.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EngineOverflow.Web.ViewModels.PageableFeedbackList
{
    public class FeedbackListViewModel
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public IEnumerable<FeedbackDisplayViewModel> Feedbacks { get; set; }
    }
}
