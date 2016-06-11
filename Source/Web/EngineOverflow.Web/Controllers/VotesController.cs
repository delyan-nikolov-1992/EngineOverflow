namespace EngineOverflow.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using EngineOverflow.Data.Common.Repository;
    using EngineOverflow.Data.Models;

    using Microsoft.AspNet.Identity;

    [Authorize]
    public class VotesController : Controller
    {
        private readonly IDeletableEntityRepository<PostVote> postVotes;
        private readonly IDeletableEntityRepository<FeedbackVote> feedbackVotes;

        public VotesController(IDeletableEntityRepository<PostVote> postVotes, IDeletableEntityRepository<FeedbackVote> feedbackVotes)
        {
            this.postVotes = postVotes;
            this.feedbackVotes = feedbackVotes;
        }

        [HttpPost]
        public ActionResult PostVote(int postId, int voteType)
        {
            if (voteType > 1)
            {
                voteType = 1;
            }
            else if (voteType < -1)
            {
                voteType = -1;
            }

            var userId = this.User.Identity.GetUserId();
            var vote = this.postVotes.All()
                .Where(x => x.AuthorId == userId && x.PostId == postId)
                .FirstOrDefault();

            if (vote == null)
            {
                vote = new PostVote
                {
                    AuthorId = userId,
                    PostId = postId,
                    Type = (VoteType)voteType
                };
                this.postVotes.Add(vote);
            }
            else
            {
                if (vote.Type == (VoteType)voteType)
                {
                    vote.Type = VoteType.Neutral;
                }
                else
                {
                    vote.Type = (VoteType)voteType;
                }
            }

            this.postVotes.SaveChanges();

            var postVotes = this.postVotes.All()
                .Where(x => x.PostId == postId)
                .Sum(x => (int)x.Type);

            return this.Json(new { Count = postVotes });
        }

        [HttpPost]
        public ActionResult FeedbackVote(int feedbackId, int voteType)
        {
            if (voteType > 1)
            {
                voteType = 1;
            }
            else if (voteType < -1)
            {
                voteType = -1;
            }

            var userId = this.User.Identity.GetUserId();
            var vote = this.feedbackVotes.All()
                .Where(x => x.AuthorId == userId && x.FeedbackId == feedbackId)
                .FirstOrDefault();

            if (vote == null)
            {
                vote = new FeedbackVote
                {
                    AuthorId = userId,
                    FeedbackId = feedbackId,
                    Type = (VoteType)voteType
                };
                this.feedbackVotes.Add(vote);
            }
            else
            {
                if (vote.Type == (VoteType)voteType)
                {
                    vote.Type = VoteType.Neutral;
                }
                else
                {
                    vote.Type = (VoteType)voteType;
                }
            }

            this.feedbackVotes.SaveChanges();

            var feedbackVotes = this.feedbackVotes.All()
                .Where(x => x.FeedbackId == feedbackId)
                .Sum(x => (int)x.Type);

            return this.Json(new { Count = feedbackVotes });
        }
    }
}
