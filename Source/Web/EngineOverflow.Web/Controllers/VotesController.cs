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
        private readonly IDeletableEntityRepository<PostVote> votes;

        public VotesController(IDeletableEntityRepository<PostVote> votes)
        {
            this.votes = votes;
        }

        [HttpPost]
        public ActionResult Vote(int postId, int voteType)
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
            var vote = this.votes.All()
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
                this.votes.Add(vote);
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

            this.votes.SaveChanges();

            var postVotes = this.votes.All()
                .Where(x => x.PostId == postId)
                .Sum(x => (int)x.Type);

            return this.Json(new { Count = postVotes });
        }
    }
}
