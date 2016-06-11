namespace EngineOverflow.Web.ViewModels.Questions
{
    using System.Collections.Generic;

    public class FeedbackListViewModel
    {
        public int PostId { get; set; }

        public string PostTitle { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public IEnumerable<FeedbackDisplayViewModel> Feedbacks { get; set; }
    }
}
