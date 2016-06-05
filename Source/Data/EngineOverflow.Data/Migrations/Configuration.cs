namespace EngineOverflow.Data.Migrations
{
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using EngineOverflow.Data.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;

            // TODO: Remove in production
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var user = context.Users.FirstOrDefault();

            if (user == null)
            {
                user = new ApplicationUser { UserName = "delyan.nikolov.1992@gmail.com" };
                context.Users.Add(user);
                context.SaveChanges();
            }

            if (!context.Feedbacks.Any())
            {
                for (int i = 1; i <= 18; i++)
                {
                    var feedback = new Feedback
                    {
                        Content = string.Format("Feedback <b>content</b> {0}", i)
                    };

                    context.Feedbacks.Add(feedback);
                }

                context.SaveChanges();
            }

            if (!context.Tags.Any())
            {
                var tagList = new List<Tag>();

                for (int i = 1; i <= 20; i++)
                {
                    var tag = new Tag
                    {
                        Name = string.Format("Tag-{0}", i)
                    };

                    context.Tags.Add(tag);
                    tagList.Add(tag);
                }

                var tagIndex = 0;
                for (int i = 1; i <= 20; i++)
                {
                    var post = new Post
                    {
                        Content = string.Format("Post content {0}", i),
                        Title = string.Format("Post Title {0}", i),
                        AuthorId = user.Id
                    };

                    for (int j = 1; j <= 5; j++)
                    {
                        post.Tags.Add(tagList[tagIndex % tagList.Count]);
                        tagIndex++;
                    }

                    context.Posts.Add(post);
                }

                context.SaveChanges();
            }
        }
    }
}
