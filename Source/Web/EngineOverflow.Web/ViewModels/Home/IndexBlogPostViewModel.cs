using EngineOverflow.Data.Models;
using EngineOverflow.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EngineOverflow.Web.ViewModels.Home
{
    public class IndexBlogPostViewModel : IMapFrom<Post>
    {
        public string Title { get; set; }
    }
}
