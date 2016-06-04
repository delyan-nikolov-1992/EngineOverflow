using EngineOverflow.Data.Models;
using EngineOverflow.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EngineOverflow.Web.ViewModels.Questions
{
    public class QuestionDisplayViewModel : IMapFrom<Post>
    {
        public string Title { get; set; }

        public string Content { get; set; }
    }
}
